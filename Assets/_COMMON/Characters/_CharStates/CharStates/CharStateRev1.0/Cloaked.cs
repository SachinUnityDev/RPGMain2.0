using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Cloaked : CharStatesBase
    {
        //+14 Dark Res
        // u lose this char state upon use of a attack skill 


        public override CharStateName charStateName => CharStateName.Cloaked;
        public override CharController charController { get; set; }
        public override int charID { get; set; }

        public override StateFor stateFor => StateFor.Mutual;

        public override int castTime { get ; protected set; }

        public override void StateApplyFX()
        {
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
          , charID, AttribName.darkRes, 14, timeFrame, castTime, true);
            allBuffIds.Add(buffID);
        }

        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "Can't be single targeted";
            charStateCardStrs.Add(str0);

            str1 = "+14 Dark Res";
            charStateCardStrs.Add(str1);

            str2 = "Lost upon attacking";
            charStateCardStrs.Add(str2);
        }
    }
}