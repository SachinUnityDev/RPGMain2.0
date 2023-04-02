using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{


    public class Rooted : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Rooted;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {  // Can't use Move skills	
           // Melee attack limit: pos 1 -> pos 1
           //-6 Dodge
           //	Immune to Lightfooted

            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, StatsName.dodge, -6, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            int immuneBuffID = charController.charStateController
                .ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                   , charID, CharStateName.Lightfooted, charStateModel.timeFrame, charStateModel.castTime);

            allImmunityBuffs.Add(immuneBuffID);

        }
        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "Can't use Move skills";
            charStateModel.charStateCardStrs.Add(str0);

            str1 = "Melee attack limit: pos 1 -> pos 1";
            charStateModel.charStateCardStrs.Add(str1);

            str2 = "-6 Dodge";
            charStateModel.charStateCardStrs.Add(str2);

            str3 = "Immune to<style=States> Inspired </style>";
            charStateModel.charStateCardStrs.Add(str3);
        }
    }
}

