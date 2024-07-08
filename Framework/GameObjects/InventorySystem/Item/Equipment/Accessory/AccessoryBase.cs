using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Equipment
{
    public abstract class AccessoryBase : EquipmentBase
    {
        
        
        
        public abstract override void OnEquip();
        public abstract override void OnUnEquip();

    }

}


