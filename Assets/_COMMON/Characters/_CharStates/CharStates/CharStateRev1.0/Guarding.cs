using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Guarding : CharStatesBase
    {
        //Single target attacks diverted from guarded ally	+1 Luck
        public override CharStateName charStateName => CharStateName.XXXX;

        public override StateFor stateFor => StateFor.Mutual;

        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, AttribName.luck, +1, timeFrame, castTime, true);
            allBuffIds.Add(buffID);
        }

        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "+1 Luck";
            allStateFxStrs.Add(str0);

            str1 = "Single target attacks diverted from guarded ally";
            allStateFxStrs.Add(str1);
        }
    }
}