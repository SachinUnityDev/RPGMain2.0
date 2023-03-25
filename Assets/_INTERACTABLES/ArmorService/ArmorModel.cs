using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Town;
using UnityEngine;


namespace Interactables
{
    //public class CharSocketData
    //{
    //    public CharNames charName;
    //    public GemNames gemDivineSocket1;
    //    public GemNames gemDivineSocket2;
    //    public GemNames gemSupportSocket3;

    //    public CharSocketData(CharNames charName)
    //    {
    //        this.charName = charName;
    //        this.gemDivineSocket1 = GemNames.None;
    //        this.gemDivineSocket2 = GemNames.None;
    //        this.gemSupportSocket3 = GemNames.None;
    //    }
    //}

    public class ArmorModel
    {
        /// <summary>
        /// ARMOR SOCKETING IS CONTROLLED BY ITEM MODEL 
        /// </summary>
        public CharNames charName;
        public ArmorType armorType;
        public Currency fortifyCost;
        public string armorTypeStr = "";
        public int minArmor;
        public int maxArmor;
        public int upMinArmor;
        public int upMaxArmor;
        public ArmorState armorState;

        public int nosOfDays;

        public ArmorModel(ArmorSO armorSO)
        {
            armorType= armorSO.armorType;   
            fortifyCost= armorSO.fortifyCost.DeepClone();
            minArmor= armorSO.minArmor;
            maxArmor= armorSO.maxArmor;
        
            armorState= armorSO.armorState; 
        }
    }
}

