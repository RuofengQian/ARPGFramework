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
        // 

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
        // ��� Buff
        public void AddBuff(BuffInfo buffInfo)
        {
            BuffBase buff = BuffCreator.GetBuffByInfo(buffInfo);

            // 
            buff.OnEffectEnable();

            if (buffInfo.triggerMode == EventTriggerMode.Intermit)
            {
                // TODO��ʹ������ļ�ʱ������Э��
                //StartCoroutine();
            }
        }
        public void RemoveBuff(BuffInfo buffInfo)
        {
            if( buffInfo.triggerMode == EventTriggerMode.Edge )
            {
                
            }
            
        }
        public void RemoveAllBuffs()
        {
            foreach (BuffBase buff in buffList.Values)
            {
                // TODO
            }
        }
        #endregion
       

    }

}




