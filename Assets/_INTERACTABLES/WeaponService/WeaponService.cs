using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Interactables
{
    public class WeaponService : MonoSingletonGeneric<WeaponService>
    {
        // enchantemnet to be done in temple or thru scroll read 

        public WeaponSO weaponSO;
        public WeaponModel weaponModel;
        public GameObject weaponPanel;

        [Header("Not TBR")]
        public WeaponViewController weaponViewController;
        public WeaponController weaponController;
        
        void Start()
        {

        }

        public bool IsGemEnchantable(CharNames charName, GemName gemName)
        {
            CharModel charModel = CharService.Instance.GetAllyCharModel(charName);
            GemName charGemName = 
                        charModel.enchantableGem4Weapon;
            if (gemName != charGemName)
                return false;
            else
                return true;
        }
        
        public bool EnchantWeapon(GemName gemName)
        {
            CharNames charName = InvService.Instance.charSelect;
            if (IsGemEnchantable(charName, gemName))
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

