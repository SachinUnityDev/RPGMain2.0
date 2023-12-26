using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Common
{

    public class Concentrated : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Concentrated;
        public override StateFor stateFor => StateFor.Mutual; 
        public override int castTime { get;protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            allBuffIds.AddRange(CharService.Instance.ApplyBuffOnPartyExceptSelf(CauseType.CharState, (int)charStateName
                       , charID, AttribName.focus, +1, timeFrame, castTime, true, CharMode.Ally));

            int buffId = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                   , charID, AttribName.morale, -1, timeFrame, castTime, false);

            allBuffIds.Add( buffId );

            allImmunityBuffs.Add(charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                   , charID, CharStateName.Confused,  timeFrame, castTime));

            charController.OnAttribCurrValSet += Tick2;
        }

        void Tick2(AttribModData charModData)  //  change Stat subscribe 
        {
            if (charModData.attribModified != AttribName.focus)
                return;

            float maxL = charController.GetAttrib(AttribName.focus).maxLimit;
            if (charController.GetAttrib(AttribName.luck).currValue < maxL)  // Exit condition 
            {
                charController.charStateController.RemoveCharState(charStateName);
            }
        }

        public override void StateApplyVFX()
        {
           
        }

        public override void StateDisplay()
        {
            //+1 Focus on allies .... - 1 Morale on self   Immune to Confused
            str0 = "+1 Focus on allies";
            allStateFxStrs.Add(str0);
            str1 = "-1 Morale on self";
            allStateFxStrs.Add(str1);
            str2 = "Immune to<style=States> Confused </style>";
            allStateFxStrs.Add(str2);
        }

        public override void EndState()
        {
            base.EndState();
            charController.OnAttribCurrValSet -= Tick2;

        }
    }
}

