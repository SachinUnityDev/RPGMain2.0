using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    //public class WeaponEnchantData
    //{
        

    //    //public WeaponEnchantData(CharNames charName, GemNames gemName)
    //    //{
    //    //    this.charName = charName;
    //    //    this.gemName = gemName;

    //    //}
    //}

    public class WeaponModel
    {
        //public List<WeaponEnchantData> allWeaponsEnchanted 
        //                                = new List<WeaponEnchantData>();
        public CharNames charName;
        public GemNames gemName;
        public WeaponState weaponState;
        public int chargeRemaining = 0;

        public WeaponModel(WeaponSO weaponSO)
        {
            this.charName = weaponSO.charName;
            this.gemName = weaponSO.gemName;
            this.weaponState = weaponSO.weaponState;
            this.chargeRemaining = weaponSO.chargeRemaining;
        }
    }
    public enum WeaponState
    {
        None, 
        Unidentified, 
        Identified, 
        Enchanted, 
        Rechargeable, // 0 charge 

    }
  

}
