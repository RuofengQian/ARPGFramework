using MyFramework.GameObjects.Attribute;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.Lifecycle
{
    // �˺��ṹ�壺��Ϣ����
    public struct Damage
    {
        public DamageType type;
        public ValueEffectMode method;
        public float value;
    }
    // ���ƽṹ��
    public struct Heal
    {
        public ValueEffectMode method;
        public float value;
    }

    // ��ʼ����Ϣ
    public struct EntityAttrInfo
    {
        public float rawPhysicDefence;
        public float rawMagicDefence;
        public float rawHealth;

        public float rawAttack;
        
    }

    public sealed class LifeEntityHealth : LifecycleBase<LifeEntityHealth>, IHealthLifecycle
    {
        #region Interface
        new public static LifeEntityHealth Attach(GameObject gameObject, float rawHealth)
        {
            LifeEntityHealth lifecycle = gameObject.AddComponent<LifeEntityHealth>();
            lifecycle.Health.SetRawValue(rawHealth);
            lifecycle.Health.Reset();
            return lifecycle;
        }

        public override void ResetLifecycle()
        {
            Health.Reset();
        }
        #endregion

        #region Attirbute
        // ����ֵ����
        private Health _health;

        // ����������
        private Dictionary<DamageType, Defence> _defenceMap; // �������͵ķ�����
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
        private bool _allowHeal = false;
        public bool allowHeal { get => _allowHeal; private set => _allowHeal = value; }

        public void AllowHeal(bool value = true)
        {
            allowHeal = value;
        }
        #endregion

        #region Init
        private void Awake()
        {
            _defenceMap = new();
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


        #region Lifecycle
        // ˵����bool����ֵָʾ�Ƿ�ɹ�����˺�/���ƣ������������ܵ��߼��ص��������ע�������

        // �ܵ��˺�
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
                if(hasShield)
                {
                    damage.value = Shield.GetDamageValueAfterShieldResist(damage.value);
                    // �˺���ȫ���ֵ�
                    if(hasShield)
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
                if(isAlive)
                {
                    takeDamageEvent?.Invoke(gameObject);
                }
                else
                {
                    base.Death();
                    return true;
                }
            }
            return true; // �ɹ��ܵ��˺�
        }
        // �ܵ�����
        public bool TakeHeal(Heal heal)
        {
            if( !allowHeal )
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

    }

}


