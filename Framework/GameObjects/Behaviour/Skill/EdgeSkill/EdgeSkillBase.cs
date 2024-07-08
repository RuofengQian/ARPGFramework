using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    public abstract class EdgeSkillBase : SkillBase
    {
        #region Action
        new private void OnTriggerEnter(Collider otherCollider)
        {
            int colliderId = otherCollider.GetInstanceID();
            if (contactedColliderIdSet.Contains(colliderId))
            {
                return;
            }
            contactedColliderIdSet.Add(colliderId);
            contactedGameObjectSet.Add(otherCollider.gameObject); // 加入接触对象集合

            OnEnterArea(otherCollider.gameObject);
        }
        private void OnTriggerExit(Collider otherCollider)
        {
            if( !contactedColliderIdSet.Contains(otherCollider.GetInstanceID()) )
            {
                return;
            }
            OnExitArea(otherCollider.gameObject);

            contactedColliderIdSet.Remove(otherCollider.GetInstanceID());
            contactedGameObjectSet.Remove(otherCollider.gameObject); // 移除接触对象集合
        }

        new private void OnDisable()
        {
            foreach ( GameObject other in contactedGameObjectSet )
            {
                OnExitArea(other);
            }
            contactedGameObjectSet.Clear();
            contactedColliderIdSet.Clear();
        }
        new private void OnDestroy()
        {
            foreach (GameObject other in contactedGameObjectSet)
            {
                OnExitArea(other);
            }
            contactedGameObjectSet.Clear();
            contactedColliderIdSet.Clear();
            contactedColliderIdSet = null;
        }

        // 退出区域
        protected virtual void OnExitArea(GameObject other)
        {
            // 在不同阵营
            if (InDiffGroup(other))
            {
                OnDiffGroupExit(other);
            }
            // 在相同阵营
            else if (InSameGroup(other))
            {
                OnSameGroupExit(other);
            }
        }
        #endregion


        #region Event
        // 正在接触的对象集合
        protected HashSet<GameObject> contactedGameObjectSet = new();

        protected virtual void OnDiffGroupExit(GameObject other) { }
        protected virtual void OnSameGroupExit(GameObject other) { }
        #endregion

    }

}


