using Common;
using System;
using System.Collections.Generic;


namespace MyFramework.GameObjects.Group
{
    // 对象阵营枚举
    public enum GroupType
    {
        // 无效组别
        Invalid = 0,
        // 友方单位
        Ally = 1,
        // 敌方单位
        Enemy = 2,
        // 无阵营单位
        Neutral = 3,
    }
    
    // Group：区分对象阵营
    public sealed class GroupManager : Singleton<GroupManager>
    {
        #region Attribute
        // 阵营单位列表：存储 GameObject.InstanceID
        // 根据 InstanceID 获取所属阵营
        private Dictionary<int, GroupType> groupMap = new();


        // 保存实例的阵营
        public void SaveInstanceGroup(int instID, GroupType group)
        {
            groupMap.Add(instID, group);
        }
        // 移除实例的阵营：删除对象时触发，回收对象池时不触发
        public void RemoveInstanceGroup(int instID)
        {
            groupMap.Remove(instID);
        }
        #endregion


        #region JudgeGroup
        public bool IsAlly(int instID) => (groupMap.ContainsKey(instID) && groupMap[instID] == GroupType.Ally);
        public bool IsEnemy(int instID) => (groupMap.ContainsKey(instID) && groupMap[instID] == GroupType.Enemy);

        public GroupType GetInstanceGroup(int instID)
        {
            return groupMap.ContainsKey(instID) ? groupMap[instID] : GroupType.Invalid;
        }
        #endregion

    }

}


