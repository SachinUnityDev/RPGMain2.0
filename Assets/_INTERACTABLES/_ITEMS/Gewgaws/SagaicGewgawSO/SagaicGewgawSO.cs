using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables 
{ 
    [CreateAssetMenu(fileName = "SagaicGewgawSO", menuName = "Item Service/SagaicGewgawSO")]
    public class SagaicGewgawSO : ScriptableObject
    {
        [Header("Fundamentals")]         
        public SagaicGewgawNames sagaicGewgawName;            
        public GewgawSlotType gewgawSlotType;

        [Header("RESTRICTIONS")]  // ONLY THE BELOW GIVEN CLASS CULTURE AND RACE TYPE CAN HAVE IT 
        // ONLY ONE RESTRICTION CAN BE APPLIED AS IN EITHER CLASS OR CULTURE OR RACE
        public List<ClassType> classRestrictions = new List<ClassType>();
        public List<CultureType> cultureRestrictions = new List<CultureType>();
        public List<RaceType> raceRestrictions = new List<RaceType>();

        [Header("COST")]
        public Currency cost;
        [HideInInspector]
        public float fluctuation =20f;  // same for all the sagaic gewgaws

        [HideInInspector]   // WILL BE FILLED AS PER ALGO 
        public int maxWorldInstance =1; // sagaic its 1, epic 3 , folklyric =6 , lyric =6 , poetic = 3, 

        //[Header("Desc")]
        //public string desc = "";

        [Header("Sprites")]
        public Sprite iconSprite;

        [TextArea(5, 10)]       
        // to be filled using code // factory fo
        public List<string> allLines = new List<string>();
    }
}
