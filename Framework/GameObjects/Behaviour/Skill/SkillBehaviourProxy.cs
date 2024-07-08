using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;
using MyProject.GameObjects.Tags;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 说明：
    // 1) 因为全局的对象间交互逻辑是一致的，所以单独设计一个代理类来实现 Skill 的行为逻辑
    //    因此，可以通过修改该模块实现具体的游戏逻辑
    // 2) 区分阵营的逻辑在外部实现，这里只考虑直接造成影响的逻辑；
    //    既然是直接造成影响，那么直接把可以造成影响的模块拿过来就可以了，这同时也确保了组合逻辑的通用性

    public static class SkillBehaviour
    {
        // 1.造成伤害
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
        // 2.造成治疗
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

        // 3.施加 Buff
        public static bool AttachBuff(GameObject other, BuffInfo buffInfo)
        {
            if( other.TryGetComponent<BuffController>(out BuffController buffControl) )
            {
                // TODO：实现逻辑

                return true;
            }
            return false;
        }
        // 4.移除 Buff
        public static bool RemoveBuff(GameObject other, BuffInfo buffInfo)
        {
            if (other.TryGetComponent<BuffController>(out BuffController buffControl))
            {
                // TODO：实现逻辑

                return true;
            }
            return false;
        }

    }

}


