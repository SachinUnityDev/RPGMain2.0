using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{


    public class Rooted : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Rooted;
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {  // Can't use Move skills	
           // Melee attack limit: pos 1 -> pos 1
           //-6 Dodge
           //	Immune to Lissome

            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, AttribName.dodge, -6, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            int immuneBuffID = charController.charStateController
                .ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                   , charID, CharStateName.Lissome, timeFrame, castTime);
            allImmunityBuffs.Add(immuneBuffID);

            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
             , charID, AttribName.haste, -2, TimeFrame.EndOfCombat, 1, false); // not to be lost on char state END
                     
            CombatEventService.Instance.OnCharOnTurnSet += MoveUnClickable;
            CombatEventService.Instance.OnCharOnTurnSet += NoMeleeAttackExceptOn1;
        }
        void MoveUnClickable(CharController charController)
        {
            if (GameService.Instance.currGameModel.gameState == GameState.InCombat)
            {
                if (this.charController.charModel.charID == charController.charModel.charID)
                {
                    charController.skillController.UnClickableSkillsByIncli(SkillInclination.Move);
                }
            }
        }

        void NoMeleeAttackExceptOn1(CharController charController)
        {
            if (GameService.Instance.currGameModel.gameState == GameState.InCombat)
            {
                if (this.charController.charModel.charID == charController.charModel.charID)
                {
                    charController.skillController.UnClickableSkillsByAttackType(AttackType.Melee,1);
                }
            }
        }


        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnCharOnTurnSet -= NoMeleeAttackExceptOn1;
            CombatEventService.Instance.OnCharOnTurnSet -= MoveUnClickable;
        }
        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "Can't use Move Skills";
            allStateFxStrs.Add(str0);

            str1 = "Melee attack limit: cast pos 1";
            allStateFxStrs.Add(str1);

            str2 = "-6 Dodge";
            allStateFxStrs.Add(str2);

            str3 = "Immune to<style=States> Inspired </style>";
            allStateFxStrs.Add(str3);

            str4 = "-2 Haste until eoc";
            allStateFxStrs.Add(str4);
        }
    }
}

