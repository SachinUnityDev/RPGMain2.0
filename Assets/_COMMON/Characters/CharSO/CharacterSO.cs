using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using System.Security.Policy;

namespace Common
{
    [CreateAssetMenu(fileName = "MainCharacter", menuName = "Character Service/CharacterSO")]
    public class CharacterSO : ScriptableObject
    {

        /// <summary>
        ///  to adjust for abbas Class type make all portraits on get funcs 
        ///  Get func will take care of "spl case Abbas the portraits and animation files" 
        /// </summary>
        public int charID;
        public CharNames charName;
        public GameObject charPrefab;  // change to prefab 
        public Sprite charSprite;
        public Sprite charSpriteUnClickedFlipped;
        public Sprite charHexPortrait;
        public Sprite dialoguePortraitClicked;
        public Sprite dialoguePortraitUnClicked;
        public Sprite bpPortraitUnLocked;
        public Sprite bpPortraitUnAvail;

        public CharMode charMode;
        public CharMode orgCharMode;

        [Header("Experience points")]
        public int mainExp;
        public int jobExp;
        public int skillPts;

        [Header("LEVELS")]
        public int charLvl;

        public string charNameStr = "";

        [Header("State")]
        public AvailOfChar availOfChar;
        public StateOfChar stateOfChar;

        [Header("Fame Behavior")]
        public FameBehavior fameBehavior;

        [Header("Location")]
        public LocationName baseCharLoc;

        [Header("THE TYPES")]
        public CharRole charRole;
        public RaceType raceType;
        public RaceTypeHero raceTypeHero; 
        public CultureType cultType;     // play part in trails only 
        public ClassType classType;
        public ArcheType archeType;
        [Header("MISC STATS")]
        public FleeBehaviour fleeBehaviour;
        //public CharFleeState charFleeState;

        [Header("DEFAULT PROVISION")]
        public List<ItemDataWithQtyNFameType> provisionItems = new List<ItemDataWithQtyNFameType>();

        [Header("Earnings In Quest")]
        // money and Item 
        public List<ItemDataWithQtyNFameType> earningsItems = new List<ItemDataWithQtyNFameType>();
        public int earningShare; 

        [Header("Companion PreReq")]
        public List<ItemDataWithQtyNFameType> CompanionPreReqOpt1 = new List<ItemDataWithQtyNFameType>(); 
        public List<ItemDataWithQtyNFameType> CompanionPreReqOpt2 = new List<ItemDataWithQtyNFameType>();
        
        [Header("Char Start Lvl")]
        public int startLevel;

        [Header("Perma Trait List")]
        public List<PermaTraitName> permaTraitNames = new List<PermaTraitName>();    

        public List<AttribData> AttribList = new List<AttribData>();
        public List<StatData> statList = new List<StatData>(); 
        [Header("Grid related")]
        public CharOccupies charOccupies;
        public List<int> posPriority = new List<int>();

     
        [Header("Weapon and Armor")]
        public ArmorType armorType;         // three gems with conditions can be socketed on armor 
        public string armorTypeStr; 
        public GemNames enchantableGem4Weapon;  // only one gem req for enchantmnet with weapon 
        public Sprite armorSprite;              // few char have no weapon skill 

