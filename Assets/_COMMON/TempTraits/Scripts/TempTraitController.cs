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
        public List<TempTraitBuffData> alltempTraitApplied = new List<TempTraitBuffData>();
        public List<TempTraitBase> allTempTraitAppliedBase = new List<TempTraitBase>();   


        public CharController charController;
        int traitID =-1;
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
            traitID = 0;
            charController = GetComponent<CharController>();
            CalendarService.Instance.OnStartOfCalDay +=(int day)=> DayTick();          
        }
        #region TRAIT APPLY & REMOVE
        public void ApplyTempTrait(CauseType causeType, int causeName, int causeByCharID
                                                             ,TempTraitName tempTraitName)
        {
            // check immunity list 
            if (HasTempTrait(tempTraitName) || HasImmunityFrmTrait(tempTraitName) 
                                            || HasImmunityFrmType(tempTraitName))
                return;

            // get temp trait from the factory add to hashset 
            int effectedCharID = charController.charModel.charID;

            int startDay = CalendarService.Instance.dayInGame; 
    
            TempTraitBase traitBase = TempTraitService.Instance
                                    .temptraitsFactory.GetNewTempTraitBase(tempTraitName);
            TempTraitSO tempTraitSO = TempTraitService.Instance.allTempTraitSO.GetTempTraitSO(tempTraitName);

            traitID++;
            int netTime = UnityEngine.Random.Range(tempTraitSO.minCastTime, tempTraitSO.maxCastTime+1);
            // mod data for record and string creation 
            CauseData4Trait modData = new CauseData4Trait(causeType, causeName, causeByCharID, effectedCharID);

            TempTraitBuffData tempTraitAppliedData
                             = new TempTraitBuffData(traitID, tempTraitName, startDay, netTime, modData);

            alltempTraitApplied.Add(tempTraitAppliedData);
            traitBase.OnApply(charController);
            allTempTraitAppliedBase.Add(traitBase); 
        }
        public void RemoveTraitByName(TempTraitName tempTraitName)
        {
            int index = alltempTraitApplied.FindIndex(t => t.tempTraitName == tempTraitName);
            if (index != -1)
            {
                RemoveTrait(alltempTraitApplied[index].traitID);  // base etc removed here
            }
            else
            {
                Debug.Log("trait not found" + tempTraitName);
            }
        }
        //public void RemoveTraitFrmLs(TempTraitModel tempTraitBuffData)
        //{
        //    alltempTraitApplied.Remove(tempTraitBuffData);
        //}

        public bool RemoveTrait(int _traitID)
        {
            int index = alltempTraitApplied.FindIndex(t => t.traitID == _traitID);
            if (index == -1) return false;
            TempTraitBuffData traitData = alltempTraitApplied[index];
            alltempTraitApplied.Remove(traitData);
            int indexBase =
                    allTempTraitAppliedBase.FindIndex(t => t.tempTraitName == traitData.tempTraitName);
            if (indexBase != -1)
            {
                allTempTraitAppliedBase[indexBase].OnTraitEnd();
                allTempTraitAppliedBase.RemoveAt(indexBase);
            }                
            return true;
        }

        public void OnClearMindPressed()
        {
            foreach (TempTraitBuffData model in alltempTraitApplied.ToList())
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
                                , TempTraitName tempTraitName, TimeFrame timeFrame, int netTime) // immunity buff for this char State
        {
            int effectedCharID = charController.charModel.charID;

            int currtime = 0;                
            if (timeFrame == TimeFrame.EndOfDay)
                currtime = CalendarService.Instance.dayInGame; 
        
            traitID++;
            CauseData4Trait causeData = new CauseData4Trait(causeType,causeName, causeByCharID, effectedCharID, true); 

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
            CauseData4Trait causeData = new CauseData4Trait(causeType, causeName, causeByCharID, effectedCharID, true);


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
            return alltempTraitApplied.Any(t => t.tempTraitName == tempTraitName);
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
            if(alltempTraitApplied.Count == 0) return;  
            foreach (TempTraitBuffData traitData in alltempTraitApplied.ToList())
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
        public CauseData4Trait modData; 

        public TempTraitBuffData(int traitID, TempTraitName tempTraitName
                                , int startTime, int netTime, CauseData4Trait modData)
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
    public class CauseData4Trait  // broadCast Data 
    {
        public CauseType causeType;  // add cause name here 
        public int causeName;
        public int causeByCharID;
        public int effectedCharID;
        public bool isImmunity;

        public CauseData4Trait(CauseType causeType, int causeName, int causeByCharID
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
        public CauseData4Trait causeData; 

        public ImmunityFrmType(int traitID, TempTraitType traitType, TimeFrame timeFrame, int netTime
                                              , int startTime, int currentTime, CauseData4Trait causeData)
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


