using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.FSM
{
    // ״̬����������ʵ��״̬���ľ��幹����¼����
    public abstract class FSMControllerBase : MonoBehaviour
    {
        // ״̬����ʼ��
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


        // �¼���⣺״̬ת��
        #region Event
        private void Update()
        {
            TestEvent();
        }
        // �����¼����
        protected abstract void TestEvent();
        // �����¼����գ�ͨ������ײ�䴥���߼�
        public abstract void InvokeEvent(string eventDesc);
        #endregion

    }
}


