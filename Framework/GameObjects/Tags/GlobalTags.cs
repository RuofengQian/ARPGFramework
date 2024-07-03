using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyProject.GameObjects.Tags
{
    // Tag：区分对象类型，决定对象行为
    public static class GlobalTags
    {
        // 说明：标记为 & 的对象类型具有生命周期
        
        // 1.场景
        // 地形 Tag
        public static readonly string TAG_TERRAIN = "Terrain";

        // 2.GameObjects
        // 实体对象 Tag
        public static readonly string TAG_ENTITY = "Entity"; // &

        // 普通技能对象 Tag：无交互能力
        public static readonly string TAG_NORMAL_SKILL = "NormalSkill";
        // 可交互技能对象 Tag：有交互能力
        public static readonly string TAG_INTACT_SKILL = "IntactSkill"; // &

        // 3.设施
        // 可破坏设施 Tag
        public static readonly string TAG_DESTABL_STRUCT = "DestroyableStruct"; // &

    }

}



