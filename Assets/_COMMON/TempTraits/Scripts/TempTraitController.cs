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
        public List<TempTraitModel> alltempTraitApplied = new List<TempTraitModel>();
        public List<TempTraitModel> allTempTraitImmunities = new List<TempTraitModel>();


        public List<TempTraitBase> allTempTraitAppliedBase = new List<TempTraitBase>();   
        [SerializeField] List<string> allTempTraitsStr = new List<string>();
      
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
            CalendarService.Instance.OnStartOfDay +=(int day)=> DayTick();          
            charController = gameObject.GetComponent<CharController>(); 
        }
        #region TRAIT APPLY & REMOVE
        public void ApplyTempTrait(CauseType causeType, int causeName, int causeByCharID
                                                             ,TempTraitName tempTraitName)
        {
            // check immunity list 
            if (HasTempTrait(tempTraitName) || HasImmunityFrmTrait(tempTraitName))
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
            TempTraitModData modData = new TempTraitModData(causeType, causeName, causeByCharID, netTime, tempTraitName);

            TempTraitModel tempTraitAppliedData
                             = new TempTraitModel(traitID, tempTraitName, startDay, netTime, modData);

            alltempTraitApplied.Add(tempTraitAppliedData);
            traitBase.OnApply(charController);
            allTempTraitAppliedBase.Add(traitBase); 
        }
        public void RemoveTraitByName(TempTraitName tempTraitName)
        {
            int index = alltempTraitApplied.FindIndex(t => t.tempTraitName == tempTraitName);
            if (index != -1)
            {
                RemoveTrait(alltempTraitApplied[index].traitID);
            }
            else
            {
                Debug.Log("trait not found" + tempTraitName);
            }
        }

        public void RemoveTraitFrmLs(TempTraitModel tempTraitBuffData)
        {
            alltempTraitApplied.Remove(tempTraitBuffData);
        }

        public bool RemoveTrait(int _traitID)
        {
            int index = alltempTraitApplied.FindIndex(t => t.traitID == _traitID);
            if (index == -1) return false;
            TempTraitModel traitData = alltempTraitApplied[index];
            RemoveTraitFrmLs(traitData);
            int indexBase =
                    allTempTraitAppliedBase.FindIndex(t => t.tempTraitName == traitData.tempTraitName);
            if (indexBase != -1)
                allTempTraitAppliedBase.RemoveAt(indexBase);
            return true;
        }

        public void OnClearMindPressed()
        {
            foreach (TempTraitModel model in alltempTraitApplied.ToList())
            {
                TempTraitSO tempSO = TempTraitService.Instance.allTempTraitSO.GetTempTraitSO(model.tempTraitName);
                if (tempSO.tempTraitType == TempTraitType.Mental)
                {                   
                        RemoveTraitByName(model.tempTraitName);                   
                }
            }
        }
    
        #endregion

        #region IMMUNITY APPLY & REMOVE

        public void ApplyImmunityBuff(CauseType causeType, int causeName, int causeByCharID
                                , TempTraitName tempTraitName, TimeFrame timeFrame, int netTime) // immunity buff for this char State
        {
            //int effectedCharID = charController.charModel.charID;
            //int currRd = CombatService.Instance.currentRound;

        
            traitID++;
            TempTraitModData modData = new TempTraitModData(causeType,causeName, causeByCharID, netTime, tempTraitName, true); 

            TempTraitModel immunityData = new TempTraitModel
                                                    (traitID, tempTraitName, netTime, netTime, modData);
            allTempTraitImmunities.Add(immunityData);
        }

        public bool RemoveTraitImmunity(TempTraitName traitName)   
        {
            int index = allTempTraitImmunities.FindIndex(t => t.tempTraitName == traitName);
            if (index == -1) return false;
            TempTraitModel traitData = alltempTraitApplied[index];
            RemoveTraitFrmLs(traitData);
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

        #endregion
        public void DayTick()
        {
            if(alltempTraitApplied.Count == 0) return;  
            foreach (TempTraitModel traitData in alltempTraitApplied)
            {
                if (traitData.timeFrame == TimeFrame.EndOfDay)
                {
                    if (traitData.currentTime >= traitData.netTime)
                    {
                        RemoveTraitFrmLs(traitData);
                    }
                    traitData.currentTime++;
                }
            }
        }

        public void Update()
        {        // Jungle Freak.....//Swampy Cramp.....//Forest Gump
            if (Input.GetKeyDown(KeyCode.V)) 
            {
                ApplyTempTrait(CauseType.CharState, (int)CharStateName.Ambushed, 1
                                                             , TempTraitName.JungleFreak);
                ApplyTempTrait(CauseType.CharState, (int)CharStateName.Ambushed, 1
                                                            , TempTraitName.SwampyCramp);
                ApplyTempTrait(CauseType.CharState, (int)CharStateName.Ambushed, 1
                                                            , TempTraitName.ForestGump);
            }
        
            if (Input.GetKeyDown(KeyCode.C))
            {
                ApplyTempTrait(CauseType.CharState, (int)CharStateName.Ambushed, 1
                                                             , TempTraitName.Confident);

                ApplyTempTrait(CauseType.CharState, (int)CharStateName.Ambushed, 1
                                                             , TempTraitName.Unwavering);

                ApplyTempTrait(CauseType.CharState, (int)CharStateName.Ambushed, 1
                                                             , TempTraitName.Initiator); 
            }
        }
    }

    [Serializable]
    public class TempTraitModel
    {
        public int traitID;
        public TempTraitName tempTraitName; 
        public int startTime;
        public TimeFrame timeFrame;
        public int netTime;
        public int currentTime;
        public TempTraitModData modData; 

        public TempTraitModel(int traitID, TempTraitName tempTraitName
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

    //[Serializable]
    //public class TempTraitImmunityData
    //{
    //    public int traitID;
    //    public TempTraitName tempTraitName;       
    //    public int startRoundNo;
    //    public TimeFrame timeFrame;
    //    public int netTime;
    //    public int currentTime;
    //    public TempTraitModData immunityModData;

    //    public TempTraitImmunityData(int traitID, TempTraitName tempTraitName, int startTime, TimeFrame timeFrame, int netTime
    //                                    , TempTraitModData immunityModData)
    //    {
    //        this.traitID = traitID;
    //        this.tempTraitName = tempTraitName;
    //        this.startRoundNo = startTime;
    //        this.timeFrame = timeFrame;
    //        this.netTime = netTime;
    //        currentTime = startTime;
    //        this.immunityModData = immunityModData.DeepClone();
    //    }
    //}

    [Serializable]
    public class TempTraitModData  // broadCast Data 
    {
        public CauseType causeType;  // add cause name here 
        public int causeName;
        public int causeByCharID;
        public int effectedCharID;
        public TempTraitName tempTraitName;
        public bool isImmunity;

        public TempTraitModData(CauseType causeType, int causeName, int causeByCharID
                                    , int effectedCharID, TempTraitName tempTraitName, bool isImmunity = false)
        {
            this.causeType = causeType;
            this.causeName = causeName;
            this.causeByCharID = causeByCharID;
            this.effectedCharID = effectedCharID;
            this.tempTraitName = tempTraitName;
            this.isImmunity = isImmunity;
        }
    }
}


