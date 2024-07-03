using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Attribute
{
    /// <summary>
    /**
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

    // 伤害结构体：信息传递
    public struct Damage
    {
        public DamageType type;
        public EffectMethod method;
        public float value;
    }
    // 治疗结构体
    public struct Heal
    {
        public EffectMethod method;
        public float value;
    }
    **/
    /// </summary>
}

namespace MyFramework.GameObjects.Behaviour
{
    // 1) 技能：对敌方伤害
    public abstract class DamageSkill : SkillBase, IDamageSkill
    {
        #region Override
        protected override void InteractDiffGroup(GameObject other)
        {
            if (SkillBehaviour.DealDamage(other, this.GetDamage()))
            {
                if (lifeAttackTime != null)
                {
                    lifeAttackTime.DoAttack();
                }
            }
        }
        protected override void InteractSameGroup(GameObject other)
        {
            return;
        }
        #endregion

        #region Action
        public abstract Damage GetDamage();
        #endregion

    }

    // 2) 技能：对敌方伤害+对我方治疗
    public abstract class DamageHealSkill : SkillBase, IDamageSkill, IHealSkill
    {
        #region Override
        protected override void InteractDiffGroup(GameObject other)
        {
            if (SkillBehaviour.DealDamage(other, this.GetDamage()))
            {
                if (lifeAttackTime != null)
                {
                    lifeAttackTime.DoAttack();
                }
            }
        }
        protected override void InteractSameGroup(GameObject other)
        {
            // 施加治疗
            SkillBehaviour.DealHeal(other, this.GetHeal());
        }
        #endregion

        #region Action
        public abstract Damage GetDamage();
        public abstract Heal GetHeal();
        #endregion

    }

    // 3) 技能：对敌方伤害+施加DeBuff
    public abstract class DamageDeBuffSkill : SkillBase, IDamageSkill, IDeBuffSkill
    {
        #region Override
        protected override void InteractDiffGroup(GameObject other)
        {
            if (SkillBehaviour.DealDamage(other, this.GetDamage()))
            {
                if (lifeAttackTime != null)
                {
                    lifeAttackTime.DoAttack();
                }
            }
            SkillBehaviour.AttachDeBuff(other, this.GetBuff());
        }
        protected override void InteractSameGroup(GameObject other)
        {
            return;
        }
        #endregion

        #region Action
        public abstract Damage GetDamage();
        public abstract BuffInfo GetBuff();
        #endregion

    }

    // 4) 技能：对我方施加Buff
    public abstract class BuffSkill : SkillBase, IBuffSkill
    {
        #region Override
        protected override void InteractDiffGroup(GameObject other)
        {
            return;
        }
        protected override void InteractSameGroup(GameObject other)
        {
            SkillBehaviour.AttachBuff(other, this.GetBuff());
        }
        #endregion


        #region Action
        public abstract BuffInfo GetBuff();
        #endregion
    }

    // 5) 技能：在碰撞范围内添加Buff
    // TODO：实现特殊逻辑
    public abstract class RangeBuffSkill : SkillBase
    {
        // 保存
        private List<BuffController> entityBuffController = new();


        
        // 流程：
        // OnTriggerEnter - if Ally - Add Persist Buff
        private void OnTriggerEnter(Collider other)
        {
            if( other )
            {

            }
        }
        // OnTriggerExit - is Ally - Remove Persist Buff
        private void OnTriggerExit(Collider other)
        {
            
        }

        public abstract BuffInfo AddBuff();
        public abstract void RemoveBuff();

    }

}


