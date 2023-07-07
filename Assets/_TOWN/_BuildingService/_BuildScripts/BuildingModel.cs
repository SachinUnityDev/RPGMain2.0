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
        UnAvaiable,
    }
    [Serializable]
    public class DialogueData
    {
        public DialogueNames dialogueName;
        public bool isUnLocked; 
    }
    [Serializable]
    public class CharInteractData
    {
        public CharNames compName;
        public NPCInteractType nPCIntType;
        public NPCState nPCState;
        public List<DialogueData> allDialogueData = new List<DialogueData>();
    }
    [Serializable]
    public class CharInteractPrefabData
    {
        public CharNames compName;
        public NPCInteractType nPCIntType;        
        public GameObject interactPrefab;
    }

    [Serializable]
    public class NPCInteractData
    {
        public NPCNames nPCNames;
        public NPCInteractType nPCIntType;
        public NPCState npcState;
        public List<DialogueData> allDialogueData = new List<DialogueData>();        
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
    public class InteractionSpriteData
    {
        public BuildInteractType intType;
        public string intTypeStr =""; 
        public Sprite spriteN;
    }

    [System.Serializable]
    public class BuildingModel
    {
        [Header("Building Name")]
        public BuildingNames buildingName;
        public BuildingState buildState;

        [Header("CharInteract")]
        public List<CharInteractData> charInteractData = new List<CharInteractData>();
        [Header("NPC Interactions")]
        public List<NPCInteractData> npcInteractData = new List<NPCInteractData>();
        [Header("Building Interactions")]
        public List<BuildIntTypeData> buildIntTypes = new List<BuildIntTypeData>();

    }

}