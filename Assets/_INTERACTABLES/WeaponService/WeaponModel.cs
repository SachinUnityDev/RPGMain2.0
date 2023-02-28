using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    [Serializable]
    public class WeaponModel
    {   
        public CharNames charName;
        public GemNames gemName;
        public WeaponState weaponState;
        public int chargeRemaining = 0;

        public WeaponModel(WeaponSO weaponSO)
        {
            this.charName = weaponSO.charName;
            this.gemName = weaponSO.gemName;
            this.weaponState = weaponSO.weaponState;
            chargeRemaining = 0; 
        }

        public bool IsChargeZero()
        {
            if (chargeRemaining == 0)
                return true;
            else
                return false; 
        }
    }
    public enum WeaponState
    {
        UnEnchantable, 
        Unidentified, 
        Identified, 
        Enchanted, 
        Rechargeable, // 0 charge 
    }
  

}
