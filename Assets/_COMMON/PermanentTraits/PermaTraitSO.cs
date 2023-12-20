using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    [CreateAssetMenu(fileName = "PermTraitSO", menuName = "Common/PermTraitSO")]

    public class PermaTraitSO : ScriptableObject
    {
        public PermaTraitName permaTraitName;
        public TempTraitBehaviour traitBehaviour;
        [Header("Class type")]
        public ClassType classType;
        [Header("culture ")]
        public CultureType cultureType;
        [Header("race ")]
        public RaceType raceType;  
        [Header("Description")]
        public string traitNameStr = "";

        [TextArea(5, 10)]
        public List<string> allLines = new List<string>();

        private void Awake()
        {
            if (traitNameStr == "")
            {
                traitNameStr = permaTraitName.ToString().CreateSpace();
            }
        }
    }

}