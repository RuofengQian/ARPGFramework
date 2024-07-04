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

            // ��ʱˢ�½Ӵ�
            StartCoroutine(ContactCountDown(colliderId));
        }
        #endregion


        #region ContactIntv
        // �Ӵ����
        private float contactIntv = 0.5f;

        public void SetContactIntv(float val = 0.5f)
        {
            if (val < 0.1f || val > 2.5f) // �������²�����Ĭ��ֵ5��
            {
                return;
            }
            contactIntv = val;
        }

        // �Ӵ�����ʱ
        private IEnumerator ContactCountDown(int instID)
        {
            yield return gameTickManager.WaitForGameSeconds(contactIntv);
            contactedColliderIdSet.Remove(instID);
        }
        #endregion

    }

}


