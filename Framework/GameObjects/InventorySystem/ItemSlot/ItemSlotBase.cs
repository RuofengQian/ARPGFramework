

namespace MyFramework.GameObjects.Backpack.ItemSlot
{
    public class ItemSlotBase<TItem>
        where TItem : class
    {
        // ��λ����
        protected TItem _content = null;
        public TItem content { get => _content; protected set => _content = value; }


        public virtual TItem OnPlaceItem(TItem newItem)
        {
            // ԭ��û����Ʒ��ֱ��ռ��
            if( content == null )
            {
                content = newItem;
                return null;
            }
            // ԭ��������Ʒ���滻
            else
            {
                TItem oriItem = content;
                content = newItem;
                return oriItem; // �ŵ��������
            }

        }

    }

}


