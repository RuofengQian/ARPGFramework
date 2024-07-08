

using MyFramework.GameObjects.Backpack.Item;

namespace MyFramework.GameObjects.Backpack.ItemSlot
{
    // 装备栏
    public class EquipmentSlot : ItemSlotBase<IEqupiableItem>
    {
        public override IEqupiableItem OnPlaceItem(IEqupiableItem newEquip)
        {
            // 原先没有物品：直接占用
            if (content == null)
            {
                content = newEquip;
                newEquip.OnEquip();
                return null;
            }
            // 原先已有物品：替换
            else
            {
                // 拆卸原来的装备
                IEqupiableItem oriEquip = content;
                oriEquip.OnUnEquip();

                // 装备新的装备
                content = newEquip;
                newEquip.OnEquip();

                return oriEquip; // 放到玩家手中
            }

        }


    }

}

