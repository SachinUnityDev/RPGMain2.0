using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 


namespace Interactables
{
    [System.Serializable]
    public class PotionModel // potion attached to a char 
    {
        public PotionName potionName;
        public CharNames charName;
        public int charID;
        public int potionID; 
        public float potionAddict = 0f;      
        public int inventoryStack;
        public Currency cost = new Currency();
        public float fluctuation;
        public int castTime;
        public int currWorldInstance; 
        public int maxWorldInstance;  // max number this potion can have in the game 
        public List<string> allLines = new List<string>();
        public SlotType potionLoc = SlotType.None;

        public PotionModel(PotionSO potionSO)
        {
            this.potionName = potionSO.potionName;
            this.potionAddict = potionSO.potionAddict;
            this.inventoryStack = potionSO.inventoryStack;
            this.cost = potionSO.cost;
            this.fluctuation = potionSO.fluctuation;
            this.castTime = Random.Range(potionSO.minCastTime, potionSO.maxCastTime); 
            this.maxWorldInstance = potionSO.maxWorldInstance;

            //this.charName = charController.charModel.charName;
            //this.charID = charController.charModel.charID;
            this.allLines = potionSO.allLines.DeepClone();
        }

    }




}

