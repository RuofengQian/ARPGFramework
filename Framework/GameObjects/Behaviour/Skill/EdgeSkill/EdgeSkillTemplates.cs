using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;
using MyProject.GameObjects.Tags;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 1) 范围添加永久性Buff技能
    // 示例：班尼特Q技能————进入对友方施加+攻击力Buff，退出对友方移除该Buff
    public abstract class EdgeBuffSkill : EdgeSkillBase, IBuffSkill
    {
        protected override bool CheckAllowSameGroupEffect(GameObject other)
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
        protected override bool CheckAllowDiffGroupEffect(GameObject other)
        {
            return false;
        }

        protected override void OnSameGroupEnter(GameObject other)
        {
            SkillBehaviour.AttachBuff(other, this.GetBuff());
        }
        protected override void OnSameGroupExit(GameObject other)
        {
            SkillBehaviour.RemoveBuff(other, this.GetBuff());
        }
        protected override void OnDiffGroupEnter(GameObject other)
        {
            return;
        }
        protected override void OnDiffGroupExit(GameObject other)
        {
            return;
        }

        public abstract BuffInfo GetBuff();

    }

    // 2) 特殊技能
    // 示例：夜兰E技能
    // 拆解：OnDiffGroupEnter————标记、将敌方单位存入独立集合，OnDiffGroupExit————对集合中的敌方依次造成伤害
    public abstract class EdgeDamageSkill : EdgeSkillBase, IDamageSkill
    {
        // 特殊技能没有固定的模板实现，需要全部重写
        public abstract Damage GetDamage();

    }

}



