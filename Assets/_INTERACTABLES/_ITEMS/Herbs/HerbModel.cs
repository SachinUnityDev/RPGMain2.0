using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Interactables
{
    public class HerbModel
    {
        public CharNames charName;
        public int charID; 

        public HerbNames herbName;
        public int maxInventoryStack = 0;
        public Currency cost;
        public float fluctuation;

        [Header("Buff on Abzazulus Consumption")]
        public int HpRegenVal;
        public int bufftimeInRds;
        public SlotType invLoc; 
        public HerbModel(HerbSO herbSO, CharController charController)
        {
            charName = charController.charModel.charName;
            charID = charController.charModel.charID;
            herbName = herbSO.herbName;
            maxInventoryStack = herbSO.maxInventoryStack;
            cost = herbSO.cost;    
            // Only for ABzazulu , he can only consume and will get these buffs
            HpRegenVal = herbSO.HpRegenVal;
            bufftimeInRds = herbSO.bufftimeInRds;
        }
    }
}
