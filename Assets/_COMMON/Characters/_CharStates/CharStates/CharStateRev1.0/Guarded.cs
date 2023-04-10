using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Guarded : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Guarded;
        public override CharController charController { get; set; }
        public override int charID { get; set; }

        public override StateFor stateFor => StateFor.Heroes;

        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, AttribName.morale, 1, timeFrame, castTime, true);
            allBuffIds.Add(buffID);
        }

        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "+1 Morale";
            charStateCardStrs.Add(str0);

            str1 = "Can't be single targeted";
            charStateCardStrs.Add(str1);
            
            str2 = "Single target attacks diverted to guarding ally";
            charStateCardStrs.Add(str2);
        }
    }
}