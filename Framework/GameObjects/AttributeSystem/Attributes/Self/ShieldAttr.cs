using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.Attribute
{
    public sealed class Shield : AttributeBase, ISingleAttr
    {
        #region
        // 护盾值
        private float _value;
        public float value { get => _value; private set => _value = value; }

        // 有效
        public bool valid { get => (value > 0f); }


        public void AddValue(float val)
        {
            value += val;
        }
        public void RemoveValue(float val)
        {
            value -= val;
            value = (value < 0f) ? 0f : value; // 避免小于0
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

            // 1) 护盾有剩余
            if (value > 0f)
            {
                damageVal = 0f;
            }
            // 2) 护盾破碎
            else
            {
                damageVal = Mathf.Abs(value); // 扣除护盾后伤害值
                value = 0f; // 护盾值消耗完毕
            }

            return damageVal;
        }
        #endregion

    }

}




