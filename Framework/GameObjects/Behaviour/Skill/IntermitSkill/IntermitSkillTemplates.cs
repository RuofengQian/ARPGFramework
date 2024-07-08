using MyFramework.GameObjects.Lifecycle;
using MyProject.GameObjects.Tags;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 1) ��Ъ�ԶԵз��˺�����
    // ʾ������ά�����ػ���ˮ������һ��ʱ��Եз�����˺�
    public abstract class IntermitDamageSkill : IntermitSkillBase, IDamageSkill
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
            if (
                SkillBehaviour.DealDamage(other, this.GetDamage()) &&
                gameObject.TryGetComponent<LifecycleController>(out LifecycleController lifecycleControl)
                )
            {
                // �����������������ڣ�����1���ж�����
                lifecycleControl.TryAction();
            }
        }

        public abstract Damage GetDamage();

    }

    // 2) ��Ъ�ԶԵз��˺�+���ҷ����Ƽ���
    // ʾ�����ĺ�E����ˮĸ���Ű���E����ˮ��
    public abstract class IntermitDamageHealSkill : IntermitSkillBase, IDamageSkill, IHealSkill
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
            SkillBehaviour.DealHeal(other, this.GetHeal());
        }
        protected override void OnDiffGroupEnter(GameObject other)
        {
            if (
                SkillBehaviour.DealDamage(other, this.GetDamage()) &&
                gameObject.TryGetComponent<LifecycleController>(out LifecycleController lifecycleControl)
                )
            {
                lifecycleControl.TryAction();
            }
        }

        public abstract Damage GetDamage();
        public abstract Heal GetHeal();

    }

}





