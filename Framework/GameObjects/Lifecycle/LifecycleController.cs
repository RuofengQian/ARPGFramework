using MyFramework.Manager.GameTick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.Lifecycle
{
    // 生命周期类型
    public enum LifecycleType
    {
        // 实体生命
        EntityHealth = 0,
        // 简单生命
        SimpleHealth = 1,
        // 受击次数
        DamagedTimes = 2,

        // 攻击次数
        AttackedTimes = 3,
        
        // 倒计时
        CountDown = 4,

    }

    // 生命周期组别：决定管理方式
    public enum LifecycleGroup
    {
        // 基于被动伤害的
        Passive,
        // 基于主动行动的
        Active,
        // 基于时间的
        TimeBased,
    }
    
    // 生命周期控制器（外观类）
    public class LifecycleController : MonoBehaviour
    {
        // 被动式生命周期
        #region Lifecycle.Passive
        // 基于伤害的生命周期
        private IDamageBasedLifecycle _damageBasedLifecycle;
        public IDamageBasedLifecycle damageLife { get => _damageBasedLifecycle; private set => _damageBasedLifecycle = value; }

        // 伤害生命有效性
        public bool damageLifeValid { get => (damageLife != null); }
        public bool damageLifeInvalid { get => (damageLife == null); }


        public bool TryTakeDamage(Damage damage)
        {
            return damageLifeValid && damageLife.allowDamage && damageLife.TakeDamage(damage);
        }
        public bool TryTakeHeal(Heal heal)
        {
            return damageLifeValid && damageLife.allowHeal && damageLife.TakeHeal(heal);
        }
        #endregion

        // 主动型生命周期
        #region Lifecycle.Active
        // 基于行动的生命周期
        private IActionBasedLifecycle _actionBasedLifecycle;
        public IActionBasedLifecycle actionLife { get => _actionBasedLifecycle; private set => _actionBasedLifecycle = value; }

        // 行动生命有效性
        public bool actionLifeValid { get => (actionLife != null); }
        public bool actionLifeInvalid { get => (actionLife == null); }


        public bool TryAction()
        {
            return actionLifeValid && actionLife.CostActionTime(1);
        }
        #endregion

        // 计时型生命周期
        #region Lifecycle.Time
        // 基于时间的生命周期
        private ITimeBasedLifecycle _timeBasedLifecycle;
        public ITimeBasedLifecycle timeLife { get => _timeBasedLifecycle; private set => _timeBasedLifecycle = value; }

        // 时间生命有效性
        public bool timeLifeValid { get => (timeLife != null); }
        public bool timeLifeInvalid { get => (timeLife == null); }
        #endregion


        #region AddComponent
        // 生命周期组别映射
        private static readonly Dictionary<LifecycleType, LifecycleGroup> groupMap = new()
        {
            { LifecycleType.EntityHealth, LifecycleGroup.Passive },
            { LifecycleType.SimpleHealth, LifecycleGroup.Passive },
            { LifecycleType.DamagedTimes, LifecycleGroup.Passive },

            { LifecycleType.AttackedTimes, LifecycleGroup.Active },

            { LifecycleType.CountDown, LifecycleGroup.TimeBased },
        };
        // 创建组件方法映射
        private static readonly Dictionary<LifecycleType, UnityAction<GameObject, object>> actionMap = new()
        {
            // TODO：绑定各类 Lifecycle 的创建初始化方法
        };


        public static void AttachLifecycleComponent<T>(GameObject attachGameObject, LifecycleType type, T data)
        {
            LifecycleController lifeController = null;
            if(!attachGameObject.TryGetComponent<LifecycleController>(out lifeController))
            {
                lifeController = attachGameObject.AddComponent<LifecycleController>();
            }

            if( !groupMap.ContainsKey(type) )
            {
                Debug.LogError($"[Error] LifecycleController 中不存在 Key: {type} 相应的 Value: Group，请添加该键值对");
                return;
            }
            else if( groupMap[type] == LifecycleGroup.Passive && lifeController.damageLifeValid )
            {
                Debug.LogError($"[Error] 添加 {type} 失败： damageLife已存在");
                return;
            }
            else if( groupMap[type] == LifecycleGroup.Active && lifeController.actionLifeValid )
            {
                Debug.LogError($"[Error] 添加 {type} 失败：actionLife已存在");
                return;
            }
            else if( groupMap[type] == LifecycleGroup.TimeBased && lifeController.timeLifeValid )
            {
                Debug.LogError($"[Error] 添加 {type} 失败：timeLife已存在");
                return;
            }

            // 执行创建方法
            actionMap[type].Invoke(attachGameObject, data);
        }
        #endregion

    }

}



