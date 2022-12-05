using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Interactables
{
    

    [System.Serializable]
    public class GemModel
    {
        // to be init at the time of Gem Creation in inventory  
        public GemName gemName ;
        public GemType gemtype;

        public CharNames charName;
        public int charID;

        // to be updated when the Gem is Socketted or Enchanted 
        public GemState gemState = GemState.None;     
        public ArmorType armorType;
        public WeaponType weaponType;
        public int currCharge =12;
        public SlotType invLoc;


        [Header("From SO")]
        public int inventoryStack;
        public Currency cost;
        public float fluctuation;
        public int maxWorldInstance;
        public List<string> allLines = new List<string>();
        public float rangeVal = 0;
        


        public GemModel(GemSO gemSO, CharController charController)
        {
            this.gemName = gemSO.gemName;
            this.gemtype = gemSO.gemType; 
            this.inventoryStack = gemSO.maxInvStackSize;
            this.cost = gemSO.cost;
            this.fluctuation = gemSO.fluctuation;
            this.maxWorldInstance = gemSO.maxWorldInstance;
            this.allLines = gemSO.allLines.DeepClone();
           // this.rangeVal = Random.Range(gemSO.minSpecrange, gemSO.maxSpecRange);
            this.charName = charController.charModel.charName;
            // write algo to find range style here and replace or add RangeVal here 

        }
    }

}

