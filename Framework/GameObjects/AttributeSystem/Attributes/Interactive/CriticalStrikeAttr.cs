using UnityEngine;


namespace MyFramework.GameObjects.Attribute
{
    public sealed class CriticalStrike : AttributeBase, IProbAttr
    {
        #region Property
        // 暴击率
        private float _chance;

        // 基础暴击伤害百分比
        private float _basicPer;
        // 附加暴击伤害百分比
        private float _extraPer;

        public float chance { get => _chance; private set => _chance = value; }

        public float basicValuePer { get => _basicPer; private set => _basicPer = value; }
        public float extraValuePer { get => _extraPer; private set => _extraPer = value; }

        // 概率
        public float valuePer { get => (1f + extraValuePer); }

        // 有效
        public bool valid { get => (_chance > 0f); }


        public void AddChance(float val)
        {
            chance += val;
        }
        public void RemoveChance(float val)
        {
            chance -= val;
        }

        public void AddExtraPer(float val)
        {
            extraValuePer += val;
        }
        public void RemoveExtraPer(float val)
        {
            extraValuePer -= val;
        }

        public void SetBasicPer(float val)
        {
            basicValuePer = val;
        }
        #endregion

        #region Construct
        public CriticalStrike(float chance = 0f, float extraPer = 0f)
        {
            this.chance = chance;
            this.extraValuePer = extraPer;
        }
        #endregion


        #region Caculate
        // 未暴击次数
        private int nonCritHits = 0;
        // 实际暴击率（整数制）
        private float smoothChance
        {
            get => (chance + GetSmoothExtraChance());
        }
        // 平滑额外暴击率计算方法
        private float GetSmoothExtraChance()
        {
            // TODO：实现暴击率平滑算法
            return (nonCritHits * 0.01f);
        }

        // 检查是否暴击
        private bool CheckSmoothCritStrike()
        {
            int randomNum = Random.Range(0, 99);

            if (smoothChance * 100 > randomNum)
            {
                nonCritHits = 0;
                return true;
            }
            else
            {
                ++nonCritHits;
                return false;
            }
        }
        private bool CheckCritStrike()
        {
            int randomNum = Random.Range(0, 99);

            if (chance * 100 > randomNum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public float GetDamageValueAfterCrit(float oriDamageValue, bool useSmoothChance = false)
        {
            if( !valid )
            {
                return oriDamageValue;
            }
            
            // 使用平滑暴击率
            if (useSmoothChance)
            {
                return CheckSmoothCritStrike() ? (oriDamageValue * valuePer) : oriDamageValue;
            }
            // 使用默认伪随机暴击率（Random本身就是一个伪随机数类）
            else
            {
                return CheckCritStrike() ? (oriDamageValue * valuePer) : oriDamageValue;
            }
        }
        #endregion

    }

}


