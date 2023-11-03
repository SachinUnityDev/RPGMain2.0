using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Hexed : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Hexed;
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }      
        public override void StateApplyFX()
        {
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                   , charID, AttribName.focus, -3, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
               , charID, AttribName.luck, -3, timeFrame, castTime, false);
            allBuffIds.Add(buffID);
        }
        
        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "-3 Focus and -3 Luck";
            allStateFxStrs.Add(str0);

            str1 = "Becomes AI priority";
            allStateFxStrs.Add(str1);

        }
    }
}