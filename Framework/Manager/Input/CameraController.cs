using Cinemachine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using static UnityEditor.SceneView;


namespace MyFramework.Manager.Input
{
    public class CameraController : MonoBehaviour
    {
        #region Attribute
        [Header("Settrings")]
        [SerializeField, Range(0.5f, 3f)] float speedMultiple = 1f;

        [Header("Refences")]
        [SerializeField] CinemachineFreeLook freeLookCam;
        private bool lockCamMove;

        private InputManager input;
        private bool isKeyPressed;


        private void Awake()
        {
            input = InputManager.Instance;
        }
        private void Start()
        {
            // 1.����
            currFOV = freeLookCam.m_Lens.FieldOfView;
            targetFOV = currFOV;

            // 2.��Ч��
            noiseProfile = freeLookCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
            
            // ȷ������ص�������
            input.EnableActions();
        }

        private void OnEnable()
        {
            input.lookEvent.AddListener(Look);
            input.zoomEvent.AddListener(Zoom);

            input.enableCameraControlEvent.AddListener(EnableCameraControl);
            input.disableCameraControlEvent.AddListener(DisableCameraControl);
        }
        private void OnDisable()
        {
            input.lookEvent.RemoveListener(Look);
            input.zoomEvent.RemoveListener(Zoom);

            input.enableCameraControlEvent.RemoveListener(EnableCameraControl);
            input.disableCameraControlEvent.RemoveListener(DisableCameraControl);
        }

        private void LateUpdate()
        {
            if( Mathf.Abs(targetFOV - currFOV) > 0.1f )
            {
                currFOV = Mathf.Lerp(currFOV, targetFOV, zoomSpeed * Time.deltaTime);
                freeLookCam.m_Lens.FieldOfView = currFOV;
            }
        }
        #endregion


        #region Zoom
        private float midRadius = 8f;
        private float tbRadius = 0.5f;
        private float heightMultiple = 0.75f;

        private float zoomSpeed = 1f;
        private float targetFOV;
        private float currFOV;

        public void RefreshZoom(float charHeight)
        {
            if( freeLookCam != null )
            {
                CinemachineOrbitalTransposer transposer = 
                    freeLookCam.GetComponent<CinemachineOrbitalTransposer>();

                // Top Rig
                freeLookCam.m_Orbits[0].m_Height = charHeight + midRadius;
                freeLookCam.m_Orbits[0].m_Radius = tbRadius;
                // Middle Rig
                freeLookCam.m_Orbits[1].m_Height = heightMultiple * charHeight;
                freeLookCam.m_Orbits[1].m_Radius = midRadius;
                // Bottom Rig
                freeLookCam.m_Orbits[2].m_Height = -heightMultiple * charHeight;
                freeLookCam.m_Orbits[2].m_Radius = tbRadius;
            }
        }
        public void Zoom(float dist)
        {
            targetFOV += dist;
        }
        public void ZoomIn(float dist = 2f)
        {
            targetFOV -= dist;
        }
        public void ZoomOut(float dist = 2f)
        {
            targetFOV += dist;
        }
        #endregion

        #region CameraShake
        private CinemachineBasicMultiChannelPerlin noiseProfile;

        public void CameraShake(float duration = 0.25f, float amplitude = 1f, float frequency = 1f)
        {
            if (noiseProfile != null)
            {
                noiseProfile.m_AmplitudeGain = amplitude;
                noiseProfile.m_FrequencyGain = frequency;
                Invoke(nameof(StopShaking), duration);
            }
        }
        private void StopShaking()
        {
            if (noiseProfile != null)
            {
                noiseProfile.m_AmplitudeGain = 0f;
                noiseProfile.m_FrequencyGain = 0f;
            }
        }
        #endregion


        #region Control
        private void Look(Vector2 cameraMove, bool isDeviceMouse)
        {
            if( lockCamMove || (isDeviceMouse && !isKeyPressed) )
            {
                return;
            }

            float deviceMultiple = isDeviceMouse ? Time.fixedDeltaTime : Time.deltaTime;

            // ����������ֵ
            freeLookCam.m_XAxis.m_InputAxisValue = cameraMove.x * speedMultiple * deviceMultiple;
            freeLookCam.m_YAxis.m_InputAxisValue = cameraMove.y * speedMultiple * deviceMultiple;
        }


        private void EnableCameraControl()
        {
            isKeyPressed = true;

            // �������
            Cursor.lockState = CursorLockMode.Locked;
            // ���ع��
            Cursor.visible = false;

            StartCoroutine(DisableForFrame());
        }
        private IEnumerator DisableForFrame()
        {
            lockCamMove = true;
            yield return new WaitForEndOfFrame();
            lockCamMove = false;
        }

        private void DisableCameraControl()
        {
            // ȡ������״̬
            isKeyPressed = false;

            // ȡ���������
            Cursor.lockState = CursorLockMode.None;
            // ��ʾ���
            Cursor.visible = true;

            freeLookCam.m_XAxis.m_InputAxisValue = 0f;
            freeLookCam.m_YAxis.m_InputAxisValue = 0f;
        }
        #endregion

    }

}


