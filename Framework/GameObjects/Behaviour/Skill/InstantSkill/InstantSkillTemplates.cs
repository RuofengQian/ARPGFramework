using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 1) 瞬间对敌方伤害技能
    // 示例：任意一段式普通攻击、E、Q技能
    public abstract class InstantDamageSkill : InstantSkillBase, IDamageSkill
    {
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
        protected override void OnSameGroupEnter(GameObject other)
        {
            SkillBehaviour.DealHeal(other, this.GetHeal());
        }

        public abstract Heal GetHeal();

    }

}

