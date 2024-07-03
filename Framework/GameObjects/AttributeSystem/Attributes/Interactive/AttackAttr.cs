using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Attribute
{
    public sealed class Attack : AttributeBase, INumericAttr
    {
        #region Property
        // 原始攻击力
        private float _rawValue;

        // 附加攻击力
        private float _extraValue;
        // 附加攻击力百分比
        private float _extraPer;

        public float rawValue { get => _rawValue; private set => _rawValue = value; }

        public float extraValue { get => _extraValue; private set => _extraValue = value; }
        public float extraPer { get => _extraPer; private set => _extraPer = value; }

        // 总攻击力百分比（计算）
        public float per { get => (1f + extraPer); }
        // 总攻击力（计算）
        public float value { get => (rawValue + extraValue) * per; }

        // 有效
        public bool valid { get => (value > 0f); }


        public void AddExtraValue(float val)
        {
            extraValue += val;
        }
        public void RemoveExtraValue(float val)
        {
            extraValue -= val;
        }
        public void ReplaceExtraValue(float oldVal, float newVal)
        {
            extraValue -= oldVal;
            extraValue += newVal;
        }

        public void AddExtraPer(float val)
        {
            extraPer += val;
        }
        public void RemoveExtraPer(float val)
        {
            extraPer -= val;
        }

        public void SetRawValue(float val)
        {
            rawValue = val;
        }
        #endregion

        #region Construct
        public Attack(float rawValue = 0f, float extraValue = 0f, float extraPer = 0f)
        {
            this.rawValue = rawValue;
            this.extraValue = extraValue;
            this.extraPer = extraPer;
        }
        #endregion

    }

}



