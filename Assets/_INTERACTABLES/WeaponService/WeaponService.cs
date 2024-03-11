using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Interactables
{
    public class WeaponService : MonoSingletonGeneric<WeaponService>
    {
        public AllWeaponSO allWeaponSO;
        public List<WeaponModel> allWeaponModel = new List<WeaponModel>();
        public List<WeaponController> allWeaponController = new List<WeaponController>();
        public GameObject weaponInvPanel;

        [Header("Not TBR")]
        public WeaponViewController weaponViewController;
       // WeaponFactory weaponFactory; 
        public void Init()
        {
            //itemFactory = GetComponent<ItemFactory>();
            //itemFactory.ItemInit();

            //foreach (CharController charController in CharService.Instance.allyInPlayControllers)
            //{
            //    ItemController itemController =
            //                charController.gameObject.AddComponent<ItemController>();
            //    itemController.Init();
            //    allItemControllers.Add(itemController);
            //}
            //CalendarService.Instance.OnStartOfCalDay += (int day) => OnDayTickOnScroll();
            //isNewGInitDone = true;
        }


        public WeaponModel GetWeaponModel(CharNames charName)
        {
            int index = allWeaponModel.FindIndex(t=>t.charName == charName);    
            if(index !=-1)           
            {
                return allWeaponModel[index];
            }
            else
            {
                Debug.Log("weapon model not found " + charName); 
                return null;
            }
        }
        public bool IsGemEnchantable(CharController charController, GemNames gemName)
        {
            CharModel charModel = charController.charModel; 
            GemNames charGemName = 
                        charModel.enchantableGem4Weapon;
            if (gemName != charGemName)
                return false;
            else
                return true;
        }
        
        public bool EnchantWeapon(GemNames gemName)
        {
            CharController charSelectCtrl = InvService.Instance.charSelectController;
            if (IsGemEnchantable(charSelectCtrl, gemName))
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

