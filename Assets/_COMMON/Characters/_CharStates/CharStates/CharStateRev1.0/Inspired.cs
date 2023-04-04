using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Common
{

    public class Inspired : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Inspired;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {
            //+1 Morale on allies - 1 Focus on self    Immune to Despaired
            CharService.Instance.ApplyBuffOnPartyExceptSelf(CauseType.CharState, (int)charStateName
                               , charID, AttribName.morale, +1, charStateModel.timeFrame, charStateModel.castTime, true, CharMode.Ally);

            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.focus, -1, charStateModel.timeFrame, charStateModel.castTime, true);

            charController.charStateController
                .ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                   , charID, CharStateName.Despaired, charStateModel.timeFrame, charStateModel.castTime);
            charController.OnStatCurrValSet += Tick2;
        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {     

            str0 = "+1 Morale on allies";

            str1 = "-1 Focus on self";

            str2 = "Immune to <style=States> Despaired </style>";
            //SkillServiceView.Instance.skillCardData.descLines.Add(str1);

            // str0 = "<style=Morale> "; 
            // add to charStateModel .. use this during skill Init
            // relieve charController from string duty ...
        }

        void Tick2(CharModData charModData)  //  change Stat subscribe 
        {
            if (charModData.statModified != AttribName.morale)
                return;

            float maxL = charController.GetStatChanceData(AttribName.morale).maxLimit;
            if (charController.GetStat(AttribName.morale).currValue < maxL)  // Exit condition 
            {
                EndState();
            }
        }
    }
}

