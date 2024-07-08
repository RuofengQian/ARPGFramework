using UnityEngine;


namespace MyFramework.GameObjects.Lifecycle
{
    public sealed class LifecycleDamagedTimes : LifecycleBase, IDamageBasedLifecycle
    {
        #region Interface
        public static void AttachLifecycleComponent(GameObject attachGameObject, object data)
        {
            // TODO
        }
        public override void ResetLifecycle()
        {
            leftTimes = initTimes;
        }
        #endregion

        #region Attribute
        // ��������
        private bool _allowHeal = false;
        public bool allowHeal { get => _allowHeal; private set => _allowHeal = value; }

        // �����ܵ��˺�
        private bool _allowDamage = true;
        public bool allowDamage { get => _allowDamage; private set => _allowDamage = value; }

        public void SetAllowHeal(bool value = true)
        {
            allowHeal = value;
        }
        public void SetAllowDamage(bool value = true)
        {
            allowDamage = value;
        }


        // ��ʼ���ܻ�����
        private int _initTimes = 0;
        public int initTimes { get => _initTimes; private set => _initTimes = value; }

        // ʣ����ܻ�����
        private int _leftTimes = 0;
        public int leftTimes { get => _leftTimes; private set => _leftTimes = value; }

        public void AddLeftTimes(int value)
        {
            leftTimes += value;
        }
        public void CostLeftTimes(int value)
        {
            leftTimes -= value;
            if (leftTimes <= 0)
            {
                base.OnDeath();
            }
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
                base.OnDeath();
            }
            return true;
        }
        public bool TakeHeal(Heal heal)
        {
            return false;
        }
        #endregion

    }

}


