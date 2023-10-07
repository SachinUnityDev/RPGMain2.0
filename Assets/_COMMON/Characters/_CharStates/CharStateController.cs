using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System.Linq;
using System;

namespace Common
{
    [Serializable]
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
    [Serializable]
    public class CharStateBuffData
    {
        public int stateID;
        public int startRoundNo;
        public TimeFrame timeFrame;
        public int netTime;
        public int currentTime;
        public CharStateModData charStateModData;


        public CharStateBuffData(int stateID,  int startRoundNo, TimeFrame timeFrame, int buffedNetTime
                                    , CharStateModData charStateModData)
        {
            this.stateID = stateID;
            this.startRoundNo = startRoundNo;
            this.timeFrame = timeFrame;
            this.netTime = buffedNetTime;
            this.currentTime = 0;
            this.charStateModData = charStateModData;          
        }
    }

    public class ImmunityBuffData   
    {
        public int immunityID;
        public int startRoundNo;
        public TimeFrame timeFrame;
        public int buffedNetTime;
        public int buffCurrentTime;
        public CharStateModData charStateModData;
       

        public ImmunityBuffData(int immunityID,  int startRoundNo, TimeFrame timeFrame, int buffedNetTime
                                    , CharStateModData charStateModData)
        {
            this.immunityID = immunityID;          
            this.startRoundNo = startRoundNo;
            this.timeFrame = timeFrame;
            this.buffedNetTime = buffedNetTime;
            this.buffCurrentTime = 0;
            this.charStateModData = charStateModData;         
        }
    }


    public class CharStateController : MonoBehaviour
    {
        [Header("Char State FX data")]
        public List<CharStateBuffData> allCharStateBuffs = new List<CharStateBuffData>();
        public List<ImmunityBuffData> allImmunityBuffs = new List<ImmunityBuffData>();
        CharController charController;


        [SerializeField] List<string> buffStrs = new List<string>();
        [SerializeField] List<string> deDuffStrs = new List<string>();

        [SerializeField] List<int> allBuffIds = new List<int>(); 
        public List<CharStatesBase> allCharBases = new List<CharStatesBase>();

        [Header("CharState and Immunity ID s")]
        [SerializeField]int stateID;
        [SerializeField] int immunityID;  
        void Start()
        {
            charController = GetComponent<CharController>();
            CombatEventService.Instance.OnEOR1 += RoundTick;
            CombatEventService.Instance.OnEOC += EOCTick; 

        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnEOR1 -= RoundTick;
            CombatEventService.Instance.OnEOC -= EOCTick;
        }

        #region BUFF & DEBUFF
        public int ApplyCharStateBuff(CauseType causeType, int causeName, int causeByCharID
                                , CharStateName charStateName, TimeFrame timeFrame = TimeFrame.Infinity, int netTime =-1)
        {
            // check immunity list 
            int effectedCharID = charController.charModel.charID;

            int currRd = -1;
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                currRd = CombatService.Instance.currentRound;
            }
            CharStateModData charStateModData = new CharStateModData(causeType, causeName, causeByCharID
                                                                    , effectedCharID, charStateName);
            stateID++;
            allBuffIds.Add(stateID);
            CharStateBuffData charStateBuffData = new CharStateBuffData(stateID, currRd, timeFrame, netTime
                                                                        , charStateModData); 

            allCharStateBuffs.Add(charStateBuffData);

           
            CharStatesService.Instance.On_CharStateStart(charStateModData);

            // add Char State Buffs FX 
            return stateID;
        }

