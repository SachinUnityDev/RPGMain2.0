using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class TavernModel 
    {

        [Header("Interact: Trophy")]
        public Iitems trophyOnWall = null; 
        public Iitems peltOnWall = null;

   

        public List<CharInteractData> charInteract = new List<CharInteractData>();

        public List<NPCInteractData> npcData = new List<NPCInteractData>();

        public List<BuildIntTypeData> buildIntTypes = new List<BuildIntTypeData>();


        public TavernModel(BuildingSO tavernSO)
        {
            buildIntTypes = tavernSO.buildingData.buildIntTypes.DeepClone();
            npcData = tavernSO.buildingData.npcInteractData.DeepClone();
            charInteract = tavernSO.buildingData.charInteractData.DeepClone();

        }
    }
}