using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Buff
{
    // Buff����
    public enum BuffDesc
    {
        // 1.Buff
        // ˲������
        InstantHeal = 000,
        // �ָ�
        Recover = 001,
        // Ѹ��
        Swiftness = 002,
        // ���ȣ�+�����ٶ�
        Urgent = 003,
        // ����
        Protection = 004,
        // ǿ׳��+������
        Strength = 005,
        // ��������
        HealthBoost = 006,
        // ���ԣ������ܿس̶�
        Toughness = 007,

        // 2.DeBuff
        // ˲���˺�
        InstantDamage = 100,
        // ��Ѫ
        Bleed = 101,
        // ����
        Slowness = 102,
        // ƣ�ͣ�-�����ٶ�
        Fatigue = 103,
        // �Ƽ�
        ArmorPiercing = 104,
        // ������-������
        Weakness = 105,

        // �Զ���Buff��Debuff��������ֵ������ȶ��

    }

    public enum BuffType
    {
        // ��ʱ��
        Temporary = 0,
        // �㶨��
        Constant = 1,
    }

    // Buff��Ϣ
    public struct BuffInfo
    {
        // ����
        public BuffDesc desc;
        // ������ʽ��
        // Rise��������Buff��ʩ��ʱ���� OnEffectEnable
        // Edge��������Buff��ʩ��ʱ���� OnEffectEnable���Ƴ�ʱ���� OnEffectDisable
        // Intermit����Ъ��Buff����ʱ���� OnEffectEnable
        public EventTriggerMode triggerMode;
        // ��������

        // ����ʱ��
        public float duration;
        // �����������ݣ���Buff�ľ���ʵ�ֶ���
        public object data;

    }

    public static class BuffCreator
    {
        // ��������ӳ��
        public static Dictionary<BuffDesc, Func<BuffBase>> createMethodMap = new()
        {
            // TODO��ע�ᴴ���߼�
        };
        
        // ���� BuffInfo ����һ�� BuffBase ʵ��
        public static BuffBase GetBuffByInfo(BuffInfo buffInfo)
        {
            if( !createMethodMap.ContainsKey(buffInfo.desc) )
            {
                Debug.LogError($"[Error] ��������Ϊ {buffInfo.desc} �� BuffЧ��");
                return null;
                
            }
            return createMethodMap[buffInfo.desc].Invoke();
        }

    }

}



