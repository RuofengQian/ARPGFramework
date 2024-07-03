using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Buff
{
    // Buff类型
    public enum BuffDesc
    {
        // 瞬间治疗
        InstantHeal,
        // 恢复
        Recover,
        // 瞬间伤害
        InstantDamage,
        // 流血
        Bleed,
    }

    // Buff信息
    public struct BuffInfo
    {
        public BuffDesc desc;
        public int level;
        public float duration;
    }

    public static class BuffMapper
    {
        public static BuffBase GetBuffByInfo(BuffInfo buffInfo)
        {
            switch()
        }

    }

}



