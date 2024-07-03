using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Attribute
{
    /// <summary>
    /**
    // �˺�����
    public enum DamageType
    {
        // ��ʵ�˺�
        Real = 0,
        // �����˺�
        Physic = 1,
        // ħ���˺�
        Magic = 2,
    }

    // �˺��ṹ�壺��Ϣ����
    public struct Damage
    {
        public DamageType type;
        public EffectMethod method;
        public float value;
    }
    // ���ƽṹ��
    public struct Heal
    {
        public EffectMethod method;
        public float value;
    }
    **/
    /// </summary>
}

namespace MyFramework.GameObjects.Behaviour
{
    // 1) ���ܣ��Եз��˺�
    public abstract class DamageSkill : SkillBase, IDamageSkill
    {
        #region Override
        protected override void InteractDiffGroup(GameObject other)
        {
            if (SkillBehaviour.DealDamage(other, this.GetDamage()))
            {
                if (lifeAttackTime != null)
                {
                    lifeAttackTime.DoAttack();
                }
            }
        }
        protected override void InteractSameGroup(GameObject other)
        {
            return;
        }
        #endregion

        #region Action
        public abstract Damage GetDamage();
        #endregion

    }

    // 2) ���ܣ��Եз��˺�+���ҷ�����
    public abstract class DamageHealSkill : SkillBase, IDamageSkill, IHealSkill
    {
        #region Override
        protected override void InteractDiffGroup(GameObject other)
        {
            if (SkillBehaviour.DealDamage(other, this.GetDamage()))
            {
                if (lifeAttackTime != null)
                {
                    lifeAttackTime.DoAttack();
                }
            }
        }
        protected override void InteractSameGroup(GameObject other)
        {
            // ʩ������
            SkillBehaviour.DealHeal(other, this.GetHeal());
        }
        #endregion

        #region Action
        public abstract Damage GetDamage();
        public abstract Heal GetHeal();
        #endregion

    }

    // 3) ���ܣ��Եз��˺�+ʩ��DeBuff
    public abstract class DamageDeBuffSkill : SkillBase, IDamageSkill, IDeBuffSkill
    {
        #region Override
        protected override void InteractDiffGroup(GameObject other)
        {
            if (SkillBehaviour.DealDamage(other, this.GetDamage()))
            {
                if (lifeAttackTime != null)
                {
                    lifeAttackTime.DoAttack();
                }
            }
            SkillBehaviour.AttachDeBuff(other, this.GetBuff());
        }
        protected override void InteractSameGroup(GameObject other)
        {
            return;
        }
        #endregion

        #region Action
        public abstract Damage GetDamage();
        public abstract BuffInfo GetBuff();
        #endregion

    }

    // 4) ���ܣ����ҷ�ʩ��Buff
    public abstract class BuffSkill : SkillBase, IBuffSkill
    {
        #region Override
        protected override void InteractDiffGroup(GameObject other)
        {
            return;
        }
        protected override void InteractSameGroup(GameObject other)
        {
            SkillBehaviour.AttachBuff(other, this.GetBuff());
        }
        #endregion


        #region Action
        public abstract BuffInfo GetBuff();
        #endregion
    }

    // 5) ���ܣ�����ײ��Χ�����Buff
    // TODO��ʵ�������߼�
    public abstract class RangeBuffSkill : SkillBase
    {
        // ����
        private List<BuffController> entityBuffController = new();


        
        // ���̣�
        // OnTriggerEnter - if Ally - Add Persist Buff
        private void OnTriggerEnter(Collider other)
        {
            if( other )
            {

            }
        }
        // OnTriggerExit - is Ally - Remove Persist Buff
        private void OnTriggerExit(Collider other)
        {
            
        }

        public abstract BuffInfo AddBuff();
        public abstract void RemoveBuff();

    }

}


