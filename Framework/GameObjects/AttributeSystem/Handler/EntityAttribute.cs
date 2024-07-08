using MyFramework.GameObjects.Lifecycle;
using System;
using UnityEngine;


namespace MyFramework.GameObjects.Attribute
{
    // �˺�����
    public enum DamageType
    {
        // ��ʵ�˺�
        Real = 0,
        // �����˺�
        Physic = 1,
        // ħ���˺�
        Magic = 2,
    }

    public sealed class EntityAttribute : MonoBehaviour
    {
        #region Interface
        public static EntityAttribute CreateInstance(GameObject gameObject)
        {
            return gameObject.AddComponent<EntityAttribute>();
        }
        #endregion

        #region Attirbute
        // ����������������
        private LifecycleEntityHealth _lifecycle;
        public LifecycleEntityHealth Lifecycle { get => _lifecycle; private set => _lifecycle = value; }
        #endregion

        #region Init
        public void BindLifecycle(LifecycleEntityHealth lifeEntity)
        {
            this.Lifecycle = lifeEntity;
        }

        private void Start()
        {
            if( Lifecycle != null )
            {
                return;
            }
            
            if (!gameObject.TryGetComponent<LifecycleEntityHealth>(out _lifecycle))
            {
                Debug.LogError("[Error] û���ڶ������ҵ� LifecycleEntityHealth ��������������������Թ�����");
                Destroy(this);
            }
        }
        #endregion


        #region Lifecycle
        // ����ֵ
        public Health Health
        {
            get => Lifecycle.Health;
        }
        // �������
        public Defence PhysicDefence
        {
            get => Lifecycle.PhysicDefence;
        }
        // ħ������
        public Defence MagicDefence
        {
            get => Lifecycle.MagicDefence;
        }
        // �˺�����
        public Defence DamageReduction
        {
            get => Lifecycle.DamageReduction;
        }
        // ����ֵ
        public Shield Shield
        {
            get => Lifecycle.Shield;
        }
        #endregion

        #region InteractiveAttribute
        // ����������
        private Attack _attack;
        // ��������
        private CriticalStrike _critStrike;

        public Attack Attack
        {
            get => _attack ??= new(0f, 0f, 0f);
            private set => _attack = value;
        }
        public CriticalStrike CritStrike
        {
            get => _critStrike ??= new(0f, 0f);
            private set => _critStrike = value;
        }

        // ��������˺�
        public bool allowDamage { get => _attack != null; }
        // ������
        public bool allowCrit { get => _critStrike != null; }
        #endregion

        #region OtherAttribute
        // �����ٶ�����
        private AttackSpeed _attackSpeed;
        // �ƶ��ٶ�����
        private MoveSpeed _moveSpeed;

        public AttackSpeed AttackSpeed
        {
            get => _attackSpeed ??= new(0f, 0f);
            private set => _attackSpeed = value;
        }
        public MoveSpeed MoveSpeed
        {
            get => _moveSpeed ??= new(1f, 0f);
            private set => _moveSpeed = value;
        }
        #endregion

    }

}



