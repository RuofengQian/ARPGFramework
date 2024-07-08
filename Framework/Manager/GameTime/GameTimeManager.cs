using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.Manager.GameTick
{
    public class GameTimeManager : MonoBehaviour
    {
        // TODO：改为MonoBehaviour版本的继承式单例
        #region Singleton
        // 单例实例
        private static GameTimeManager _instance; 

        public static GameTimeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // 在场景中找到现有的 GameTickHandler 实例
                    _instance = FindObjectOfType<GameTimeManager>();

                    // 如果场景中没有，创建一个新的空对象并附加 GameTickHandler
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("GameTickHandler");
                        _instance = obj.AddComponent<GameTimeManager>();
                    }
                }
                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject); // 保证在场景切换时不销毁该对象
            }
            else
            {
                Destroy(gameObject); // 如果已存在其他实例，则销毁该实例
            }
        }
        #endregion


        #region GameTimeControl
        // 游戏暂停标志
        private bool _isPaused = false;
        public bool isPaused
        {
            get => _isPaused;
            private set => _isPaused = value;
        }

        // 暂停游戏时间
        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0f; // 停止时间缩放
        }
        // 恢复游戏时间
        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1f; // 恢复时间缩放为正常速度
        }
        #endregion


        #region Attribute
        // 协程信息
        private class CoroutineInfo
        {
            // 剩余时间
            public float leftTime;
            // 协程本体
            public Coroutine co;
        }

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

                // TODO：优化？？
                while( isPaused )
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
            if (coMap.ContainsKey(coTitle))
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
            if (!coMap.ContainsKey(coTitle))
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





