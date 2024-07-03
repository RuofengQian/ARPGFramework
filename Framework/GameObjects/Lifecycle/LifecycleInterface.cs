using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Lifecycle
{
    public interface IHealthLifecycle
    {
        bool allowHeal { get; }

        bool TakeDamage(Damage damage);
        bool TakeHeal(Heal heal);

    }

}

