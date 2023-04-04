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
        public CharRole charRole;
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

        public List<AttribData> StatsList = new List<AttribData>();

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

            if (StatsList.Count < 1)   // patch fix to prevent recreation of fields 
            {
                StatsList.Clear();
            for (int i = 1; i < Enum.GetNames(typeof(AttribName)).Length; i++)
            {
                AttribData s = new AttribData();
                s.AttribName = (AttribName)i;
                Debug.Log("SO awake  function called");
                switch (s.AttribName)
                {
                    case AttribName.None:
                        break;
                    case AttribName.health:
                        s.minLimit = 0f;
                        s.maxLimit = 0;
                        break;
                    case AttribName.stamina:
                        s.minLimit = 0f;
                        s.maxLimit = 0f;
                        break;
                    case AttribName.fortOrg:
                        s.minLimit = -24f;
                        s.maxLimit = 24f; break;
                    case AttribName.fortitude:
                        s.minLimit = -30f;
                        s.maxLimit = 30f; break;
                    case AttribName.hunger:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case AttribName.thirst:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case AttribName.damage:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case AttribName.acc:
                        s.minLimit = 0f;
                        s.maxLimit = 15; break;
                    case AttribName.focus:
                        s.minLimit = 12f;
                        s.maxLimit = 96f; break;
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
                    case AttribName.armor:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case AttribName.dodge:
                        s.minLimit = 0f;
                        s.maxLimit = 100f; break;
                    case AttribName.fireRes:
                        s.minLimit = -60f;
                        s.maxLimit = 90f; break;
                    case AttribName.earthRes:
                        s.minLimit = -60f;
                        s.maxLimit = 90f; break;
                    case AttribName.waterRes:
                        s.minLimit = -60f;
                        s.maxLimit = 90f; break;
                    case AttribName.airRes:
                        s.minLimit = -60f;
                        s.maxLimit = 90f; break;
                    case AttribName.lightRes:
                        s.minLimit = -30f;
                        s.maxLimit = 60f; break;
                    case AttribName.darkRes:
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
                float healthMax = StatsList.Find(t => t.AttribName == AttribName.vigor).currValue * 4;
                StatsList.Find(t => t.AttribName == AttribName.health).currValue = healthMax;
                StatsList.Find(t => t.AttribName == AttribName.health).maxLimit = healthMax; 

                float staminaMax = StatsList.Find(t => t.AttribName == AttribName.willpower).currValue * 3;
                StatsList.Find(t => t.AttribName == AttribName.stamina).currValue = staminaMax;
                StatsList.Find(t => t.AttribName == AttribName.stamina).maxLimit  = staminaMax;
                FillDesc();
        }

    }

        #region CORE STATS RELATED
        void FillDesc()
        {
            string str = ""; 
            for (int i = 0; i < StatsList.Count; i++)
            {
                
                switch (StatsList[i].AttribName)
                {
                    case AttribName.None:
                        str = ""; 
                        break;
                    case AttribName.health:
                        str= "Health is health, you know it..."; 
                        break;
                    case AttribName.stamina:
                        str = "Resource to use skills."; 
                        break;
                    case AttribName.fortitude:
                        str = "Be among those who dare to dare!"; 
                        break;
                    case AttribName.hunger:
                        str = "Start losing Health once Hunger is 100%."; 
                        break;
                    case AttribName.thirst:
                        str = "Start losing Stamina once Thirst is 100%.";
                        break;
                    case AttribName.damage:
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
                    case AttribName.armor:
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
                StatsList[i].desc = str; 
            }




        }
        #endregion
    }






}


