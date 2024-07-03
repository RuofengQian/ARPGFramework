using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


namespace MyFramework.GameObjects.Attribute
{
    public sealed class Health : AttributeBase, INumericAttr
    {
        #region Property
        // 原始生命值
        private float _rawValue;
        public float rawValue { get => _rawValue; private set => _rawValue = value; }

        // 附加生命值
        private float _extraValue;
        public float extraValue { get => _extraValue; private set => _extraValue = value; }

        // 附加生命值百分比
        private float _extraPer;
        public float extraPer { get => _extraPer; private set => _extraPer = value; }

        // 总生命值百分比（计算）
        public float per { get => (1f + extraPer); }

        // 当前生命值
        private float _currValue;
        public float currValue { get => _currValue; private set => _currValue = value; }

        // 总生命值
        private float _maxValue;
        public float maxValue { get => _maxValue; private set => _maxValue = value; }
        private void CaculateMaxValue()
        {
            maxValue = (rawValue + extraValue) * per;
            if( currValue > maxValue )
            {
                currValue = maxValue;
            }
        }

        // 有效
        public bool valid { get => (currValue > 0f); }


        public void AddExtraValue(float val)
        {
            extraValue += val;
            CaculateMaxValue();
        }
        public void RemoveExtraValue(float val)
        {
            extraValue -= val;
            CaculateMaxValue();
        }

        public void AddExtraPer(float val)
        {
            extraPer += val;
            CaculateMaxValue();
        }
        public void RemoveExtraPer(float val)
        {
            extraPer -= val;
            CaculateMaxValue();
        }

        public void SetRawValue(float val)
        {
            rawValue = val;
            CaculateMaxValue();
        }

        public void Reset()
        {
            currValue = maxValue;
        }
        public void Reset(float extraValue, float extraPer)
        {
            this.extraValue = extraValue;
            this.extraPer = extraPer;
            CaculateMaxValue();
            currValue = maxValue;
        }
        public void Clear()
        {
            currValue = 0f;
        }
        #endregion

        #region Construct
        public Health(float rawValue, float extraValue = 0f, float extraPer = 0f)
        {
            this.rawValue = rawValue;
            this.extraValue = extraValue;
            this.extraPer = extraPer;

            // 将初始生命值置满
            currValue = maxValue;
        }
        #endregion


        #region Action
        public void CostCurrValue(float val)
        {
            // 扣除生命值
            currValue -= val;
        }
        public void CostCurrValueByPer(float per)
        {
            // 按百分比扣除生命值
            currValue -= currValue * per;
        }
        public void AddCurrValue(float val)
        {
            // 恢复生命值
            currValue += val;
            if (currValue > maxValue)
            {
                currValue = maxValue;
            }
        }
        public void AddCurrValueByPer(float per)
        {
            // 按百分比恢复生命值
            currValue += currValue * per;
            if (currValue > maxValue)
            {
                currValue = maxValue;
            }
        }
        #endregion

    }

}



