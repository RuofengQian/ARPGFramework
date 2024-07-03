

namespace MyFramework.GameObjects.Attribute
{
    public sealed class Defence : AttributeBase, INumericAttr
    {
        #region Property
        // ���÷�ʽ��������ֵ/�ٷֱȼ���
        private ValueEffectMode method;

        // ԭʼ������
        private float _rawValue;

        // ���ӷ�����
        private float _extraValue;
        // ���ӷ������ٷֱ�
        private float _extraPer;

        public float rawValue { get => _rawValue; private set => _rawValue = value; }

        public float extraValue { get => _extraValue; private set => _extraValue = value; }
        public float extraPer { get => _extraPer; private set => _extraPer = value; }

        // �ܷ������ٷֱȣ����㣩
        public float per { get => (1f + extraPer); }
        // �ܷ����������㣩
        public float value { get => (rawValue + extraValue) * per; }

        
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
        public Defence(ValueEffectMode method, float rawValue = 0f, float extraValue = 0f, float extraPer = 0f)
        {
            this.method = method;

            this.rawValue = rawValue;
            this.extraValue = extraValue;
            this.extraPer = extraPer;
        }
        #endregion


        #region Caculate
        public float GetDamageValueAfterDefenceDec(float oriDamage, float leastDamage = 1f)
        {
            // ע�⣺������� leastDamage ��Ϊ��ֵ�����ܵ������˻�Ѫ��
            float afterDamage;

            // �������ٷֱȼ���˥�����˺�
            if (method == ValueEffectMode.Percentage)
            {
                afterDamage = oriDamage * ((100f - value) * 0.01f);
            }
            // ����������ֵ����˥�����˺�
            else
            {
                afterDamage = oriDamage - value;
            }

            return (afterDamage < leastDamage) ? leastDamage : afterDamage; // ������� leastValue ���˺�
        }
        #endregion

    }

}


