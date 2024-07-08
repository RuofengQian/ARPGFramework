using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Equipment
{
    // 服饰种类
    public enum ClothingGroup
    {
        // 头部：头盔、面具、眼罩...
        Face = 0,
        // 身体：上衣、连衣裙
        Body = 1,
        // 腿部：裤、短裙
        Legs = 2,
        // 足部：鞋子、特殊鞋类
        Feet = 3,
    }

    // 饰品种类
    public enum AccessoryGroup
    {
        // 头饰
        Headdress,
        // 项链
        Necklace,
        // 手环
        Bracelets,
    }

    
    
    public class EquipmentController : MonoBehaviour
    {
        #region Init
        private void Awake()
        {
            // 服饰
            clothingMap = new();
            foreach (ClothingGroup group in Enum.GetValues(typeof(ClothingGroup)))
            {
                clothingMap[group] = null;
            }
            // 饰品栏
            accessories = new(accSlotCount);
        }
        #endregion


        // Clothing - 按 Group 进行管理，每 Group 限 1 个
        #region Clothing

        private Dictionary<ClothingGroup, ClothingBase> clothingMap;


        public void Change(ClothingBase clothing)
        {

        }
        #endregion

        // Accessory - 无 Group 限制，但有最大数量限制
        #region Accessory
        // 饰品栏
        private List<AccessoryBase> accessories;
        // 饰品栏槽位数量
        private int accSlotCount = 5;

        
        // 精确装备：返回卸下的装备
        public AccessoryBase EquipAccessory(AccessoryBase accessory, int slotIndex)
        {
            if( slotIndex >= accessories.Count )
            {
                Debug.LogError($"[Error] 试图向栏位 {slotIndex} 装备饰品，超出了最大饰品栏数量 {accessories.Count}");
                return null;
            }

            if( accessories[slotIndex] == null )
            {
                accessories[slotIndex] = accessory;
                return null;
            }
            else
            {
                AccessoryBase unequippedAccessory = accessories[slotIndex];
                accessories[slotIndex] = accessory;
                return unequippedAccessory;
            }
        }
        // 快速装备：返回卸下的装备
        public AccessoryBase QuickEquipAccessory(AccessoryBase accessory)
        {
            // 检测第一个空栏位
            for( int i=0; i<accSlotCount; ++i )
            {
                if( accessories[i] == null )
                {
                    return EquipAccessory(accessory, i);
                }
            }
            // 没有找到空栏位，替换第0个饰品
            return EquipAccessory(accessory, 0);
        }
        #endregion

    }

}



