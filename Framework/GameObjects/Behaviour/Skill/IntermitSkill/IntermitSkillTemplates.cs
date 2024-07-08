using MyFramework.GameObjects.Lifecycle;
using MyProject.GameObjects.Tags;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 1) 间歇性对敌方伤害技能
    // 示例：那维莱特重击的水柱，隔一段时间对敌方造成伤害
    public abstract class IntermitDamageSkill : IntermitSkillBase, IDamageSkill
    {
        protected override bool CheckAllowSameGroupEffect(GameObject other)
        {
            return false;
        }
        protected override bool CheckAllowDiffGroupEffect(GameObject other)
        {
            if (
                other.CompareTag(GlobalTags.TAG_ENTITY) ||
                other.CompareTag(GlobalTags.TAG_INTACT_SKILL) ||
                other.CompareTag(GlobalTags.TAG_DESTABL_STRUCT)
                )
            {
                return true;
            }
            return false;
        }

        protected override void OnSameGroupEnter(GameObject other)
        {
            return;
        }
        protected override void OnDiffGroupEnter(GameObject other)
        {
            if (
                SkillBehaviour.DealDamage(other, this.GetDamage()) &&
                gameObject.TryGetComponent<LifecycleController>(out LifecycleController lifecycleControl)
                )
            {
                // 对于主动型生命周期，消耗1次行动次数
                lifecycleControl.TryAction();
            }
        }

        public abstract Damage GetDamage();

    }

    // 2) 间歇性对敌方伤害+对我方治疗技能
    // 示例：心海E技能水母，芭芭拉E技能水环
    public abstract class IntermitDamageHealSkill : IntermitSkillBase, IDamageSkill, IHealSkill
    {
        protected override bool CheckAllowSameGroupEffect(GameObject other)
        {
            if (
                other.CompareTag(GlobalTags.TAG_ENTITY) ||
                other.CompareTag(GlobalTags.TAG_INTACT_SKILL)
                )
            {
                return true;
            }
            return false;
        }
        protected override bool CheckAllowDiffGroupEffect(GameObject other)
        {
            if (
                other.CompareTag(GlobalTags.TAG_ENTITY) ||
                other.CompareTag(GlobalTags.TAG_INTACT_SKILL) ||
                other.CompareTag(GlobalTags.TAG_DESTABL_STRUCT)
                )
            {
                return true;
            }
            return false;
        }

        protected override void OnSameGroupEnter(GameObject other)
        {
            SkillBehaviour.DealHeal(other, this.GetHeal());
        }
        protected override void OnDiffGroupEnter(GameObject other)
        {
            if (
                SkillBehaviour.DealDamage(other, this.GetDamage()) &&
                gameObject.TryGetComponent<LifecycleController>(out LifecycleController lifecycleControl)
                )
            {
                lifecycleControl.TryAction();
            }
        }

        public abstract Damage GetDamage();
        public abstract Heal GetHeal();

    }

}





