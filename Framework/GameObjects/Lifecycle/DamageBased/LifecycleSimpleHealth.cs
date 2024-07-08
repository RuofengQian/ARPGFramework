using UnityEngine;


namespace MyFramework.GameObjects.Lifecycle
{
    public sealed class LifecycleSimpleHealth : LifecycleBase, IDamageBasedLifecycle
    {
        #region Interface
        public override void ResetLifecycle()
        {
            currHealth = maxHealth;
        }
        #endregion

        #region Attribute
        // 允许被治疗
        private bool _allowHeal = false;
        public bool allowHeal { get => _allowHeal; private set => _allowHeal = value; }

        // 允许受到伤害
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


        // 最大生命值
        private float _maxHealth = 1f;
        public float maxHealth { get => _maxHealth; private set => _maxHealth = value; }

        // 当前生命值
        private float _currHealth = 1f;
        public float currHealth { get => _currHealth; private set => _currHealth = value; }

        public void SetMaxHealth(float value)
        {
            maxHealth = value;
            if( currHealth > maxHealth )
            {
                currHealth = maxHealth;
            }
        }
        public void SetCurrHealth(float value)
        {
            currHealth = value;
            if (currHealth > maxHealth)
            {
                currHealth = maxHealth;
            }
        }
        #endregion


        #region Action
        public bool TakeDamage(Damage damage)
        {
            if(damage.method == ValueEffectMode.Percentage)
            {
                return false;
            }

            // else if (damage.method == ValueEffectMode.Absolute) // 仅绝对值伤害有效
            currHealth -= damage.value;
            if (currHealth <= 0f)
            {
                base.OnDeath();
            }
            return true;
        }
        public bool TakeHeal(Heal heal)
        {
            if (!allowHeal || heal.method == ValueEffectMode.Percentage)
            {
                return false;
            }

            // else if (heal.method == ValueEffectMode.Absolute) // 仅绝对值治疗有效
            currHealth += heal.value;
            if (currHealth > maxHealth)
            {
                currHealth = maxHealth;
            }
            return true;
        }
        #endregion

    }

}



