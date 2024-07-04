using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;
using MyProject.GameObjects.Tags;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // ˵����
    // 1) ��Ϊȫ�ֵĶ���佻���߼���һ�µģ����Ե������һ����������ʵ�� Skill ����Ϊ�߼�
    // 2) ������Ӫ���߼����ⲿʵ�֣�����ֻ����ֱ�����Ӱ����߼�
    public static class SkillBehaviour
    {
        // 1.����˺�
        public static bool DealDamage(GameObject other, Damage damage)
        {
            // 1) ����˺�����ʵ�� Entity
            if( other.CompareTag(GlobalTags.TAG_ENTITY) )
            {
                if (other.TryGetComponent<LifeEntityHealth>(out LifeEntityHealth entityLife))
                {
                    return entityLife.TakeDamage(damage);
                }
            }
            // 2) ����˺����Կɽ������� InteractiveSkill
            else if( other.CompareTag(GlobalTags.TAG_INTACT_SKILL) )
            {
                if (other.TryGetComponent<LifeSmipleHealth>(out LifeSmipleHealth skillLifeHealth))
                {
                    return skillLifeHealth.TakeDamage(damage);
                }
                else if (other.TryGetComponent<LifeDamagedTime>(out LifeDamagedTime skillLifeDamaged))
                {
                    return skillLifeDamaged.TakeDamage(damage);
                }
            }
            // 3) ����˺����Կ��ƻ���ʩ DestroyableStructure
            else if( other.CompareTag(GlobalTags.TAG_DESTABL_STRUCT) )
            {
                if (other.TryGetComponent<LifeSmipleHealth>(out LifeSmipleHealth structLifeHealth))
                {
                    return structLifeHealth.TakeDamage(damage);
                }
                else if (other.TryGetComponent<LifeDamagedTime>(out LifeDamagedTime structLifeDamaged))
                {
                    return structLifeDamaged.TakeDamage(damage);
                }
            }
            return false;
        }
        // 2.�������
        public static bool DealHeal(GameObject other, Heal heal)
        {
            // 1) ������ƣ���ʵ�� Entity
            if (other.CompareTag(GlobalTags.TAG_ENTITY))
            {
                if (other.TryGetComponent<LifeSmipleHealth>(out LifeSmipleHealth entityLife) && entityLife.allowHeal)
                {
                    return entityLife.TakeHeal(heal);
                }
            }
            // 2) ������ƣ��Կɽ������� InteractiveSkill
            else if (other.CompareTag(GlobalTags.TAG_INTACT_SKILL))
            {
                if (other.TryGetComponent<LifeSmipleHealth>(out LifeSmipleHealth skillLife) && skillLife.allowHeal)
                {
                    return skillLife.TakeHeal(heal);
                }
            }
            return false;
        }

        // 3.ʩ�� Buff
        public static bool AttachBuff(GameObject other, BuffInfo buffInfo)
        {
            if( other.CompareTag(GlobalTags.TAG_ENTITY) )
            {
                // TODO��ʵ���߼�

                return true;
            }
            return false;
        }
        // 4.�Ƴ� Buff
        public static bool RemoveBuff(GameObject other, BuffInfo buffInfo)
        {
            if (other.CompareTag(GlobalTags.TAG_ENTITY))
            {
                // TODO��ʵ���߼�

                return true;
            }
            return false;
        }

    }

}


