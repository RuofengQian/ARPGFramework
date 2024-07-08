

namespace MyFramework.UI.DO
{
    public struct ItemInfoDO
    {
        // 物品名称
        public string ItemName;
        // 物品种类：材料、武器、配饰...
        public string ItemType;

        // 物品描述
        public string ItemDesc;


        public static ItemInfoDO BuildDO()
        {
            ItemInfoDO itemInfoDO = new ItemInfoDO();

            // TODO


            return itemInfoDO;
        }

    }

}


