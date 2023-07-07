using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    [Serializable]
    public class TavernModel: BuildingModel
    {
        [Header("Interact: Trophy")]
        public Iitems trophyOnWall = null; 
        public Iitems peltOnWall = null;

        [Header("Buy Drinks")]
        public int selfDrinks = 0;
        public bool canOfferDrink = true; 

        //public List<CharInteractData> charInteract = new List<CharInteractData>();

        //public List<NPCInteractData> npcData = new List<NPCInteractData>();

        //public List<BuildIntTypeData> buildIntTypes = new List<BuildIntTypeData>();

        public TavernModel(BuildingSO tavernSO)
        {
            buildingName= tavernSO.buildingName;    
            buildState = tavernSO.buildingState; 

            buildIntTypes = tavernSO.buildIntTypes.DeepClone();
            npcInteractData = tavernSO.npcInteractData.DeepClone();
            charInteractData = tavernSO.charInteractData.DeepClone();
        }
    }
}