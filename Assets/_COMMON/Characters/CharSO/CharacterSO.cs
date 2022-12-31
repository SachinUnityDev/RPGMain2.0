using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;

namespace Common
{
    [CreateAssetMenu(fileName = "MainCharacter", menuName = "Character Service/CharacterSO")]
    public class CharacterSO : ScriptableObject
    {
        public int charID;
        public CharNames charName;
        public GameObject charPrefab;  // change to prefab 
        public Sprite charSprite;
        public Sprite charHexPortrait;
        public Sprite dialoguePortraitClicked;
        public Sprite dialoguePortraitUnClicked;
        public Sprite bpPortraitUnLocked;
        public Sprite bpPortraitUnAvail; 
       


        public CharMode charMode;
        public CharMode orgCharMode;

        [Header("Experience points")]
        public int expPoints;
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
        public RaceType raceType;
        public RaceTypeHero raceTypeHero; 
        public CultureType cultType;     // play part in trails only 
        public ClassType classType;
        public ArcheType archeType;
        [Header("MISC STATS")]
        public float tameAnimalsStrength;
        public FleeBehaviour fleeBehaviour;
        public bool canBeAmbushed;
        public bool canBeCaught;

        [Header("DEFAULT PROVISION")]
        public List<ItemData> provisionItems = new List<ItemData>();

        [Header("Earnings In Quest")]
        // money and Item 
        public List<ItemData> earningsItems = new List<ItemData>();
        public float earningShare; 

        [Header("Companion PreReq")]
        public List<ItemData> CompanionPreReqOpt1 = new List<ItemData>();
        public List<ItemData> CompanionPreReqOpt2 = new List<ItemData>();

        public int startLevel;
        public int staminaRegen = 2;

        public List<StatData> StatsList = new List<StatData>();

        [Header("Grid related")]
        public CharOccupies charOccupies;
        public List<int> posPriority = new List<int>();

     
        [Header("Weapon and Armor")]
        public ArmorType armorType;         // three gems with conditions can be socketed on armor 
        public string armorTypeStr; 
        public GemNames enchantableGem4Weapon;  // only one gem req for enchantmnet with weapon 
                                            // few char have no weapon skill 

        private void Awake()
        {

            if (StatsList.Count < 1)   // patch fix to prevent recreation of fields 
            {
                StatsList.Clear();
            for (int i = 1; i < Enum.GetNames(typeof(StatsName)).Length; i++)
            {
                StatData s = new StatData();
                s.statsName = (StatsName)i;
                Debug.Log("SO awake  function called");
                switch (s.statsName)
                {
                    case StatsName.None:
                        break;
                    case StatsName.health:
                        s.minLimit = 0f;
                        s.maxLimit = 0;
                        break;
                    case StatsName.stamina:
                        s.minLimit = 0f;
                        s.maxLimit = 0f;
                        break;
                    case StatsName.fortOrg:
                        s.minLimit = -24f;
                        s.maxLimit = 24f; break;
                    case StatsName.fortitude:
                        s.minLimit = -30f;
                        s.maxLimit = 30f; break;
                    case StatsName.hunger:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case StatsName.thirst:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case StatsName.damage:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case StatsName.acc:
                        s.minLimit = 0f;
                        s.maxLimit = 15; break;
                    case StatsName.focus:
                        s.minLimit = 12f;
                        s.maxLimit = 96f; break;
                    case StatsName.luck:
                        s.minLimit = 0f;
                        s.maxLimit = 12f; break;
                    case StatsName.morale:
                        s.minLimit = 0f;
                        s.maxLimit = 12f; break;
                    case StatsName.haste:
                        s.minLimit = 0f;
                        s.maxLimit = 12f; break;
                    case StatsName.vigor:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case StatsName.willpower:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case StatsName.armor:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case StatsName.dodge:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case StatsName.fireRes:
                        s.minLimit = -60f;
                        s.maxLimit = 90f; break;
                    case StatsName.earthRes:
                        s.minLimit = -60f;
                        s.maxLimit = 90f; break;
                    case StatsName.waterRes:
                        s.minLimit = -60f;
                        s.maxLimit = 90f; break;
                    case StatsName.airRes:
                        s.minLimit = -60f;
                        s.maxLimit = 90f; break;
                    case StatsName.lightRes:
                        s.minLimit = -30f;
                        s.maxLimit = 60f; break;
                    case StatsName.darkRes:
                        s.minLimit = -30f;
                        s.maxLimit = 60f; break;
                    default:
                        break;
                }
                StatsList.Add(s);
            }
        }
        else
        {
                //Debug.Log("Updated vigor n willpower");
                float healthMax = StatsList.Find(t => t.statsName == StatsName.vigor).currValue * 4;
                StatsList.Find(t => t.statsName == StatsName.health).currValue = healthMax;
                StatsList.Find(t => t.statsName == StatsName.health).maxLimit = healthMax; 

                float staminaMax = StatsList.Find(t => t.statsName == StatsName.willpower).currValue * 3;
                StatsList.Find(t => t.statsName == StatsName.stamina).currValue = staminaMax;
                StatsList.Find(t => t.statsName == StatsName.stamina).maxLimit  = staminaMax;
                FillDesc();
        }

    }

