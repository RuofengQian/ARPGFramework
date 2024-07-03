using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.Manager.GameTick
{
    public class GameTickManager : MonoBehaviour
    {
        #region Instance
        // ����ʵ��
        private static GameTickManager _instance; 

        public static GameTickManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // �ڳ������ҵ����е� GameTickHandler ʵ��
                    _instance = FindObjectOfType<GameTickManager>();

                    // ���������û�У�����һ���µĿն��󲢸��� GameTickHandler
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
                DontDestroyOnLoad(gameObject); // ��֤�ڳ����л�ʱ�����ٸö���
            }
            else
            {
                Destroy(gameObject); // ����Ѵ�������ʵ���������ٸ�ʵ��
            }
        }
        #endregion


        #region State
        // ��ͣ��Ϸʱ�����
        private bool _isPaused = false;

        public bool isPaused
        {
            get => _isPaused;
            private set => _isPaused = value;
        }


        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0f; // ֹͣʱ������
        }
        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1f; // �ָ�ʱ������Ϊ�����ٶ�
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
        // ��ʼ�й�һ��Э��
        public Coroutine StartGametimeCoroutine(IEnumerator routine)
        {
            // ����Э��
            Coroutine co = StartCoroutine(GametimeCoroutine(routine));
            // ����һ������ʵ��
            return co;
        }
        // ��ֹһ���йܵ�Э��
        public void StopGametimeCoroutine(Coroutine coToStop)
        {
            StopCoroutine(coToStop);
        }

        // ��Ϸ����ʱЭ�̣���Ϸ������ִ�е�Э��
        private IEnumerator GametimeCoroutine(IEnumerator routine)
        {
            yield return null;
            while (true)
            {
                // ��ͣ״̬����������ִ��
                if( isPaused )
                {
                    yield return null;
                }

                // ����״̬
                if(!routine.MoveNext())
                {
                    // Э��ִ����ϣ���ֹ
                    yield break;
                }
                yield return routine.Current;
            }
        }

        public IEnumerator WaitForGameSeconds(float delay)
        {
            double startTime = elapsedGameTime; // �ڲ���¼�ȴ���ʼʱ����Ϸʱ��

            while ( isPaused || elapsedGameTime - startTime < delay )
            {
                yield return null;
            }
            yield break;
        }
        #endregion

    }

}





