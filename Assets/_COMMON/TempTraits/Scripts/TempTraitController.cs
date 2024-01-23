using Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using UnityEngine;



namespace Common
{
    public class TempTraitController : MonoBehaviour
    {
        [Header("Immunity")]
        public List<TempTraitBuffData> allTempTraitImmunities = new List<TempTraitBuffData>();
        public List<ImmunityFrmType> allImmunitiesFrmType = new List<ImmunityFrmType>();

        [Header(" All Temp Trait Applied")] 
        public List<TempTraitBuffData> alltempTraitBuffData = new List<TempTraitBuffData>();
        public List<TempTraitBase> allTempTraitBase = new List<TempTraitBase>();   

        public List<TempTraitModel> allTempTraitModels = new List<TempTraitModel>();


        public CharController charController;
        int traitID =-1;
        [SerializeField] List<int> allBuffIds = new List<int>();

        /// <summary>
        ///  You can have 3 sickness.If you trigger 1 more sickness when already have 3
        ///  , you will become gravely ill.When you are gravely ill, you are immune to sickness
        /// .But you automatically die if you aren't healed in 3 days. (you receive G.ill in day 1, you die day 4.)				
        /// You can have 3 physical positive and 3 mental positive traits.first in first out 								
        ///You can have 3 mental negative traits and 3 physical negative traits.If you trigger 1 more
        ///, you become Madness(for mental) or Weakness(for physical)
        ///
        /// 
        ///  FIFO rule for pos traits
        /// </summary>

        void Start()
        {
            traitID = -1;
            charController = GetComponent<CharController>();
            CalendarService.Instance.OnStartOfCalDay +=(int day)=> DayTick();          
        }
        #region TRAIT APPLY & REMOVE


        void PosTrait_FIFOChk(TempTraitType tempTraitType)
        {
            // You can have 3 physical positive and 3 mental positive traits.first in first out 
            int count = 0; int firstId = -1; 
            foreach (TempTraitModel tempTraitModel in allTempTraitModels)
            {
               if(tempTraitModel.temptraitBehavior != TraitBehaviour.Positive)
                    continue; 
               if (tempTraitModel.tempTraitType == tempTraitType)                     
               {
                   //// 
                    if(tempTraitModel.tempTraitID < firstId)
                    {
                        firstId= tempTraitModel.tempTraitID;
                    }
                    count++; 
               }
            }
            if (count >= 3)
            {
                RemoveTrait(firstId); 
            }
        }
        int NegTraitStackChk(TempTraitType tempTraitType, CauseType causeType, int causeName, int causeByCharID)
        {
            ///You can have 3 mental negative traits and 3 physical negative traits.If you trigger 1 more
            ///, you become Madness(for mental) or Weakness(for physical)
            List<int> allIds = new List<int>(); 
            foreach (TempTraitModel tempTraitModel in allTempTraitModels)
            {
                if (tempTraitModel.temptraitBehavior != TraitBehaviour.Negative)
                    continue;
                if (tempTraitModel.tempTraitType == tempTraitType)
                {   
                    allIds.Add(tempTraitModel.tempTraitID);                    
                }
            }
            if (allIds.Count >= 3)
            {
                allIds.ForEach(t => RemoveTrait(t));
                int traitID = -1; 
                if(tempTraitType == TempTraitType.Mental)
                  traitID =  ApplyTempTrait(causeType, causeName, causeByCharID, TempTraitName.Insane);
                if (tempTraitType == TempTraitType.Physical)
                    traitID = ApplyTempTrait(causeType, causeName, causeByCharID, TempTraitName.Weakness);
                if (tempTraitType == TempTraitType.Sickness)
                    traitID = ApplyTempTrait(causeType, causeName, causeByCharID, TempTraitName.GravelyIll);
                return traitID; 
            }
            return -1; 

        }

        bool HasBossTraitOfSameType(TempTraitName tempTraitName)
        {
            TempTraitSO tempTraitSO = TempTraitService.Instance.allTempTraitSO.GetTempTraitSO(tempTraitName);
            TraitBehaviour tempTraitBehavior = tempTraitSO.temptraitBehavior;
            TempTraitType tempTraitType = tempTraitSO.tempTraitType;

            switch (tempTraitType)
            {
                case TempTraitType.None:
                    return false;                     
                case TempTraitType.Mental:
                    return HasTempTrait(TempTraitName.Insane);                     
                case TempTraitType.Physical:
                    return HasTempTrait(TempTraitName.Weakness);                    
                case TempTraitType.Sickness:
                    return HasTempTrait(TempTraitName.GravelyIll);                    
                default:
                    return false;                    
            }

        }