        #region CORE STATS RELATED
        void FillDesc()
        {
            string str = ""; 
            for (int i = 0; i < StatsList.Count; i++)
            {
                
                switch (StatsList[i].statsName)
                {
                    case StatsName.None:
                        str = ""; 
                        break;
                    case StatsName.health:
                        str= "Health is health, you know it..."; 
                        break;
                    case StatsName.stamina:
                        str = "Resource to use skills."; 
                        break;
                    case StatsName.fortitude:
                        str = "Be among those who dare to dare!"; 
                        break;
                    case StatsName.hunger:
                        str = "Start losing Health once Hunger is 100%."; 
                        break;
                    case StatsName.thirst:
                        str = "Start losing Stamina once Thirst is 100%.";
                        break;
                    case StatsName.damage:
                       str = 
                            "Base value for Physical and Magical attacks."; 
                        break;
                    case StatsName.acc:
                       str =
                            "Chance to land Physical attack to a target."; 
                        break;
                    case StatsName.focus:
                        str = "Determinant value for hitting correct target by Magical attacks or Misfire."; 
                        break;
                    case StatsName.luck:
                        str=
                        "Determinant value for Critical and Feeble hits."; 
                        break;
                    case StatsName.morale:
                        str= "Determinant value for extra action or pass action.";
                        break;
                    case StatsName.haste:
                        str = 
                        "Main determinant for turn order. Increases chance to keep action after using Move."; 
                        break;
                    case StatsName.vigor:
                        str =
                            "Determinant value for base Health. Increases chance to withstand Hunger.";
                        break;
                    case StatsName.willpower:
                        str =
                        "Determinant value for base Stamina. Increases chance to withstand Thirst."; 
                        break;
                    case StatsName.armor:
                        str =
                            "Value to mitigate incoming Physical damage."; 
                        break;
                    case StatsName.dodge:
                        str =
                            "Chance to evade a Physical attack."; 
                        break;
                    case StatsName.fireRes:
                        str =
                            "Percentage value to mitigate incoming Fire damage.";
                        break;
                    case StatsName.earthRes:
                        str =
                            "Percentage value to mitigate incoming Earth damage.";
                        break;
                    case StatsName.waterRes:
                        str = "Percentage value to mitigate incoming Water damage.";
                        break;
                    case StatsName.airRes:
                        str =
                            "Percentage value to mitigate incoming Air damage.";
                        break;
                    case StatsName.lightRes:
                        str =
                        "Percentage value to mitigate incoming Light damage.";
                        break;
                    case StatsName.darkRes:
                        str = "Percentage value to mitigate incoming Dark damage.";

                        break;
                   
                    default:
                        break;
                }
                StatsList[i].desc = str; 
            }




        }
        #endregion
    }






}


