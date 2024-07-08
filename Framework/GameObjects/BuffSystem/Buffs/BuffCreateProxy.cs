using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Buff
{
    // Buff类型
    public enum BuffDesc
    {
        // 1.Buff
        // 瞬间治疗
        InstantHeal = 000,
        // 恢复
        Recover = 001,
        // 迅捷
        Swiftness = 002,
        // 急迫：+攻击速度
        Urgent = 003,
        // 保护
        Protection = 004,
        // 强壮：+攻击力
        Strength = 005,
        // 生命提升
        HealthBoost = 006,
        // 韧性：减少受控程度
        Toughness = 007,

        // 2.DeBuff
        // 瞬间伤害
        InstantDamage = 100,
        // 流血
        Bleed = 101,
        // 缓慢
        Slowness = 102,
        // 疲劳：-攻击速度
        Fatigue = 103,
        // 破甲
        ArmorPiercing = 104,
        // 虚弱：-攻击力
        Weakness = 105,

        // 自定义Buff、Debuff：不分配值（待商榷）

    }

    public enum BuffType
    {
        // 临时的
        Temporary = 0,
        // 恒定的
        Constant = 1,
    }

    // Buff信息
    public struct BuffInfo
    {
        // 描述
        public BuffDesc desc;
        // 触发方式：
        // Rise：永久性Buff，施加时触发 OnEffectEnable
        // Edge：持续性Buff，施加时触发 OnEffectEnable，移除时触发 OnEffectDisable
        // Intermit：间歇性Buff，定时触发 OnEffectEnable
        public EventTriggerMode triggerMode;
        // 生命周期

        // 持续时间
        public float duration;
        // 其他关联数据：随Buff的具体实现而异
        public object data;

    }

    public static class BuffCreator
    {
        // 创建方法映射
        public static Dictionary<BuffDesc, Func<BuffBase>> createMethodMap = new()
        {
            // TODO：注册创建逻辑
        };
        
        // 根据 BuffInfo 创建一个 BuffBase 实例
        public static BuffBase GetBuffByInfo(BuffInfo buffInfo)
        {
            if( !createMethodMap.ContainsKey(buffInfo.desc) )
            {
                Debug.LogError($"[Error] 不存在名为 {buffInfo.desc} 的 Buff效果");
                return null;
                
            }
            return createMethodMap[buffInfo.desc].Invoke();
        }

    }

}



