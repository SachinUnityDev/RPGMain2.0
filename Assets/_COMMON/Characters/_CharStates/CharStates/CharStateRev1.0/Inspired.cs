using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Common
{

    public class Inspired : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Inspired;
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            //+1 Morale on allies - 1 Focus on self    Immune to Despaired
            allBuffIds.AddRange(
            CharService.Instance.ApplyBuffOnPartyExceptSelf(CauseType.CharState, (int)charStateName
                               , charID, AttribName.morale, +1, timeFrame, castTime, true, CharMode.Ally));

            int buffId = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.morale, +3, timeFrame, castTime, true);
            allBuffIds.Add(buffId);
            
            buffId = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.focus, -1, timeFrame, castTime, false);
            allBuffIds.Add(buffId); 

            int immunityID = 
            charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                                                , charID, CharStateName.Despaired, timeFrame, castTime);
            allImmunityBuffs.Add(immunityID);
            charController.OnAttribCurrValSet += Tick2;
        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {     

            str0 = "+1 Morale on allies";
            allStateFxStrs.Add(str0);
            str1 = "-1 Focus on self";
            allStateFxStrs.Add(str1);
            str2 = "Immune to <style=States> Despaired </style>";
            allStateFxStrs.Add(str2);            
        }

        void Tick2(AttribModData charModData)  //  change Stat subscribe 
        {
            if (charModData.attribModified != AttribName.morale)
                return;

            float maxL = charController.GetAttrib(AttribName.morale).maxLimit;
            if (charController.GetAttrib(AttribName.morale).currValue < maxL)  // Exit condition 
            {
                charController.charStateController.RemoveCharState(charStateName);
            }
        }
    }
}

