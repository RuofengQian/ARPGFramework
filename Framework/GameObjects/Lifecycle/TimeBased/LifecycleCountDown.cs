using MyFramework.Manager.GameTick;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.Lifecycle
{
    public sealed class LifecycleCountDown : LifecycleBase, ITimeBasedLifecycle
    {
        #region Interface
        private void OnEnable()
        {
            // 开启生命倒计时
            StartCoroutine(LifeCountDown(base.OnDeath));
        }

        public override void ResetLifecycle()
        {
            leftTime = initTime;
        }
        #endregion

        #region Attribute
        // 游戏时间管理器（静态）
        private static GameTimeManager GameTime = GameTimeManager.Instance;

        // 初始时间
        private float _initTime = 1f;
        public float initTime { get => _initTime; private set => initTime = value; }

        // 剩余时间
        private float _leftTime = 1f;
        public float leftTime { get => _leftTime; private set => _leftTime = value; }


        public bool AddLeftTime(float extendTime)
        {
            leftTime += extendTime;
            return true;
        }
        public bool CostLeftTime(float declineTime)
        {
            leftTime -= declineTime;
            return true;
        }

        public void SetInitTime(float val)
        {
            initTime = val;
        }
        #endregion


        #region Action
        private IEnumerator LifeCountDown(UnityAction callbackAction)
        {
            while (leftTime > 0f)
            {
                // TODO：验证是否等价于观察GameTime.isPasued
                yield return new WaitForFixedUpdate(); // 等待下次更新

                // 动态修改剩余时间
                leftTime -= Time.deltaTime; // 使用 Time.deltaTime 确保流逝的时间和实际时间一致
            }
            leftTime = 0f; // 确保时间不会小于零

            // 触发回调事件
            callbackAction();

            yield break;
        }
        #endregion

    }

}


