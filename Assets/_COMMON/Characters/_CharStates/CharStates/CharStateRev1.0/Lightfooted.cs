using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Common
{
    public class Lightfooted : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Lightfooted;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        //+1 Haste on allies 	-1 Luck on self Immune to Rooted
        public override void StateApplyFX()
        {
            allBuffIds.AddRange
                 (CharService.Instance.ApplyBuffOnPartyExceptSelf(CauseType.CharState, (int)charStateName
                                , charID, AttribName.haste, +1, charStateModel.timeFrame, charStateModel.castTime, true, CharMode.Ally));
            allBuffIds.Add(charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.luck, -1, charStateModel.timeFrame, charStateModel.castTime, true));

            int immuneBuff =
            charController.charStateController
                .ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                   , charID, CharStateName.Rooted, charStateModel.timeFrame, charStateModel.castTime);
            allImmunityBuffs.Add(immuneBuff);
            charController.OnStatCurrValSet += Tick2;
        }

     
        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "+1 Haste on allies";
            charStateModel.charStateCardStrs.Add(str0);

            str1 = "-1 Luck on self";
            charStateModel.charStateCardStrs.Add(str1);

            str2 = "Immune to<style=States> Rooted </style>";
            charStateModel.charStateCardStrs.Add(str2);
        }

        void Tick2(CharModData charModData)  //  change Stat subscribe 
        {
            if (charModData.statModified != AttribName.haste)
                return;

            float maxL = charController.GetStatChanceData(AttribName.haste).maxLimit;
            if (charController.GetStat(AttribName.haste).currValue < maxL)  // Exit condition 
            {
                EndState();
            }
        }
    }
}
