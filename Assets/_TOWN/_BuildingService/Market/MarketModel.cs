using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


namespace Town
{
    public class MarketModel: BuildingModel
    {       

        [Header("Craft potion")]
        public Iitems healthPotion;
        public Iitems staminaPotion;
        public Iitems fortPotion;
        public int costOfCraftInBronze;

        //public List<CharInteractData> charInteract = new List<CharInteractData>();

        //public List<NPCInteractData> npcData = new List<NPCInteractData>();

        //public List<BuildIntTypeData> buildIntTypes = new List<BuildIntTypeData>();
        public MarketModel(BuildingSO marketSO)
        {
            buildState = marketSO.buildingState;

            buildIntTypes = marketSO.buildIntTypes.DeepClone();
            npcInteractData = marketSO.npcInteractData.DeepClone();
            charInteractData = marketSO.charInteractData.DeepClone();
            costOfCraftInBronze = 9;
        }
    }
}