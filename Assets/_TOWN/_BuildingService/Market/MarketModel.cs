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
            buildingName = marketSO.buildingName;
            buildState = marketSO.buildingState;

            buildIntTypes = marketSO.buildIntTypes.DeepClone();
            npcInteractData = marketSO.npcInteractData.DeepClone();
            charInteractData = marketSO.charInteractData.DeepClone();
            costOfCraftInBronze = 9;
        }
        public MarketModel(BuildingModel buildModel )
        {
            buildingName = buildModel.buildingName;
            buildState = buildModel.buildState;
            buildIntTypes = buildModel.buildIntTypes.DeepClone();
            npcInteractData = buildModel.npcInteractData.DeepClone();
            charInteractData = buildModel.charInteractData.DeepClone();
            costOfCraftInBronze = 9;
        }
    }
}