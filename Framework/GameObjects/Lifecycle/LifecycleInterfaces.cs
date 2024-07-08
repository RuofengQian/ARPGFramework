

namespace MyFramework.GameObjects.Lifecycle
{
    // 生命周期按计数方式分为三组：受伤、行动次数、时间
    // 如果想要在同组别内实现组合逻辑，继承接口并在外观类中添加相应逻辑即可
    
    
    // 1) 以受伤计数的生命周期
    // 管理方法组：TakeDamage & TakeHeal
    public interface IDamageBasedLifecycle
    {
        // 允许伤害
        bool allowDamage { get; }
        // 允许治疗
        bool allowHeal { get; }

        // 受到伤害
        bool TakeDamage(Damage damage);
        // 受到治疗
        bool TakeHeal(Heal heal);

    }

    // 2) 以主动行动次数计数的生命周期
    // 管理方法组：
    public interface IActionBasedLifecycle
    {
        // 起始行动次数
        int initActionTimes { get; }
        // 剩余行动次数
        int leftActionTimes { get; }
        
        // 扣除行动次数
        bool CostActionTime(int costTimes);
        // 补充行动次数
        bool AddActionTime(int addTimes);

    }

    // 3) 以时间计数的生命周期
    public interface ITimeBasedLifecycle
    {
        // 起始时间
        float initTime { get; }
        // 剩余时间
        float leftTime { get; }
        
        // 扣除剩余时间
        bool CostLeftTime(float declineTime);
        // 延长剩余时间
        bool AddLeftTime(float extendTime);

    }

}

