using Combat;
using System;
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
        public override StateFor stateFor => StateFor.Mutual;

        public override int castTime { get ; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                    , charID, AttribName.darkRes, 14, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            CombatEventService.Instance.OnDamageApplied -= MeleeNRangedAttackClearsCloaked;
            CombatEventService.Instance.OnDamageApplied += MeleeNRangedAttackClearsCloaked; 
        }

        void MeleeNRangedAttackClearsCloaked(DmgAppliedData dmgAppliedData)
        {
            if(dmgAppliedData.striker.charModel.charID == charID)
            {
                if(dmgAppliedData.attackType == AttackType.Melee || dmgAppliedData.attackType == AttackType.Ranged)
                {
                    EndState();
                }
            }
        }

        public override void StateApplyVFX()
        {
            
        }

        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnDamageApplied -= MeleeNRangedAttackClearsCloaked;
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