using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{
    public class Flatfooted : CharStatesBase
    {
        //-3 Haste
        public override CharStateName charStateName => CharStateName.FlatFooted;    
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
             , charID, AttribName.haste, -3, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            SkillService.Instance.ignoreHasteChk = true; 
        }
        public override void EndState()
        {
            base.EndState();
            SkillService.Instance.ignoreHasteChk = false;
        }
        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "-3 Haste";
            charStateCardStrs.Add(str0);

            str1 = "No extra Move action";
            charStateCardStrs.Add(str1);
        }
    }
}
