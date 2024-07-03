using MyFramework.GameObjects.Lifecycle;
using UnityEngine;


namespace MyFramework.GameObjects.Attribute
{
    // 伤害类型
    public enum DamageType
    {
        // 真实伤害
        Real = 0,
        // 物理伤害
        Physic = 1,
        // 魔法伤害
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
        // 关联对象生命周期
        private LifeEntityHealth _lifecycle;
        public LifeEntityHealth Lifecycle
        {
            get => _lifecycle;
            private set => _lifecycle = value;
        }
        #endregion

        #region Init
        private void Start()
        {
            if (!gameObject.TryGetComponent<LifeEntityHealth>(out _lifecycle))
            {
                Debug.LogError("[Error] 没有在 Entity 对象上找到生命周期组件，销毁属性管理器");
                Destroy(this);
            }
        }
        #endregion


        #region Lifecycle
        // 生命值
        public Health Health
        {
            get => Lifecycle.Health;
        }
        // 物理防御
        public Defence PhysicDefence
        {
            get => Lifecycle.PhysicDefence;
        }
        // 魔法防御
        public Defence MagicDefence
        {
            get => Lifecycle.MagicDefence;
        }
        // 伤害减免
        public Defence DamageReduction
        {
            get => Lifecycle.DamageReduction;
        }
        // 护盾值
        public Shield Shield
        {
            get => Lifecycle.Shield;
        }
        #endregion

        #region InteractiveAttribute
        // 攻击力属性
        private Attack _attack;
        // 暴击属性
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

        // 可以造成伤害
        public bool allowDamage { get => _attack != null; }
        // 允许暴击
        public bool allowCrit { get => _critStrike != null; }
        #endregion

        #region OtherAttribute
        // 攻击速度属性
        private AttackSpeed _attackSpeed;
        // 移动速度属性
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



