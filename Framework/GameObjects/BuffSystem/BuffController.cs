using MyFramework.GameObjects.Attribute;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Buff
{
    public class BuffController : MonoBehaviour
    {
        #region Attribute
        // ʵ������
        private EntityAttribute entity;

        // Buff �б�ÿ��Buffֻ����һ��
        private Dictionary<BuffType, BuffBase> buffList = new();
        #endregion

        #region Init
        private void Start()
        {
            if (!gameObject.TryGetComponent<EntityAttribute>(out entity))
            {
                Debug.LogError("[Error] û���� Entity �������ҵ��������������Buff������");
                Destroy(this);
            }
        }
        #endregion


        #region Action
        public void AddBuff(BuffInfo buffInfo)
        {

        }
        public void RemoveBuff(BuffInfo buffInfo)
        {

        }
        public void RemoveAllBuffs()
        {
            foreach (BuffBase buff in buffList.Values)
            {
                buff.OnDisable();
            }
        }
        #endregion






        private TBuff CreateBuff<TBuff>(string buffDesc)
            where TBuff : BuffBase
        {
            // TODO���������� Buff ��ʵ������Ϊ�丳��۲����
            return null;
        }

        #region Buff.Temporary
        // ��� Buff����ʱ�ԣ�����ʱ��������Ч��
        public void AddBuff(string buffDesc, float lastTime)
        {
            // ��ȡ Buff
            TemporaryBuffBase buff = CreateBuff<TemporaryBuffBase>(buffDesc);
            if (buff == null)
            {
                Debug.LogError($"[Error] Buff.{buffDesc} ����ʧ�ܣ����ܲ���һ����ʱЧ�����򲻴��ڸ����Ƶ�Buff");
                return;
            }
            // ���� Buff ����
            buff.leftTime = lastTime;
            // ʩ�� Buff Ч��
            StartCoroutine(ApplyTemporaryBuff(buff));
        }
        private IEnumerator ApplyTemporaryBuff(TemporaryBuffBase buff)
        {
            // ���� Buff Ч��
            buff.EnableEffect();

            // ����ʱ��Ԥ���ӿڣ����ⲿ UI ��ȡʣ��ʱ����Ϣ����ʾ
            while (buff.leftTime > 0)
            {
                yield return new WaitForSeconds(1f);
                --buff.leftTime;
            }

            // ���� Buff Ч��������ʱ����
            buff.DisableEffect();
            yield break;
        }
        #endregion

    }

}




