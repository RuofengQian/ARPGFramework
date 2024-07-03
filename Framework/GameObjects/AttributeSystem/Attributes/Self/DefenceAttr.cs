

namespace MyFramework.GameObjects.Attribute
{
    public sealed class Defence : AttributeBase, INumericAttr
    {
        #region Property
        // 作用方式：按绝对值/百分比减伤
        private ValueEffectMode method;

        // 原始防御力
        private float _rawValue;

        // 附加防御力
        private float _extraValue;
        // 附加防御力百分比
        private float _extraPer;

        public float rawValue { get => _rawValue; private set => _rawValue = value; }

        public float extraValue { get => _extraValue; private set => _extraValue = value; }
        public float extraPer { get => _extraPer; private set => _extraPer = value; }

        // 总防御力百分比（计算）
        public float per { get => (1f + extraPer); }
        // 总防御力（计算）
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
            // 注意：请谨慎将 leastDamage 设为负值！可能导致受伤回血！
            float afterDamage;

            // 按防御百分比计算衰减后伤害
            if (method == ValueEffectMode.Percentage)
            {
                afterDamage = oriDamage * ((100f - value) * 0.01f);
            }
            // 按防御绝对值计算衰减后伤害
            else
            {
                afterDamage = oriDamage - value;
            }

            return (afterDamage < leastDamage) ? leastDamage : afterDamage; // 至少造成 leastValue 点伤害
        }
        #endregion

    }

}


