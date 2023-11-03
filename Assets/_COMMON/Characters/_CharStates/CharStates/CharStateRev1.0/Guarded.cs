using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Guarded : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Guarded;
        public override StateFor stateFor => StateFor.Heroes;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
           // 	+1 Morale
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, AttribName.morale, 1, timeFrame, castTime, true);
              allBuffIds.Add(buffID);

            // Can't be single targeted	......done thru On_TargetSelect ..CombatEventService
            // Single target attacks diverted to guarding ally ...done thru On_TargetSelect ..CombatEventService

        }

        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "+1 Morale";
            allStateFxStrs.Add(str0);

            str1 = "Single target attacks diverted to guarding ally";
            allStateFxStrs.Add(str1);
        }
    }
}