using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class TempleModel
    {
        [Header("Build State")]
        public BuildingState buildState;

        [Header("UPGRADE")]
        public bool isBuildingUpgraded = false;

        public List<BuildIntTypeData> buildIntTypes = new List<BuildIntTypeData>();
        public List<NPCInteractData> allNPCInteractData= new List<NPCInteractData>();
        public List<CharInteractData> allCharInteractData = new List<CharInteractData>();
        public TempleModel(BuildingSO templeSO)
        {
            buildState = templeSO.buildingData.buildingState;



            buildIntTypes = templeSO.buildingData.buildIntTypes.DeepClone();
            allNPCInteractData = templeSO.buildingData.npcInteractData.DeepClone();
            allCharInteractData = templeSO.buildingData.charInteractData.DeepClone(); 
        }
    }
}