using Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
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

    public class TempTraitBuffData
    {
        public int buffID;
        public bool isBuff;   // true if BUFF and false if DEBUFF
        public int startRoundNo;
        public TimeFrame timeFrame;
        public int buffedNetTime;
        public int buffCurrentTime;
        public TempTraitModData tempTraitModData;
        public string directString;

        public TempTraitBuffData(int buffID, bool isBuff, int startRoundNo, TimeFrame timeFrame, int buffedNetTime
                                    , TempTraitModData tempTraitModData, string directString = "")
        {
            this.buffID = buffID;
            this.isBuff = isBuff;
            this.startRoundNo = startRoundNo;
            this.timeFrame = timeFrame;
            this.buffedNetTime = buffedNetTime;
            this.buffCurrentTime = 0;
            this.tempTraitModData = tempTraitModData;
            this.directString = directString;
        }
    }

    public class TempTraitImmunityData
    {
        public int buffID;
        public bool isBuff;   // true if BUFF and false if DEBUFF
        public int startRoundNo;
        public TimeFrame timeFrame;
        public int buffedNetTime;
        public int buffCurrentTime;
        public TempTraitModData tempTraitModData;
        public string directString;

        public TempTraitImmunityData(int buffID, bool isBuff, int startRoundNo, TimeFrame timeFrame, int buffedNetTime
                                    , TempTraitModData tempTraitModData, string directString = "")
        {
            this.buffID = buffID;
            this.isBuff = isBuff;
            this.startRoundNo = startRoundNo;
            this.timeFrame = timeFrame;
            this.buffedNetTime = buffedNetTime;
            this.buffCurrentTime = 0;
            this.tempTraitModData = tempTraitModData;
            this.directString = directString;
        }
    }


    public class TempTraitController : MonoBehaviour
    {

        public List<TempTraitBuffData> alltempTraitBuffs = new List<TempTraitBuffData>();
        public List<TempTraitImmunityData> allTempTraitImmunityBuffs = new List<TempTraitImmunityData>();
        CharController charController;
        [SerializeField] List<string> buffStrs = new List<string>();
        [SerializeField] List<string> deDuffStrs = new List<string>();

        public List<TempTraitBase> allTempTraitBases = new List<TempTraitBase>();


        int buffID;

        void Start()
        {
            buffID = 0;
            charController = GetComponent<CharController>();
            CombatEventService.Instance.OnEOR += RoundTick;
            CombatEventService.Instance.OnEOC += EOCTick;

        }
        #region BUFF & DEBUFF
        public void ApplyTempTraitBuff(CauseType causeType, int causeName, int causeByCharID
                                , TempTraitName tempTraitName, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "")
        {
            // check immunity list 
            int effectedCharID = charController.charModel.charID;

            int currRd = CombatService.Instance.currentRound;
            TempTraitModData tempTraitModData = new TempTraitModData(causeType, causeName, causeByCharID
                                                                    , effectedCharID, tempTraitName);

            buffID++; 
            TempTraitBuffData tempTraitBuffData = new TempTraitBuffData(buffID, isBuff, currRd, timeFrame, netTime
                                                                        , tempTraitModData);            
            alltempTraitBuffs.Add(tempTraitBuffData);
            // add Char State Buffs FX 
        }

        public void ApplyImmunityBuff(CauseType causeType, int causeName, int causeByCharID
                                , TempTraitName tempTraitName, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "") // immunity buff for this char State
        {
            int effectedCharID = charController.charModel.charID;

            int currRd = CombatService.Instance.currentRound;
            TempTraitModData tempTraitModData = new TempTraitModData(causeType, causeName, causeByCharID
                                                                    , effectedCharID, tempTraitName, true);

            buffID++; 
            TempTraitImmunityData immunityBuffData = new TempTraitImmunityData(buffID, isBuff, currRd, timeFrame, netTime
                                                                        , tempTraitModData);
            allTempTraitImmunityBuffs.Add(immunityBuffData);
        }

        public void RemoveBuffData(TempTraitBuffData tempTraitBuffData)
        {

            // remove buff FX
            alltempTraitBuffs.Remove(tempTraitBuffData);
        }

        public List<string> GetCharStateBuffList()
        {
            foreach (TempTraitBuffData tempTraitBuffData in alltempTraitBuffs)
            {
                if (tempTraitBuffData.isBuff)
                {
                    // buffStrs.Add(.directString);//// get strings from SO
                }
            }
            return buffStrs;
        }
        public List<string> GetTempTraitsImmunityList()
        {
            foreach (TempTraitImmunityData immunityBuffData in allTempTraitImmunityBuffs)
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
            foreach (TempTraitBuffData buffData in alltempTraitBuffs)
            {
                if (buffData.timeFrame == TimeFrame.EndOfRound)
                {
                    if (buffData.buffCurrentTime >= buffData.buffedNetTime)
                    {
                        RemoveBuffData(buffData);
                    }
                    buffData.buffCurrentTime++;
                }
            }
        }

        public void EOCTick()
        {
            foreach (TempTraitBuffData buffData in alltempTraitBuffs)
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
}

