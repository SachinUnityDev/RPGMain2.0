using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Diagnostics.Contracts;

namespace Interactables
{
    public class WeaponService : MonoSingletonGeneric<WeaponService>
    {
        public AllWeaponSO allWeaponSO;
        public List<WeaponModel> allWeaponModel = new List<WeaponModel>();
        public List<WeaponController> allWeaponController = new List<WeaponController>();
        public GameObject weaponPanel;

        [Header("Not TBR")]
        public WeaponViewController weaponViewController;


        public bool IsGemEnchantable(CharNames charName, GemNames gemName)
        {
            CharModel charModel = CharService.Instance.GetAllyCharModel(charName);
            GemNames charGemName = 
                        charModel.enchantableGem4Weapon;
            if (gemName != charGemName)
                return false;
            else
                return true;
        }
        
        public bool EnchantWeapon(GemNames gemName)
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

