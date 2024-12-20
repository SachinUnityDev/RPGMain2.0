﻿using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Common
{
    public class Lissome : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Lissome;        
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        //+1 Haste on allies 	-1 Luck on self Immune to Rooted
        public override void StateApplyFX()
        {
            allBuffIds.AddRange
                 (CharService.Instance.ApplyBuffOnPartyExceptSelf(CauseType.CharState, (int)charStateName
                                , charID, AttribName.haste, +1, timeFrame, castTime, true, CharMode.Ally));

           int buffId = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                                        , charID, AttribName.haste, +3, timeFrame, castTime, true); 
            allBuffIds.Add(buffId);

            allBuffIds.Add(charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.luck, -1, timeFrame, castTime, false));

            int immuneBuff = charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName
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
            allStateFxStrs.Add(str0);

            str1 = "+3 Haste and -1 Luck on self";
            allStateFxStrs.Add(str1);

            str2 = "Immune to<style=States> Rooted </style>";
            allStateFxStrs.Add(str2);
        }

        void Tick2(AttribModData charModData)  //  change Stat subscribe 
        {
            if (charModData.attribModified != AttribName.haste)
                return;

            float maxL = charController.GetAttrib(AttribName.haste).maxLimit;
            if (charController.GetAttrib(AttribName.haste).currValue < maxL)  // Exit condition 
            {
                charController.charStateController.RemoveCharState(charStateName);
            }
        }
        public override void EndState()
        {
            base.EndState();
            charController.OnAttribCurrValSet -= Tick2;

        }
    }
}
