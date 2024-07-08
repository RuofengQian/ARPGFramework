using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Equipment
{
    // ��������
    public enum ClothingGroup
    {
        // ͷ����ͷ������ߡ�����...
        Face = 0,
        // ���壺���¡�����ȹ
        Body = 1,
        // �Ȳ����㡢��ȹ
        Legs = 2,
        // �㲿��Ь�ӡ�����Ь��
        Feet = 3,
    }

    // ��Ʒ����
    public enum AccessoryGroup
    {
        // ͷ��
        Headdress,
        // ����
        Necklace,
        // �ֻ�
        Bracelets,
    }

    
    
    public class EquipmentController : MonoBehaviour
    {
        #region Init
        private void Awake()
        {
            // ����
            clothingMap = new();
            foreach (ClothingGroup group in Enum.GetValues(typeof(ClothingGroup)))
            {
                clothingMap[group] = null;
            }
            // ��Ʒ��
            accessories = new(accSlotCount);
        }
        #endregion


        // Clothing - �� Group ���й���ÿ Group �� 1 ��
        #region Clothing

        private Dictionary<ClothingGroup, ClothingBase> clothingMap;


        public void Change(ClothingBase clothing)
        {

        }
        #endregion

        // Accessory - �� Group ���ƣ����������������
        #region Accessory
        // ��Ʒ��
        private List<AccessoryBase> accessories;
        // ��Ʒ����λ����
        private int accSlotCount = 5;

        
        // ��ȷװ��������ж�µ�װ��
        public AccessoryBase EquipAccessory(AccessoryBase accessory, int slotIndex)
        {
            if( slotIndex >= accessories.Count )
            {
                Debug.LogError($"[Error] ��ͼ����λ {slotIndex} װ����Ʒ�������������Ʒ������ {accessories.Count}");
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
        // ����װ��������ж�µ�װ��
        public AccessoryBase QuickEquipAccessory(AccessoryBase accessory)
        {
            // ����һ������λ
            for( int i=0; i<accSlotCount; ++i )
            {
                if( accessories[i] == null )
                {
                    return EquipAccessory(accessory, i);
                }
            }
            // û���ҵ�����λ���滻��0����Ʒ
            return EquipAccessory(accessory, 0);
        }
        #endregion

    }

}



