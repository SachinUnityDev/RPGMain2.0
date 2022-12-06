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

        [SerializeField] List<int> allBuffIds = new List<int>(); 
        public List<CharStatesBase> allCharBases = new List<CharStatesBase>();
        

        int buffID;
        void Start()
        {
            charController = GetComponent<CharController>();
            CombatEventService.Instance.OnEOR += RoundTick;
            CombatEventService.Instance.OnEOC += EOCTick; 

        }
    #region BUFF & DEBUFF
        public int ApplyCharStateBuff(CauseType causeType, int causeName, int causeByCharID
                                , CharStateName charStateName, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "")
        {
            // check immunity list 
            int effectedCharID = charController.charModel.charID;

            int currRd = CombatService.Instance.currentRound;
            CharStateModData charStateModData = new CharStateModData(causeType, causeName, causeByCharID
                                                                    , effectedCharID, charStateName);
            buffID++;
            CharStateBuffData charStateBuffData = new CharStateBuffData(buffID, isBuff, currRd, timeFrame, netTime
                                                                        , charStateModData); 

            allCharStateBuffs.Add(charStateBuffData);
            // add Char State Buffs FX 
            return buffID;
        }

        public int ApplyImmunityBuff(CauseType causeType, int causeName, int causeByCharID
                                , CharStateName charStateName, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "") // immunity buff for this char State
        {
            int effectedCharID = charController.charModel.charID;
            int currRd = CombatService.Instance.currentRound;
            CharStateModData charStateModData = new CharStateModData(causeType, causeName, causeByCharID
                                                                    , effectedCharID, charStateName, true);
            buffID++; 
            ImmunityBuffData immunityBuffData = new ImmunityBuffData(buffID, isBuff, currRd, timeFrame, netTime
                                                                        , charStateModData);
            allImmunityBuffs.Add(immunityBuffData);
            return buffID;
        }


        public int ApplyDOTImmunityBuff(CauseType causeType, int causeName, int causeByCharID
                                , CharStateName charStateName, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "")
        {
            int firstDOTBuffID=0; 
            if (charStateName == CharStateName.BleedLowDOT || charStateName == CharStateName.BleedMedDOT
                                                            || charStateName == CharStateName.BleedHighDOT)
            {   firstDOTBuffID =              
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.BleedLowDOT, timeFrame, netTime, false);                
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.BleedMedDOT, timeFrame, netTime, false);
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.BleedHighDOT, timeFrame, netTime, false);

            }
            if (charStateName == CharStateName.PoisonedHighDOT || charStateName == CharStateName.PoisonedMedDOT
                                                              || charStateName == CharStateName.PoisonedLowDOT)
            {
                firstDOTBuffID =
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.PoisonedHighDOT, timeFrame, netTime, false);
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.PoisonedMedDOT, timeFrame, netTime, false);
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.PoisonedLowDOT, timeFrame, netTime, false);
            }
            if (charStateName == CharStateName.BurnHighDOT || charStateName == CharStateName.BurnMedDOT
                                                            || charStateName == CharStateName.BurnLowDOT)
            {
                firstDOTBuffID =
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.BurnHighDOT, timeFrame, netTime, false);
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.BurnMedDOT, timeFrame, netTime, false);
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.BurnLowDOT, timeFrame, netTime, false);
            }
            return firstDOTBuffID;
        }

        public void RemoveBuffData(CharStateBuffData charStateBuffData)
        {          
          
            // remove buff FX
            allCharStateBuffs.Remove(charStateBuffData);
        }

        public List<string> GetCharStateBuffList()
        {
            foreach (CharStateBuffData charStateBuffData in allCharStateBuffs)
            {
                if (charStateBuffData.isBuff)
                {
                   // buffStrs.Add(.directString);//// get strings from SO
                }
            }
            return buffStrs;
        }
        public List<string> GetImmunityList()
        {
            foreach (ImmunityBuffData immunityBuffData in allImmunityBuffs)
            {
                // get immunitylist 
                //if (!buffData.isBuff)
                //{
                //    deDuffStrs.Add(buffData.directString);
                //}
            }
            return deDuffStrs;
        }

        public bool HasCharDOTState(CharStateName _charStateName)
        {
            if (_charStateName == CharStateName.BurnHighDOT
               || _charStateName == CharStateName.BurnMedDOT
               || _charStateName == CharStateName.BurnLowDOT)
            {
                return (HasCharState( CharStateName.BurnHighDOT)
                  || HasCharState( CharStateName.BurnMedDOT)
                  || HasCharState( CharStateName.BurnLowDOT));
            }

            if (_charStateName == CharStateName.BleedHighDOT
              || _charStateName == CharStateName.BleedMedDOT
              || _charStateName == CharStateName.BleedLowDOT)
            {
                return (HasCharState( CharStateName.BleedHighDOT)
                   || HasCharState( CharStateName.BleedMedDOT)
                   || HasCharState( CharStateName.BleedLowDOT));
            }

            if (_charStateName == CharStateName.PoisonedHighDOT
              || _charStateName == CharStateName.PoisonedMedDOT
              || _charStateName == CharStateName.PoisonedLowDOT)
            {
                return (HasCharState( CharStateName.PoisonedHighDOT)
                 || HasCharState( CharStateName.PoisonedMedDOT)
                 || HasCharState( CharStateName.PoisonedLowDOT));
            }
            return false;
        }

        public bool HasCharState(CharStateName _charStateName)
        {
            foreach (CharStateBuffData buffData in allCharStateBuffs)
            {
                if(buffData.charStateModData.charStateName == _charStateName)
                {
                    return true; 
                }
            }
            return false;
        }

        public bool HasDOTImmunity(CharStateName _charStateName)
        {
            if (_charStateName == CharStateName.BurnHighDOT
               || _charStateName == CharStateName.BurnMedDOT
               || _charStateName == CharStateName.BurnLowDOT)
            {
                return (HasImmunity(CharStateName.BurnHighDOT)
                        || HasImmunity(CharStateName.BurnMedDOT)
                        || HasImmunity(CharStateName.BurnLowDOT)); 
            }

            if (_charStateName == CharStateName.BleedHighDOT
              || _charStateName == CharStateName.BleedMedDOT
              || _charStateName == CharStateName.BleedLowDOT)
            {
                return (HasImmunity(CharStateName.BleedHighDOT)
                        || HasImmunity(CharStateName.BleedMedDOT)
                        || HasImmunity(CharStateName.BleedLowDOT));
            }

            if (_charStateName == CharStateName.PoisonedHighDOT
              || _charStateName == CharStateName.PoisonedMedDOT
              || _charStateName == CharStateName.PoisonedLowDOT)
            {
                return (HasImmunity(CharStateName.PoisonedHighDOT)
                         || HasImmunity(CharStateName.PoisonedMedDOT)
                         || HasImmunity(CharStateName.PoisonedLowDOT));
            }
            return false; 
        }

        public bool HasImmunity(CharStateName charStateName)
        {
            foreach (ImmunityBuffData immunityBuffData in allImmunityBuffs)
            {
                if(immunityBuffData.charStateModData.charStateName == charStateName)
                {
                    return true;
                }
            }
            return false; 
        }

        public void RoundTick()
        {
            foreach (CharStateBuffData buffData in allCharStateBuffs)
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
            foreach (CharStateBuffData buffData in allCharStateBuffs)
            {
                if (buffData.timeFrame == TimeFrame.EndOfCombat)
                {
                    RemoveBuffData(buffData);
                }
            }
        }





