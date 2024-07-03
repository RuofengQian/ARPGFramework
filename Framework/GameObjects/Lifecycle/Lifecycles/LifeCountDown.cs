using MyFramework.Manager.GameTick;
using System.Collections;
using UnityEngine;


namespace MyFramework.GameObjects.Lifecycle
{
    public sealed class LifeCountDown : LifecycleBase<LifeCountDown>
    {
        #region Interface
        new public static LifeCountDown Attach(GameObject gameObject, float duration)
        {
            LifeCountDown lifecycle = gameObject.AddComponent<LifeCountDown>();
            lifecycle.initDuration = duration;
            return lifecycle;
        }

        public override void ResetLifecycle()
        {
            extraDuration = 0f;
        }
        private void OnEnable()
        {
            // ��������ʱ
            StartCoroutine(DestroyCountDown());
        }
        #endregion


        #region Life
        // ��Ϸ��ʱ��
        private static GameTickManager GameTick = GameTickManager.Instance;

        // ��ʧ����ʱ
        private float initDuration = 1f;
        // ��ʱ
        private float extraDuration = 0f;


        public void AddExtraDuration(float val)
        {
            extraDuration += val;
        }

        private IEnumerator DestroyCountDown()
        {
            // ���ܹ��г���ʱ��
            yield return GameTick.WaitForGameSeconds(initDuration);
            // ������ʱ
            while (extraDuration > 0f)
            {
                float deltaDelay = extraDuration;
                extraDuration = 0f;
                yield return GameTick.WaitForGameSeconds(deltaDelay);
            }

            // ����
            base.Death();
            yield break;
        }
        #endregion

    }

}


