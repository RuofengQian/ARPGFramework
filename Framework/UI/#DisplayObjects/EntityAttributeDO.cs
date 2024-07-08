using MyFramework.GameObjects.Attribute;


namespace MyFramework.UI.DO
{
    public struct EntityAttributeDO
    {
        // �������ֵ
        public float maxHealth;
        // ��ǰ����ֵ
        public float currHealth;

        // �������
        public float physicDefence;
        // ħ������
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