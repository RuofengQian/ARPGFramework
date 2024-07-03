using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.FSM
{
    // 状态机控制器：实现状态机的具体构造和事件检测
    public abstract class FSMControllerBase : MonoBehaviour
    {
        // 状态机初始化
        #region Construct
        private FSM _fsm = null;
        protected FSM fsm
        {
            get => _fsm;
            private set => _fsm = value;
        }

        private void Start()
        {
            fsm = gameObject.AddComponent<FSM>();
            ConstructFSM();
        }
        protected abstract void ConstructFSM();
        #endregion


        // 事件检测：状态转移
        #region Event
        private void Update()
        {
            TestEvent();
        }
        // 主动事件检测
        protected abstract void TestEvent();
        // 被动事件接收：通常由碰撞箱触发逻辑
        public abstract void InvokeEvent(string eventDesc);
        #endregion

    }
}


