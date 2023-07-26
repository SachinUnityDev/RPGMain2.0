using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ink.Runtime;
using Quest;

namespace Town
{
    [CreateAssetMenu(fileName = "BuildingSO", menuName = "Town Service/BuildingSO")]
    public class BuildingSO : ScriptableObject
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
        public List<CharIntData> charInteractData = new List<CharIntData>();
        public List<CharInteractPrefabData> CharInteractPrefab = new List<CharInteractPrefabData>();

        [Header("NPC Interactions")]
        public List<NPCIntData> npcInteractData = new List<NPCIntData>();
        public List<NPCInteractPrefabData> npcDataPrefab = new List<NPCInteractPrefabData>();


        [Header("Building Interactions")]
        public List<BuildIntTypeData> buildIntTypes = new List<BuildIntTypeData>();
        public List<BuildIntTypePrefabData> buildIntPrefab = new List<BuildIntTypePrefabData>();

        [Header("Ship Tipping Txt")]
        public List<string> tipStrs = new List<string>();

    }
    [Serializable]
    public class BuildBarkData
    {
        [TextArea(2,5)]
        public string str="";
        public AudioClip audioClip; 
    }
}

