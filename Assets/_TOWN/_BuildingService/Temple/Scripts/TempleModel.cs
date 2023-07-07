using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    [Serializable]
    public class TempleModel: BuildingModel
    {
        //[Header("Build State")]
        //public BuildingState buildState;

        [Header("UPGRADE")]
        public bool isBuildingUpgraded = false;

        //public List<BuildIntTypeData> buid = new List<BuildIntTypeData>();
        //public List<NPCInteractData> allNPCInteractData= new List<NPCInteractData>();
        //public List<CharInteractData> allCharInteractData = new List<CharInteractData>();
        public TempleModel(BuildingSO templeSO)
        {
            buildingName= templeSO.buildingName;
            buildState = templeSO.buildingState;
            buildIntTypes = templeSO.buildIntTypes.DeepClone();
            npcInteractData = templeSO.npcInteractData.DeepClone();
            charInteractData = templeSO.charInteractData.DeepClone(); 
        }
    }
}