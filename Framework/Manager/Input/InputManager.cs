using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static MyFramework.Manager.Input.PlayerInputActions;

using Common;


namespace MyFramework.Manager.Input
{
    public class InputManager : Singleton<InputManager>, IPlayerActions
    {
        #region Attribute
        private PlayerInputActions inputActions;

        private InputManager()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerInputActions();
                // 将当前类的实例，作为回调目标注册到 PlayerInputActions 的 Player 操作中
                inputActions.Player.SetCallbacks(this);
            }
            EnableActions();
        }
        public void EnableActions()
        {
            inputActions.Enable();
        }
        public void DisableActions()
        {
            inputActions.Disable();
        }


        // 移动事件
        private UnityEvent<Vector2> _moveEvent;
        // 观察事件
        private UnityEvent<Vector2, bool> _lookEvent;

        // 启用视角控制
        private UnityEvent _enableCameraControlEvent;
        // 禁用视角控制
        private UnityEvent _disableCameraControlEvent;
        // 视角缩放
        private UnityEvent<float> _zoomEvent;

        public UnityEvent<Vector2> moveEvent 
        { 
            get => _moveEvent ??= new UnityEvent<Vector2>(); 
            set => _moveEvent = value; 
        }
        public UnityEvent<Vector2, bool> lookEvent 
        { 
            get => _lookEvent ??= new UnityEvent<Vector2, bool>(); 
            set => _lookEvent = value; }
        public UnityEvent enableCameraControlEvent
        {
            get => _enableCameraControlEvent ??= new UnityEvent(); 
            set => _enableCameraControlEvent = value;
        }
        public UnityEvent disableCameraControlEvent
        { 
            get => _disableCameraControlEvent ??= new UnityEvent();
            set => _disableCameraControlEvent = value;
        }
        public UnityEvent<float> zoomEvent
        {
            get => _zoomEvent ??= new UnityEvent<float>();
            set => _zoomEvent = value;
        }
        #endregion

        #region Camera
        public void OnLook(InputAction.CallbackContext context)
        {
            lookEvent?.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
        }
        public void OnCameraControl(InputAction.CallbackContext context)
        {
            switch(context.phase)
            {
                case InputActionPhase.Started:
                    {
                        enableCameraControlEvent?.Invoke();
                        break;
                    }
                case InputActionPhase.Canceled:
                    {
                        disableCameraControlEvent?.Invoke();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public void OnZoom(InputAction.CallbackContext context)
        {
            float scrollValue = context.ReadValue<Vector2>().y;
            zoomEvent?.Invoke(scrollValue * 0.01f);
        }

        private bool IsDeviceMouse(InputAction.CallbackContext context)
        {
            return context.control.device.name == "Mouse";
        }
        #endregion


        #region Move
        public Vector2 direction { get => inputActions.Player.Move.ReadValue<Vector2>(); }
        

        public void OnMove(InputAction.CallbackContext context)
        {
            moveEvent?.Invoke(context.ReadValue<Vector2>());
        }
        public void OnSprint(InputAction.CallbackContext context)
        {
            //throw new System.NotImplementedException();
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            //throw new System.NotImplementedException();
        }
        #endregion


        #region Action
        public void OnAttack(InputAction.CallbackContext context)
        {
            //throw new System.NotImplementedException();
        }

        
        #endregion

    }
}


