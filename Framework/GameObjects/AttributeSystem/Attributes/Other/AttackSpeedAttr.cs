

namespace MyFramework.GameObjects.Attribute
{
    public sealed class AttackSpeed : AttributeBase, IMagnificAttr
    {
        #region Property
        // 基础攻击速度（Hz）
        private float _basicValue;

        // 额外攻击速度百分比
        private float _extraPer;
        // 额外攻击速度间隔（s）
        private float _extraIntv;

        public float basicValue { get => _basicValue; private set => throw new System.NotImplementedException(); }

        public float extraPer { get => _extraPer; private set => throw new System.NotImplementedException(); }
        public float extraIntv { get => _extraIntv; private set => _extraIntv = value; }

        // 攻击速度百分比
        public float per { get => (1f + extraPer); }

        // 攻击速度间隔
        private float _intv = 1f;

        public float intv { get => _intv; private set => _intv = value; }
        private void RefreshIntv()
        {
            intv = ((1 / basicValue) + extraIntv) / per;
        }


        public void AddExtraPer(float val)
        {
            extraPer += val;
            RefreshIntv();
        }
        public void RemoveExtraPer(float val)
        {
            extraPer -= val;
            RefreshIntv();
        }

        public void DecExtraIntv(float val) // 减小攻击间隔（Buff）
        {
            extraIntv -= val;
            RefreshIntv();
        }
        public void IncExtraIntv(float val) // 增大攻击间隔（Debuff）
        {
            extraIntv += val;
            RefreshIntv();
        }

        public void SetBasicValueByMul(float mul)
        {
            basicValue = AttributeDefaults.AttackSpeed.BASIC_ATTACK_SPEED * mul;
            RefreshIntv();
        }
        #endregion

        #region Construct
        public AttackSpeed(float mul = 1f, float extraPer = 0f, float extraIntv = 0f)
        {
            this.basicValue = AttributeDefaults.AttackSpeed.BASIC_ATTACK_SPEED * mul;

            this.extraPer = extraPer;
            this.extraIntv = extraIntv;
            RefreshIntv();
        }
        #endregion

    }

}



