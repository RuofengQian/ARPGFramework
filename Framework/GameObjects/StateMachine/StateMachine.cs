using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// 明确需求：FSM应该如何设计
/// 1) 行为：针对 GameObject，添加状态关联的行为组件到其上
/// 2) 构建：
///    接收 [currState, event, nextState]，构建状态转换图；
///    设置初始状态
/// 3) 转移：
///    接收event -> 根据 currState 转移至 nextState；
///    执行状态行为
/// </summary>


namespace MyFramework.GameObjects.FSM
{
    public sealed class FSM : MonoBehaviour
    {
        #region Logic
        // 根据外部事件切换状态：
        public void ChangeState(string eventDesc)
        {
            // 判断当前状态是否存在转移路径
            if (!stateTransMap.ContainsKey(currState.desc))
            {
                Debug.LogWarning($"[Warning] 当前状态 {currState.desc} 不存在任何转移路径，请检查状态机设计逻辑是否正确");
                return;
            }
            // 判断是否存在对应 eventDesc 的转移路径
            else if (!stateTransMap[currState.desc].ContainsKey(eventDesc))
            {
                Debug.Log(
                    $"[Info] 当前状态 {currState.desc} 不存在条件为 {eventDesc} 的转移路径，不改变状态"
                    );
                return;
            }

            // 获取下一个状态
            StateBase nextState = stateMap[stateTransMap[currState.desc][eventDesc]];
            if (nextState == null)
            {
                Debug.Log(
                    $"[Info] 当前状态 {currState.desc} 经由条件 {eventDesc} 转移的下一个状态为 null，不改变状态"
                    );
                return;
            }

            // 当前状态执行完毕
            if (currState.finished)
            {
                // 切换至下一个状态
                ChangeStateTo(nextState);
            }
            // 未执行完毕但满足中断条件：是可中断状态，且具有
            // （具有相同优先级）
            else if (nextState.HasGEPriorityThan(currState) && currState is IInterruptableState currIntrState)
            {
                // 调用当前状态的中断方法，并切换至下一个状态
                currIntrState.OnInterrupt();
                ChangeStateTo(nextState);
            }
        }
        private void ChangeStateTo(StateBase nextState)
        {
            // 判断是否是自循环状态
            if (currState.desc == nextState.desc)
            {
                // 是自循环，不执行任何操作
                return;
            }

            // 清空当前状态的帧更新和物理更新事件


            // 调用当前状态的 OnExit 方法
            currState.OnExit();

            // 设置当前状态：描述 & 行为
            currState = nextState;

            // 调用新状态的 OnEnter 方法
            currState.OnEnter();

            // 注册下一状态的帧更新和物理更新事件
            if (nextState)
            {

            }

        }

        // 帧更新事件
        UnityEvent frameUpdateEvent = null;
        // 物理更新事件
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
        // 状态转移映射
        private Dictionary<string, Dictionary<string, string>> stateTransMap = new();
        // 当前状态
        private StateBase currState = null;
        // 默认状态
        private StateBase defaultState = null;

        // 设置初始状态
        public void SetInitState(StateBase initState)
        {
            // 底层依然通过 state.desc 设置，保证传入的是一个已经添加到 FSM 中的状态实例
            SetInitState(initState.desc);
        }
        // 通过状态名称设置初始状态
        public void SetInitState(string desc)
        {
            if (!stateMap.ContainsKey(desc))
            {
                Debug.LogError($"[Error] 设置初始状态失败：当前 FSM 中不存在名为 {desc} 的状态");
                return;
            }
            currState = stateMap[desc];
        }
        // 设置默认状态
        public void SetDefaultState()
        {

        }

        // 添加状态转移条件
        public void AddStateTrans(StateBase prevState, string eventDesc, StateBase nextState)
        {
            if (!stateMap.ContainsKey(prevState.desc))
            {
                Debug.LogError($"[Error] 设置状态转移失败：当前 FSM 中不存在名为 {prevState.desc} 的状态");
                return;
            }
            else if (!stateMap.ContainsKey(nextState.desc))
            {
                Debug.LogError($"[Error] 设置状态转移失败：当前 FSM 中不存在名为 {nextState.desc} 的状态");
                return;
            }
            else if (!eventSet.Contains(eventDesc))
            {
                Debug.LogWarning($"[Warning] 当前 FSM 中未注册名为 {eventDesc} 的事件，将继续插入该路径；请检查设计是否正确");
            }

            if (!stateTransMap.ContainsKey(prevState.desc))
            {
                stateTransMap[prevState.desc] = new();
            }
            stateTransMap[prevState.desc].Add(eventDesc, nextState.desc);
        }
        #endregion

        #region State.Add
        // 状态映射
        private Dictionary<string, StateBase> stateMap = new();
        // 事件映射
        private HashSet<string> eventSet = null;

        // 向 FSM 中添加状态
        public void AddState(StateBase state)
        {
            if (stateMap.ContainsKey(state.desc))
            {
                Debug.LogWarning($"[Warning] 名为 {state.desc} 的状态实例已存在于 FSM，将替换该实例；请检查设计中是否存在同名状态");
            }
            stateMap[state.desc] = state;

            // 保证初始状态不为null
            if (currState == null)
            {
                currState = state;
            }
        }
        // 向 FSM 中添加状态转移条件
        public void AddEvent(string eventDesc)
        {
            eventSet ??= new(); // 空合并赋值运算：是 null，将其赋值为 new() 创建的新实例；不为 null，无操作
            eventSet.Add(eventDesc);
        }
        #endregion


    }
}


