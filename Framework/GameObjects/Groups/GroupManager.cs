using Common;
using System;
using System.Collections.Generic;


namespace MyFramework.GameObjects.Group
{
    // ������Ӫö��
    public enum GroupType
    {
        // ��Ч���
        Invalid = 0,
        // �ѷ���λ
        Ally = 1,
        // �з���λ
        Enemy = 2,
        // ����Ӫ��λ
        Neutral = 3,
    }
    
    // Group�����ֶ�����Ӫ
    public sealed class GroupManager : Singleton<GroupManager>
    {
        #region Attribute
        // ��Ӫ��λ�б��洢 GameObject.InstanceID
        // ���� InstanceID ��ȡ������Ӫ
        private Dictionary<int, GroupType> groupMap = new();


        // ����ʵ������Ӫ
        public void SaveInstanceGroup(int instID, GroupType group)
        {
            groupMap.Add(instID, group);
        }
        // �Ƴ�ʵ������Ӫ��ɾ������ʱ���������ն����ʱ������
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


