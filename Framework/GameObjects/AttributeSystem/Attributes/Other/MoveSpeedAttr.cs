using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Attribute
{
    public sealed class MoveSpeed : AttributeBase, IMagnificAttr
    {
        #region Property
        // 基础移动速度
        private float _basicValue;

        // 额外移动速度百分比
        private float _extraPer;

        public float basicValue { get => _basicValue; private set => _basicValue = value; }

        public float extraPer { get => _extraPer; private set => _extraPer = value; }

        // 移动速度百分比
        public float per { get => (1 + extraPer); }
        // 移动速度
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
            // 说明：通过对基准速度的速度乘数，设置 basicSpeed 的初始值
            this.basicValue = AttributeDefaults.MoveSpeed.BASIC_MOVE_SPEED * mul;
            this.extraPer = extraPer;
        }
        #endregion

    }




}





