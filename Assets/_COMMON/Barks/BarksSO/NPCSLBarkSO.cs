using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum NPCInteractType
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
        public NPCInteractType interactType;
        [TextArea(3, 10)]
        public List<string> barkLines = new List<string>();
        public List<AudioClip> audioClips = new List<AudioClip>();
    }
  
    [CreateAssetMenu(fileName = "NPCSLBarkSO", menuName = "Common/BarkService/NPCSLBarkSO")]
    public class NPCSLBarkSO : ScriptableObject
    {
        public NPCNames nPCName; 
        public List<NPCSLBarkData> NPCBarkData = new List<NPCSLBarkData>(); 
    }
}