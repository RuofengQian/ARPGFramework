

namespace MyFramework.GameObjects.Attribute
{
    // 1.数值型
    // 描述：通用数值拓展方案，{固定值, 可变值, 倍率}
    // 案例：生命值、攻击力、防御力等
    // 基础属性列表：原始值 + 附加值 + 附加百分比
    // 说明：为什么要分离设计？方便用一个字段（rawValue）直接设置值，而不移除其他项目
    public interface INumericAttr
    {
        float rawValue { get; }

        float extraValue { get; }
        float extraPer { get; }

        float per { get; }


        void AddExtraValue(float val);
        void RemoveExtraValue(float val);

        void AddExtraPer(float val);
        void RemoveExtraPer(float val);

        void SetRawValue(float val);

    }

    // 2.倍率型
    // 描述：拥有不同基础值，以倍率实现不同行为
    // 案例：攻击速度、移动速度
    // 基础属性列表：基础值 + 附加百分比
    public interface IMagnAttr
    {
        float basicValue { get; }

        float extraPer { get; }

        float per { get; }


        void AddExtraPer(float val);
        void RemoveExtraPer(float val);

        void SetBasicValueByMul(float mul);

    }

    // 3.概率型
    // 描述：概率触发，成功触发则输出乘以倍率的值
    // 案例：暴击、幸运值
    // 基础属性列表：概率 + 附加百分比
    public interface IProbAttr
    {
        float chance { get; }

        float basicValuePer { get; }
        float extraValuePer { get; }

        float valuePer { get; }

        // 是否有效
        bool valid { get; }


        void AddChance(float val);
        void RemoveChance(float val);

        void AddExtraPer(float val);
        void RemoveExtraPer(float val);

        void SetBasicPer(float val);

    }

    // 4.单一型
    // 描述：对一个属性的直接操作
    // 案例：护盾值、与游戏类型相关的独有属性（部署费用等）
    public interface ISingleAttr
    {
        float value { get; }

        bool valid { get; }


        void AddValue(float val);
        void RemoveValue(float val);
        void ClearValue();

    }

}


