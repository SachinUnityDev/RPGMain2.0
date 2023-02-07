using System;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

[Serializable]
public enum NPCTalkType
{
    Welcome,
    Leave,
    Interact,
}


namespace Common
{
    [Serializable]
    public class NPCSLBarkData
    {
        public FameType FameType;
        public NPCTalkType interactType;
        public List<BarkLineData> barkLineData = new List<BarkLineData>();  
    }

    [Serializable]
    public class BuildInteractBarkData
    {       
        public BuildInteractType interactType;  
        public BarkLineData barkLine;
    }
    [Serializable]
    public class NPCInteractBarkData
    {
        public TempTraitName tempTraitName;
        public NPCInteractType interactType;
        public BarkLineData barkLine;
    }
  
    [CreateAssetMenu(fileName = "NPCSLBarkSO", menuName = "Common/BarkService/NPCSLBarkSO")]
    public class NPCSLBarkSO : ScriptableObject
    {
        public NPCNames nPCName; 
        public List<NPCSLBarkData> NPCBarkData = new List<NPCSLBarkData>();
        public List<NPCInteractBarkData> allNPCInteractBarkData = new List<NPCInteractBarkData>();
        public List<BuildInteractBarkData> allBuildInteractBarkData = new List<BuildInteractBarkData>();
    }
}