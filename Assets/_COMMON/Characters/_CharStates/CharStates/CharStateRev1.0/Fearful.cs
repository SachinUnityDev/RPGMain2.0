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
        public override StateFor stateFor => StateFor.Heroes;
        public override int castTime { get ; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            castTime = 2; 
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                   , charID, AttribName.focus, -3, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.morale, -3, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.luck, -3, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.haste, -3, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.dodge, -3, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            //buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            //, charID, AttribName.armorMin, -2, timeFrame, castTime, true);
            //allBuffIds.Add(buffID);
            //buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            //      , charID, AttribName.armorMax, -3, timeFrame, castTime, true);
            //allBuffIds.Add(buffID);

            allBuffIds.AddRange(charController.buffController.BuffAllRes(CauseType.CharState, (int)charStateName
                 , charID, -20, timeFrame, castTime, false));

            CombatEventService.Instance.OnCharOnTurnSet += LoseAP;
            charController.ClampStatToggle(StatName.fortitude, true);
            charController.OnStatChg += FortChkOnStatDecr; 
        }

        void FortChkOnStatDecr(StatModData statModData)
        {
            if (statModData.statModified != StatName.fortitude) return;
            if (statModData.valChg > 0) return;
            FortChks(); 
            
        }
        void FortChks()
        {
            // Flee Combat /// flee quest // nothing happens
        }

        void LoseAP(CharController charController)
        {
            if (charController.charModel.charID == charID)
                charController.combatController.actionPts--;
        }
        public override void StateApplyVFX()
        {
            
        }
        public override void EndState()
        {
            base.EndState();
            charController.ClampStatToggle(StatName.fortitude, false);
            CombatEventService.Instance.OnCharOnTurnSet -= LoseAP;
            charController.OnStatChg -= FortChkOnStatDecr;
        }
        public override void StateDisplay()
        {
            str0 = "-3 Utility Attributes";
            allStateFxStrs.Add(str0);

            str1 = "-20 All Res";
            allStateFxStrs.Add(str1);

            str2 = "-1 AP";
            allStateFxStrs.Add(str2);
        }
    }
}