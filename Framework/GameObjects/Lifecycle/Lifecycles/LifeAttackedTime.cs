using UnityEngine;


namespace MyFramework.GameObjects.Lifecycle
{
    public sealed class LifeAttackedTime : LifecycleBase<LifeAttackedTime>
    {
        #region Interface
        new public static LifeAttackedTime Attach(GameObject gameObject, int initAttackTimes)
        {
            LifeAttackedTime attackTime = gameObject.AddComponent<LifeAttackedTime>();
            attackTime.initTimes = initAttackTimes;
            attackTime.leftTimes = initAttackTimes;
            return attackTime;
        }

        public override void ResetLifecycle()
        {
            leftTimes = initTimes;
        }
        #endregion


        #region Life
        // ��ʼ�ɹ�������
        private int initTimes = 0;
        // ʣ��ɹ�������
        private int leftTimes = 0;


        public void AddLeftTimes(int value)
        {
            leftTimes += value;
        }

        public void DoAttack()
        {
            --leftTimes;
            if (leftTimes <= 0)
            {
                base.Death();
            }
        }
        #endregion

    }

}


