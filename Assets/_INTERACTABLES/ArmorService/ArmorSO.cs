using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Town;
using System;

namespace Interactables
{
    [Serializable]
    public class ArmorDataVsLoc
    {
        public LocationName locationName;
        public Currency priceFortify;
        public Currency priceFortifyOnUpgrade; 
        public Currency priceUnSocketDiv;
        public Currency priceUnSocketSupport;
        public ArmorState armorState; 
    }

    [CreateAssetMenu(fileName = "ArmorSO", menuName = "Interactable/ArmorSO")]

    public class ArmorSO : ScriptableObject
    {
        public List<CharNames> charNames= new List<CharNames>();
        public ArmorType armorType;

        public string armorTypeStr = "";
        public int minArmor; 
        public int maxArmor;
        public int minArmorUp; 
        public int maxArmorUp;  
   
        public List<CharArmorData> allCharArmorData = new List<CharArmorData>();
        public List<ArmorDataVsLoc> allArmorDataVsLoc = new List<ArmorDataVsLoc>();    
        [TextArea(5,10)]
        public List<string> allLines=new List<string>();
        private void Awake()
        {
            if(allLines.Count == 0)
            {
                string str = $"Fortify to gain Armor buff"; 
                allLines.Add(str);
            }
        }
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