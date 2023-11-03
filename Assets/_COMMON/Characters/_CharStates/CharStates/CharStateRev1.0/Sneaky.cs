using Combat;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Sneaky : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Sneaky;
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            //+1 AP if starts turn on pos 5,6,7
            //Retaliate
            //- 14 Light Res   +2 Dodge
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.lightRes, -14, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyDmgArmorByPercent(CauseType.CharState, (int)charStateName
             , charID, AttribName.dodge, +2f, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            CombatEventService.Instance.OnCharOnTurnSet += ExtraAPFor567;
            CombatEventService.Instance.OnDamageApplied -= Retaliate;
            CombatEventService.Instance.OnDamageApplied += Retaliate;

        }
        void Retaliate(DmgAppliedData dmgAppliedData)
        {
            if (dmgAppliedData.attackType != AttackType.Melee) return;
            if(dmgAppliedData.targetController.charModel.charID != charID) return;

            charController.strikeController.AddRetailiateBuff(CauseType.CharState, (int)charStateName
                , charStateModel.timeFrame, charStateModel.castTime); 
        }
        void ExtraAPFor567(CharController charController)
        {
            if (charController.charModel.charID != charID) return;
            DynamicPosData dyna = GridService.Instance.GetDyna4GO(charController.gameObject); 
            if(dyna.currentPos == 5 || dyna.currentPos == 6|| dyna.currentPos == 7)
            {
                charController.combatController.actionPts++; 
            }
        }
        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnCharOnTurnSet -= ExtraAPFor567;
            CombatEventService.Instance.OnDamageApplied -= Retaliate;
        }
        public override void StateApplyVFX()
        {
        }

        public override void StateDisplay()
        {
            str0 = "+2 Dodge and -14 Light Res"; 
            allStateFxStrs.Add(str0);
            str1 = "Retaliate vs Melee attacks";
            allStateFxStrs.Add(str1);

            str2 = "If on backrow at sot: +1 AP";
            allStateFxStrs.Add(str2);
        }
    }
}