        private void Awake()
        {
            if (AttribList.Count < 1)   // patch fix to prevent recreation of fields 
            {
                AttribList.Clear();
                for (int i = 1; i < Enum.GetNames(typeof(AttribName)).Length; i++)
                {
                AttribData s = new AttribData();
                s.AttribName = (AttribName)i;
                Debug.Log("SO awake  function called");
                switch (s.AttribName)
                {
                    case AttribName.None:
                        break;        
                    case AttribName.fortOrg:
                        s.minLimit = -24f;
                        s.maxLimit = 24f; break;                        
                   
                    case AttribName.acc:
                        s.minLimit = 0f;
                        s.maxLimit = 15; break;
                    case AttribName.focus:
                        s.minLimit = 0f;
                        s.maxLimit = 12f; break;
                    case AttribName.luck:
                        s.minLimit = 0f;
                        s.maxLimit = 12f; break;
                    case AttribName.morale:
                        s.minLimit = 0f;
                        s.maxLimit = 12f; break;
                    case AttribName.haste:
                        s.minLimit = 0f;
                        s.maxLimit = 12f; break;
                    case AttribName.vigor:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case AttribName.willpower:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;        
                    case AttribName.dodge:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case AttribName.fireRes:
                        s.minLimit = -60f;
                        s.maxLimit = 80f; break;
                    case AttribName.earthRes:
                        s.minLimit = -60f;
                        s.maxLimit = 80f; break;
                    case AttribName.waterRes:
                        s.minLimit = -60f;
                        s.maxLimit = 80f; break;
                    case AttribName.airRes:
                        s.minLimit = -60f;
                        s.maxLimit = 80f; break;
                    case AttribName.lightRes:
                        s.minLimit = -30f;
                        s.maxLimit = 60f; break;
                    case AttribName.darkRes:
                        s.minLimit = -30f;
                        s.maxLimit = 60f; break;
                    case AttribName.hpRegen:
                        s.minLimit = 0f;
                        s.maxLimit = 10f; break;
                    case AttribName.staminaRegen:
                        s.minLimit = 0f;
                        s.maxLimit = 10f; break;
                    case AttribName.armorMin:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case AttribName.armorMax:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case AttribName.dmgMin:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case AttribName.dmgMax:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;

                    default:
                        break;
                }
                AttribList.Add(s);
                }
                statList.Clear();
                for (int i = 1; i < Enum.GetNames(typeof(StatName)).Length; i++)
                {
                    StatData s = new StatData();
                    s.statName = (StatName)i;
                    Debug.Log("SO awake  function called" + charName);
                    switch (s.statName)
                    {
                        case StatName.None:
                            break;
                        //case StatName.health:
                        //    s.minLimit = 0;
                        //    s.maxLimit = 200f; break;
                        
                        //case StatName.stamina:
                        //    s.minLimit = 0;
                        //    s.maxLimit = 200f; break;
                        
                        case StatName.fortitude:
                            s.minLimit =-30;
                            s.maxLimit = 30f; break;
                        
                        case StatName.hunger:
                            s.minLimit = 0;
                            s.maxLimit = 100f; break;
                        
                        case StatName.thirst:
                            s.minLimit = 0;
                            s.maxLimit = 100f; break;                        
                    }
                    statList.Add(s);
                }
             }
            else
            {
                Debug.Log("Updated vigor n willpower");
                float healthMax = AttribList.Find(t => t.AttribName == AttribName.vigor).currValue * 4;
                statList.Find(t => t.statName == StatName.health).currValue = (int)healthMax;
                statList.Find(t => t.statName == StatName.health).maxLimit = healthMax; 

                float staminaMax = AttribList.Find(t => t.AttribName == AttribName.willpower).currValue * 3;
                statList.Find(t => t.statName == StatName.stamina).currValue = (int)staminaMax;
                statList.Find(t => t.statName == StatName.stamina).maxLimit  = staminaMax;
            }

            FillDesc();

        }

        #region CORE STATS RELATED
        void FillDesc()
        {
            string str = "";
            for (int i = 0; i < statList.Count; i++)
            {

                switch (statList[i].statName)
                {
                    case StatName.health:
                        str = "Health is health, you know it...";
                        break;
                    case StatName.stamina:
                        str = "Resource to use skills.";
                        break;
                    case StatName.fortitude:
                        str = "Be among those who dare to dare!";
                        break;
                    case StatName.hunger:
                        str = "Start losing Health once Hunger is 100%.";
                        break;
                    case StatName.thirst:
                        str = "Start losing Stamina once Thirst is 100%.";
                        break;
                }
                statList[i].desc = str;
            }
            for (int i = 0; i < AttribList.Count; i++)
            {                
                switch (AttribList[i].AttribName)
                {                
                    case AttribName.dmgMin:
                       str = 
                            "Base value for Physical and Magical attacks."; 
                        break;
                    case AttribName.acc:
                       str =
                            "Chance to land Physical attack to a target."; 
                        break;
                    case AttribName.focus:
                        str = "Determinant value for hitting correct target by Magical attacks or Misfire."; 
                        break;
                    case AttribName.luck:
                        str=
                        "Determinant value for Critical and Feeble hits."; 
                        break;
                    case AttribName.morale:
                        str= "Determinant value for extra action or pass action.";
                        break;
                    case AttribName.haste:
                        str = 
                        "Main determinant for turn order. Increases chance to keep action after using Move."; 
                        break;
                    case AttribName.vigor:
                        str =
                            "Determinant value for base Health. Increases chance to withstand Hunger.";
                        break;
                    case AttribName.willpower:
                        str =
                        "Determinant value for base Stamina. Increases chance to withstand Thirst."; 
                        break;
                    case AttribName.armorMin:
                        str =
                            "Value to mitigate incoming Physical damage."; 
                        break;
                    case AttribName.dodge:
                        str =
                            "Chance to evade a Physical attack."; 
                        break;
                    case AttribName.fireRes:
                        str =
                            "Percentage value to mitigate incoming Fire damage.";
                        break;
                    case AttribName.earthRes:
                        str =
                            "Percentage value to mitigate incoming Earth damage.";
                        break;
                    case AttribName.waterRes:
                        str = "Percentage value to mitigate incoming Water damage.";
                        break;
                    case AttribName.airRes:
                        str =
                            "Percentage value to mitigate incoming Air damage.";
                        break;
                    case AttribName.lightRes:
                        str =
                        "Percentage value to mitigate incoming Light damage.";
                        break;
                    case AttribName.darkRes:
                        str = "Percentage value to mitigate incoming Dark damage.";

                        break;
                   
                    default:
                        break;
                }
                AttribList[i].desc = str; 
            }




        }
        #endregion
    }
    [Serializable]
    public class ItemDataWithQtyNFameType
    {
        public ItemDataWithQty itemDataQty;
        public FameType fameType; 


    }

}


