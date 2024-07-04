using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 1) ˲��Եз��˺�����
    // ʾ��������һ��ʽ��ͨ������E��Q����
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

    // 2) ˲��Եз��˺�+ʩ��DeBuff����
    // ʾ������Ѫ������ͨ����������˺�����ѪDeBuff
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

    // 3) ˲����ҷ����Ƽ���
    // ʾ������������Ļָ����κζ����Ե����ö����Ա���Ϊ���ܣ�
    public abstract class InstantHealSkill : InstantSkillBase, IHealSkill
    {
        protected override void OnSameGroupEnter(GameObject other)
        {
            SkillBehaviour.DealHeal(other, this.GetHeal());
        }

        public abstract Heal GetHeal();

    }

}

