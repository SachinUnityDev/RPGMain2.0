using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Management;
using UnityEngine;
using UnityEngine.UI; 
namespace Interactables
{
    [Serializable]
    public class WeaponStateImgNColor
    {
        public WeaponState weaponState;
        public Sprite stateImg;
        public Color stateColor; 
    }


    [CreateAssetMenu(fileName = "AllWeaponSO", menuName = "Interactable/AllWeaponSO")]
    public class AllWeaponSO : ScriptableObject
    {
        public List<WeaponSO> allWeaponSO = new List<WeaponSO>();
        [Header("weapon SO")]
        public Sprite emptySlot;

        [Header("Enchant and Recharge Price")]
        public Currency enchantValue;
        public Currency rechargeValue;

        [Header("Identify, enchant and recharge sprite")]
        public List<WeaponStateImgNColor> allWeaponStateImgNColors = new List<WeaponStateImgNColor>();     


        public WeaponSO GetWeaponSO(CharNames charName)
        {
            int index = allWeaponSO.FindIndex(t=>t.charName == charName);
            if(index != -1)            
                return allWeaponSO[index];            
            else            
                Debug.Log("Weapon SO not found" + charName);                
            return null; 
        }

        public WeaponStateImgNColor GetWeaponStateSpecs(WeaponState weaponState)
        {
            int index =  allWeaponStateImgNColors.FindIndex(t=>t.weaponState== weaponState);
            if(index != -1)
                return allWeaponStateImgNColors[index];
            else
            {
                Debug.Log("Weapon state specs not found"); 
                return null;
            }
        }
    }
}