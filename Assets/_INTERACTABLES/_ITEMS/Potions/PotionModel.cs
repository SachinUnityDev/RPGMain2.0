using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 


namespace Interactables
{
    [System.Serializable]
    public class PotionModel // potion attached to a char 
    {
        public PotionNames potionName;
        public CharNames charName;  // owned by 
        public NPCNames npcName;
        public int itemID;
        public float potionAddict = 0f;             
        public Currency cost = new Currency();// SO data
        public float fluctuation;
        public int castTime;// real time calculate from a range like 12-24 etc 
        public int currWorldInstance;                 
        public SlotType potionLoc = SlotType.None; // Inv Slot where item is stored 

        public PotionModel(PotionSO potionSO)
        {
        //    this.potionName = potionSO.potionName;
        //    this.potionAddict = potionSO.potionAddict;
        //    this.inventoryStack = potionSO.inventoryStack;
        //    this.cost = potionSO.cost;
        //    this.fluctuation = potionSO.fluctuation;
        //    this.castTime = Random.Range(potionSO.minCastTime, potionSO.maxCastTime); 
        //    this.maxWorldInstance = potionSO.maxWorldInstance;

        //    //this.charName = charController.charModel.charName;
        //    //this.charID = charController.charModel.charID;
        //    this.allLines = potionSO.allLines.DeepClone();
        }

    }




}

