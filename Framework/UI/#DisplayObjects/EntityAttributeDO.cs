using MyFramework.GameObjects.Attribute;


namespace MyFramework.UI.DO
{
    public struct EntityAttributeDO
    {
        // 最大生命值
        public float maxHealth;
        // 当前生命值
        public float currHealth;

        // 物理防御
        public float physicDefence;
        // 魔法防御
        public float magicDefence;


        public static EntityAttributeDO BuildDO(EntityAttribute entityAttr)
        {
            EntityAttributeDO entityAttrDO = new EntityAttributeDO();
            
            entityAttrDO.maxHealth = entityAttr.Health.maxValue;
            entityAttrDO.currHealth = entityAttr.Health.currValue;

            entityAttrDO.physicDefence = entityAttr.PhysicDefence.value;
            entityAttrDO.magicDefence = entityAttr.MagicDefence.value;

            return entityAttrDO;
        }

    }

}