using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.Manager.GameTick
{
    public class GameTimeManager : MonoBehaviour
    {
        // TODO����ΪMonoBehaviour�汾�ļ̳�ʽ����
        #region Singleton
        // ����ʵ��
        private static GameTimeManager _instance; 

        public static GameTimeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // �ڳ������ҵ����е� GameTickHandler ʵ��
                    _instance = FindObjectOfType<GameTimeManager>();

                    // ���������û�У�����һ���µĿն��󲢸��� GameTickHandler
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
                DontDestroyOnLoad(gameObject); // ��֤�ڳ����л�ʱ�����ٸö���
            }
            else
            {
                Destroy(gameObject); // ����Ѵ�������ʵ���������ٸ�ʵ��
            }
        }
        #endregion


        #region GameTimeControl
        // ��Ϸ��ͣ��־
        private bool _isPaused = false;
        public bool isPaused
        {
            get => _isPaused;
            private set => _isPaused = value;
        }

        // ��ͣ��Ϸʱ��
        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0f; // ֹͣʱ������
        }
        // �ָ���Ϸʱ��
        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1f; // �ָ�ʱ������Ϊ�����ٶ�
        }
        #endregion


        #region Attribute
        // Э����Ϣ
        private class CoroutineInfo
        {
            // ʣ��ʱ��
            public float leftTime;
            // Э�̱���
            public Coroutine co;
        }

        // Э��ӳ���б�
        private Dictionary<string, CoroutineInfo> coMap = new();
        #endregion

        #region Action
        private IEnumerator GametimeCountDown(string coTitle, UnityAction callbackAction)
        {
            CoroutineInfo coInfo = coMap[coTitle];
            while (coInfo.leftTime > 0f)
            {
                yield return null; // �ȴ�һ֡

                // TODO���Ż�����
                while( isPaused )
                {
                    yield return null; // �ȴ�һ֡
                }

                // ��̬�޸�ʣ��ʱ��
                coInfo.leftTime -= Time.deltaTime; // ʹ�� Time.deltaTime ȷ�����ŵ�ʱ���ʵ��ʱ��һ��
            }

            coInfo.leftTime = 0f; // ȷ��ʱ�䲻��С����

            // �����ص��¼�
            callbackAction();
            // �Ƴ���ЧЭ��
            coMap.Remove(coTitle);
            yield break;
        }
        #endregion


        #region Interface
        // ����һ������ʱЭ��
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
        // ֹͣһ������ʱЭ��
        public void StopCountDown(string coTitle)
        {
            if (!coMap.ContainsKey(coTitle))
            {
                return;
            }

            StopCoroutine(coMap[coTitle].co);
            coMap.Remove(coTitle);
        }

        // �ӳ�һ������ʱЭ��
        public void ExtendCountDown(string coTitle, float extendTime)
        {
            if (!coMap.ContainsKey(coTitle))
            {
                return;
            }

            coMap[coTitle].leftTime += extendTime;
        }
        // ����һ������ʱЭ��
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





