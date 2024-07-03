using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.Manager.GameTick
{
    public class GameTickManager : MonoBehaviour
    {
        #region Instance
        // 单例实例
        private static GameTickManager _instance; 

        public static GameTickManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // 在场景中找到现有的 GameTickHandler 实例
                    _instance = FindObjectOfType<GameTickManager>();

                    // 如果场景中没有，创建一个新的空对象并附加 GameTickHandler
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject("GameTickHandler");
                        _instance = obj.AddComponent<GameTickManager>();
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


        #region State
        // 暂停游戏时间管理
        private bool _isPaused = false;

        public bool isPaused
        {
            get => _isPaused;
            private set => _isPaused = value;
        }


        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0f; // 停止时间缩放
        }
        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1f; // 恢复时间缩放为正常速度
        }
        #endregion


        #region Time
        private double gameStartTime;
        private double elapsedGameTime;


        private void FixedUpdate()
        {
            elapsedGameTime += Time.deltaTime;
        }
        private void ResetElapsedTime()
        {
            elapsedGameTime = 0f; 
        }
        #endregion


        #region HandleCoroutine
        // 开始托管一个协程
        public Coroutine StartGametimeCoroutine(IEnumerator routine)
        {
            // 创建协程
            Coroutine co = StartCoroutine(GametimeCoroutine(routine));
            // 返回一个访问实例
            return co;
        }
        // 终止一个托管的协程
        public void StopGametimeCoroutine(Coroutine coToStop)
        {
            StopCoroutine(coToStop);
        }

        // 游戏运行时协程：游戏内真正执行的协程
        private IEnumerator GametimeCoroutine(IEnumerator routine)
        {
            yield return null;
            while (true)
            {
                // 暂停状态：跳过，不执行
                if( isPaused )
                {
                    yield return null;
                }

                // 正常状态
                if(!routine.MoveNext())
                {
                    // 协程执行完毕，终止
                    yield break;
                }
                yield return routine.Current;
            }
        }

        public IEnumerator WaitForGameSeconds(float delay)
        {
            double startTime = elapsedGameTime; // 内部记录等待开始时的游戏时间

            while ( isPaused || elapsedGameTime - startTime < delay )
            {
                yield return null;
            }
            yield break;
        }
        #endregion

    }

}





