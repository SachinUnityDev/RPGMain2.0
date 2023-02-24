using Interactables;
using System.Collections;
using System.Collections.Generic;
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

        private void Awake()
        {
            if(traitNameStr == "")
            {
                traitNameStr = tempTraitName.ToString().CreateSpace();
            }

        }
    }
}