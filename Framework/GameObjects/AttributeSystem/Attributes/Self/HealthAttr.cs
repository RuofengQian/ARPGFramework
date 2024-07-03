using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


namespace MyFramework.GameObjects.Attribute
{
    public sealed class Health : AttributeBase, INumericAttr
    {
        #region Property
        // ԭʼ����ֵ
        private float _rawValue;
        public float rawValue { get => _rawValue; private set => _rawValue = value; }

        // ��������ֵ
        private float _extraValue;
        public float extraValue { get => _extraValue; private set => _extraValue = value; }

        // ��������ֵ�ٷֱ�
        private float _extraPer;
        public float extraPer { get => _extraPer; private set => _extraPer = value; }

        // ������ֵ�ٷֱȣ����㣩
        public float per { get => (1f + extraPer); }

        // ��ǰ����ֵ
        private float _currValue;
        public float currValue { get => _currValue; private set => _currValue = value; }

        // ������ֵ
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

        // ��Ч
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

            // ����ʼ����ֵ����
            currValue = maxValue;
        }
        #endregion


        #region Action
        public void CostCurrValue(float val)
        {
            // �۳�����ֵ
            currValue -= val;
        }
        public void CostCurrValueByPer(float per)
        {
            // ���ٷֱȿ۳�����ֵ
            currValue -= currValue * per;
        }
        public void AddCurrValue(float val)
        {
            // �ָ�����ֵ
            currValue += val;
            if (currValue > maxValue)
            {
                currValue = maxValue;
            }
        }
        public void AddCurrValueByPer(float per)
        {
            // ���ٷֱȻָ�����ֵ
            currValue += currValue * per;
            if (currValue > maxValue)
            {
                currValue = maxValue;
            }
        }
        #endregion

    }

}



