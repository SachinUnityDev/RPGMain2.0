﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Soaked : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Soaked;
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {
        
            //-2 morale.... immune to burning...	+24 fire res....-40 air res
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                       , charID, AttribName.morale, -2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                     , charID, AttribName.fireRes, +24, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                     , charID, AttribName.airRes, -40, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

        
            List<int> immuneDOTBuffIDs =     
            charController.charStateController
                .ApplyDOTImmunityBuff(CauseType.CharState, (int)charStateName
                   , charID, CharStateName.BurnHighDOT, timeFrame, castTime, false);

            allImmunityBuffs.AddRange(immuneDOTBuffIDs);
        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "-2 Morale";
            charStateCardStrs.Add(str0);

            str1 = "+24 Fire Res and -40 Air Res";
            charStateCardStrs.Add(str1);

            str2 = "Immune to <style=Burn> Burning </style>";
            charStateCardStrs.Add(str2);
        }
    }
}
