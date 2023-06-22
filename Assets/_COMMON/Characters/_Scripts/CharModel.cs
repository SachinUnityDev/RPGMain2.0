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
    public class AttribData
    {
        public AttribName AttribName;
        public float currValue;
        public float baseValue;   
        public string desc;      
        public float minLimit; 
        public float maxLimit; 
        public bool isClamped = false; 
    }
    [System.Serializable]
    public class StatData
    {
        public StatName statName;
        public float currValue;        
        public string desc;
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

        [Header("Hunger and Thirst")]
        public int hungerMod;
        public int thirstMod; 

        [Header("Experience points")]
        public int expPoints;
        public int jobExp;
        public int skillPts;
        public int expBonusModPercent = 0;
        

        [Header("LEVELS")]
        public int charLvl =0;
        public List<int> PrevLvlrec = new List<int>(); // to record all the prev levels

        [Header("CHAR EXTD STATS")]
        public FleeBehaviour fleeBehaviour;  
        public CharFleeState charFleeState; 

        [Header("DEFAULT PROVISION")]
        public List<ItemData> provisionItems = new List<ItemData>();

        [Header("ITEM STATS")]
        public int netPotionAddictChance = 0; 

        [Header("Gift")]
        // money and Item 
        public List<ItemData> earningsItems = new List<ItemData>();

        [Header("Money Share")]
        public int earningsShare;   // earning share (percent) out of 100
        public float earningShareBonus_percent; 
        
        [Header("Companion PreReq")]
        public List<ItemData> CompanionPreReqOpt1 = new List<ItemData>();
        public List<ItemData> CompanionPreReqOpt2 = new List<ItemData>();

        public int startLevel;
        
        
        //public int fortitudeOrg = 0;
        public int lootBonus = 0;
        //public AttribData staminaRegen = new AttribData();
        //public AttribData HpRegen = new AttribData(); 
        public List<PermaTraitName> permanentTraitList;

        [Header("Grid related")]
        public CharOccupies _charOccupies;
        public List<int> _posPriority; 
        
        [Header("Permanent Traits LIST")]
        //public List<TempTraitName> Immune2TraitsList = new List<TempTraitName>();
        //public List<AttribName> Immune2StatChangeList = new List<AttribName>();
        //public List<CharStateName> Immune2CharStateList = new List<CharStateName>();
        
        [Header("States & Traits _LIST")]
        public List<PermaTraitName> PermaTraitList = new List<PermaTraitName>();
        //public List<CharStateName> InCharStatesList = new List<CharStateName>();
        //public List<TempTraitName> InTempTraitList = new List<TempTraitName>();

        [Header("Armor Socket  and WeaponEnchantment")]
        public GemNames enchantableGem4Weapon = GemNames.None;
        public bool isGemEnchantableIdentified = false;  // to be deprecated

        public ArmorType armorType; 

        [Header("Stats")]
        public List<AttribData> attribList = new List<AttribData>();
        public List<StatData> statList = new List<StatData>();
        public float GetDeltaRatio()
        {
            List<LvlExpData> allLvlExpData =
                    CharService.Instance.lvlNExpSO.allLvlExpData;
            float barRatio = 0f;
            if (charLvl > 0)
            {
                int delta =
                 allLvlExpData.Find(t => t.charLvl == (charLvl + 1)).deltaExpPts;
                int expPts = allLvlExpData.Find(t => t.charLvl == charLvl).totalExpPts;
                barRatio = (float)Mathf.Abs(expPoints - expPts) / delta;
            }
            return barRatio;
        }



        #region SAVE AND LOAD 
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

        #endregion  

        #region  CHARMODEL INIT FROM SO 

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
            charLvl = _charSO.charLvl;
          
            // Fame behavior
            fameBehavior = _charSO.fameBehavior;

            // location 
            baseCharLoc = _charSO.baseCharLoc;
            currCharLoc = baseCharLoc;
            // State
            availOfChar = _charSO.availOfChar;
            stateOfChar = _charSO.stateOfChar;
            
            // MISC STATS
            
            fleeBehaviour = _charSO.fleeBehaviour;
            charFleeState = _charSO.charFleeState;

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
         
            lootBonus = 0;


            //staminaRegen.currValue = 2;
            //HpRegen.currValue = 0; 


            // GRID Related
            _charOccupies = _charSO.charOccupies;
            _posPriority = _charSO.posPriority;

            // Weapon and armor 
            enchantableGem4Weapon = _charSO.enchantableGem4Weapon;
            armorType = _charSO.armorType;

            attribList = new List<AttribData>();
            statList = new List<StatData>();
            int listLength = _charSO.AttribList.Count;

            StatsVsChanceSO statsVsChanceSO = CharService.Instance.statChanceSO; 
            //Debug.Log("list length " + listLength);
            for (int i = 0; i < listLength; i++)
            {
                AttribData attribData = new AttribData();
                attribData.AttribName = _charSO.AttribList[i].AttribName;
                attribData.currValue = _charSO.AttribList[i].currValue;
                attribData.baseValue = attribData.currValue;
                attribData.desc = _charSO.AttribList[i].desc;             

                AttribChanceData statChanceData = statsVsChanceSO.allStatChanceData
                                    .Find(t => t.attribName == _charSO.AttribList[i].AttribName);
                attribData.minLimit = _charSO.AttribList[i].minLimit;
                //_charSO.StatsList[i].minLimit;  // get stat Chance and copy from there
                attribData.maxLimit = _charSO.AttribList[i].maxLimit;

                attribList.Add(attribData);
            }
            for (int i = 0; i < _charSO.statList.Count; i++)
            {
                StatData statData = new StatData();
                statData.statName = _charSO.statList[i].statName;
                statData.currValue = _charSO.statList[i].currValue;            
                statData.desc = _charSO.AttribList[i].desc;

                statData.minLimit = _charSO.AttribList[i].minLimit;

                statData.maxLimit = _charSO.AttribList[i].maxLimit;

                statList.Add(statData);
            }


        }
        #endregion

    }
}


//#region ACTIVE INV SLOT
//public void AddItemToPotionSlot(Iitems item, int slotID)
//{
//    ItemData itemData = new ItemData(item.itemType, item.itemName);
//    if (slotID == 0)
//    {
//        potionSlot1 = itemData;
//    }else if (slotID == 1)
//    {
//        potionSlot2 = itemData; 
//    }else if (slotID == 2)
//    {
//        provisionSlot = itemData; 
//    }            
//}
//public void RemoveItmFrmPotionSlot(int slotID)
//{          
//    if (slotID == 0)
//    {
//        potionSlot1 = null; 
//    }
//    else if (slotID == 1)
//    {
//        potionSlot2 = null;
//    }
//    else if (slotID == 2)
//    {
//        provisionSlot = null;
//    }
//}

//#endregion 