        void ClearForBossTempTrait(TempTraitName tempTraitName)
        {
            switch (tempTraitName)
            { 
                case TempTraitName.Insane:
                    ClearAllNegTraitOfType(TempTraitType.Mental, TraitBehaviour.Negative);
                    ClearAllNegTraitOfType(TempTraitType.Mental, TraitBehaviour.Positive);
                    break; 
                case TempTraitName.Weakness:
                    ClearAllNegTraitOfType(TempTraitType.Physical, TraitBehaviour.Negative);
                    ClearAllNegTraitOfType(TempTraitType.Physical, TraitBehaviour.Positive);
                    break;
                case TempTraitName.GravelyIll:
                    ClearAllNegTraitOfType(TempTraitType.Sickness, TraitBehaviour.Negative);
                    break;
                default:
                    break; 
            }
        }

        void ClearAllNegTraitOfType(TempTraitType tempTraitType, TraitBehaviour traitBehaviour)
        {            
            foreach (TempTraitModel tempTraitModel in allTempTraitModels)
            {
                if (tempTraitModel.temptraitBehavior != traitBehaviour)
                    continue;
                if (tempTraitModel.tempTraitType == tempTraitType)
                {
                    RemoveTrait(tempTraitModel.tempTraitID);
                }
            }            
        }

        public int ApplyTempTrait(CauseType causeType, int causeName, int causeByCharID
                                                             ,TempTraitName tempTraitName)
        {
            // check immunity list 
            if (HasTempTrait(tempTraitName) || HasImmunityFrmTrait(tempTraitName) 
                                            || HasImmunityFrmType(tempTraitName)
                                            || HasBossTraitOfSameType(tempTraitName)) // boss trait chk 
                return -1;
            
            
            TempTraitSO tempTraitSO = TempTraitService.Instance.allTempTraitSO.GetTempTraitSO(tempTraitName);
            PosTrait_FIFOChk(tempTraitSO.tempTraitType);

            if(tempTraitName != TempTraitName.GravelyIll || tempTraitName != TempTraitName.Insane
                                                         || tempTraitName != TempTraitName.Weakness)
            {
                int negId = NegTraitStackChk(tempTraitSO.tempTraitType, causeType, causeName, causeByCharID);
                if (negId != -1)
                    return negId;
            }
            else
            {
                ClearForBossTempTrait(tempTraitName); 
            }
            int effectedCharID = charController.charModel.charID;

            int startDay = CalendarService.Instance.dayInGame; 
    
            TempTraitBase traitBase = TempTraitService.Instance
                                    .temptraitsFactory.GetNewTempTraitBase(tempTraitName);
            

            traitID = allBuffIds.Count+1;
            allBuffIds.Add(traitID); 

            int netTime = UnityEngine.Random.Range(tempTraitSO.minCastTime, tempTraitSO.maxCastTime+1);
            // mod data for record and string creation 
            TempTraitModData modData = new TempTraitModData(causeType, causeName, causeByCharID, effectedCharID);

            TempTraitBuffData tempTraitBuffData
                             = new TempTraitBuffData(traitID, tempTraitName, startDay, netTime, modData);

            alltempTraitBuffData.Add(tempTraitBuffData);

            traitBase.TempTraitInit(tempTraitSO, charController, traitID);
            traitBase.TraitBaseApply(); 
            traitBase.OnApply();
            allTempTraitBase.Add(traitBase);
            TempTraitService.Instance.On_TempTraitStart(tempTraitBuffData);

            return traitID; 
      
        }
        public void RemoveTraitByName(TempTraitName tempTraitName)
        {
            int index = alltempTraitBuffData.FindIndex(t => t.tempTraitName == tempTraitName);
            if (index != -1)
            {

                int id = alltempTraitBuffData[index].traitID; 
                RemoveTrait(id);  // base etc removed here
            }
            else
            {
                Debug.Log("trait not found" + tempTraitName);
            }
        }
       
