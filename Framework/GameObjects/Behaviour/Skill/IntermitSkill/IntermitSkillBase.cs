using System.Collections;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    public abstract class IntermitSkillBase : SkillBase
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

            OnEnterArea(otherCollider.gameObject);

            // 定时刷新接触
            StartCoroutine(ContactCountDown(colliderId));
        }
        #endregion


        #region ContactIntv
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
        private IEnumerator ContactCountDown(int instID)
        {
            yield return gameTickManager.WaitForGameSeconds(contactIntv);
            contactedColliderIdSet.Remove(instID);
        }
        #endregion

    }

}


