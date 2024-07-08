

namespace MyFramework.GameObjects.Backpack.ItemSlot
{
    public class ItemSlotBase<TItem>
        where TItem : class
    {
        // 槽位内容
        protected TItem _content = null;
        public TItem content { get => _content; protected set => _content = value; }


        public virtual TItem OnPlaceItem(TItem newItem)
        {
            // 原先没有物品：直接占用
            if( content == null )
            {
                content = newItem;
                return null;
            }
            // 原先已有物品：替换
            else
            {
                TItem oriItem = content;
                content = newItem;
                return oriItem; // 放到玩家手中
            }

        }

    }

}


