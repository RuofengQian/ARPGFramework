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
            // TODO：创建具体 Buff 类实例，并为其赋予观察对象
            return null;
        }

        #region Buff.Temporary
        // 添加 Buff（临时性）：临时增益或减益效果
        public void AddBuff(string buffDesc, float lastTime)
        {
            // 获取 Buff
            TemporaryBuffBase buff = CreateBuff<TemporaryBuffBase>(buffDesc);
            if (buff == null)
            {
                Debug.LogError($"[Error] Buff.{buffDesc} 创建失败：可能不是一个临时效果，或不存在该名称的Buff");
                return;
            }
            // 设置 Buff 属性
            buff.leftTime = lastTime;
            // 施用 Buff 效果
            StartCoroutine(ApplyTemporaryBuff(buff));
        }
        private IEnumerator ApplyTemporaryBuff(TemporaryBuffBase buff)
        {
            // 启用 Buff 效果
            buff.EnableEffect();

            // 倒计时：预留接口，供外部 UI 获取剩余时间信息并显示
            while (buff.leftTime > 0)
            {
                yield return new WaitForSeconds(1f);
                --buff.leftTime;
            }

            // 禁用 Buff 效果：倒计时结束
            buff.DisableEffect();
            yield break;
        }
        #endregion

    }

}




