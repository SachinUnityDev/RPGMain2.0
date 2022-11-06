using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 


namespace Interactables
{
    [Serializable]
    public class WeaponSpriteData
    {
        public CharNames charName;
        public Sprite weaponSprite; 
    }

    [CreateAssetMenu(fileName = "WeaponSO", menuName = "Interactable/WeaponSO")]
    public class WeaponSO : ScriptableObject
    {
        [Header("Weapon Slot sprites")]
        public Sprite emptySlot;

        public List<WeaponSpriteData> allWeaponSprites = new List<WeaponSpriteData>();

        public Sprite GetSprite(CharNames charName)
        {
            if (charName == CharNames.None) return null;
            Sprite sprite = allWeaponSprites.Find(t => t.charName == charName).weaponSprite;
            if (sprite != null)
                return sprite;
            else
                Debug.Log("Weapon sprite is null");
            return null;
        }

    }
}

