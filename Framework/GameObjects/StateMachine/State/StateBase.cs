using UnityEngine;


namespace MyFramework.GameObjects.FSM
{
    // ״̬����
    public abstract class StateBase : MonoBehaviour
    {
        #region Attribute
        // ����
        private string _desc = string.Empty;
        public string desc
        {
            get => _desc;
            protected set => _desc = value;
        }

        // ״̬�Ƿ����
        private bool _finished = false;

        public bool finished
        {
            get => _finished;
            protected set => _finished = value;
        }

        // ���ȼ��������ȼ���״̬ ���ᱻ �����ȼ�״̬ �жϣ���֮����
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
        // ����״̬ʱִ�еķ���
        public abstract void OnEnter();

        // �뿪״̬ʱִ�еķ���
        public abstract void OnExit();
        #endregion

    }
}


