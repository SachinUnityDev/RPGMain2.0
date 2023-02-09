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
    }
    [Serializable]
    public class CharInteractPrefabData
    {
        public CharNames compName;
        public NPCInteractType nPCIntType;        
        public GameObject interactPrefab;
    }

    [Serializable]
    public class NPCDataInBuild
    {
        public NPCNames nPCNames;
        public NPCInteractType nPCIntType;
        public NPCState npcState; 
    }
    [Serializable]
    public class NPCInteractPrefabData
    {
        public NPCNames nPCNames;
        public NPCInteractType nPCIntType;        
        public GameObject interactPrefab;
    }
    [Serializable]
    public class BuildIntTypeData
    {
        public BuildInteractType BuildIntType;        
        public bool isUnLocked = false; 
    }
    [Serializable]
    public class BuildIntTypePrefabData
    {
        public BuildInteractType BuildIntType;
        public GameObject interactPrefab;
        
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

        [Header("CharInteract")]
        public List<CharInteractData> charInteractData = new List<CharInteractData>();
        public List<CharInteractPrefabData> CharInteractPrefab = new List<CharInteractPrefabData>();

        [Header("NPC Interactions")]        
        public List<NPCDataInBuild> npcData = new List<NPCDataInBuild>();
        public List<NPCInteractPrefabData> npcDataPrefab = new List<NPCInteractPrefabData>();
        

        [Header("Building Interactions")]
        public List<BuildIntTypeData> buildIntTypes = new List<BuildIntTypeData>();
        public List<BuildIntTypePrefabData> buildIntPrefab = new List<BuildIntTypePrefabData>();    

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
    }

    //[System.Serializable]
    //public class BuildingModel
    //{
    //    public List<BuildingData> allBuildingData = new List<BuildingData>();
    //    public List<InteractionSpriteData> allIntSprites = new List<InteractionSpriteData>();
    //}

}