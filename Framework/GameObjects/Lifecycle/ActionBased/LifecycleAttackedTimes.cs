

namespace MyFramework.GameObjects.Lifecycle
{
    public sealed class LifecycleAttackedTimes : LifecycleBase, IActionBasedLifecycle
    {
        #region Interface
        public override void ResetLifecycle()
        {
            leftActionTimes = initActionTimes;
        }
        #endregion


        #region Life
        // ��ʼ�ɹ�������
        private int _initAttackTimes = 0;
        public int initActionTimes { get => _initAttackTimes; private set => _initAttackTimes = value; }

        // ʣ��ɹ�������
        private int _leftAttackTimes = 0;
        public int leftActionTimes { get => _leftAttackTimes; private set => _leftAttackTimes = value; }


        public bool CostActionTime(int costTimes = 1)
        {
            leftActionTimes -= costTimes;
            if (leftActionTimes <= 0)
            {
                base.OnDeath();
            }
            return true;
        }
        public bool AddActionTime(int addTimes)
        {
            leftActionTimes += addTimes;
            return true;
        }
        #endregion

    }

}


