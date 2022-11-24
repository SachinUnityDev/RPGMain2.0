using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System.Linq; 


namespace Common
{
    public class CharStateModData  // broadCast Data 
    {       
        public CauseType causeType;  // add cause name here 
        public int causeName;
        public int causeByCharID;
        public int effectedCharID;
        public CharStateName charStateName;
        public bool isImmunity; 

        public CharStateModData(CauseType causeType, int causeName, int causeByCharID
                                    , int effectedCharID, CharStateName charStateName, bool isImmunity= false)
        {
            this.causeType = causeType;
            this.causeName = causeName;
            this.causeByCharID = causeByCharID;
            this.effectedCharID = effectedCharID;
            this.charStateName = charStateName;
            this.isImmunity = isImmunity;
        }
    }

    public class CharStateBuffData
    {
        public int buffID;
        public bool isBuff;   // true if BUFF and false if DEBUFF
        public int startRoundNo;
        public TimeFrame timeFrame;
        public int buffedNetTime;
        public int buffCurrentTime;
        public CharStateModData charStateModData;
        public string directString;

        public CharStateBuffData(int buffID, bool isBuff, int startRoundNo, TimeFrame timeFrame, int buffedNetTime
                                    , CharStateModData charStateModData, string directString="")
        {
            this.buffID = buffID;
            this.isBuff = isBuff;
            this.startRoundNo = startRoundNo;
            this.timeFrame = timeFrame;
            this.buffedNetTime = buffedNetTime;
            this.buffCurrentTime = 0;
            this.charStateModData = charStateModData;
            this.directString = directString;
        }
    }

    public class ImmunityBuffData   
    {
        public int buffID;
        public bool isBuff;   // true if BUFF and false if DEBUFF
        public int startRoundNo;
        public TimeFrame timeFrame;
        public int buffedNetTime;
        public int buffCurrentTime;
        public CharStateModData charStateModData;
        public string directString;

        public ImmunityBuffData(int buffID, bool isBuff, int startRoundNo, TimeFrame timeFrame, int buffedNetTime
                                    , CharStateModData charStateModData, string directString = "")
        {
            this.buffID = buffID;
            this.isBuff = isBuff;
            this.startRoundNo = startRoundNo;
            this.timeFrame = timeFrame;
            this.buffedNetTime = buffedNetTime;
            this.buffCurrentTime = 0;
            this.charStateModData = charStateModData;
            this.directString = directString;
        }
    }


    public class CharStateController : MonoBehaviour
    {
        public List<CharStateBuffData> allCharStateBuffs = new List<CharStateBuffData>();
        public List<ImmunityBuffData> allImmunityBuffs = new List<ImmunityBuffData>();
        CharController charController;
        [SerializeField] List<string> buffStrs = new List<string>();
        [SerializeField] List<string> deDuffStrs = new List<string>();

        public List<CharStatesBase> allCharBases = new List<CharStatesBase>();
        

        int buffID;
        void Start()
        {
            charController = GetComponent<CharController>();
            CombatEventService.Instance.OnEOR += RoundTick;
            CombatEventService.Instance.OnEOC += EOCTick; 

        }
    #region BUFF & DEBUFF
        public void ApplyCharStateBuff(CauseType causeType, int causeName, int causeByCharID
                                , CharStateName charStateName, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "")
        {
            int effectedCharID = charController.charModel.charID;

            int currRd = CombatService.Instance.currentRound;
            CharStateModData charStateModData = new CharStateModData(causeType, causeName, causeByCharID
                                                                    , effectedCharID, charStateName); 


            CharStateBuffData charStateBuffData = new CharStateBuffData(buffID, isBuff, currRd, timeFrame, netTime
                                                                        , charStateModData); 

            allCharStateBuffs.Add(charStateBuffData);

        }

        public void ApplyImmunityBuff(CauseType causeType, int causeName, int causeByCharID
                                , CharStateName charStateName, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "") // immunity buff for this char State
        {
            int effectedCharID = charController.charModel.charID;

            int currRd = CombatService.Instance.currentRound;
            CharStateModData charStateModData = new CharStateModData(causeType, causeName, causeByCharID
                                                                    , effectedCharID, charStateName, true);


            ImmunityBuffData immunityBuffData = new ImmunityBuffData(buffID, isBuff, currRd, timeFrame, netTime
                                                                        , charStateModData);

            allImmunityBuffs.Add(immunityBuffData);

        }



        public void ApplyBuffOnRange(CauseType causeType, int causeName, int causeByCharID, StatsName statName
            , float minChgR, float maxChgR, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "")
        {
            //CharModData charModData = charController.ChangeStatRange(causeType, causeName, causeByCharID
            //                               , statName, minChgR, maxChgR, true);

            //int currRd = CombatService.Instance.currentRound;

            //BuffData buffData = new BuffData(isBuff, currRd, timeFrame, netTime,
            //           charModData, directStr);

            //allBuffs.Add(buffData);
        }

        bool IsRangeChange(BuffData buffData)
        {
            if (buffData.charModData.modChgMinR == 0 &&
                buffData.charModData.modChgMaxR == 0)
                return false;
            else
                return true;
        }

        public void RemoveBuffData(BuffData buffData)
        {
            if (IsRangeChange(buffData))
            {

                charController.ChangeStatRange(buffData.charModData.causeType,
                                     buffData.charModData.causeName, buffData.charModData.causeByCharID
                                     , buffData.charModData.statModified
                                     , buffData.charModData.modChgMinR, buffData.charModData.modChgMinR, true);

            }
            else
            {
                charController.ChangeStat(buffData.charModData.causeType,
                                     buffData.charModData.causeName, buffData.charModData.causeByCharID
                                     , buffData.charModData.statModified, buffData.charModData.modCurrVal, true);
            }
            allCharStateBuffs.Remove(buffData);
        }

        public List<string> GetBuffList()
        {
            foreach (BuffData buffData in allCharStateBuffs)
            {
                if (buffData.isBuff)
                {
                    buffStrs.Add(buffData.directString);
                }
            }
            return buffStrs;
        }
        public List<string> GetDeBuffList()
        {
            foreach (BuffData buffData in allCharStateBuffs)
            {
                if (!buffData.isBuff)
                {
                    deDuffStrs.Add(buffData.directString);
                }
            }
            return deDuffStrs;
        }
        public void RoundTick()
        {
            foreach (BuffData buffData in allCharStateBuffs)
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
            foreach (BuffData buffData in allCharStateBuffs)
            {
                if (buffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    RemoveBuffData(buffData);
                }
            }
        }

#endregion


        public void RemoveCharState(CharStateName _charStateName)
        {
            foreach (BuffData buffData in allCharStateBuffs.ToList())
            {
                if (buffData.charModData.causeName == (int)_charStateName)
                {
                    RemoveBuffData(buffData);
                }
            }
        }

        public void UpdateBuffData(CharStateName charStateName)
        {  
            foreach (BuffData buffData in allCharStateBuffs)
            {
                if(buffData.charModData.causeType == CauseType.CharSkill)
                {
                    if(buffData.charModData.causeName == (int)charStateName)
                    {
                        buffData.buffCurrentTime = 0;
                    }
                }
            }
        }


        public CharStatesBase GetCharStateBase(CharStateName _charStateName)
        {
            if (allCharBases.Any(t=>t.charStateName == _charStateName))
            {
                return allCharBases.Find(t => t.charStateName == _charStateName); 
            }

            Debug.Log("Char State Base Not found"); 
            return null; 
        }


    }




}
