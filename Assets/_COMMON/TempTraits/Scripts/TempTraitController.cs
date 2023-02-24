using Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Common
{
    public class TempTraitController : MonoBehaviour
    {

        public List<TempTraitAppliedData> alltempTraitApplied = new List<TempTraitAppliedData>();
        public List<TempTraitImmunityData> allTempTraitImmunities = new List<TempTraitImmunityData>();
        public List<TempTraitBase> allTempTraitAppliedBase = new List<TempTraitBase>();   
        [SerializeField] List<string> allTempTraitsStr = new List<string>();


        CharController charController;
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
            CombatEventService.Instance.OnEOR += RoundTick;
            CombatEventService.Instance.OnEOC += EOCTick;
        }
        #region BUFF & DEBUFF
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


            TempTraitAppliedData tempTraitBuffData
                             = new TempTraitAppliedData(traitID, tempTraitName, startDay, netTime);
            alltempTraitApplied.Add(tempTraitBuffData);

            traitBase.OnApply();
        }

        public void ApplyImmunityBuff(CauseType causeType, int causeName, int causeByCharID
                                , TempTraitName tempTraitName, TimeFrame timeFrame, int netTime) // immunity buff for this char State
        {
            int effectedCharID = charController.charModel.charID;
            int currRd = CombatService.Instance.currentRound;

        
            traitID++; 
            //TempTraitImmunityData immunityBuffData = new TempTraitImmunityData(traitID, isBuff, currRd, timeFrame, netTime
            //                                                            , tempTraitModData);
           // allTempTraitImmunities.Add(immunityBuffData);
        }

        public void ClearTempTrait(TempTraitName tempTraitName)
        {
            int tobeClearedBuffId = -1; 
            foreach (TempTraitAppliedData tempTrait in alltempTraitApplied)
            {
                if(tempTrait.tempTraitName == tempTraitName)
                {
                    tempTrait.traitID = tobeClearedBuffId;   
                }   
            }
            if(tobeClearedBuffId != -1)
            {
                RemoveBuff(tobeClearedBuffId);
            }
        }

        public void RemoveBuffData(TempTraitAppliedData tempTraitBuffData)
        {

            // remove buff FX
            alltempTraitApplied.Remove(tempTraitBuffData);
        }

        public bool RemoveBuff(int _buffID)
        {
            int index = alltempTraitApplied.FindIndex(t => t.traitID == _buffID);
            if (index == -1) return false;
            TempTraitAppliedData buffData = alltempTraitApplied[index];
            RemoveBuffData(buffData);
            return true;
        }

        public bool HasTempTrait(TempTraitName tempTraitName)
        {
            return alltempTraitApplied.Any(t => t.tempTraitName == tempTraitName);
        }

        public bool HasImmunityFrmTrait(TempTraitName tempTraitName)
        {
            return allTempTraitImmunities.Any(t => t.tempTraitName == tempTraitName);
        }


        public List<string> GetCharStateBuffList()
        {
            foreach (TempTraitAppliedData tempTraitBuffData in alltempTraitApplied)
            {
                
            }
            return allTempTraitsStr;
        }
        public List<string> GetTempTraitsImmunityList()
        {
            foreach (TempTraitImmunityData immunityBuffData in allTempTraitImmunities)
            {
                // get immunitylist 
                //if (!buffData.isBuff)
                //{
                //    deDuffStrs.Add(buffData.directString);
                //}
            }
            return null; 
          
        }
        public void RoundTick()
        {
            foreach (TempTraitAppliedData buffData in alltempTraitApplied)
            {
                if (buffData.timeFrame == TimeFrame.EndOfRound)
                {
                    if (buffData.currentTime >= buffData.netTime)
                    {
                        RemoveBuffData(buffData);
                    }
                    buffData.currentTime++;
                }
            }
        }

        public void EOCTick()
        {
            foreach (TempTraitAppliedData buffData in alltempTraitApplied)
            {
                if (buffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    RemoveBuffData(buffData);
                }
            }
        }

        #endregion

        //public void ResetCharStateBuff(TempTraitName tempTraitName)
        //{
        //    foreach (TempTraitBuffData buffData in alltempTraitBuffs)
        //    {
        //        //if(buffData.charStateModData.causeType == CauseType.CharSkill)
        //        //{
        //        if (buffData.charStateModData.causeName == (int)tempTraitName)
        //        {
        //            buffData.buffCurrentTime = 0;
        //        }
        //        //}
        //    }
        //}

        //TO BE DONE 
        //public CharStatesBase GetCurrCharStateBase(CharStateName _charStateName)
        //{
        //    if (allCharBases.Any(t => t.charStateName == _charStateName))
        //    {
        //        return allCharBases.Find(t => t.charStateName == _charStateName);
        //    }

        //    Debug.Log("Char State Base Not found");
        //    return null;
        //}

    }
 

    public class TempTraitAppliedData
    {
        public int traitID;
        public TempTraitName tempTraitName; 
        public int startTime;
        public TimeFrame timeFrame;
        public int netTime;
        public int currentTime;

        public TempTraitAppliedData(int traitID, TempTraitName tempTraitName
                                , int startTime, int netTime)
        {
            this.traitID = traitID;
            this.tempTraitName = tempTraitName;
            this.startTime = startTime;            
            this.netTime = netTime;
            currentTime = startTime; 
        }
    }

    public class TempTraitImmunityData
    {
        public int traitID;
        public TempTraitName tempTraitName;       
        public int startRoundNo;
        public TimeFrame timeFrame;
        public int netTime;
        public int currentTime;


    }

}


//public class TempTraitModData  // broadCast Data 
//{
//    public CauseType causeType;  // add cause name here 
//    public int causeName;
//    public int causeByCharID;
//    public int effectedCharID;
//    public TempTraitName tempTraitName;
//    public bool isImmunity;

//    public TempTraitModData(CauseType causeType, int causeName, int causeByCharID
//                                , int effectedCharID, TempTraitName tempTraitName, bool isImmunity = false)
//    {
//        this.causeType = causeType;
//        this.causeName = causeName;
//        this.causeByCharID = causeByCharID;
//        this.effectedCharID = effectedCharID;
//        this.tempTraitName = tempTraitName;
//        this.isImmunity = isImmunity;
//    }
//}

