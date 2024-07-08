using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;
using MyProject.GameObjects.Tags;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 1) 瞬间对敌方伤害技能
    // 示例：任意一段式普通攻击、E、Q技能
    public abstract class InstantDamageSkill : InstantSkillBase, IDamageSkill
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
            SkillBehaviour.DealDamage(other, this.GetDamage());
        }

        public abstract Damage GetDamage();

    }

    // 2) 瞬间对敌方伤害+施加DeBuff技能
    // 示例：流血狗的普通攻击，造成伤害和流血DeBuff
    public abstract class InstantDamageDeBuffSkill : InstantSkillBase, IDamageSkill, IDeBuffSkill
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
            SkillBehaviour.AttachBuff(other, this.GetDeBuff());
        }
        protected override void OnDiffGroupEnter(GameObject other)
        {
            SkillBehaviour.DealDamage(other, this.GetDamage());
        }

        public abstract Damage GetDamage();
        public abstract BuffInfo GetDeBuff();

    }

    // 3) 瞬间对我方治疗技能
    // 示例：七天神像的恢复（任何对属性的作用都可以被视为技能）
    public abstract class InstantHealSkill : InstantSkillBase, IHealSkill
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
            return false;
        }

        protected override void OnSameGroupEnter(GameObject other)
        {
            SkillBehaviour.DealHeal(other, this.GetHeal());
        }

        public abstract Heal GetHeal();

    }

}

