using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;
using MyProject.GameObjects.Tags;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 说明：
    // 1) 因为全局的对象间交互逻辑是一致的，所以单独设计一个代理类来实现 Skill 的行为逻辑
    // 2) 区分阵营的逻辑在外部实现，这里只考虑直接造成影响的逻辑
    public static class SkillBehaviour
    {
        // 1.造成伤害
        public static bool DealDamage(GameObject other, Damage damage)
        {
            // 1) 造成伤害：对实体 Entity
            if( other.CompareTag(GlobalTags.TAG_ENTITY) )
            {
                if (other.TryGetComponent<LifeEntityHealth>(out LifeEntityHealth entityLife))
                {
                    return entityLife.TakeDamage(damage);
                }
            }
            // 2) 造成伤害：对可交互技能 InteractiveSkill
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
            // 3) 造成伤害：对可破坏设施 DestroyableStructure
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
        // 2.造成治疗
        public static bool DealHeal(GameObject other, Heal heal)
        {
            // 1) 造成治疗：对实体 Entity
            if (other.CompareTag(GlobalTags.TAG_ENTITY))
            {
                if (other.TryGetComponent<LifeSmipleHealth>(out LifeSmipleHealth entityLife) && entityLife.allowHeal)
                {
                    return entityLife.TakeHeal(heal);
                }
            }
            // 2) 造成治疗：对可交互技能 InteractiveSkill
            else if (other.CompareTag(GlobalTags.TAG_INTACT_SKILL))
            {
                if (other.TryGetComponent<LifeSmipleHealth>(out LifeSmipleHealth skillLife) && skillLife.allowHeal)
                {
                    return skillLife.TakeHeal(heal);
                }
            }
            return false;
        }

        // 3.施加 Buff
        public static bool AttachBuff(GameObject other, BuffInfo buffInfo)
        {
            if( other.CompareTag(GlobalTags.TAG_ENTITY) )
            {
                // TODO：实现逻辑

                return true;
            }
            return false;
        }
        // 4.移除 Buff
        public static bool RemoveBuff(GameObject other, BuffInfo buffInfo)
        {
            if (other.CompareTag(GlobalTags.TAG_ENTITY))
            {
                // TODO：实现逻辑

                return true;
            }
            return false;
        }

    }

}


