using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Combat;
using Interactables;
using System.Linq;
namespace Common
{

    [System.Serializable]
    public class StatData
    {
        public StatsName statsName;
        public float currValue;
        public float baseValue;   
        public string desc;
        public float minRange;
        public float maxRange;
        public float minLimit; 
        public float maxLimit; 
        public bool isClamped = false; 
    }

    [System.Serializable]
    public class CharModel
    {
        [Header("CORE STATS")]
        public int charID;
        public CharNames charName;
        public Sprite charSprite;
        public Sprite charHexSprite; 
        public CharMode charMode;
        public CharMode orgCharMode; 
      
        public RaceType raceType;
        public RaceTypeHero raceTypeHero;
        public CultureType cultType;
        public ArcheType heroType;
        public ClassType classType;
        public string charNameStr; 

        [Header("State")]
        public StateOfChar stateOfChar;
        public AvailOfChar availOfChar;

        [Header("Fame behavior")]
        public FameBehavior fameBehavior;

        [Header("Location")]
        public LocationName baseCharLoc; 
        public LocationName currCharLoc;

        [Header("Experience points")]
        public int expPoints;
        public int jobExp;
        public int skillPts;
        public int expBonusModPercent = 0;
        

        [Header("LEVELS")]
        public int charLvl =0;
        public List<int> PrevLvlrec = new List<int>(); // to record all the prev levels

        [Header("CHAR EXTD STATS")]
        public float tameAnimalsStrength;
        public FleeBehaviour fleeBehaviour;
        public bool canBeAmbushed;
        public bool canBeCaught;


        [Header("DEFAULT PROVISION")]
        public List<ItemData> provisionItems = new List<ItemData>();

        [Header("ACTIVE INV POTIONS")]
        public ItemData beltSlot1;
        public ItemData beltSlot2;
        public ItemData provisionSlot;

        [Header("ACTIVE INV GEWGAWS")]
        public ItemData gewgawSlot1;
        public ItemData gewgawSlot2;
        public ItemData gewgawSlot3;

        [Header("ITEM STATS")]
        public int netPotionAddictChance = 0; 

        [Header("Gift")]
        // money and Item 
        public List<ItemData> earningsItems = new List<ItemData>();

        [Header("Money Share")]
        public float earningsShare;   // Money
        public float earningShareBonus_percent; 
        
        [Header("Companion PreReq")]
        public List<ItemData> CompanionPreReqOpt1 = new List<ItemData>();
        public List<ItemData> CompanionPreReqOpt2 = new List<ItemData>();

        public int startLevel;
        
        
        public int fortitudeOrg = 0;
        public int lootBonus = 0;
        public StatData staminaRegen = new StatData();
        public StatData HpRegen = new StatData(); 
        public List<PermanentTraitName> permanentTraitList;

        [Header("Grid related")]
        public CharOccupies _charOccupies;
        public List<int> _posPriority;


        //[Header("Damage Modifier")]
        //public float meleeAttackTypeMod = 0f;
        //public float rangedAttackTypeMod = 0f;
        //public float remoteAttackTypeMod = 0f;

        //public float physicalSIMod = 0f;
        //public float magicalSIMod = 0f;
         
        
        [Header("IMMUNITY LIST")]
        public List<TempTraitName> Immune2TraitsList = new List<TempTraitName>();
        public List<StatsName> Immune2StatChangeList = new List<StatsName>();
        public List<CharStateName> Immune2CharStateList = new List<CharStateName>();


        [Header("States & Traits _LIST")]
        public List<PermanentTraitName> InPermaTraitList = new List<PermanentTraitName>();
        public List<CharStateName> InCharStatesList = new List<CharStateName>();
        public List<TempTraitName> InTempTraitList = new List<TempTraitName>();

        [Header("Armor Socket  and WeaponEnchantment")]

        public GemName enchantableGem4Weapon = GemName.None;
        public bool isGemEnchantableIdentified = false;  // to be deprecated

        public ArmorType armorType; 

        [Header("Stats")]
        public List<StatData> statsList = new List<StatData>();
      
