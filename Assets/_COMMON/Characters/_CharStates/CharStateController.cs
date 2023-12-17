using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System.Linq;
using System;
using Quest;
using UnityEngine.SceneManagement;

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

        [Header(" All Char State Models")]
        public List<CharStateModel> allCharStateModels = new List<CharStateModel>();    


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
            SceneManager.sceneLoaded += OnSceneLoaded;
            charController.OnStatChg += StatChg;
            charController.OnAttribCurrValSet += AttribChg; 
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            charController.OnStatChg -= StatChg;
            charController.OnAttribCurrValSet -= AttribChg;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                CombatEventService.Instance.OnEOR1 += RoundTick;
                CombatEventService.Instance.OnEOC += EOCTick;               
            }
            else
            {
                CombatEventService.Instance.OnEOR1 -= RoundTick;
                CombatEventService.Instance.OnEOC -= EOCTick;
            }
        }

        #region BUFF & DEBUFF
        public int ApplyCharStateBuff(CauseType causeType, int causeName, int causeByCharID
                                , CharStateName charStateName, TimeFrame timeFrame = TimeFrame.Infinity, int castTime =-1)
        {
            if (!IsCharStateApplicable(charStateName)) return -1; // if char State not applicable
            // check immunity list 
            if (IsDOT(charStateName))
            {
                if (HasDOTImmunity(charStateName)) return -1; 
            }
            else
            {
                if (HasImmunity(charStateName))
                    return -1;
                if (HasCharState(charStateName))                
                    return -1;
            }
        

            int effectedCharID = charController.charModel.charID;

            int currRd = -1;
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                currRd = CombatService.Instance.currentRound;
            }
            CharStateModData charStateModData = new CharStateModData(causeType, causeName, causeByCharID
                                                                    , effectedCharID, charStateName);
            stateID++;// to be corrected
            allBuffIds.Add(stateID);
            
      
            CharStatesBase charStateBase = CharStatesService.Instance.GetNewCharState(charStateName);
            allCharBases.Add(charStateBase);
            CharStateSO1 charStateSO = CharStatesService.Instance.allCharStateSO.GetCharStateSO(charStateName);

            // char State init only when its applied 
            charStateBase.StateInit(charStateSO, charController, timeFrame, castTime, stateID);
            charStateBase.StateBaseApply();
            charStateBase.StateApplyFX();
            charStateBase.StateApplyVFX();


            CharStateBuffData charStateBuffData = new CharStateBuffData(stateID, currRd, timeFrame, castTime
                                                                        , charStateModData); 

            allCharStateBuffs.Add(charStateBuffData);
           
            CharStatesService.Instance.On_CharStateStart(charStateModData);
            //if (!IsDOT(charStateName))  // no char State can occur twice to the same char ...DOT have independent rules
            //{
            //    ApplyImmunityBuff(causeType, causeName, causeByCharID, charStateName, timeFrame, castTime);
            //}

            return stateID;
        }

        bool IsCharStateApplicable(CharStateName charStateName)
        {
            CharStateSO1 charStateSO = CharStatesService.Instance.allCharStateSO.GetCharStateSO(charStateName);
            CharMode charMode = charController.charModel.orgCharMode; 
            if(charStateSO.stateFor == StateFor.Mutual)
            {
                return true; 
            }
            if (charStateSO.stateFor == StateFor.Heroes)
            {
                return charMode == CharMode.Ally;                 
            }
            return false; 
        }


        public void RemoveCharState(CharStateName charStateName)
        {
            int index = allCharBases.FindIndex(t => t.charStateName == charStateName);
            if (index != -1)
            {
                allCharBases[index].ClearBuffs();   // all related buffs to be cleared from here
                allCharBases.RemoveAt(index);
                RemoveImmunityByCharState(charStateName);
            }
            int index2 = allCharStateModels.FindIndex(t => t.charStateName == charStateName);
            if (index != -1)
            {
                allCharStateModels.RemoveAt(index2);
            }
        }
        public void ResetCharStateBuff(CharStateName charStateName)
        {
            foreach (CharStateBuffData buffData in allCharStateBuffs)
            {
                //if(buffData.charStateModData.causeType == CauseType.CharSkill)
                //{
                if (buffData.charStateModData.causeName == (int)charStateName)
                {
                    buffData.currentTime = 0;
                }
                //}
            }
        }

        #endregion

        #region  IMMUNITY 
        public int ApplyImmunityBuff(CauseType causeType, int causeName, int causeByCharID
                                , CharStateName charStateName, TimeFrame timeFrame, int netTime) // immunity buff for this char State
        {
            charController = GetComponent<CharController>();

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
            foreach (ImmunityBuffData immunityBuffData in allImmunityBuffs.ToList())
            {
                if (immunityBuffData.charStateModData.charStateName == charStateName)
                {
                    allImmunityBuffs.Remove(immunityBuffData);
                }
            }
        }

        public void RemoveImmunityBuff(int immunityID)
        {
            foreach (ImmunityBuffData immunityBuffData in allImmunityBuffs.ToList())
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
        #endregion

        #region HAS STATE CHECKS 
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

        #endregion

        #region TICKS
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

        #region CLEAR DOT

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
        public bool IsDOT(CharStateName charStateName)
        {
            if (charStateName == CharStateName.PoisonedHighDOT
              || charStateName == CharStateName.PoisonedMedDOT
              || charStateName == CharStateName.PoisonedLowDOT)
            {
                return true; 
            }
           if (charStateName == CharStateName.BleedHighDOT
                  || charStateName == CharStateName.BleedMedDOT
                  || charStateName == CharStateName.BleedLowDOT)
           {
                return true;
           }

           if (charStateName == CharStateName.BurnHighDOT
                || charStateName == CharStateName.BurnMedDOT
                || charStateName == CharStateName.BurnLowDOT)
           {
                return true;
           }
            return false; 
        }


        # endregion 
        public CharStatesBase GetCurrCharStateBase(CharStateName _charStateName)
        {
            if (allCharBases.Any(t=>t.charStateName == _charStateName))
            {
                return allCharBases.Find(t => t.charStateName == _charStateName); 
            }
            Debug.Log("Char State Base Not found"); 
            return null; 
        }

        public CharStateBuffData GetCharStateBuffData(CharStateName charStateName) 
        {
            int index =
            allCharStateBuffs.FindIndex(t => t.charStateModData.charStateName == charStateName); 
            if(index!= -1)
            {
                return allCharStateBuffs[index]; 
            }
            else
            {
                Debug.Log(" charState buff not found" + charStateName);
            }
            return null; 
        }

        #region STATE TRIGGERS 

        // hp =0 

        void StatChg(StatModData statModData)
        {
            if(statModData.statModified == StatName.health)
            {
                if(statModData.modVal== 0 && charController.charModel.orgCharMode == CharMode.Ally)
                {
                    ApplyCharStateBuff(CauseType.StatChange, (int)statModData.statModified, statModData.causeByCharID,
                        CharStateName.LastDropOfBlood); 
                }else if (HasCharState(CharStateName.LastDropOfBlood) && statModData.modVal > 0)
                {
                    RemoveCharState(CharStateName.LastDropOfBlood); 
                }
            }
            if (statModData.statModified == StatName.stamina)
            {
                //if (statModData.modVal == 0)
                //{
                //    ApplyCharStateBuff(CauseType.StatChange, (int)statModData.statModified, statModData.causeByCharID,
                //        CharStateName.LastBreath);
                //}
                //else if (HasCharState(CharStateName.LastBreath) && statModData.modVal >0)
                //{
                //    RemoveCharState(CharStateName.LastBreath);
                //}
            }
            if (statModData.statModified == StatName.fortitude)
            {
                if (statModData.modVal == -30)
                {
                    ApplyCharStateBuff(CauseType.StatChange, (int)statModData.statModified, statModData.causeByCharID,
                        CharStateName.Fearful, TimeFrame.EndOfRound, 2);
                }
                if (statModData.modVal == +30)
                {
                    ApplyCharStateBuff(CauseType.StatChange, (int)statModData.statModified, statModData.causeByCharID,
                        CharStateName.Faithful, TimeFrame.EndOfRound, 2);
                }
            }
        }

        void AttribChg(AttribModData attribModData) 
        {
            if (attribModData.attribModified == AttribName.focus)
            {
                if (attribModData.modCurrVal == 12f)
                {
                    ApplyCharStateBuff(CauseType.AttribMinMaxLimit, (int)attribModData.attribModified, attribModData.causeByCharID,
                      CharStateName.Concentrated);
                }
                else if (HasCharState(CharStateName.Concentrated) && attribModData.modCurrVal < 12)
                {
                    CharStateBuffData charStateBuffData = GetCharStateBuffData(CharStateName.Concentrated);
                    if(charStateBuffData.charStateModData.causeType == CauseType.AttribMinMaxLimit)
                        RemoveCharState(CharStateName.Concentrated);
                }
                if (attribModData.modCurrVal == 0f)
                {
                    ApplyCharStateBuff(CauseType.AttribMinMaxLimit, (int)attribModData.attribModified, attribModData.causeByCharID,
                      CharStateName.Confused);
                }
                else if (HasCharState(CharStateName.Confused) && attribModData.modCurrVal > 0)
                {
                    CharStateBuffData charStateBuffData = GetCharStateBuffData(CharStateName.Confused);
                    if (charStateBuffData.charStateModData.causeType == CauseType.AttribMinMaxLimit)
                        RemoveCharState(CharStateName.Confused);
                }
            }
            if (attribModData.attribModified == AttribName.luck)
            {
                if (attribModData.modCurrVal == 12f)
                {
                    ApplyCharStateBuff(CauseType.AttribMinMaxLimit, (int)attribModData.attribModified, attribModData.causeByCharID,
                      CharStateName.LuckyDuck);
                }
                else if (HasCharState(CharStateName.LuckyDuck) && attribModData.modCurrVal < 12)
                {
                    CharStateBuffData charStateBuffData = GetCharStateBuffData(CharStateName.LuckyDuck);
                    if (charStateBuffData.charStateModData.causeType == CauseType.AttribMinMaxLimit)
                        RemoveCharState(CharStateName.LuckyDuck);
                }
                if (attribModData.modCurrVal == 0f)
                {
                    ApplyCharStateBuff(CauseType.AttribMinMaxLimit, (int)attribModData.attribModified, attribModData.causeByCharID,
                      CharStateName.Feebleminded);
                }
                else if (HasCharState(CharStateName.Feebleminded) && attribModData.modCurrVal > 0)
                {
                    CharStateBuffData charStateBuffData = GetCharStateBuffData(CharStateName.Feebleminded);
                    if (charStateBuffData.charStateModData.causeType == CauseType.AttribMinMaxLimit)
                        RemoveCharState(CharStateName.Feebleminded);
                }
            }
            if (attribModData.attribModified == AttribName.haste)
            {
                if (attribModData.modCurrVal == 12f)
                {
                    ApplyCharStateBuff(CauseType.AttribMinMaxLimit, (int)attribModData.attribModified, attribModData.causeByCharID,
                      CharStateName.Lissome);
                }
                else if (HasCharState(CharStateName.Lissome) && attribModData.modCurrVal < 12)
                {
                    CharStateBuffData charStateBuffData = GetCharStateBuffData(CharStateName.Lissome);
                    if (charStateBuffData.charStateModData.causeType == CauseType.AttribMinMaxLimit)
                        RemoveCharState(CharStateName.Lissome);
                }
                if (attribModData.modCurrVal == 0f)
                {
                    ApplyCharStateBuff(CauseType.AttribMinMaxLimit, (int)attribModData.attribModified, attribModData.causeByCharID,
                      CharStateName.Rooted);
                }
                else if (HasCharState(CharStateName.Rooted) && attribModData.modCurrVal > 0)
                {
                    CharStateBuffData charStateBuffData = GetCharStateBuffData(CharStateName.Rooted);
                    if (charStateBuffData.charStateModData.causeType == CauseType.AttribMinMaxLimit)
                        RemoveCharState(CharStateName.Rooted);
                }

            }
            if (attribModData.attribModified == AttribName.morale)
            {
                if (attribModData.modCurrVal == 12f)
                {
                    ApplyCharStateBuff(CauseType.AttribMinMaxLimit, (int)attribModData.attribModified, attribModData.causeByCharID,
                      CharStateName.Inspired);
                }
                else if (HasCharState(CharStateName.Inspired) && attribModData.modCurrVal < 12)
                {
                    CharStateBuffData charStateBuffData = GetCharStateBuffData(CharStateName.Inspired);
                    if (charStateBuffData.charStateModData.causeType == CauseType.AttribMinMaxLimit)
                        RemoveCharState(CharStateName.Inspired);
                }
                if (attribModData.modCurrVal == 0f)
                {
                    ApplyCharStateBuff(CauseType.AttribMinMaxLimit, (int)attribModData.attribModified, attribModData.causeByCharID,
                      CharStateName.Despaired);
                }
                else if (HasCharState(CharStateName.Despaired) && attribModData.modCurrVal > 0)
                {
                    CharStateBuffData charStateBuffData = GetCharStateBuffData(CharStateName.Despaired);
                    if (charStateBuffData.charStateModData.causeType == CauseType.AttribMinMaxLimit)
                        RemoveCharState(CharStateName.Despaired);
                }
            }
            // Earth Res

            if (attribModData.attribModified == AttribName.earthRes)
            {
                if (attribModData.modCurrVal == 80f)
                {
                    ApplyCharStateBuff(CauseType.AttribMinMaxLimit, (int)attribModData.attribModified, attribModData.causeByCharID,
                      CharStateName.Calloused);
                }
                else if (HasCharState(CharStateName.Calloused) && attribModData.modCurrVal < 80)
                {
                    CharStateBuffData charStateBuffData = GetCharStateBuffData(CharStateName.Calloused);
                    if (charStateBuffData.charStateModData.causeType == CauseType.AttribMinMaxLimit)
                        RemoveCharState(CharStateName.Calloused);
                }
                
            }
            if (attribModData.attribModified == AttribName.airRes)
            {
                if (attribModData.modCurrVal == 80f)
                {
                    ApplyCharStateBuff(CauseType.AttribMinMaxLimit, (int)attribModData.attribModified, attribModData.causeByCharID,
                      CharStateName.Charged);
                }
                else if (HasCharState(CharStateName.Charged) && attribModData.modCurrVal < 80)
                {
                    CharStateBuffData charStateBuffData = GetCharStateBuffData(CharStateName.Charged);
                    if (charStateBuffData.charStateModData.causeType == CauseType.AttribMinMaxLimit)
                        RemoveCharState(CharStateName.Charged);
                }
            }
            if (attribModData.attribModified == AttribName.fireRes)
            {
                if (attribModData.modCurrVal == 80f)
                {
                    ApplyCharStateBuff(CauseType.AttribMinMaxLimit, (int)attribModData.attribModified, attribModData.causeByCharID,
                      CharStateName.Enraged);
                }
                else if (HasCharState(CharStateName.Enraged) && attribModData.modCurrVal < 80)
                {
                    CharStateBuffData charStateBuffData = GetCharStateBuffData(CharStateName.Enraged);
                    if (charStateBuffData.charStateModData.causeType == CauseType.AttribMinMaxLimit)
                        RemoveCharState(CharStateName.Enraged);
                }
            }
            if (attribModData.attribModified == AttribName.waterRes)
            {
                if (attribModData.modCurrVal == 80f)
                {
                    ApplyCharStateBuff(CauseType.AttribMinMaxLimit, (int)attribModData.attribModified, attribModData.causeByCharID,
                      CharStateName.Aquaborne);
                }
                else if (HasCharState(CharStateName.Aquaborne) && attribModData.modCurrVal < 80)
                {
                    CharStateBuffData charStateBuffData = GetCharStateBuffData(CharStateName.Aquaborne);
                    if (charStateBuffData.charStateModData.causeType == CauseType.AttribMinMaxLimit)
                        RemoveCharState(CharStateName.Aquaborne);
                }
            }
            if (attribModData.attribModified == AttribName.lightRes)
            {
                if (attribModData.modCurrVal == 60f)
                {
                    ApplyCharStateBuff(CauseType.AttribMinMaxLimit, (int)attribModData.attribModified, attribModData.causeByCharID,
                      CharStateName.Radiant);
                }
                else if (HasCharState(CharStateName.Radiant) && attribModData.modCurrVal < 60)
                {
                    CharStateBuffData charStateBuffData = GetCharStateBuffData(CharStateName.Radiant);
                    if (charStateBuffData.charStateModData.causeType == CauseType.AttribMinMaxLimit)
                        RemoveCharState(CharStateName.Radiant);
                }
            }
            if (attribModData.attribModified == AttribName.darkRes)
            {
                if (attribModData.modCurrVal == 60f)
                {
                    ApplyCharStateBuff(CauseType.AttribMinMaxLimit, (int)attribModData.attribModified, attribModData.causeByCharID,
                      CharStateName.Lunatic);
                }
                else if (HasCharState(CharStateName.Lunatic) && attribModData.modCurrVal < 60)
                {
                    CharStateBuffData charStateBuffData = GetCharStateBuffData(CharStateName.Lunatic);
                    if (charStateBuffData.charStateModData.causeType == CauseType.AttribMinMaxLimit)
                        RemoveCharState(CharStateName.Lunatic);
                }
            }
        }

        #endregion


    }




}
