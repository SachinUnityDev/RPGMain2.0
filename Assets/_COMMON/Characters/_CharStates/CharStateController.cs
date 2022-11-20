using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System.Linq; 


namespace Common
{
    public class CharStateController : MonoBehaviour
    {
        public List<BuffData> allBuffs = new List<BuffData>();

        CharController charController;
        [SerializeField] List<string> buffStrs = new List<string>();
        [SerializeField] List<string> deDuffStrs = new List<string>();

        public List<CharStatesBase> allCharBases = new List<CharStatesBase>(); 
        
        void Start()
        {
            charController = GetComponent<CharController>();
            CombatEventService.Instance.OnEOR += RoundTick;
            CombatEventService.Instance.OnEOC += EOCTick; 

        }
    #region BUFF & DEBUFF
        public void ApplyBuff(CauseType causeType, int causeName, int causeByCharID
                                , StatsName statName, float value, TimeFrame timeFrame, int netTime, bool isBuff, string directStr = "")
        {
            //CharModData charModVal = charController.ChangeStat(causeType, causeName, causeByCharID
            //                                , statName, value, true);

            //int currRd = CombatService.Instance.currentRound;

            //BuffData buffData = new BuffData(isBuff, currRd, timeFrame, netTime,
            //                                                  charModVal, directStr);
            //allBuffs.Add(buffData);

        }

        //public void ApplyStaminaRegenBuff(CauseType causeType, int causeName, int causeByCharID,
        //    StatsName statName, float StaminaChg, TimeFrame timeFrame, int netTime, bool isBuff)
        //{


        //    CharModData charModVal = charController.ChangeStat(causeType, causeName, causeByCharID,
        //                                statName, StaminaChg); 

        //    int currRd = CombatService.Instance.currentRound;

        //    //BuffData buffData = new BuffData(isBuff, currRd, timeFrame, netTime,
        //    //                                                  charModVal, directStr);



        //}

        public void ApplyHealthRegenBuff(CauseType causeType, int causeName, int causeByCharID, float HpChg
            ,  TimeFrame timeFrame, int netTime, bool isBuff)
        {
            // Should be in the buff controller 
            


        }

        public void ApplyImmunityBuff(CharStateName charStateName) // immunity buff for this char State
        {
            // add immunity and remove after the cast time 


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
            allBuffs.Remove(buffData);
        }

        public List<string> GetBuffList()
        {
            foreach (BuffData buffData in allBuffs)
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
            foreach (BuffData buffData in allBuffs)
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
            foreach (BuffData buffData in allBuffs)
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
            foreach (BuffData buffData in allBuffs)
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
            foreach (BuffData buffData in allBuffs.ToList())
            {
                if (buffData.charModData.causeName == (int)_charStateName)
                {
                    RemoveBuffData(buffData);
                }
            }
        }

        public void UpdateBuffData(CharStateName charStateName)
        {  
            foreach (BuffData buffData in allBuffs)
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
