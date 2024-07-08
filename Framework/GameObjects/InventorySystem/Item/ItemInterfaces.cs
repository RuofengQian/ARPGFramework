

namespace MyFramework.GameObjects.Backpack.Item
{
    // 物品接口定义
    // 规则：关注共同的行为逻辑，不必专注于具体的物品种类；物品种类用专门的数据字段来定义
    // （接口-数据字段 选择方法）


    // 1.装备系统
    // 可装备物品：服饰、饰品
    public interface IEqupiableItem
    {
        // 装备
        void OnEquip();
        // 卸下
        void OnUnEquip();

    }

    // 2.武器系统（手持物品、技能生成器）
    public interface IHandheldItem
    {
        // 手持逻辑
        void OnHandle();
        // 收起逻辑
        void OnPutBack();

    }

    // 3.合成系统
    // 可合成物品
    public interface ICraftableItem
    {
        // 合成逻辑
        void OnCraft();

    }

    // 4.可放置物品
    // 可放置物品
    public interface IPlaceableItem
    {
        // 放置逻辑
        void OnPlace();

    }

}


