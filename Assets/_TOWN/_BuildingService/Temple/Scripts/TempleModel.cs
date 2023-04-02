using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class TempleModel
    {

        [Header("UPGRADE")]
        public bool isBuildingUpgraded = false;

        public List<BuildIntTypeData> buildIntTypes = new List<BuildIntTypeData>();
        public List<NPCInteractData> allNPCInteractData= new List<NPCInteractData>();
        public List<CharInteractData> allCharInteractData = new List<CharInteractData>();
        public TempleModel(BuildingSO templeSO)
        {
            buildIntTypes = templeSO.buildingData.buildIntTypes.DeepClone();
            allNPCInteractData = templeSO.buildingData.npcInteractData.DeepClone();
            allCharInteractData = templeSO.buildingData.charInteractData.DeepClone(); 
        }
    }
}