        public void SaveModel()
        {
           string mydataPath = "/SAVE_SYSTEM/savedFiles/" + SaveService.Instance.slotSelect.ToString()
                                                                                    + "/Char/charModels.txt";
            Debug.Log(" INSIDE SAVE MODEL ");
            if (!File.Exists(Application.dataPath + mydataPath))
            {
                Debug.Log("does not exist");
                File.CreateText(Application.dataPath + mydataPath);
            }
            string charData =  JsonUtility.ToJson(this); 
            string saveStr = charData +"|"; 
            
            File.AppendAllText(Application.dataPath + mydataPath, saveStr);
        }

        public void LoadModel()
        {
            
        }

        public float GetDeltaRatio()
        {
            List<LvlExpData> allLvlExpData = 
                    CharService.Instance.lvlNExpSO.allLvlExpData;
            float barRatio = 0f; 
            if(charLvl > 0)
            {
                int delta =
                 allLvlExpData.Find(t => t.charLvl == (charLvl + 1)).deltaExpPts;
                int expPts = allLvlExpData.Find(t => t.charLvl == charLvl).totalExpPts;
                barRatio = (float)Mathf.Abs(expPoints - expPts) / delta;
            }        
            return barRatio; 
        }
        public CharModel(CharacterSO _charSO)
        {

            charID = _charSO.charID;
            charName = _charSO.charName;
            charSprite = _charSO.charSprite;
            charHexSprite = _charSO.charHexPortrait; 
            charMode = _charSO.charMode;
            orgCharMode = _charSO.orgCharMode;
            raceType =_charSO.raceType;
            raceTypeHero = _charSO.raceTypeHero;
            cultType = _charSO.cultType;
            heroType = _charSO.archeType;
            classType = _charSO.classType;
            charNameStr = _charSO.charNameStr;

            expPoints = _charSO.expPoints;             
            jobExp  = _charSO.jobExp;
            skillPts = _charSO.skillPts;

          
            // Fame behavior
            fameBehavior = _charSO.fameBehavior;

            // location 
            baseCharLoc = _charSO.baseCharLoc;
            currCharLoc = baseCharLoc;
            // State
            availOfChar = _charSO.availOfChar;
            stateOfChar = _charSO.stateOfChar;
            
            // MISC STATS
            tameAnimalsStrength = _charSO.tameAnimalsStrength;
            fleeBehaviour = _charSO.fleeBehaviour;
            canBeCaught = _charSO.canBeCaught;
            canBeAmbushed = _charSO.canBeAmbushed;

            // "DEFAULT PROVISION
            provisionItems = _charSO.provisionItems.DeepClone();

            //"Gift"
            // money and Item 
            earningsItems = _charSO.earningsItems.DeepClone();
            earningsShare = _charSO.earningShare;

            // Companion PreReq
            CompanionPreReqOpt1 = _charSO.CompanionPreReqOpt1.DeepClone();
            CompanionPreReqOpt2 = _charSO.CompanionPreReqOpt2.DeepClone();

            startLevel = _charSO.startLevel;
            fortitudeOrg = 0;  // TBD 
            lootBonus = 0;

            staminaRegen.currValue = 2;
            HpRegen.currValue = 0; 


            // GRID Related
            _charOccupies = _charSO.charOccupies;
            _posPriority = _charSO.posPriority;

            // Weapon and armor 
            enchantableGem4Weapon = _charSO.enchantableGem4Weapon;
            armorType = _charSO.armorType;

            statsList = new List<StatData>();
            int listLength = _charSO.StatsList.Count;

            StatsVsChanceSO statsVsChanceSO = CharService.Instance.statChanceSO; 
            //Debug.Log("list length " + listLength);
            for (int i = 0; i < listLength; i++)
            {
                StatData statdata = new StatData();
                statdata.statsName = _charSO.StatsList[i].statsName;
                statdata.currValue = _charSO.StatsList[i].currValue;
                statdata.baseValue = statdata.currValue;
                statdata.desc = _charSO.StatsList[i].desc;
                statdata.minRange = _charSO.StatsList[i].minRange;
                statdata.maxRange = _charSO.StatsList[i].maxRange;

                StatChanceData statChanceData = statsVsChanceSO.allStatChanceData
                                    .Find(t => t.statName == _charSO.StatsList[i].statsName);
                statdata.minLimit = statChanceData.minLimit; 
                    //_charSO.StatsList[i].minLimit;  // get stat Chance and copy from there
                statdata.maxLimit = statChanceData.maxLimit;

                statsList.Add(statdata);
            }
        }


    }
}






