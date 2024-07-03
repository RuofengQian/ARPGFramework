using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.Attribute
{
    public sealed class Shield : AttributeBase, ISingleAttr
    {
        #region
        // ����ֵ
        private float _value;
        public float value { get => _value; private set => _value = value; }

        // ��Ч
        public bool valid { get => (value > 0f); }


        public void AddValue(float val)
        {
            value += val;
        }
        public void RemoveValue(float val)
        {
            value -= val;
            value = (value < 0f) ? 0f : value; // ����С��0
        }

        public void ClearValue()
        {
            value = 0f;
        }
        #endregion

        #region
        public Shield(float value = 0f)
        {
            this.value = value;
        }
        #endregion


        #region Caculate
        public float GetDamageValueAfterShieldResist(float damageVal)
        {
            value -= damageVal;

            // 1) ������ʣ��
            if (value > 0f)
            {
                damageVal = 0f;
            }
            // 2) ��������
            else
            {
                damageVal = Mathf.Abs(value); // �۳����ܺ��˺�ֵ
                value = 0f; // ����ֵ�������
            }

            return damageVal;
        }
        #endregion

    }

}




