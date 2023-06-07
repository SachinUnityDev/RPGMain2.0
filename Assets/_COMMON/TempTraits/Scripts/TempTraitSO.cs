using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(fileName = "TempTraitSO", menuName = "Common/TempTraitSO")]

    public class TempTraitSO : ScriptableObject
    {
        public TempTraitName tempTraitName;
        public TempTraitType tempTraitType;
        public TraitBehaviour temptraitBehavior;

        public int minCastTime;
        public int maxCastTime;

        [Header("Description")]
        public string traitNameStr = "";

        [Header("Sprites")]
        public Sprite iconSprite;
        
        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();

        [Header("Sickness data")]
        public SicknessData sicknessData= new SicknessData();

        private void Awake()
        {
            if(traitNameStr == "")
            {
                traitNameStr = tempTraitName.ToString().CreateSpace();
            }
        }
    }
    
    [Serializable]
    public class SicknessData
    {
        public HerbNQuantity herb1;
        public HerbNQuantity herb2;
        public int restTimeInday;  
    }

    [Serializable]
    public class HerbNQuantity
    {
        public HerbNames herbName;
        public int qty; 
    }

}