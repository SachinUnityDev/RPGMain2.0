using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Interactables
{

    [Serializable]
    public class ArmorSpriteData
    {
        public CharNames charName;
        public Sprite armorSprite;
    }

    [CreateAssetMenu(fileName = "ArmorSO", menuName = "Interactable/ArmorSO")]
    public class ArmorSO : ScriptableObject
    {

        public Sprite emptySlotSprite;

        public List<ArmorSpriteData> allArmorSprites = new List<ArmorSpriteData>();
        public Sprite GetSprite(CharNames charName)
        {
            if (charName == CharNames.None) return null; 
            Sprite sprite = allArmorSprites?.Find(t => t.charName == charName).armorSprite;
            if (sprite != null)
                return sprite;
            else
                Debug.Log("Armor sprite is null");
            return null; 
        }
    }
}
