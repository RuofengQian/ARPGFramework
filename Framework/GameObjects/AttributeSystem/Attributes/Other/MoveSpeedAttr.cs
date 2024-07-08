using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Attribute
{
    public sealed class MoveSpeed : AttributeBase, IMagnificAttr
    {
        #region Property
        // �����ƶ��ٶ�
        private float _basicValue;

        // �����ƶ��ٶȰٷֱ�
        private float _extraPer;

        public float basicValue { get => _basicValue; private set => _basicValue = value; }

        public float extraPer { get => _extraPer; private set => _extraPer = value; }

        // �ƶ��ٶȰٷֱ�
        public float per { get => (1 + extraPer); }
        // �ƶ��ٶ�
        public float value { get => basicValue * per; }


        public void AddExtraPer(float val)
        {
            extraPer += val;
        }
        public void RemoveExtraPer(float val)
        {
            extraPer -= val;
        }

        public void SetBasicValueByMul(float mul)
        {
            basicValue = AttributeDefaults.MoveSpeed.BASIC_MOVE_SPEED * mul;
        }
        #endregion

        #region Construct
        public MoveSpeed(float mul = 1f, float extraPer = 0f)
        {
            // ˵����ͨ���Ի�׼�ٶȵ��ٶȳ��������� basicSpeed �ĳ�ʼֵ
            this.basicValue = AttributeDefaults.MoveSpeed.BASIC_MOVE_SPEED * mul;
            this.extraPer = extraPer;
        }
        #endregion

    }




}





