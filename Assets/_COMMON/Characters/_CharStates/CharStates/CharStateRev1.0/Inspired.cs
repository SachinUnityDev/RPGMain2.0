using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Common
{

    public class Inspired : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Inspired;
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {
            //+1 Morale on allies - 1 Focus on self    Immune to Despaired
            CharService.Instance.ApplyBuffOnPartyExceptSelf(CauseType.CharState, (int)charStateName
                               , charID, AttribName.morale, +1, timeFrame, castTime, true, CharMode.Ally);

            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.focus, -1, timeFrame, castTime, true);

            charController.charStateController
                .ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                   , charID, CharStateName.Despaired, timeFrame, castTime);
            charController.OnAttribCurrValSet += Tick2;
        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {     

            str0 = "+1 Morale on allies";
            charStateCardStrs.Add(str0);
            str1 = "-1 Focus on self";
            charStateCardStrs.Add(str1);
            str2 = "Immune to <style=States> Despaired </style>";
            charStateCardStrs.Add(str2);            
        }

        void Tick2(AttribModData charModData)  //  change Stat subscribe 
        {
            if (charModData.attribModified != AttribName.morale)
                return;

            float maxL = charController.GetAttrib(AttribName.morale).maxLimit;
            if (charController.GetAttrib(AttribName.morale).currValue < maxL)  // Exit condition 
            {
                EndState();
            }
        }
    }
}

