using MyFramework.GameObjects.Attribute;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Lifecycle
{
    public sealed class LifeSmipleHealth : LifecycleBase<LifeSmipleHealth>, IHealthLifecycle
    {
        #region Interface
        new public static LifeSmipleHealth Attach(GameObject gameObject, float maxHealth)
        {
            LifeSmipleHealth lifecycle = gameObject.AddComponent<LifeSmipleHealth>();
            lifecycle.maxHealth = maxHealth;
            lifecycle.currHealth = maxHealth;
            return lifecycle;
        }

        public override void ResetLifecycle()
        {
            currHealth = maxHealth;
        }
        #endregion


        #region Life
        // �������ֵ
        private float maxHealth = 1f;
        // ��ǰ����ֵ
        private float currHealth = 1f;

        // ��������
        private bool _allowHeal = false;
        public bool allowHeal { get => _allowHeal; private set => _allowHeal = value; }

        public void AllowHeal(bool value = true)
        {
            allowHeal = value;
        }
        
        public bool TakeDamage(Damage damage)
        {
            if(damage.method == ValueEffectMode.Percentage)
            {
                return false;
            }

            // else if (damage.method == ValueEffectMode.Absolute) // ������ֵ�˺���Ч
            currHealth -= damage.value;
            if (currHealth <= 0f)
            {
                base.Death();
            }
            return true;
        }
        public bool TakeHeal(Heal heal)
        {
            if (!allowHeal || heal.method == ValueEffectMode.Percentage)
            {
                return false;
            }

            // else if (damage.method == ValueEffectMode.Absolute) // ������ֵ������Ч
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



