using Cinemachine;
using UnityEngine;


namespace MyFramework.Manager.Input
{
    public class PlayerController : MonoBehaviour
    {
        #region Attribute
        [SerializeField] CinemachineFreeLook freeLookCam;
        Transform mainCamTrans;

        private CharacterController character;
        private InputManager input;

        private float moveSpeed = 6f;
        private float rotationSpeed = 15f;
        private float smoothTime = 0.2f;


        private void Awake()
        {
            // ��ȡ��ɫ������
            character = gameObject.GetComponent<CharacterController>();
            if( character == null )
            {
                character = gameObject.AddComponent<CharacterController>();
            }
            // ���ò������ɾ�����Դ������
            SetPlayerModuleAttr(1.4f);
            
            input = InputManager.Instance;
            
            // ��ȡ�����
            mainCamTrans = Camera.main.transform;

            // �������������������
            freeLookCam.Follow = gameObject.transform;
            freeLookCam.LookAt = gameObject.transform;
            freeLookCam.OnTargetObjectWarped(
                gameObject.transform,
                transform.position - freeLookCam.transform.position - Vector3.forward
                );
        }
        private void SetPlayerModuleAttr(float height)
        {
            // TODO����һ������ģ�Ͳ����޸�
            character.height = height;
            character.center = new Vector3(0, height / 2, 0);
        }
        #endregion


        #region Action
        [SerializeField] Animator animator;
        float currSpeed;
        float velocity;


        private void LateUpdate()
        {
            HandleMovement();
            UpdateAnimator();
        }


        private void HandleMovement()
        {
            // ��ȡ�ƶ�����ĵ�λ����
            // Vector3.normalized - ��ȡ��λ����
            Vector3 moveDir = new Vector3(input.direction.x, 0f, input.direction.y).normalized;

            // ��Ҫ������λ������
            Vector3 adjustDir = Quaternion.AngleAxis(mainCamTrans.eulerAngles.y, Vector3.up) * moveDir;

            // Vector3.magnitude - ��ȡ����ģ��
            if ( adjustDir.magnitude > 0f )
            {
                // ��ת
                HandleCharacterRotation(adjustDir);
                // �ƶ����
                HandleCharacterMovement(adjustDir);
            }
            else
            {
                currSpeed = SmoothSpeed(0f); // ��ƽ���ٶ�
            }
        }

        // �����ɫ��ת
        private void HandleCharacterRotation(Vector3 adjustDir)
        {
            Quaternion targetRotation = Quaternion.LookRotation(adjustDir);
            gameObject.transform.rotation = 
                Quaternion.RotateTowards(
                    transform.rotation, 
                    targetRotation, 
                    rotationSpeed
                    );
            gameObject.transform.LookAt(gameObject.transform.position + adjustDir);
        }
        // �����ɫ�ƶ�
        private void HandleCharacterMovement(Vector3 adjustDir)
        {
            Vector3 adjustMove = adjustDir * (moveSpeed * Time.deltaTime);
            character.Move(adjustMove);
            currSpeed = SmoothSpeed(adjustMove.magnitude);
        }
        // ƽ���ٶ�
        private float SmoothSpeed(float value)
        {
            return Mathf.SmoothDamp(currSpeed, value, ref velocity, smoothTime);
        }

        private void UpdateAnimator()
        {

        }
        #endregion

    }

}





