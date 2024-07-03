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
                // ����ǰ���ʵ������Ϊ�ص�Ŀ��ע�ᵽ PlayerInputActions �� Player ������
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


        // �ƶ��¼�
        private UnityEvent<Vector2> _moveEvent;
        // �۲��¼�
        private UnityEvent<Vector2, bool> _lookEvent;

        // �����ӽǿ���
        private UnityEvent _enableCameraControlEvent;
        // �����ӽǿ���
        private UnityEvent _disableCameraControlEvent;
        // �ӽ�����
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


