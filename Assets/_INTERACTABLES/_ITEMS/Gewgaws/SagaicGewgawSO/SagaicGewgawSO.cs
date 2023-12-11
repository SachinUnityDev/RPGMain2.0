using Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Interactables 
{ 
    [CreateAssetMenu(fileName = "SagaicGewgawSO", menuName = "Item Service/SagaicGewgawSO")]
    public class SagaicGewgawSO : ScriptableObject
    {
        [Header("Fundamentals")]         
        public SagaicGewgawNames sagaicGewgawName;
        [Header("Desc")]
        public string desc = "";
        public GewgawSlotType gewgawSlotType;

        [Header("RESTRICTIONS")]  // ONLY THE BELOW GIVEN CLASS CULTURE AND RACE TYPE CAN HAVE IT 
        // ONLY ONE RESTRICTION CAN BE APPLIED AS IN EITHER CLASS OR CULTURE OR RACE
        public List<ClassType> classRestrictions = new List<ClassType>();
        public List<CultureType> cultureRestrictions = new List<CultureType>();
        public List<RaceType> raceRestrictions = new List<RaceType>();
        public int lvlRestriction=4;
        [Header("COST")]
        public Currency cost;
        [HideInInspector]
        public float fluctuation =20f;  // same for all the sagaic gewgaws

        [HideInInspector]   // WILL BE FILLED AS PER ALGO 
        public int maxWorldInstance; // sagaic its 1, epic 3 , folklyric =6 , lyric =6 , poetic = 3, 
        public int maxInvStackSize; 
        //[Header("Desc")]
        //public string desc = "";

        [Header("Sprites")]
        public Sprite iconSprite;

        [TextArea(5, 10)]       
        // to be filled using code // factory fo
        public List<string> allLines = new List<string>();
        private void Awake()
        {
            maxWorldInstance = 1; 
            maxInvStackSize = 1;
        }
        [SerializeField] string resLs = "";
        public string GetRestrictionsType()
        {
            string resStr = "";
            if (classRestrictions.Count > 0)
            {
                resStr = "Class Res";
            }
            if (cultureRestrictions.Count > 0)
            {
                resStr = "Culture Res";
            }
            if (raceRestrictions.Count > 0)
            {
                resStr = "Race Res";
            }
            return resStr;
        }

        public string GetRestrictionLs()
        {
            string resStrLS = "";
            string finalStr = "";
            if (classRestrictions.Count > 0)
            {
                foreach (var cl in classRestrictions)
                {
                    resStrLS = resStrLS + cl.ToString() + ", ";
                }
            }
            if (cultureRestrictions.Count > 0)
            {
                foreach (var cult in cultureRestrictions)
                {
                    resStrLS = resStrLS + cult.ToString() + ", ";
                }
            }
            if (raceRestrictions.Count > 0)
            {
                foreach (var race in raceRestrictions)
                {
                    resStrLS = resStrLS + race.ToString() + ", ";
                }
            }
            if (resStrLS.Length > 2)
                finalStr = resStrLS.Substring(0, resStrLS.Length - 2);
            return finalStr;
        }
        public bool ChkEquipRestriction(CharController charController) // if true then only items can be equip
        {
            CharModel charModel = charController.charModel;
            if (!(lvlRestriction <= charModel.charLvl) )
                return false;
            if (classRestrictions.Count > 0)
            {
                ClassType classType = charModel.classType;
                return classRestrictions.Any(t => t == classType);
            }
            if (cultureRestrictions.Count > 0)
            {
                CultureType cultType = charModel.cultType;
                return cultureRestrictions.Any(t => t == cultType);
            }
            if (raceRestrictions.Count > 0)
            {
                RaceType raceType = charModel.raceType;
                return raceRestrictions.Any(t => t == raceType);
            }
            return false;
        }
    }
}
