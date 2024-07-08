using MyFramework.Manager.GameTick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.TimeCount
{
    public class TimeCounter : MonoBehaviour
    {
        #region Attribute
        // 协程信息
        private class CoroutineInfo
        {
            // 剩余时间
            public float leftTime;
            // 协程本体
            public Coroutine co;
        }

        // 全局游戏时间管理器
        private GameTimeManager gameTickManager = GameTimeManager.Instance;

        // 协程映射列表
        private Dictionary<string, CoroutineInfo> coMap = new();
        #endregion

        #region Action
        private IEnumerator GametimeCountDown(string coTitle, UnityAction callbackAction)
        {
            CoroutineInfo coInfo = coMap[coTitle];
            while (coInfo.leftTime > 0f)
            {
                yield return null; // 等待一帧

                // TODO：注册到全局事件唤醒器
                while (gameTickManager.isPaused)
                {
                    yield return null; // 等待一帧
                }

                // 动态修改剩余时间
                coInfo.leftTime -= Time.deltaTime; // 使用 Time.deltaTime 确保流逝的时间和实际时间一致
            }

            coInfo.leftTime = 0f; // 确保时间不会小于零

            // 触发回调事件
            callbackAction();
            // 移除无效协程
            coMap.Remove(coTitle);
            yield break;
        }
        #endregion


        #region Interface
        // 开启一个倒计时协程
        public void StartCountDown(string coTitle, float initTime, UnityAction callbackAction)
        {
            if( coMap.ContainsKey(coTitle) )
            {
                return;
            }

            coMap[coTitle] = new()
            {
                leftTime = initTime,
                co = StartCoroutine(GametimeCountDown(coTitle, callbackAction)),
            };
        }
        // 停止一个倒计时协程
        public void StopCountDown(string coTitle)
        {
            if( !coMap.ContainsKey(coTitle) )
            {
                return;
            }

            StopCoroutine(coMap[coTitle].co);
            coMap.Remove(coTitle);
        }

        // 延长一个倒计时协程
        public void ExtendCountDown(string coTitle, float extendTime)
        {
            if (!coMap.ContainsKey(coTitle))
            {
                return;
            }

            coMap[coTitle].leftTime += extendTime;
        }
        // 缩短一个倒计时协程
        public void ReduceCountDown(string coTitle, float declineTime)
        {
            if (!coMap.ContainsKey(coTitle))
            {
                return;
            }

            coMap[coTitle].leftTime -= declineTime;
        }
        #endregion

    }

}


