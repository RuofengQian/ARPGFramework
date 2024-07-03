

namespace MyFramework.GameObjects
{
    // 说明：
    // 游戏中的所有逻辑都可以被抽象为两类：数值变化和事件触发（E-C-S设计范式中的 组件Component/属性 和 系统System/事件）
    // 因此，这二者的若干种模式可以为全局所通用
    

    // 数值变化模式
    public enum ValueEffectMode
    {
        // 绝对值变化
        Absolute = 0,
        // 最终值/最大值百分比变化
        Percentage = 1,
        // 当前值百分比变化
        CurrValuePercentage = 2,

    }
    
    // 事件触发模式
    public enum EventTriggerMode
    {
        // 一次性触发：仅上升沿
        Once = 0,
        // 边沿触发：上升/下降沿
        Edge = 1,

        // 间歇性触发：持续时间间歇性
        Intermit = 2,

        // 帧更新触发：在 Update 中执行逻辑
        FrameUpdate = 3,
        // 物理更新触发：在 FixedUpdate 中执行逻辑
        PhysicUpdate = 4,

    }

}


