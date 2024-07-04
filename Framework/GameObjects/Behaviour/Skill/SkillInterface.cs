using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 1.所有与 Entity 属性相关的技能接口
    // 伤害技能
    public interface IDamageSkill
    {
        // 获取伤害值
        Damage GetDamage();

    }
    // 治疗技能
    public interface IHealSkill
    {
        // 获取治疗量
        Heal GetHeal();

    }

    // 2.所有与 Entity 属性无关的技能接口
    // 附加Buff技能：为对象施加 Buff，包括定时数值效果
    public interface IBuffSkill
    {
        BuffInfo GetBuff();

    }

    public interface IDeBuffSkill
    {
        BuffInfo GetDeBuff();

    }

}



