using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    [System.Serializable]
    public class BuildingData
    {
        public BuildingNames buildingName;
        public BuildingState buildingState;
        public List<CharNames> charNames = new List<CharNames>();
        public List<NPCNames> npcNames = new List<NPCNames>();

        public List<IntType> interactionType = new List<IntType>();
    }

    [System.Serializable]
    public class InteractionSpriteData
    {
        public IntType intType;
        public string intTypeStr =""; 
        public Sprite spriteN;
        public Sprite spriteHL;

    }

    [System.Serializable]
    public class BuildingModel
    {
        public List<BuildingData> allBuildingData = new List<BuildingData>();
        public List<InteractionSpriteData> allIntSprites = new List<InteractionSpriteData>();
    }

}