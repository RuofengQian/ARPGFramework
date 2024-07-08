using MyFramework.GameObjects.Attribute;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.Lifecycle
{
    // ��Ϣ����
    // 1) �˺��ṹ��
    public struct Damage
    {
        // ���÷�ʽ������ֵ���ٷֱ�
        public ValueEffectMode method;
        // �˺�����
        public DamageType type;
        // ֵ
        public float value;
    }
    // 2) ���ƽṹ��
    public struct Heal
    {
        // ���÷�ʽ
        public ValueEffectMode method;
        // ֵ
        public float value;
    }

    public sealed class LifecycleEntityHealth : LifecycleBase, IDamageBasedLifecycle
    {
        #region Interface
        public override void ResetLifecycle()
        {
            Health.Reset();
        }
        #endregion

        #region Attirbute
        // ����ֵ����
        private Health _health;

        // ����������
        private Dictionary<DamageType, Defence> _defenceMap = new(); // �������͵ķ�����
        // ����ֵ����
        private Shield _shield;

        
        public Defence PhysicDefence
        {
            get => _defenceMap[DamageType.Physic] ??= new(ValueEffectMode.Absolute, 0f, 0f);
            private set => _defenceMap[DamageType.Physic] = value;
        }
        public Defence MagicDefence
        {
            get => _defenceMap[DamageType.Magic] ??= new(ValueEffectMode.Percentage, 0f, 0f);
            private set => _defenceMap[DamageType.Magic] = value;
        }
        // TODO�����˺������Ϊһ��ISimpleValue�Ķ�������
        public Defence DamageReduction
        {
            get => _defenceMap[DamageType.Real] ??= new(ValueEffectMode.Percentage, 0f, 0f);
            private set => _defenceMap[DamageType.Real] = value;
        }
        public Shield Shield
        {
            get => _shield ??= new(0f);
            private set => _shield = value;
        }
        public Health Health
        {
            get => _health ??= new(1f, 0f, 0f);
            private set => _health = value;
        }

        // ���
        public bool isAlive { get => _health.valid; }
        // ӵ�л���
        public bool hasShield { get => (_shield != null) && _shield.valid; }


        // ��������
        private bool _allowHeal = true;
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
        #endregion


        #region Action
        // ˵����bool����ֵָʾ�Ƿ�ɹ�����˺�/���ƣ������������ܵ��߼��ص��������ע�������

        // ���Լ���
        public bool TakeDamage(Damage damage)
        {
            // ���ٷֱȿ۳�Ѫ��
            if (damage.method == ValueEffectMode.Percentage)
            {
                Health.CostCurrValueByPer(damage.value);
            }
            // ������ֵ�۳�Ѫ����Ĭ�ϣ�
            else
            {
                // ��ʵ�˺������ӷ���ֵ�ͻ���ֵ
                // ���������˺������ɷ���ֵ����ó����ٳ��Կ۳���Ӧ����ֵ�����۳�����ֵ

                // 1) ������
                if (damage.type == DamageType.Physic)
                {
                    damage.value = PhysicDefence.GetDamageValueAfterDefenceDec(damage.value);
                }
                else if (damage.type == DamageType.Magic)
                {
                    damage.value = MagicDefence.GetDamageValueAfterDefenceDec(damage.value);
                }
                // ��������
                damage.value = DamageReduction.GetDamageValueAfterDefenceDec(damage.value);

                // 2) ����ֵ
                if (hasShield)
                {
                    damage.value = Shield.GetDamageValueAfterShieldResist(damage.value);
                    // �˺���ȫ���ֵ�
                    if (hasShield)
                    {
                        shieldResistEvent?.Invoke(gameObject);
                        return true; // ���أ������������˺�
                    }
                    // �˺������ֵֵ�
                    else
                    {
                        shieldBreakEvent?.Invoke(gameObject);
                    }
                }

                // 3) ����ֵ
                Health.CostCurrValue(damage.value);
                if (isAlive)
                {
                    takeDamageEvent?.Invoke(gameObject);
                }
                else
                {
                    base.OnDeath();
                    return true;
                }
            }
            return true; // �ɹ��ܵ��˺�
        }
        // ���Իָ�
        public bool TakeHeal(Heal heal)
        {
            if (!allowHeal)
            {
                return false;
            }

            // ���ٷֱȻָ�Ѫ��
            if (heal.method == ValueEffectMode.Percentage)
            {
                Health.AddCurrValueByPer(heal.value);
            }
            // ������ֵ�ָ�Ѫ��
            else
            {
                Health.AddCurrValue(heal.value);
            }
            takeHealEvent?.Invoke(gameObject);
            return true;
        }
        #endregion

        #region Event.Health
        // ���˻ص��¼�
        private UnityEvent<GameObject> takeDamageEvent;
        // �����ָ��ص��¼�
        private UnityEvent<GameObject> takeHealEvent;

        public void RegisterTakeDamageEvent(UnityAction<GameObject> action)
        {
            takeDamageEvent ??= new UnityEvent<GameObject>();
            takeDamageEvent.AddListener(action);
        }
        public void RemoveTakeDamageEvent(UnityAction<GameObject> action)
        {
            takeDamageEvent?.RemoveListener(action);
        }

        public void RegisterTakeHealEvent(UnityAction<GameObject> action)
        {
            takeHealEvent ??= new UnityEvent<GameObject>();
            takeHealEvent.AddListener(action);
        }
        public void RemoveTakeHealEvent(UnityAction<GameObject> action)
        {
            takeHealEvent?.RemoveListener(action);
        }
        #endregion

        #region Event.Shield
        // ���ֵܵ��¼�
        private UnityEvent<GameObject> shieldResistEvent;
        // ���������¼�
        private UnityEvent<GameObject> shieldBreakEvent;

        public void RegisterShieldResistEvent(UnityAction<GameObject> action)
        {
            shieldResistEvent ??= new UnityEvent<GameObject>();
            shieldResistEvent.AddListener(action);
        }
        public void RemoveShieldResistEvent(UnityAction<GameObject> action)
        {
            shieldResistEvent?.RemoveListener(action);
        }

        public void RegisterShieldBreakEvent(UnityAction<GameObject> action)
        {
            shieldBreakEvent ??= new UnityEvent<GameObject>();
            shieldBreakEvent.AddListener(action);
        }
        public void RemoveShieldBreakEvent(UnityAction<GameObject> action)
        {
            shieldBreakEvent?.RemoveListener(action);
        }
        #endregion

    }

}