#endregion

        public void ClearDOT(CharStateName _charStateName)
        {
            if (_charStateName == CharStateName.BurnHighDOT
         || _charStateName == CharStateName.BurnMedDOT
         || _charStateName == CharStateName.BurnLowDOT)
            {
                RemoveCharState(CharStateName.BurnHighDOT);
                RemoveCharState(CharStateName.BurnMedDOT);
                RemoveCharState(CharStateName.BurnLowDOT);
            }

            if (_charStateName == CharStateName.BleedHighDOT
              || _charStateName == CharStateName.BleedMedDOT
              || _charStateName == CharStateName.BleedLowDOT)
            {
                RemoveCharState(CharStateName.BleedHighDOT);
                RemoveCharState(CharStateName.BleedMedDOT);
                RemoveCharState(CharStateName.BleedLowDOT);
            }

            if (_charStateName == CharStateName.PoisonedHighDOT
              || _charStateName == CharStateName.PoisonedMedDOT
              || _charStateName == CharStateName.PoisonedLowDOT)
            {
                RemoveCharState(CharStateName.PoisonedHighDOT);
                RemoveCharState(CharStateName.PoisonedMedDOT);
                RemoveCharState(CharStateName.PoisonedLowDOT);
            }
        }

        public void RemoveCharState(CharStateName charStateName)
        {            
            int index = allCharBases.FindIndex(t => t.charStateName == charStateName);
            allCharBases[index].EndState();   // all related buffs to be cleared from here
            allCharBases.RemoveAt(index);
        }
   
        public void ResetCharStateBuff(CharStateName charStateName)
        {  
            foreach (CharStateBuffData buffData in allCharStateBuffs)
            {
                //if(buffData.charStateModData.causeType == CauseType.CharSkill)
                //{
                    if(buffData.charStateModData.causeName == (int)charStateName)
                    {
                        buffData.buffCurrentTime = 0;
                    }
                //}
            }
        }


        public CharStatesBase GetCurrCharStateBase(CharStateName _charStateName)
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
