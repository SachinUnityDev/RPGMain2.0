using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class WeaponController : MonoBehaviour
    {
        public WeaponModel weaponModel;
        CharController charController;
        private void Start()
        {
            charController= GetComponent<CharController>();
            weaponModel = new WeaponModel(WeaponService.Instance.allWeaponSO
                            .GetWeaponSO(charController.charModel.charName)); 
        }
        public bool IsGemEnchantable(GemNames gemName)
        {
           GemNames charGemName = weaponModel.gemName; 
                  
            if (gemName != charGemName) 
                return false;
            WeaponState weaponState= weaponModel.weaponState;
            if(weaponState == WeaponState.Rechargeable || weaponState == WeaponState.Identified)
                return true;
            else
                return false;   
        }

        public bool EnchantWeapon(GemNames gemName)
        {           
            if (IsGemEnchantable(gemName))
            {
                // get gembase enchant weapon 
                // Unlock the weapon skill
                return true;
            }
            else
            {
                // error message
                return false;
            }

        }

    }
}
