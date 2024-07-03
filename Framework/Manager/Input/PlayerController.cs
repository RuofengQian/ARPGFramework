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
            // 获取角色控制器
            character = gameObject.GetComponent<CharacterController>();
            if( character == null )
            {
                character = gameObject.AddComponent<CharacterController>();
            }
            // 设置参数（由具体资源而定）
            SetPlayerModuleAttr(1.4f);
            
            input = InputManager.Instance;
            
            // 获取主相机
            mainCamTrans = Camera.main.transform;

            // 设置虚拟自由相机跟随
            freeLookCam.Follow = gameObject.transform;
            freeLookCam.LookAt = gameObject.transform;
            freeLookCam.OnTargetObjectWarped(
                gameObject.transform,
                transform.position - freeLookCam.transform.position - Vector3.forward
                );
        }
        private void SetPlayerModuleAttr(float height)
        {
            // TODO：进一步根据模型参数修改
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
            // 获取移动方向的单位向量
            // Vector3.normalized - 获取单位向量
            Vector3 moveDir = new Vector3(input.direction.x, 0f, input.direction.y).normalized;

            // 需要调整的位移向量
            Vector3 adjustDir = Quaternion.AngleAxis(mainCamTrans.eulerAngles.y, Vector3.up) * moveDir;

            // Vector3.magnitude - 获取向量模长
            if ( adjustDir.magnitude > 0f )
            {
                // 旋转
                HandleCharacterRotation(adjustDir);
                // 移动玩家
                HandleCharacterMovement(adjustDir);
            }
            else
            {
                currSpeed = SmoothSpeed(0f); // 仅平滑速度
            }
        }

        // 处理角色旋转
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
        // 处理角色移动
        private void HandleCharacterMovement(Vector3 adjustDir)
        {
            Vector3 adjustMove = adjustDir * (moveSpeed * Time.deltaTime);
            character.Move(adjustMove);
            currSpeed = SmoothSpeed(adjustMove.magnitude);
        }
        // 平滑速度
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





