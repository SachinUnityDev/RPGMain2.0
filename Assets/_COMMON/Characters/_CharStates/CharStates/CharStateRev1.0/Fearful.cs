using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{
    public class Fearful : CharStatesBase
    {
        // Immune to Fortitude attacks for 2 rds and after 2 rds go back to origin
        //	-3 utility stats, -3 Dodge and -(2-3) Armor, -30 resistances	
        //	%60 flee, 40% nothing happens	
        public override CharStateName charStateName => CharStateName.Fearful;
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Heroes;
        public override int castTime { get ; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            castTime = 2; 
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                   , charID, AttribName.focus, -3, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.morale, -3, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.luck, -3, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.haste, -3, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
          , charID, AttribName.dodge, -3, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, AttribName.armorMin, -2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);
            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.armorMax, -3, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                 , charID, AttribName.earthRes, -30, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                 , charID, AttribName.waterRes, -30, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                 , charID, AttribName.fireRes, -30, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                 , charID, AttribName.airRes, -30, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            charController.ClampStatToggle(StatName.fortitude, true);

        }

        public override void StateApplyVFX()
        {
            
        }
        public override void EndState()
        {
            base.EndState();
            charController.ClampStatToggle(StatName.fortitude, false);
        }
        public override void StateDisplay()
        {
            str0 = "-3 Utility Stats,-3 Dodge";
            charStateCardStrs.Add(str0);

            str1 = "-(2-3) Armor, -30 Elemental Res";
            charStateCardStrs.Add(str1);

            str2 = "May Flee Combat";
            charStateCardStrs.Add(str2);
        }
    }
}