        public bool RemoveTrait(int _traitID)
        {
            int index = alltempTraitBuffData.FindIndex(t => t.traitID == _traitID);
            if (index == -1) return false;
            TempTraitBuffData traitData = alltempTraitBuffData[index];
            alltempTraitBuffData.Remove(traitData);
            int indexBase =
                    allTempTraitBase.FindIndex(t => t.tempTraitName == traitData.tempTraitName);
            if (indexBase != -1)  // base
            {
                allTempTraitBase[indexBase].EndTrait();
                allTempTraitBase.RemoveAt(indexBase);
            }
            else
            {
                Debug.Log("temp trait base not  found " + traitData.tempTraitName);
                return false; 
            }

       
            int index2 = allTempTraitModels.FindIndex(t => t.tempTraitName == traitData.tempTraitName);
            if (index != -1) // model 
            {
                allTempTraitModels.RemoveAt(index2);
            }
            else
            {
                Debug.Log("temp trait model not  found " + traitData.tempTraitName);
                return false;
            }
            TempTraitService.Instance.On_TempTraitEnd(traitData);

            return true;        
        }

        public void OnClearMindPressed()
        {
            foreach (TempTraitBuffData model in alltempTraitBuffData.ToList())
            {
                TempTraitSO tempSO = TempTraitService.Instance.allTempTraitSO.GetTempTraitSO(model.tempTraitName);
                if (tempSO.tempTraitType == TempTraitType.Mental)
                {                   
                        RemoveTraitByName(model.tempTraitName);                   
                }
            }
        }

        public void OnHealBtnPressed(TempTraitBuffData tempTraitBuffData)
        {
            RemoveTraitByName(tempTraitBuffData.tempTraitName);
        }

        #endregion

        #region IMMUNITY APPLY & REMOVE
        public void ApplyImmunityBuff(CauseType causeType, int causeName, int causeByCharID
                                        , TempTraitName tempTraitName, TimeFrame timeFrame, int netTime) // immunity buff for this temp trait
        {
            int effectedCharID = charController.charModel.charID;
            int currtime = 0;                
            if (timeFrame == TimeFrame.EndOfDay)
                currtime = CalendarService.Instance.dayInGame;         
            traitID++;
            TempTraitModData causeData = new TempTraitModData(causeType,causeName, causeByCharID, effectedCharID, true); 

            TempTraitBuffData immunityData = new TempTraitBuffData
                                                    (traitID, tempTraitName, currtime, netTime, causeData);
            allTempTraitImmunities.Add(immunityData);
        }

        public bool RemoveTraitImmunity(TempTraitName traitName)   
        {
            int index = allTempTraitImmunities.FindIndex(t => t.tempTraitName == traitName);
            if (index == -1) return false;
            allTempTraitImmunities.RemoveAt(index);
            return true;    
        }

        public void ApplyTraitTypeImmunityBuff(CauseType causeType, int causeName, int causeByCharID
                            , TempTraitType tempTraitType, TimeFrame timeFrame, int netTime) // immunity buff for this char State
        {
            int effectedCharID = charController.charModel.charID;
            int currtime = -1;           
            if (timeFrame == TimeFrame.EndOfDay)
                currtime = CalendarService.Instance.dayInGame;

            traitID++;
            TempTraitModData causeData = new TempTraitModData(causeType, causeName, causeByCharID, effectedCharID, true);


            ImmunityFrmType immunityFrmTypeData = new ImmunityFrmType(traitID, tempTraitType, timeFrame, netTime
                                                                    , currtime, currtime, causeData); 

            allImmunitiesFrmType.Add(immunityFrmTypeData); 
        }
        public bool RemoveTraitImmunityFrmType(TempTraitType tempTraitType)
        {
            int index = allImmunitiesFrmType.FindIndex(t => t.traitType == tempTraitType);
            if (index == -1) return false;
            allImmunitiesFrmType.RemoveAt(index);
            return true;
        }

        #endregion

        #region CHECKS TRAIT & IMMUNITY
        public bool HasTempTrait(TempTraitName tempTraitName)
        {
            return alltempTraitBuffData.Any(t => t.tempTraitName == tempTraitName);
        }
        public bool HasImmunityFrmTrait(TempTraitName tempTraitName)
        {
            return allTempTraitImmunities.Any(t => t.tempTraitName == tempTraitName);
        }

