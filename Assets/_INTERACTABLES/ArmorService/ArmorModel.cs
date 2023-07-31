using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Town;
using UnityEngine;


namespace Interactables
{
    [Serializable]
    public class ArmorModel
    {
        /// <summary>
        /// ARMOR SOCKETING IS CONTROLLED BY ITEM MODEL 
        /// </summary>
        public CharNames charName;
        public ArmorType armorType;        
        public string armorTypeStr = "";
        public int minArmor;
        public int maxArmor;
        public int minArmorUp;
        public int maxArmorUp;
   
        // Armor Data contains state, currency cost for fortify Etc. 
        public List<ArmorDataVsLoc> allArmorDataVsLoc = new List<ArmorDataVsLoc>();

        public ArmorModel(ArmorSO armorSO)
        {
            armorType= armorSO.armorType;               
            minArmor= armorSO.minArmor;
            maxArmor= armorSO.maxArmor;
            minArmorUp = armorSO.minArmorUp; 
            maxArmorUp =armorSO.maxArmorUp;
            armorTypeStr= armorSO.armorTypeStr;
            
            allArmorDataVsLoc = armorSO.allArmorDataVsLoc;           
        }

        public Currency GetFortifyCost(LocationName locName)
        {
            ArmorDataVsLoc armorData = GetArmorDataVsLoc(locName);
            return armorData.currFortify;
            
        }

        public Currency GetUnSocketCostDiv(LocationName locName)         
        {
            ArmorDataVsLoc armorData = GetArmorDataVsLoc(locName);           
            return armorData.currSocketDiv;
            
        }

        public ArmorDataVsLoc GetArmorDataVsLoc(LocationName locName)
        {
            int index = allArmorDataVsLoc.FindIndex(t=>t.locationName==locName); 
            if(index != -1)
                return allArmorDataVsLoc[index];
            else
                Debug.Log("armor data not found" + locName);
            return null;
        }

    }
}

