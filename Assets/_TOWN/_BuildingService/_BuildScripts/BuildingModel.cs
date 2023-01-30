using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Policy;

namespace Town
{
    [System.Serializable]
    public enum BuildingState
    {
        Locked,
        UnLocked,
        Open,
        Close,
    }
    [Serializable]
    public class NPCDataInBuild
    {
        public NPCNames nPCNames;
        public NPCIntType nPCIntType;
        public bool isUnLocked; 

    }
    [Serializable]
    public class BuildIntTypeData
    {
        public BuildIntType BuildIntType;
        public bool isUnLocked = false; 
    }

    [System.Serializable]
    public class BuildingData
    {
        public BuildingNames buildingName;
        public BuildingState buildingState;


        [Header("Building Exterior")]
        public Sprite buildExtDayN;
        public Sprite buildExtDayHL; 

        public Sprite buildExtNight;
        public Sprite buildExtNightHL; 

        [Header("Building Interior")]
        public Sprite buildIntDay;
        public Sprite buildIntNight; 

        public List<CharNames> charNames = new List<CharNames>();
        public List<NPCDataInBuild> npcData = new List<NPCDataInBuild>();

        public List<BuildIntTypeData> buildIntType = new List<BuildIntTypeData>();

        public string statusLockedStr="";
        public string statusUnAvailStr = "";
    }

    [System.Serializable]
    public class InteractionSpriteData
    {
        public BuildIntType intType;
        public string intTypeStr =""; 
        public Sprite spriteN;
        public Sprite spriteHL;

    }

    //[System.Serializable]
    //public class BuildingModel
    //{
    //    public List<BuildingData> allBuildingData = new List<BuildingData>();
    //    public List<InteractionSpriteData> allIntSprites = new List<InteractionSpriteData>();
    //}

}