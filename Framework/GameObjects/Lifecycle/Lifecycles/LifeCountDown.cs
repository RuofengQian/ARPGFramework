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
            // 生命倒计时
            StartCoroutine(DestroyCountDown());
        }
        #endregion


        #region Life
        // 游戏计时器
        private static GameTickManager GameTick = GameTickManager.Instance;

        // 消失倒计时
        private float initDuration = 1f;
        // 延时
        private float extraDuration = 0f;


        public void AddExtraDuration(float val)
        {
            extraDuration += val;
        }

        private IEnumerator DestroyCountDown()
        {
            // 技能固有持续时长
            yield return GameTick.WaitForGameSeconds(initDuration);
            // 技能延时
            while (extraDuration > 0f)
            {
                float deltaDelay = extraDuration;
                extraDuration = 0f;
                yield return GameTick.WaitForGameSeconds(deltaDelay);
            }

            // 死亡
            base.Death();
            yield break;
        }
        #endregion

    }

}


