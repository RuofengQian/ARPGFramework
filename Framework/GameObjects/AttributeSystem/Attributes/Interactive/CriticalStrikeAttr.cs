using UnityEngine;


namespace MyFramework.GameObjects.Attribute
{
    public sealed class CriticalStrike : AttributeBase, IProbAttr
    {
        #region Property
        // ������
        private float _chance;

        // ���������˺��ٷֱ�
        private float _basicPer;
        // ���ӱ����˺��ٷֱ�
        private float _extraPer;

        public float chance { get => _chance; private set => _chance = value; }

        public float basicValuePer { get => _basicPer; private set => _basicPer = value; }
        public float extraValuePer { get => _extraPer; private set => _extraPer = value; }

        // ����
        public float valuePer { get => (1f + extraValuePer); }

        // ��Ч
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
        // δ��������
        private int nonCritHits = 0;
        // ʵ�ʱ����ʣ������ƣ�
        private float smoothChance
        {
            get => (chance + GetSmoothExtraChance());
        }
        // ƽ�����Ⱪ���ʼ��㷽��
        private float GetSmoothExtraChance()
        {
            // TODO��ʵ�ֱ�����ƽ���㷨
            return (nonCritHits * 0.01f);
        }

        // ����Ƿ񱩻�
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
            
            // ʹ��ƽ��������
            if (useSmoothChance)
            {
                return CheckSmoothCritStrike() ? (oriDamageValue * valuePer) : oriDamageValue;
            }
            // ʹ��Ĭ��α��������ʣ�Random�������һ��α������ࣩ
            else
            {
                return CheckCritStrike() ? (oriDamageValue * valuePer) : oriDamageValue;
            }
        }
        #endregion

    }

}


