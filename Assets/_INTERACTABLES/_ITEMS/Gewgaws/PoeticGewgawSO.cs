using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
 

    [CreateAssetMenu(fileName = "PoeticGewgawSO", menuName = "Item Service/PoeticGewgawSO")]
    public class PoeticGewgawSO : ScriptableObject
    {
        // unique feature 
        public PoeticGewgawNames poeticGewgawName;
        public PoeticSetName poeticSetName; 
        public GewgawMidNames gewgawMidName;
        
        public GewgawSlotType gewgawSlotType;
       
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
        
        [Header("Sprites")]
        public Sprite iconSprite;

        [Header("VerseDesc")]
        public int setNumber; 
        public string verseDesc="";
     
        private void Awake()
        {
            maxInvStackSize = 1;
        }
    }
}
