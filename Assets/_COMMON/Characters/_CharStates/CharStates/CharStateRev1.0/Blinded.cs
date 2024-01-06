using Combat;
using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Blinded : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Blinded;
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.acc, -5, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.dodge, -3, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.lightRes, 12, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            int immuneBuffID = charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                                                    , charID, CharStateName.Horrified, timeFrame, castTime);
            allImmunityBuffs.Add(immuneBuffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.dmgMax, 3, timeFrame, castTime, true);
            allBuffIds.Add(buffID);
        }
        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "-5 Acc and -3 Dodge";
            allStateFxStrs.Add(str0);

            str1 = "+3 Max Dmg, +12 Light Res";
            allStateFxStrs.Add(str1);

            str2 = "Immune to Horrified";
            allStateFxStrs.Add(str2);

        }
    }
}