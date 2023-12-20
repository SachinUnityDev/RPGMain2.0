using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Shocked : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Shocked;
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            //-3 Focus.... immune to Poison...	+24 earth res....+1-3 armor....
            //.Cannot use move skill Incli

           int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                       , charID, AttribName.focus, -3, timeFrame, castTime, true);
            allBuffIds.Add(buffID);
            
            buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                     , charID, AttribName.earthRes, +24, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = 
            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                       , charID, AttribName.armorMin, +1,  timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID =
           charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                      , charID, AttribName.armorMax, +3, timeFrame, castTime, true);
            allBuffIds.Add(buffID);


            List<int> immuneBuffIDs = charController.charStateController
               .ApplyDOTImmunityBuff(CauseType.CharState, (int)charStateName
                  , charID, CharStateName.Poisoned, timeFrame, castTime, false);

            allImmunityBuffs.AddRange(immuneBuffIDs);

            CombatEventService.Instance.OnCharOnTurnSet += CantUseMoveSkills; 
        }
        void CantUseMoveSkills(CharController charController)
        {
            if(GameService.Instance.gameModel.gameState == GameState.InCombat)
            {
                if (this.charController.charModel.charID == charController.charModel.charID)
                {
                    charController.skillController.UnClickableSkillsByIncli(SkillInclination.Move); 
                }
            }
        }
        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "-3 Focus";
            allStateFxStrs.Add(str0);

            str1 = "+1-2 Armor";
            allStateFxStrs.Add(str1);

            str2 = "+24 Earth Res";
            allStateFxStrs.Add(str2);

            str3 = "Immune to <style=Poison> Poisoned </style>";
            allStateFxStrs.Add(str3);

            str4 = "Can not use<style=Move> Move </style>skills";
            allStateFxStrs.Add(str4);
        }
        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnCharOnTurnSet -= CantUseMoveSkills;

        }
    }
}