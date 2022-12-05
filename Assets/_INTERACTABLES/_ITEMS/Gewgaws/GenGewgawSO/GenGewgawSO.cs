using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    
    [CreateAssetMenu(fileName = "GenGewgawSO", menuName = "Interactable/GenGewgawSO")]
    public class GenGewgawSO : ScriptableObject
    {
        public GenGewgawNames genGewgawName;
        public PrefixNames prefixName;
        public GenGewgawMidNames gewgawName;
        public SuffixNames suffixName;

        public GewgawSlotType gewgawSlotType;
       // public int maxWorldInstance;.. cannot be defined here Dep 
        public int maxInvStackSize; 

        [Header("RESTRICTIONS")]  // ONLY THE BELOW GIVEN CLASS CULTURE AND RACE TYPE CAN HAVE IT 
        // ONLY ONE RESTRICTION CAN BE APPLIED AS IN EITHER CLASS OR CULTURE OR RACE
        public List<ClassType> classRestrictions = new List<ClassType>();
        public List<CultureType> cultureRestrictions = new List<CultureType>();
        public List<RaceType> raceRestrictions = new List<RaceType>();

        [Header("Cost")]
        public Currency cost;

        [Header("Fluctuation rate")]
        public float fluctuationRate = 20f; 
        [Header("Desc")]
        public string desc = "";

        [Header("Sprites")]
        public Sprite iconSprite;


        private void Awake()
        {
            maxInvStackSize = 1;
          
        }


    }
}

