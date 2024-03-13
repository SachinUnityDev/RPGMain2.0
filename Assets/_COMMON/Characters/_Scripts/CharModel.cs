using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Combat;
using Interactables;
using System.Linq;
using System.Security.Policy;

namespace Common
{

    [System.Serializable]
    public class AttribData
    {
        public AttribName AttribName;
        public int currValue;
        public int baseValue;   
        public string desc;      
        public float minLimit; 
        public float maxLimit; 
        public bool isClamped = false; 
    }
    [System.Serializable]
    public class StatData
    {
        public StatName statName;
        public int currValue;        
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
        public Archetype archeType;
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

        [Header("Gewgaw Restrictions")]
        public List<GewgawSlotType> extraSlotTypeAllowed = new List<GewgawSlotType>();

        [Header("Experience points")]
        public int mainExp;
        public int jobExp;
        public int skillPts;
        public int expBonusModPercent = 0;
        

        [Header("LEVELS")]
        public int charLvl =0;
        public List<int> PrevLvlrec = new List<int>(); // to record all the prev levels

        [Header("CHAR EXTD STATS")]
        public FleeBehaviour fleeBehaviour;  
       // public CharFleeState charFleeState;

        //[Header("Active Inv Slot items")]
        //public List<Iitems> activeInvItems = new List<Iitems>();

        [Header("DEFAULT PROVISION")]
        public List<ItemDataWithQtyNFameType> provisionItems = new List<ItemDataWithQtyNFameType>();

        [Header("ITEM STATS")]
        public int netPotionAddictChance = 0; 

        [Header("Gift")]
        // money and Item 
        public List<ItemDataWithQtyNFameType> earningsItems = new List<ItemDataWithQtyNFameType>();

        [Header("Money Share")]
        public int earningsShare;   // earning share (percent) out of 100
        public float earningShareBonus_percent;

        [Header("Companion PreReq")]
        public List<ItemDataWithQtyNFameType> CompanionPreReqOpt1 = new List<ItemDataWithQtyNFameType>(); 
        public List<ItemDataWithQtyNFameType> CompanionPreReqOpt2 = new List<ItemDataWithQtyNFameType>(); 

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
                barRatio = (float)Mathf.Abs(mainExp - expPts) / delta;
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
            archeType = _charSO.archeType;
            classType = _charSO.classType;
            charNameStr = _charSO.charNameStr;

            mainExp = _charSO.mainExp;             
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
                attribData.baseValue = _charSO.AttribList[i].baseValue;
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
                //if(statData.statName == StatName.health)
                //{
                //    Debug.Log("Stop here" + statData.maxLimit); 
                //}
                statData.currValue = _charSO.statList[i].currValue;            
                statData.desc = _charSO.statList[i].desc;

                statData.minLimit = _charSO.statList[i].minLimit;

                statData.maxLimit = _charSO.statList[i].maxLimit;

                statList.Add(statData);
            }
        }
        #endregion

        #region  GET PreReq, Earning and provision

        public ItemDataWithQty GetProvisionItem()
        {
            ItemDataWithQty itemQty = null;
            FameType fameType = FameService.Instance.fameController.fameModel.fameType;

            itemQty = GetItemDataWithQty(fameType, provisionItems); 
            return itemQty; 
        }
        public List<ItemDataWithQty> GetPrereqsItem()
        {
            ItemDataWithQty itemQty1 = null;
            ItemDataWithQty itemQty2 = null;    
            FameType fameType = FameService.Instance.fameController.fameModel.fameType;

            itemQty1 = GetItemDataWithQty(fameType, CompanionPreReqOpt1);
            itemQty2 = GetItemDataWithQty(fameType, CompanionPreReqOpt2);


            List<ItemDataWithQty> allPreReqs = new List<ItemDataWithQty>(); 
            if(itemQty1!= null)
                allPreReqs.Add(itemQty1);
            if(itemQty2!= null)
                allPreReqs.Add(itemQty2);
   
            return allPreReqs;
        }
        public ItemDataWithQty GetEarningItem()  // Qty here is redundant eery time a item is earned(in loot etc....) is it is taken by the char 
        {
            ItemDataWithQty itemQty1 = null;
            FameType fameType = FameService.Instance.fameController.fameModel.fameType;

            itemQty1 = GetItemDataWithQty(fameType, earningsItems);
            return itemQty1;
        }
        ItemDataWithQty GetItemDataWithQty(FameType fameType, List<ItemDataWithQtyNFameType> allItemQty)
        {
            ItemDataWithQty itemQty = null;
         
            foreach (ItemDataWithQtyNFameType itemFame in allItemQty)
            {
                if (itemFame.fameType == fameType)
                    itemQty = itemFame.itemDataQty;
            }
            if (itemQty == null)
            {
                int index = allItemQty.FindIndex(t => t.fameType == FameType.None);
                if (index != -1)
                    itemQty = allItemQty[index].itemDataQty;
                else
                    Debug.Log("None not found" + charName);
            }
            return itemQty;
        }


        #endregion
        #region CHAR LVL

        public void LvlNExpUpdate(int currExp)
        {
            mainExp = currExp;
            if (!LvlUpCharChk(currExp)) return;    
                charLvl++;
        }

        public bool LvlUpCharChk(int currExp)
        {
            int thresholdExp = CharService.Instance.lvlNExpSO.GetThresholdExpPts4Lvl(charLvl+1);
            if (currExp < thresholdExp) return false;
            else
                return true; 
        }


        #endregion

    }
}


