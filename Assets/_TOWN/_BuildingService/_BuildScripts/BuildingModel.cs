using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;

namespace Town
{
    [System.Serializable]
    public enum BuildingState
    {
        Locked,    // when you click that building it gives a notification box...locked bark        
        Available,  // enter 
        UnAvailable, // unavailable bark
        None,
    }
    [Serializable]
    public enum NPCState
    {
        Locked,
        UnLockedNAvail,
        UnAvailable,
    }
    [Serializable]
    public class DialogueData
    {
        public DialogueNames dialogueName;
        public bool isUnLocked;
    }
    [Serializable]
    public class CharIntData
    {
        public CharNames compName;
        public NPCState compState;
        public List<IntTypeData> allInteract = new List<IntTypeData>();
    }
    [Serializable]
    public class CharInteractPrefabData
    {
        public CharNames compName;
        public IntType nPCIntType;
        public GameObject interactPrefab;
    }

    [Serializable]
    public class NPCIntData
    {
        public NPCNames nPCNames;
        public NPCState npcState;
        public List<IntTypeData> allInteract = new List<IntTypeData>();

    }
    [Serializable]
    public class IntTypeData
    {
        public IntType nPCIntType;
        public List<DialogueData> allDialogueData = new List<DialogueData>();
    }

    [Serializable]
    public class NPCInteractPrefabData
    {
        public NPCNames nPCNames;
        public IntType nPCIntType;
        public GameObject interactPrefab;
    }
    [Serializable]
    public class BuildIntTypeData
    {
        public BuildInteractType BuildIntType;
        public bool isUnLocked = false;
        public bool isUpgraded = false;
    }
    [Serializable]
    public class BuildIntTypePrefabData
    {
        public BuildInteractType BuildIntType;
        public GameObject interactPrefab;
    }
    [System.Serializable]
    public class InteractionSpriteData
    {
        public BuildInteractType intType;
        public string intTypeStr = "";
        public Sprite spriteN;
    }

    [System.Serializable]
    public class BuildingModel
    {
        [Header("Building Name")]
        public BuildingNames buildingName;
        public BuildingState buildState;

        [Header("CharInteract")]
        public List<CharIntData> charInteractData = new List<CharIntData>();
        [Header("NPC Interactions")]
        public List<NPCIntData> npcInteractData = new List<NPCIntData>();
        [Header("Building Interactions")]
        public List<BuildIntTypeData> buildIntTypes = new List<BuildIntTypeData>();


        public void UnLockBuildIntType(BuildInteractType buildIntType)
        {
            foreach (BuildIntTypeData buildInt in buildIntTypes)
            {
                if (buildInt.BuildIntType == buildIntType)
                {
                    buildInt.isUnLocked = true; break; 
                }
            }
        }
       


        public bool IsBuildIntUnLocked(BuildInteractType buildIntType)
        {
            foreach (BuildIntTypeData buildInt in buildIntTypes)
            { 
                if(buildInt.BuildIntType == buildIntType)
                {
                    return buildInt.isUnLocked;
                }
            }
            return false; 
        }

        public bool IsBuildIntUpgraded(BuildInteractType buildIntType)
        {
            int index = buildIntTypes.FindIndex(t=>t.BuildIntType == buildIntType);
            if(index != -1)
            {
                return buildIntTypes[index].isUpgraded; 
            }
            return false; 
        }
        public void BuildIntChg(BuildInteractType buildIntType, bool isUpgrade)
        {
            int index = buildIntTypes.FindIndex(t => t.BuildIntType == buildIntType);
            if (index != -1)
            {
                buildIntTypes[index].isUpgraded = isUpgrade;
            }
        }

    }

}