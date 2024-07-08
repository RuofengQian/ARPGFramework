using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    public abstract class IntermitSkillBase : SkillBase
    {
        #region Action
        // 注意：避免在触发器边缘游走时反复触发 OnEnter
        new private void OnTriggerEnter(Collider otherCollider)
        {
            int colliderId = otherCollider.GetInstanceID();
            if (contactedColliderIdSet.Contains(colliderId))
            {
                return;
            }
            contactedColliderIdSet.Add(colliderId);

            // 定时触发接触效果
            contactCoMap[colliderId] = StartCoroutine(ContactCountDown(otherCollider.gameObject, colliderId));
        }
        private void OnTriggerExit(Collider otherCollider)
        {
            int colliderId = otherCollider.GetInstanceID();
            if (!contactedColliderIdSet.Contains(colliderId))
            {
                return;
            }
            // 停止接触效果
            StopCoroutine(contactCoMap[colliderId]);

            contactCoMap.Remove(colliderId);
            contactedColliderIdSet.Remove(colliderId);
        }
        new private void OnDisable()
        {
            // 清除碰撞箱接触记录
            contactedColliderIdSet.Clear();
            // 停止所有协程，清空协程列表
            foreach( Coroutine co in contactCoMap.Values )
            {
                StopCoroutine(co);
            }
            contactCoMap.Clear();
        }
        new private void OnDestroy()
        {
            // 清除碰撞箱接触记录
            contactedColliderIdSet.Clear();
            contactedColliderIdSet = null;
            // 停止所有协程，清空协程列表
            foreach (Coroutine co in contactCoMap.Values)
            {
                StopCoroutine(co);
            }
            contactCoMap.Clear();
            contactCoMap = null;
        }
        #endregion


        #region ContactIntv
        // 接触协程列表
        private Dictionary<int, Coroutine> contactCoMap = new(); 
        // 接触间隔
        private float contactIntv = 0.5f;

        public void SetContactIntv(float val = 0.5f)
        {
            if (val < 0.1f || val > 2.5f) // 限制上下不超过默认值5倍
            {
                return;
            }
            contactIntv = val;
        }

        // 接触倒计时
        private IEnumerator ContactCountDown(GameObject other, int instID)
        {
            while( true )
            {
                // 等待一段时间
                yield return new WaitForSeconds(contactIntv);
                // 触发接触效果
                OnEnterArea(other);
            }
        }
        #endregion

    }

}


