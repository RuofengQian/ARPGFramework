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
            contactedGameObjectSet.Add(otherCollider.gameObject); // ����Ӵ����󼯺�

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
            contactedGameObjectSet.Remove(otherCollider.gameObject); // �Ƴ��Ӵ����󼯺�
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

        // �˳�����
        protected virtual void OnExitArea(GameObject other)
        {
            // �ڲ�ͬ��Ӫ
            if (InDiffGroup(other))
            {
                OnDiffGroupExit(other);
            }
            // ����ͬ��Ӫ
            else if (InSameGroup(other))
            {
                OnSameGroupExit(other);
            }
        }
        #endregion


        #region Event
        // ���ڽӴ��Ķ��󼯺�
        protected HashSet<GameObject> contactedGameObjectSet = new();

        protected virtual void OnDiffGroupExit(GameObject other) { }
        protected virtual void OnSameGroupExit(GameObject other) { }
        #endregion

    }

}


