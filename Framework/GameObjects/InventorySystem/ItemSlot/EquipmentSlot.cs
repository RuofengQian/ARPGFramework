

using MyFramework.GameObjects.Backpack.Item;

namespace MyFramework.GameObjects.Backpack.ItemSlot
{
    // װ����
    public class EquipmentSlot : ItemSlotBase<IEqupiableItem>
    {
        public override IEqupiableItem OnPlaceItem(IEqupiableItem newEquip)
        {
            // ԭ��û����Ʒ��ֱ��ռ��
            if (content == null)
            {
                content = newEquip;
                newEquip.OnEquip();
                return null;
            }
            // ԭ��������Ʒ���滻
            else
            {
                // ��жԭ����װ��
                IEqupiableItem oriEquip = content;
                oriEquip.OnUnEquip();

                // װ���µ�װ��
                content = newEquip;
                newEquip.OnEquip();

                return oriEquip; // �ŵ��������
            }

        }


    }

}

