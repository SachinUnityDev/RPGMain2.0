using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class LuckyDuck : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.LuckyDuck;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {
            allBuffIds.AddRange(
            CharService.Instance.ApplyBuffOnPartyExceptSelf(CauseType.CharState, (int)charStateName
                         , charID, StatsName.luck, +1, charStateModel.timeFrame, charStateModel.castTime, true, CharMode.Ally));
            int buffId = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, StatsName.haste, -1, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffId); 

            int immuneBuffID =
            charController.charStateController
                    .ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                       , charID, CharStateName.Feebleminded, charStateModel.timeFrame, charStateModel.castTime);
            allImmunityBuffs.Add(immuneBuffID);

            charController.OnStatCurrValSet += Tick2;

        }

        public override void StateApplyVFX()
        {

        }
        public override void StateDisplay()
        {           
            str0 = "+1 Luck for allies";
            charStateModel.charStateCardStrs.Add(str0);
            str1 = "-1 Haste for self";
            charStateModel.charStateCardStrs.Add(str1);
            str2 = "Immune to<style=States> Feebleminded </style>";
            charStateModel.charStateCardStrs.Add(str2);  
        }

        void Tick2(CharModData charModData)  //  change Stat subscribe 
        {
            if (charModData.statModified != StatsName.luck)
                return;

            float maxL = charController.GetStatChanceData(StatsName.luck).maxLimit;
            if (charController.GetStat(StatsName.luck).currValue < maxL)  // Exit condition 
            {
                EndState();
            }
        }
    }
}
