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
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        //+1 Haste on allies 	-1 Luck on self Immune to Rooted
        public override void StateApplyFX()
        {
            allBuffIds.AddRange
                 (CharService.Instance.ApplyBuffOnPartyExceptSelf(CauseType.CharState, (int)charStateName
                                , charID, AttribName.haste, +1, timeFrame, castTime, true, CharMode.Ally));
            allBuffIds.Add(charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.luck, -1, timeFrame, castTime, true));

            int immuneBuff =
            charController.charStateController
                .ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                   , charID, CharStateName.Rooted, timeFrame, castTime);
            allImmunityBuffs.Add(immuneBuff);
            charController.OnAttribCurrValSet += Tick2;
        }

     
        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "+1 Haste on allies";
            charStateCardStrs.Add(str0);

            str1 = "-1 Luck on self";
            charStateCardStrs.Add(str1);

            str2 = "Immune to<style=States> Rooted </style>";
            charStateCardStrs.Add(str2);
        }

        void Tick2(AttribModData charModData)  //  change Stat subscribe 
        {
            if (charModData.attribModified != AttribName.haste)
                return;

            float maxL = charController.GetAttrib(AttribName.haste).maxLimit;
            if (charController.GetAttrib(AttribName.haste).currValue < maxL)  // Exit condition 
            {
                EndState();
            }
        }
    }
}
