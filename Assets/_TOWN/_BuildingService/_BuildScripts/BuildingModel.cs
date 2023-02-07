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
        Available,
        UnAvailable,
    }
    [Serializable]
    public enum NPCState
    {
        Locked,
        UnLocked,
        Available,
        UnAvaiable,
    }

    [Serializable]
    public class CharInteractData
    {
        public CharNames compName;
        public NPCInteractType nPCIntType;
        public NPCState nPCState;
        public GameObject interactPrefab;

    }

    [Serializable]
    public class NPCDataInBuild
    {
        public NPCNames nPCNames;
        public NPCInteractType nPCIntType;
        public NPCState npcState; 
        public GameObject interactPrefab;

    }
    [Serializable]
    public class BuildIntTypeData
    {
        public BuildInteractType BuildIntType;
        public GameObject interactPrefab;
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

        public List<CharInteractData> charNames = new List<CharInteractData>();
        [Header("NPC Interactions")]
        public List<NPCDataInBuild> npcData = new List<NPCDataInBuild>();
        // click on NPC portrait or sprite

        [Header("Building Interactions")]
        public List<BuildIntTypeData> buildIntType = new List<BuildIntTypeData>();
        // buttons at the bottom panel

        [Header("UnLocked and Unavailable")]
        [TextArea (4,10)]
        public List<string> statusLockedStr= new List<string>();
        [TextArea(4, 10)]
        public List<string> statusUnAvailStr = new List<string>(); 
    }

    [System.Serializable]
    public class InteractionSpriteData
    {
        public BuildInteractType intType;
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