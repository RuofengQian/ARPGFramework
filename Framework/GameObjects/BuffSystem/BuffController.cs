using MyFramework.GameObjects.Attribute;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Buff
{
    public class BuffController : MonoBehaviour
    {
        #region Attribute
        // 实体属性
        private EntityAttribute entity;

        // Buff 列表：每种Buff只能有一个
        private Dictionary<BuffType, BuffBase> buffList = new();
        // 

        #endregion

        #region Init
        private void Start()
        {
            if (!gameObject.TryGetComponent<EntityAttribute>(out entity))
            {
                Debug.LogError("[Error] 没有在 Entity 对象上找到属性组件，销毁Buff控制器");
                Destroy(this);
            }
        }
        #endregion


        #region Action
        // 添加 Buff
        public void AddBuff(BuffInfo buffInfo)
        {
            BuffBase buff = BuffCreator.GetBuffByInfo(buffInfo);

            // 
            buff.OnEffectEnable();

            if (buffInfo.triggerMode == EventTriggerMode.Intermit)
            {
                // TODO：使用自身的计时器开启协程
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




