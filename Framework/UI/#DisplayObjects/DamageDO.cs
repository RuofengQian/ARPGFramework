using MyFramework.GameObjects.Attribute;
using MyFramework.GameObjects.Lifecycle;


namespace MyFramework.UI.DO
{
    public struct DamageDO
    {
        public DamageType type;
        
        public float value;


        public static DamageDO BuildDO(Damage damage)
        {
            DamageDO damageDO = new DamageDO();

            damageDO.type = damage.type;
            damageDO.value = damage.value;

            return damageDO;
        }

    }

}

