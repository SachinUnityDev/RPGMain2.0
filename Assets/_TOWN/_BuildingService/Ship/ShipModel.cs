using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class ShipModel
    {

        public List<CharInteractData> charInteract = new List<CharInteractData>();

        public List<NPCInteractData> npcData = new List<NPCInteractData>();

        public List<BuildIntTypeData> buildIntTypes = new List<BuildIntTypeData>();
        public ShipModel(BuildingSO shipSO)
        {
            buildIntTypes = shipSO.buildingData.buildIntTypes.DeepClone();
            npcData = shipSO.buildingData.npcInteractData.DeepClone();
            charInteract = shipSO.buildingData.charInteractData.DeepClone();

        }
    }
}