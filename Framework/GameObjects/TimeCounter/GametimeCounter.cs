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
        // Э����Ϣ
        private class CoroutineInfo
        {
            // ʣ��ʱ��
            public float leftTime;
            // Э�̱���
            public Coroutine co;
        }

        // ȫ����Ϸʱ�������
        private GameTimeManager gameTickManager = GameTimeManager.Instance;

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

                // TODO��ע�ᵽȫ���¼�������
                while (gameTickManager.isPaused)
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
        // ֹͣһ������ʱЭ��
        public void StopCountDown(string coTitle)
        {
            if( !coMap.ContainsKey(coTitle) )
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


