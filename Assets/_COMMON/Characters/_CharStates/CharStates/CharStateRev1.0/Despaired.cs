using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{
    public class Despaired : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Despaired;     
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {  // .... -5 fortitude per rd ..."-20 Light Res........Immune to Inspired         

            ApplyRoundFX();
            CombatEventService.Instance.OnSOT += ApplyRoundFX;

            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                                            , charID, AttribName.lightRes, -20, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            int immuneBuffID = charController.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                                                , charID, CharStateName.Inspired, timeFrame, castTime);
            allImmunityBuffs.Add(immuneBuffID);

            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
             , charID, AttribName.morale, -2, TimeFrame.EndOfCombat, 1, false); // not to be lost on char state END

        }

        void ApplyRoundFX()
        {
            if (CombatService.Instance.currCharOnTurn.charModel.charID != charID) return;
            if (CombatService.Instance.currCharOnTurn.charModel.orgCharMode == CharMode.Ally)
            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
                                                               , StatName.fortitude, -5);

            CombatEventService.Instance.OnCharOnTurnSet += PatienceUnClickable;
        }

        void PatienceUnClickable(CharController charController)
        {
            if (GameService.Instance.currGameModel.gameScene == GameScene.COMBAT)
            {
                if (this.charController.charModel.charID == charController.charModel.charID)
                {
                    charController.skillController.UnClickableSkillsByIncli(SkillInclination.Patience);
                }
            }
        }
        public override void StateApplyVFX()
        {

        }
        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnSOT -= ApplyRoundFX;
            CombatEventService.Instance.OnCharOnTurnSet -= PatienceUnClickable;
        }
        public override void StateDisplay()
        {          
            str0 = "-5<style=Fortitude> Fortitude </style> per rd";
            allStateFxStrs.Add(str0);

            str1 = "-20 Light Res";
            allStateFxStrs.Add(str1);

            str2 = "Can't use Patience Skills";
            allStateFxStrs.Add(str2);

            str3 = "Immune to<style=States> Inspired </style>";
            allStateFxStrs.Add(str3);
            str4 = "-2 Morale until eoc";
            allStateFxStrs.Add(str4);
        }

    }
}
