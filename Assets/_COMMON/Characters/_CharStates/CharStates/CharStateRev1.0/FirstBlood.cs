using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class FirstBlood : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.FirstBlood;    
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        List<float> chances;
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                   , charID, AttribName.focus, +1, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.morale, +1, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.luck, +1, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.haste, +1, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            // overriden the end of State
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.fortOrg, +1, TimeFrame.EndOfQuest, 1, true); 
        }
        

        public override void StateApplyVFX()
        {

        }
        public override void StateDisplay()
        {
            str0 = "+1 Utility Attributes";
            charStateCardStrs.Add(str0);

            str1 = "+1 Fort Org until eoq";
            charStateCardStrs.Add(str1);

        }

    }
}