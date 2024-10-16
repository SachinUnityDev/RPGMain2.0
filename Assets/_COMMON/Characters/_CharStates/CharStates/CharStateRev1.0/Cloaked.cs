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
            allBuffIds.Clear();
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                    , charID, AttribName.darkRes, 14, timeFrame, castTime, true);
            allBuffIds.Add(buffID);
            CombatEventService.Instance.OnDamageApplied -= MeleeNRangedAttackClearsCloaked;
            CombatEventService.Instance.OnDamageApplied += MeleeNRangedAttackClearsCloaked;

            CombatEventService.Instance.OnCharOnTurnSet -= CharOnTurnSet;
            CombatEventService.Instance.OnCharOnTurnSet += CharOnTurnSet;
            // increase dmg of retaliate 
            charController.skillController.GetSkillModel(SkillNames.Retaliate).damageMod += 40f;
          
        }

        private void CharOnTurnSet(CharController _charController)
        {
            if (_charController.charModel.charID != charController.charModel.charID) return;
            List<DynamicPosData> inFrontSameParty = new List<DynamicPosData>();
            CharMode charMode = charController.charModel.charMode;

            inFrontSameParty = GridService.Instance.GetAllInFrontINSameParty(GridService.Instance.GetDyna4GO(charController.gameObject));

            foreach (DynamicPosData dyna in inFrontSameParty)
            {
              int buffID =   charController.buffController.ApplyBuff(CauseType.CharSkill, (int)charStateName,
                                   charController.charModel.charID, AttribName.dodge, +1, TimeFrame.EndOfCombat, 1, true);
                allBuffIds.Add(buffID);
            }
        }   

        void MeleeNRangedAttackClearsCloaked(DmgAppliedData dmgAppliedData)
        {
            if(dmgAppliedData.striker.charModel.charID == charID)
            {
                if(dmgAppliedData.attackType == AttackType.Melee || dmgAppliedData.attackType == AttackType.Ranged)
                {
                    charController.charStateController.RemoveCharState(charStateName);
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
            CombatEventService.Instance.OnCharOnTurnSet -= CharOnTurnSet;
            charController.skillController.GetSkillModel(SkillNames.Retaliate).damageMod -= 40f;
        }

        public override void StateDisplay()
        {
            str0 = "+1 Dodge until eoc, if ally in front in sot";
            allStateFxStrs.Add(str0);

            str1 = "+14 Dark Res";
            allStateFxStrs.Add(str1);

            str2 = "+40% Retaliate dmg";
            allStateFxStrs.Add(str2);

            str3 = "Lost upon attacking";
            allStateFxStrs.Add(str3);


        }
    }
}