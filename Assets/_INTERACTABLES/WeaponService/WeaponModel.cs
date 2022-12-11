using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class WeaponEnchantData
    {
        public CharNames charName;
        public GemNames gemName; 
        public int chargeRemaining = 0;
        public int noOfRechargeDone = 0;

        public WeaponEnchantData(CharNames charName, GemNames gemName)
        {
            this.charName = charName;
            this.gemName = gemName;
        }
    }

    public class WeaponModel
    {        
        public List<WeaponEnchantData> allWeaponsEnchanted 
                                        = new List<WeaponEnchantData>();
    }
}
