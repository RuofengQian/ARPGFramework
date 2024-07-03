using MyFramework.GameObjects.Attribute;
using UnityEngine;


namespace MyFramework.GameObjects.Lifecycle
{
    public sealed class LifeDamagedTime : LifecycleBase<LifeDamagedTime>
    {
        #region Interface
        new public static LifeDamagedTime Attach(GameObject gameObject, int initDamageTimes)
        {
            LifeDamagedTime lifecycle = gameObject.AddComponent<LifeDamagedTime>();
            lifecycle.initTimes = initDamageTimes;
            lifecycle.leftTimes = initDamageTimes;
            return lifecycle;
        }

        public override void ResetLifecycle()
        {
            leftTimes = initTimes;
        }
        #endregion


        #region Life
        // 初始可受击次数
        private int initTimes = 0;
        // 剩余可受击次数
        private int leftTimes = 0;


        public void AddLeftTimes(int value)
        {
            leftTimes += value; 
        }

        public bool TakeDamage(Damage damage)
        {
            if( damage.method == ValueEffectMode.Percentage )
            {
                return false;
            }

            // else if (damage.method == ValueEffectMode.Absolute)
            --leftTimes;
            if( leftTimes <= 0 )
            {
                base.Death();
            }
            return true;
        }
        #endregion

    }

}


