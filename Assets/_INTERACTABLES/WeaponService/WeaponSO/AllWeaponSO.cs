using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{

    [CreateAssetMenu(fileName = "AllWeaponSO", menuName = "Interactable/AllWeaponSO")]
    public class AllWeaponSO : ScriptableObject
    {
        public List<WeaponSO> allWeaponSO = new List<WeaponSO>();
        [Header("weapon SO")]
        public Sprite emptySlot;

        [Header("Enchant and Recharge Price")]
        public Currency enchantValue;
        public Currency rechargeValue; 

    }
}