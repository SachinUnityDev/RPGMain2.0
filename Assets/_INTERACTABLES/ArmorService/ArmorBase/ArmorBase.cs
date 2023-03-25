using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public abstract class ArmorBase
    {
        public abstract ArmorType armorType { get; }
        
        protected CharController charController;
        public abstract void OnArmorFortify(CharController charController);         
        public abstract void OnArmorFortifyUpgraded(CharController charController);
        public abstract void OnArmorFortifyEnd(); 
    }
}