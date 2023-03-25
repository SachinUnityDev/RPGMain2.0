using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Town;
using System;

namespace Interactables
{
    

    [CreateAssetMenu(fileName = "ArmorSO", menuName = "Interactable/ArmorSO")]

    public class ArmorSO : ScriptableObject
    {
        public List<CharNames> charNames= new List<CharNames>();
        public ArmorType armorType;       
        public Currency fortifyCost;
        public string armorTypeStr = "";
        public int minArmor; 
        public int maxArmor;   
        public ArmorState armorState;    
        public List<CharArmorData> allCharArmorData = new List<CharArmorData>();
        public int nosOfDays; 
        [TextArea(5,10)]
        public List<string> allLines=new List<string>();

        public Sprite GetArmorSprite(CharNames charName)
        {
            int index = allCharArmorData.FindIndex(t=>t.charName == charName);
            if(index !=-1)
            {
                return allCharArmorData[index].armorSprite; 
            }
            Debug.Log("armor sprite not found" + charName);
            return null; 
        }

    }
    [Serializable]
    public class CharArmorData
    {
        public CharNames charName;
        public Sprite armorSprite; 
    }


}