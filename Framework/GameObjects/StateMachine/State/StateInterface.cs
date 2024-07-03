using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.FSM
{
    // ���ж�״̬
    public interface IInterruptableState
    {
        void OnInterrupt();
    }

    // ������״̬
    // 1) ֡����
    public interface IFrameUpdateState
    {
        void OnFrameUpdate();
    }
    // 2) �������
    public interface IPhysicsUpdateState
    {
        void OnPhysicsUpdate();
    }
}






