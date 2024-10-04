using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System.ComponentModel;

namespace Common
{

    public class Faithful : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Faithful;
        public override StateFor stateFor => StateFor.Heroes;
        public override int castTime { get; protected set;}
        public override float chance { get; set; }
        //Immune to Fortitude attacks for 3 rds
        //after 3 rds go back to origin	
        //+2 Focus, haste, Luck, Morale, Dodge and +(2-2) Armor + 20 resistances once per combat
        bool isAPRewardGained = false;
        public override void StateBaseApply()
        {
            base.StateBaseApply();
            CombatEventService.Instance.OnEOR1 -= ResetReward;
            CombatEventService.Instance.OnEOR1 += ResetReward;
        }
        void ResetReward(int rd)
        {
            isAPRewardGained = false;
        }
        public override void StateApplyFX()
        {
            castTime = 2;
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                   , charID, AttribName.focus, 2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.morale, 2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.luck, 2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.haste, 2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

           // buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
           // , charID, AttribName.armorMin, 2,timeFrame, castTime, true);
           // allBuffIds.Add(buffID);

           // buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
           //, charID, AttribName.armorMax, 2, timeFrame, castTime, true);
           // allBuffIds.Add(buffID);

            allBuffIds.AddRange( charController.buffController.BuffAllRes(CauseType.CharState, (int)charStateName
                 , charID, 12, timeFrame, castTime, true));
            CombatEventService.Instance.OnCharOnTurnSet += GainAP; 
            charController.ClampStatToggle(StatName.fortitude, true);
        }
        void GainAP(CharController charController)
        {
            if(charController.charModel.charID== charID && !isAPRewardGained)
            {
                charController.combatController.IncrementAP();
                isAPRewardGained = true;
            }
        }
        public override void EndState()
        {
            base.EndState();
            charController.ClampStatToggle(StatName.fortitude, false);
            CombatEventService.Instance.OnCharOnTurnSet -= GainAP;
            CombatEventService.Instance.OnEOR1 -= ResetReward;
        }
        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "+2 Utility Attributes";
            allStateFxStrs.Add(str0);
            str1 = "+12 All Res";
            allStateFxStrs.Add(str1);
            str2 = "+1 AP";
            allStateFxStrs.Add(str2);

        }
    }
}

