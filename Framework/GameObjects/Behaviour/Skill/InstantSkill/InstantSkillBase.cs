using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    public abstract class InstantSkillBase : SkillBase
    {
        #region Action
        new private void OnTriggerEnter(Collider otherCollider)
        {
            if (contactedColliderIdSet.Contains(otherCollider.GetInstanceID()))
            {
                return;
            }
            contactedColliderIdSet.Add(otherCollider.GetInstanceID());

            OnEnterArea(otherCollider.gameObject);

        }
        #endregion

    }

}