        public bool HasImmunityFrmType(TempTraitName traitName)
        {
            TempTraitSO tempSO = TempTraitService.Instance.allTempTraitSO.GetTempTraitSO(traitName);
            TempTraitType traitType = tempSO.tempTraitType; 
            return allImmunitiesFrmType.Any(t=>t.traitType== traitType);
        }
        #endregion
        public void DayTick()
        {
            if(alltempTraitBuffData.Count == 0) return;  
            foreach (TempTraitBuffData traitData in alltempTraitBuffData.ToList())
            {
                if (traitData.timeFrame == TimeFrame.Infinity)
                    continue; 
                if (traitData.timeFrame == TimeFrame.EndOfDay)
                {
                    if (traitData.currentTime >= traitData.netTime)
                    {
                        RemoveTrait(traitData.traitID);
                    }
                    traitData.currentTime++;
                }
            }
        }

        public void Update()
        {        // Jungle Freak.....//Swampy Cramp.....//Forest Gump
            if (Input.GetKeyDown(KeyCode.V)) 
            {
                ApplyTempTrait(CauseType.CharState, (int)CharStateName.FlatFooted, 1
                                                             , TempTraitName.JungleFreak);
                ApplyTempTrait(CauseType.CharState, (int)CharStateName.FlatFooted, 1
                                                            , TempTraitName.SwampyCramp);
                ApplyTempTrait(CauseType.CharState, (int)CharStateName.FlatFooted, 1
                                                            , TempTraitName.ForestGump);
                ApplyTempTrait(CauseType.CharState, (int)CharStateName.FlatFooted, 1
                                                          , TempTraitName.Confident);

                ApplyTempTrait(CauseType.CharState, (int)CharStateName.FlatFooted, 1
                                                             , TempTraitName.Unwavering);

                ApplyTempTrait(CauseType.CharState, (int)CharStateName.FlatFooted, 1
                                                             , TempTraitName.Initiator);
            }
        
            if (Input.GetKeyDown(KeyCode.C))
            {
                ApplyTempTrait(CauseType.CharState, (int)CharStateName.FlatFooted, 1
                                                                , TempTraitName.Diarrhea);
                ApplyTempTrait(CauseType.CharState, (int)CharStateName.FlatFooted, 1
                                                             , TempTraitName.Tapeworm);
                ApplyTempTrait(CauseType.CharState, (int)CharStateName.FlatFooted, 1
                                                             , TempTraitName.Constipation);               
            }
        }
    }

    [Serializable]
    public class TempTraitBuffData
    {
        public int traitID;
        public TempTraitName tempTraitName; 
        public int startTime;
        public TimeFrame timeFrame;
        public int netTime;
        public int currentTime;
        public TempTraitModData modData; 

        public TempTraitBuffData(int traitID, TempTraitName tempTraitName
                                , int startTime, int netTime, TempTraitModData modData)
        {
            this.traitID = traitID;
            this.tempTraitName = tempTraitName;
            this.startTime = startTime;            
            this.netTime = netTime;
            currentTime = startTime;
            this.timeFrame = TimeFrame.EndOfDay; 
            this.modData = modData.DeepClone(); 
        }
    }

    [Serializable]
    public class TempTraitModData  // broadCast Data 
    {
        public CauseType causeType;  // add cause name here 
        public int causeName;
        public int causeByCharID;
        public int effectedCharID;
        public bool isImmunity;

        public TempTraitModData(CauseType causeType, int causeName, int causeByCharID
                                    , int effectedCharID, bool isImmunity = false)
        {
            this.causeType = causeType;
            this.causeName = causeName;
            this.causeByCharID = causeByCharID;
            this.effectedCharID = effectedCharID;
            this.isImmunity = isImmunity;
        }
    }

    public class ImmunityFrmType
    {
        public int traitID;
        public TempTraitType traitType;        
        public TimeFrame timeFrame;
        public int netTime;
        public int startTime;
        public int currentTime;
        public TempTraitModData causeData; 

        public ImmunityFrmType(int traitID, TempTraitType traitType, TimeFrame timeFrame, int netTime
                                              , int startTime, int currentTime, TempTraitModData causeData)
        {
            this.traitID = traitID;
            this.traitType = traitType;
            this.timeFrame = timeFrame;
            this.netTime = netTime;
            this.startTime = startTime;
            this.currentTime = currentTime;
            this.causeData = causeData;
        }
    }

}


