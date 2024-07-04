using MyFramework.GameObjects.Lifecycle;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 1) ��Ъ�ԶԵз��˺�����
    // ʾ������ά�����ػ���ˮ������һ��ʱ��Եз�����˺�
    public abstract class IntermitDamageSkill : IntermitSkillBase, IDamageSkill
    {
        protected override void OnSameGroupEnter(GameObject other)
        {
            return;
        }
        protected override void OnDiffGroupEnter(GameObject other)
        {
            if (SkillBehaviour.DealDamage(other, this.GetDamage()))
            {
                // �˴��߼�����Ǩ�����������������
                if (gameObject.TryGetComponent<LifeAttackedTime>(out LifeAttackedTime lifeAttackTime))
                {
                    lifeAttackTime.CostAttackTimes();
                }
            }
        }

        public abstract Damage GetDamage();

    }

    // 2) ��Ъ�ԶԵз��˺�+���ҷ����Ƽ���
    // ʾ�����ĺ�E����ˮĸ���Ű���E����ˮ��
    public abstract class IntermitDamageHealSkill : IntermitSkillBase, IDamageSkill, IHealSkill
    {
        protected override void OnSameGroupEnter(GameObject other)
        {
            SkillBehaviour.DealHeal(other, this.GetHeal());
        }
        protected override void OnDiffGroupEnter(GameObject other)
        {
            if (SkillBehaviour.DealDamage(other, this.GetDamage()))
            {
                // �˴��߼�����Ǩ�����������������
                if (gameObject.TryGetComponent<LifeAttackedTime>(out LifeAttackedTime lifeAttackTime))
                {
                    lifeAttackTime.CostAttackTimes();
                }
            }
        }

        public abstract Damage GetDamage();
        public abstract Heal GetHeal();

    }

}





