using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class FeebleMinded : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Feebleminded;
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            // Can't use Buff or Heal skills	
            //	-20 Cold Res (water earth dark)	
            // ...Immune to LuckyDuck...

            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, AttribName.waterRes, -20, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, AttribName.earthRes, -20, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, AttribName.darkRes, -20, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
             , charID, AttribName.luck, -2, TimeFrame.EndOfCombat, 1, false); // not to be lost on char state END

            int immuneBuffID = charController.charStateController
               .ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                  , charID, CharStateName.LuckyDuck, timeFrame, castTime);
            allImmunityBuffs.Add(immuneBuffID);
            CombatEventService.Instance.OnCharOnTurnSet += BuffUnClickable;

        }

        void BuffUnClickable(CharController charController)
        {
            if (GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                if (this.charController.charModel.charID == charController.charModel.charID)
                {
                    charController.skillController.UnClickableSkillsByIncli(SkillInclination.Buff);
                }
            }
        }

        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnCharOnTurnSet -= BuffUnClickable;

        }
        public override void StateApplyVFX()
        {

        }
        public override void StateDisplay()
        {
            str0 = "Can't use Buff Skills";
            allStateFxStrs.Add(str0);
            str1 = "-20 Cold Resistances";
            allStateFxStrs.Add(str1);
            str2 = "Immune to<style=States> Lucky Duck</style>";
            allStateFxStrs.Add(str2);
            str3 = "-2 Luck until eoc";
            allStateFxStrs.Add(str3);
        }
    }
}

