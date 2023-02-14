using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 


namespace Interactables
{

    [CreateAssetMenu(fileName = "WeaponSO", menuName = "Interactable/WeaponSO")]
    public class WeaponSO : ScriptableObject
    {
        public CharNames charName;
        public GemNames gemName;
        public WeaponState weaponState;
        public int chargeRemaining = 0;

        [Header("Weapon sprites")]
        public Sprite emptySlot;
        public Sprite weaponSprite; 

    }
}

