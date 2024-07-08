using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    public abstract class IntermitSkillBase : SkillBase
    {
        #region Action
        // ע�⣺�����ڴ�������Ե����ʱ�������� OnEnter
        new private void OnTriggerEnter(Collider otherCollider)
        {
            int colliderId = otherCollider.GetInstanceID();
            if (contactedColliderIdSet.Contains(colliderId))
            {
                return;
            }
            contactedColliderIdSet.Add(colliderId);

            // ��ʱ�����Ӵ�Ч��
            contactCoMap[colliderId] = StartCoroutine(ContactCountDown(otherCollider.gameObject, colliderId));
        }
        private void OnTriggerExit(Collider otherCollider)
        {
            int colliderId = otherCollider.GetInstanceID();
            if (!contactedColliderIdSet.Contains(colliderId))
            {
                return;
            }
            // ֹͣ�Ӵ�Ч��
            StopCoroutine(contactCoMap[colliderId]);

            contactCoMap.Remove(colliderId);
            contactedColliderIdSet.Remove(colliderId);
        }
        new private void OnDisable()
        {
            // �����ײ��Ӵ���¼
            contactedColliderIdSet.Clear();
            // ֹͣ����Э�̣����Э���б�
            foreach( Coroutine co in contactCoMap.Values )
            {
                StopCoroutine(co);
            }
            contactCoMap.Clear();
        }
        new private void OnDestroy()
        {
            // �����ײ��Ӵ���¼
            contactedColliderIdSet.Clear();
            contactedColliderIdSet = null;
            // ֹͣ����Э�̣����Э���б�
            foreach (Coroutine co in contactCoMap.Values)
            {
                StopCoroutine(co);
            }
            contactCoMap.Clear();
            contactCoMap = null;
        }
        #endregion


        #region ContactIntv
        // �Ӵ�Э���б�
        private Dictionary<int, Coroutine> contactCoMap = new(); 
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
        private IEnumerator ContactCountDown(GameObject other, int instID)
        {
            while( true )
            {
                // �ȴ�һ��ʱ��
                yield return new WaitForSeconds(contactIntv);
                // �����Ӵ�Ч��
                OnEnterArea(other);
            }
        }
        #endregion

    }

}


