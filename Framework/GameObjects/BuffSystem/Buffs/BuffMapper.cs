using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Buff
{
    // Buff����
    public enum BuffDesc
    {
        // ˲������
        InstantHeal,
        // �ָ�
        Recover,
        // ˲���˺�
        InstantDamage,
        // ��Ѫ
        Bleed,
    }

    // Buff��Ϣ
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