        public int ApplyImmunityBuff(CauseType causeType, int causeName, int causeByCharID
                                , CharStateName charStateName, TimeFrame timeFrame, int netTime) // immunity buff for this char State
        {
            int effectedCharID = charController.charModel.charID;
            int currRd = -1;
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                currRd = CombatService.Instance.currentRound;
            }
            CharStateModData charStateModData = new CharStateModData(causeType, causeName, causeByCharID
                                                                    , effectedCharID, charStateName, true);
            stateID++; 
            ImmunityBuffData immunityBuffData = new ImmunityBuffData(stateID,  currRd, timeFrame, netTime
                                                                        , charStateModData);
            allImmunityBuffs.Add(immunityBuffData);
            return stateID;
        }

        public void RemoveImmunityByCharState(CharStateName charStateName) 
        {
            foreach (ImmunityBuffData immunityBuffData in allImmunityBuffs)
            {
                if (immunityBuffData.charStateModData.charStateName == charStateName)
                {
                    allImmunityBuffs.Remove(immunityBuffData);
                }
            }
        }



        public void RemoveImmunityBuff(int immunityID)
        {
            foreach (ImmunityBuffData immunityBuffData in allImmunityBuffs)
            {
                if(immunityBuffData.immunityID == immunityID)
                {
                   allImmunityBuffs.Remove(immunityBuffData);
                }
            }
        }
        public List<int> ApplyDOTImmunityBuff(CauseType causeType, int causeName, int causeByCharID
                                , CharStateName charStateName, TimeFrame timeFrame, int netTime, bool isBuff = true)
        {
            List<int> allImmuneBuffID = new List<int>();
            if (charStateName == CharStateName.BleedLowDOT || charStateName == CharStateName.BleedMedDOT
                                                            || charStateName == CharStateName.BleedHighDOT)
            {   
                int id = 
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.BleedLowDOT, timeFrame, netTime);                
               // ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.BleedMedDOT, timeFrame, netTime);
               allImmuneBuffID.Add(id);
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.BleedHighDOT, timeFrame, netTime);
                allImmuneBuffID.Add(id);
            }
            if (charStateName == CharStateName.PoisonedHighDOT || charStateName == CharStateName.PoisonedMedDOT
                                                              || charStateName == CharStateName.PoisonedLowDOT)
            {
                int id =
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.PoisonedHighDOT, timeFrame, netTime);
                allImmuneBuffID.Add(id);
                //ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.PoisonedMedDOT, timeFrame, netTime);
                id =
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.PoisonedLowDOT, timeFrame, netTime);
                allImmuneBuffID.Add(id);   
            }
            if (charStateName == CharStateName.BurnHighDOT || charStateName == CharStateName.BurnMedDOT
                                                            || charStateName == CharStateName.BurnLowDOT)
            {
                int id = 
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.BurnHighDOT, timeFrame, netTime);
                allImmuneBuffID.Add(id);
                //ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.BurnMedDOT, timeFrame, netTime);
                id = 
                ApplyImmunityBuff(causeType, causeName, causeByCharID, CharStateName.BurnLowDOT, timeFrame, netTime);
                allImmuneBuffID.Add(id);   
            }
            return allImmuneBuffID;
        }
               
        public List<string> GetCharStateBuffList()
        {
            foreach (CharStateBuffData charStateBuffData in allCharStateBuffs)
            {


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

        public void RoundTick(int roundNo)
        {
            if (allCharStateBuffs.Count == 0) return; 
            foreach (CharStateBuffData stateData in allCharStateBuffs)
            {
                if (stateData.timeFrame == TimeFrame.EndOfRound)
                {
                    if (stateData.currentTime >= stateData.netTime)
                    {
                        allCharStateBuffs.Remove(stateData); // list
                        RemoveCharState(stateData.charStateModData.charStateName);
                    }
                    stateData.currentTime++;
                }
            }
        }

        public void EOCTick()
        {
            foreach (CharStateBuffData stateData in allCharStateBuffs)
            {
                if (stateData.timeFrame == TimeFrame.EndOfCombat)
                {
                    allCharStateBuffs.Remove(stateData); // list
                    RemoveCharState(stateData.charStateModData.charStateName);
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
                        buffData.currentTime = 0;
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
