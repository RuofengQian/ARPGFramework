using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Attribute
{
    public static class AttributeDefaults
    {
        public static class Health
        {
            
        }

        public static class Attack
        {
            
        }

        public static class CritStrike
        {
            public static readonly float BASIC_CRIT_DAMAGE_PER = 1.5f; // 150%
        }
        
        public static class AttackSpeed
        {
            public static readonly float BASIC_ATTACK_SPEED = 1f; // 1Hz
        }

        public static class MoveSpeed
        {
            public static readonly float BASIC_MOVE_SPEED = 150f; // 根据游戏尺度规格定义
        }


    }
}


