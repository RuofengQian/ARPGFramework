using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;
using MyProject.GameObjects.Tags;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // ˵����
    // 1) ��Ϊȫ�ֵĶ���佻���߼���һ�µģ����Ե������һ����������ʵ�� Skill ����Ϊ�߼�
    //    ��ˣ�����ͨ���޸ĸ�ģ��ʵ�־������Ϸ�߼�
    // 2) ������Ӫ���߼����ⲿʵ�֣�����ֻ����ֱ�����Ӱ����߼���
    //    ��Ȼ��ֱ�����Ӱ�죬��ôֱ�Ӱѿ������Ӱ���ģ���ù����Ϳ����ˣ���ͬʱҲȷ��������߼���ͨ����

    public static class SkillBehaviour
    {
        // 1.����˺�
        public static bool DealDamage(GameObject other, Damage damage)
        {
            if( other.TryGetComponent<LifecycleController>(out LifecycleController lifecycleControl) )
            {
                return (
                    lifecycleControl.damageLifeValid && 
                    lifecycleControl.damageLife.allowDamage && 
                    lifecycleControl.damageLife.TakeDamage( damage )
                    );
            }
            return false;
        }
        // 2.�������
        public static bool DealHeal(GameObject other, Heal heal)
        {
            if (other.TryGetComponent<LifecycleController>(out LifecycleController lifecycleControl))
            {
                return (
                    lifecycleControl.damageLifeValid &&
                    lifecycleControl.damageLife.allowHeal &&
                    lifecycleControl.damageLife.TakeHeal(heal)
                    );
            }
            return false;
        }

        // 3.ʩ�� Buff
        public static bool AttachBuff(GameObject other, BuffInfo buffInfo)
        {
            if( other.TryGetComponent<BuffController>(out BuffController buffControl) )
            {
                // TODO��ʵ���߼�

                return true;
            }
            return false;
        }
        // 4.�Ƴ� Buff
        public static bool RemoveBuff(GameObject other, BuffInfo buffInfo)
        {
            if (other.TryGetComponent<BuffController>(out BuffController buffControl))
            {
                // TODO��ʵ���߼�

                return true;
            }
            return false;
        }

    }

}


