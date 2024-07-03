using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// ��ȷ����FSMӦ��������
/// 1) ��Ϊ����� GameObject�����״̬��������Ϊ���������
/// 2) ������
///    ���� [currState, event, nextState]������״̬ת��ͼ��
///    ���ó�ʼ״̬
/// 3) ת�ƣ�
///    ����event -> ���� currState ת���� nextState��
///    ִ��״̬��Ϊ
/// </summary>


namespace MyFramework.GameObjects.FSM
{
    public sealed class FSM : MonoBehaviour
    {
        #region Logic
        // �����ⲿ�¼��л�״̬��
        public void ChangeState(string eventDesc)
        {
            // �жϵ�ǰ״̬�Ƿ����ת��·��
            if (!stateTransMap.ContainsKey(currState.desc))
            {
                Debug.LogWarning($"[Warning] ��ǰ״̬ {currState.desc} �������κ�ת��·��������״̬������߼��Ƿ���ȷ");
                return;
            }
            // �ж��Ƿ���ڶ�Ӧ eventDesc ��ת��·��
            else if (!stateTransMap[currState.desc].ContainsKey(eventDesc))
            {
                Debug.Log(
                    $"[Info] ��ǰ״̬ {currState.desc} ����������Ϊ {eventDesc} ��ת��·�������ı�״̬"
                    );
                return;
            }

            // ��ȡ��һ��״̬
            StateBase nextState = stateMap[stateTransMap[currState.desc][eventDesc]];
            if (nextState == null)
            {
                Debug.Log(
                    $"[Info] ��ǰ״̬ {currState.desc} �������� {eventDesc} ת�Ƶ���һ��״̬Ϊ null�����ı�״̬"
                    );
                return;
            }

            // ��ǰ״ִ̬�����
            if (currState.finished)
            {
                // �л�����һ��״̬
                ChangeStateTo(nextState);
            }
            // δִ����ϵ������ж��������ǿ��ж�״̬���Ҿ���
            // ��������ͬ���ȼ���
            else if (nextState.HasGEPriorityThan(currState) && currState is IInterruptableState currIntrState)
            {
                // ���õ�ǰ״̬���жϷ��������л�����һ��״̬
                currIntrState.OnInterrupt();
                ChangeStateTo(nextState);
            }
        }
        private void ChangeStateTo(StateBase nextState)
        {
            // �ж��Ƿ�����ѭ��״̬
            if (currState.desc == nextState.desc)
            {
                // ����ѭ������ִ���κβ���
                return;
            }

            // ��յ�ǰ״̬��֡���º���������¼�


            // ���õ�ǰ״̬�� OnExit ����
            currState.OnExit();

            // ���õ�ǰ״̬������ & ��Ϊ
            currState = nextState;

            // ������״̬�� OnEnter ����
            currState.OnEnter();

            // ע����һ״̬��֡���º���������¼�
            if (nextState)
            {

            }

        }

        // ֡�����¼�
        UnityEvent frameUpdateEvent = null;
        // ��������¼�
        UnityEvent physicsUpdateEvent = null;

        private void Update()
        {
            frameUpdateEvent?.Invoke();
        }
        private void FixedUpdate()
        {
            physicsUpdateEvent?.Invoke();
        }
        #endregion


        #region State.Trans
        // ״̬ת��ӳ��
        private Dictionary<string, Dictionary<string, string>> stateTransMap = new();
        // ��ǰ״̬
        private StateBase currState = null;
        // Ĭ��״̬
        private StateBase defaultState = null;

        // ���ó�ʼ״̬
        public void SetInitState(StateBase initState)
        {
            // �ײ���Ȼͨ�� state.desc ���ã���֤�������һ���Ѿ���ӵ� FSM �е�״̬ʵ��
            SetInitState(initState.desc);
        }
        // ͨ��״̬�������ó�ʼ״̬
        public void SetInitState(string desc)
        {
            if (!stateMap.ContainsKey(desc))
            {
                Debug.LogError($"[Error] ���ó�ʼ״̬ʧ�ܣ���ǰ FSM �в�������Ϊ {desc} ��״̬");
                return;
            }
            currState = stateMap[desc];
        }
        // ����Ĭ��״̬
        public void SetDefaultState()
        {

        }

        // ���״̬ת������
        public void AddStateTrans(StateBase prevState, string eventDesc, StateBase nextState)
        {
            if (!stateMap.ContainsKey(prevState.desc))
            {
                Debug.LogError($"[Error] ����״̬ת��ʧ�ܣ���ǰ FSM �в�������Ϊ {prevState.desc} ��״̬");
                return;
            }
            else if (!stateMap.ContainsKey(nextState.desc))
            {
                Debug.LogError($"[Error] ����״̬ת��ʧ�ܣ���ǰ FSM �в�������Ϊ {nextState.desc} ��״̬");
                return;
            }
            else if (!eventSet.Contains(eventDesc))
            {
                Debug.LogWarning($"[Warning] ��ǰ FSM ��δע����Ϊ {eventDesc} ���¼��������������·������������Ƿ���ȷ");
            }

            if (!stateTransMap.ContainsKey(prevState.desc))
            {
                stateTransMap[prevState.desc] = new();
            }
            stateTransMap[prevState.desc].Add(eventDesc, nextState.desc);
        }
        #endregion

        #region State.Add
        // ״̬ӳ��
        private Dictionary<string, StateBase> stateMap = new();
        // �¼�ӳ��
        private HashSet<string> eventSet = null;

        // �� FSM �����״̬
        public void AddState(StateBase state)
        {
            if (stateMap.ContainsKey(state.desc))
            {
                Debug.LogWarning($"[Warning] ��Ϊ {state.desc} ��״̬ʵ���Ѵ����� FSM�����滻��ʵ��������������Ƿ����ͬ��״̬");
            }
            stateMap[state.desc] = state;

            // ��֤��ʼ״̬��Ϊnull
            if (currState == null)
            {
                currState = state;
            }
        }
        // �� FSM �����״̬ת������
        public void AddEvent(string eventDesc)
        {
            eventSet ??= new(); // �պϲ���ֵ���㣺�� null�����丳ֵΪ new() ��������ʵ������Ϊ null���޲���
            eventSet.Add(eventDesc);
        }
        #endregion


    }
}


