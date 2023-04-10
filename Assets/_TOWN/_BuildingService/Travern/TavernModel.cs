using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    [Serializable]
    public class TavernModel 
    {

        [Header("Build State")]
        public BuildingState buildState;


        [Header("Interact: Trophy")]
        public Iitems trophyOnWall = null; 
        public Iitems peltOnWall = null;

        [Header("Buy Drinks")]
        public int selfDrinks = 0;
        public bool canOfferDrink = true; 

        public List<CharInteractData> charInteract = new List<CharInteractData>();

        public List<NPCInteractData> npcData = new List<NPCInteractData>();

        public List<BuildIntTypeData> buildIntTypes = new List<BuildIntTypeData>();

        public TavernModel(BuildingSO tavernSO)
        {
            buildState = tavernSO.buildingData.buildingState; 

            buildIntTypes = tavernSO.buildingData.buildIntTypes.DeepClone();
            npcData = tavernSO.buildingData.npcInteractData.DeepClone();
            charInteract = tavernSO.buildingData.charInteractData.DeepClone();
        }
    }
}