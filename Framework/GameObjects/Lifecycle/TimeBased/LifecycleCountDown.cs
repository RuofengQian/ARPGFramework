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
            // ������������ʱ
            StartCoroutine(LifeCountDown(base.OnDeath));
        }

        public override void ResetLifecycle()
        {
            leftTime = initTime;
        }
        #endregion

        #region Attribute
        // ��Ϸʱ�����������̬��
        private static GameTimeManager GameTime = GameTimeManager.Instance;

        // ��ʼʱ��
        private float _initTime = 1f;
        public float initTime { get => _initTime; private set => initTime = value; }

        // ʣ��ʱ��
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
                // TODO����֤�Ƿ�ȼ��ڹ۲�GameTime.isPasued
                yield return new WaitForFixedUpdate(); // �ȴ��´θ���

                // ��̬�޸�ʣ��ʱ��
                leftTime -= Time.deltaTime; // ʹ�� Time.deltaTime ȷ�����ŵ�ʱ���ʵ��ʱ��һ��
            }
            leftTime = 0f; // ȷ��ʱ�䲻��С����

            // �����ص��¼�
            callbackAction();

            yield break;
        }
        #endregion

    }

}


