using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{
    public class Despaired : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Despaired;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {  // .... -5 fortitude per rd ..."-20 Light Res........Immune to Inspired         

            ApplyRoundFX();
            CombatEventService.Instance.OnSOT += ApplyRoundFX;

            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                   , charID, AttribName.lightRes, -20, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            int immuneBuffID = charController.charStateController
                  .ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                     , charID, CharStateName.Inspired, charStateModel.timeFrame, charStateModel.castTime);

            allImmunityBuffs.Add(immuneBuffID);
        }

        void ApplyRoundFX()
        {
            if (CombatService.Instance.currCharOnTurn.charModel.charID != charID) return;
            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
                                                               , StatName.fortitude, -5);
        }

        public override void StateApplyVFX()
        {

        }
        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnSOT -= ApplyRoundFX;
        }
        public override void StateDisplay()
        {          
            str0 = "-5<style=Fortitude> Fortitude </style> per rd";
            charStateModel.charStateCardStrs.Add(str0);

            str1 = "-20 Light Res";
            charStateModel.charStateCardStrs.Add(str1);

            //str2 = "30% chance to force use Patience";
            //charStateModel.charStateCardStrs.Add(str2);

            str2 = "Immune to<style=States> Inspired </style>";
            charStateModel.charStateCardStrs.Add(str2);
        }

    }
}
