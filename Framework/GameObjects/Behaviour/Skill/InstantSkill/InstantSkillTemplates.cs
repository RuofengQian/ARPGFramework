using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;
using MyProject.GameObjects.Tags;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 1) ˲��Եз��˺�����
    // ʾ��������һ��ʽ��ͨ������E��Q����
    public abstract class InstantDamageSkill : InstantSkillBase, IDamageSkill
    {
        protected override bool CheckAllowSameGroupEffect(GameObject other)
        {
            return false;
        }
        protected override bool CheckAllowDiffGroupEffect(GameObject other)
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
        protected override bool CheckAllowSameGroupEffect(GameObject other)
        {
            return false;
        }
        protected override bool CheckAllowDiffGroupEffect(GameObject other)
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
        protected override bool CheckAllowSameGroupEffect(GameObject other)
        {
            if (
                other.CompareTag(GlobalTags.TAG_ENTITY) ||
                other.CompareTag(GlobalTags.TAG_INTACT_SKILL)
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
            SkillBehaviour.DealHeal(other, this.GetHeal());
        }

        public abstract Heal GetHeal();

    }

}

