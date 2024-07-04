using MyFramework.GameObjects.Lifecycle;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 1) 间歇性对敌方伤害技能
    // 示例：那维莱特重击的水柱，隔一段时间对敌方造成伤害
    public abstract class IntermitDamageSkill : IntermitSkillBase, IDamageSkill
    {
        protected override void OnSameGroupEnter(GameObject other)
        {
            return;
        }
        protected override void OnDiffGroupEnter(GameObject other)
        {
            if (SkillBehaviour.DealDamage(other, this.GetDamage()))
            {
                // 此处逻辑可以迁移至具体的派生类中
                if (gameObject.TryGetComponent<LifeAttackedTime>(out LifeAttackedTime lifeAttackTime))
                {
                    lifeAttackTime.CostAttackTimes();
                }
            }
        }

        public abstract Damage GetDamage();

    }

    // 2) 间歇性对敌方伤害+对我方治疗技能
    // 示例：心海E技能水母，芭芭拉E技能水环
    public abstract class IntermitDamageHealSkill : IntermitSkillBase, IDamageSkill, IHealSkill
    {
        protected override void OnSameGroupEnter(GameObject other)
        {
            SkillBehaviour.DealHeal(other, this.GetHeal());
        }
        protected override void OnDiffGroupEnter(GameObject other)
        {
            if (SkillBehaviour.DealDamage(other, this.GetDamage()))
            {
                // 此处逻辑可以迁移至具体的派生类中
                if (gameObject.TryGetComponent<LifeAttackedTime>(out LifeAttackedTime lifeAttackTime))
                {
                    lifeAttackTime.CostAttackTimes();
                }
            }
        }

        public abstract Damage GetDamage();
        public abstract Heal GetHeal();

    }

}





