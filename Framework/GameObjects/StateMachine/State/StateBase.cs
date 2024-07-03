using UnityEngine;


namespace MyFramework.GameObjects.FSM
{
    // 状态基类
    public abstract class StateBase : MonoBehaviour
    {
        #region Attribute
        // 描述
        private string _desc = string.Empty;
        public string desc
        {
            get => _desc;
            protected set => _desc = value;
        }

        // 状态是否完成
        private bool _finished = false;

        public bool finished
        {
            get => _finished;
            protected set => _finished = value;
        }

        // 优先级：高优先级高状态 不会被 低优先级状态 中断，反之可以
        private int _priority = 0;

        public int priority
        {
            get => _priority;
            protected set => _priority = value;
        }

        public bool HasGEPriorityThan(StateBase otherState)
        {
            return priority >= otherState.priority;
        }
        protected abstract void Init();
        #endregion


        #region Action
        // 进入状态时执行的方法
        public abstract void OnEnter();

        // 离开状态时执行的方法
        public abstract void OnExit();
        #endregion

    }
}


