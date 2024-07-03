using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.FSM
{
    // 可中断状态
    public interface IInterruptableState
    {
        void OnInterrupt();
    }

    // 持续型状态
    // 1) 帧更新
    public interface IFrameUpdateState
    {
        void OnFrameUpdate();
    }
    // 2) 物理更新
    public interface IPhysicsUpdateState
    {
        void OnPhysicsUpdate();
    }
}






