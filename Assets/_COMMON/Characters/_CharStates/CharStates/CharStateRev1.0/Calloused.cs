using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System;
using Interactables;

namespace Common
{

    public class Calloused : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Calloused;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get ; protected set; }

        public override void StateApplyFX()
        {
            // +3 Luck and -2 Haste
            // +2-3 Armor and -30 Air Res
            // Immune to Poisoned
            // Upon consume Elixir of Hardening: Gain 30 Hp and 12 Fortitude
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, StatsName.luck, +3, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, StatsName.haste, -2, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuffOnRange(CauseType.CharState, (int)charStateName
                  , charID, StatsName.armor, 2,3,charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);


            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, StatsName.airRes, -30, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            List<int> immuneBuffIDs = charController.charStateController
                  .ApplyDOTImmunityBuff(CauseType.CharState, (int)charStateName
                     , charID, CharStateName.PoisonedHighDOT, charStateModel.timeFrame, charStateModel.castTime);
            allImmunityBuffs.AddRange(immuneBuffIDs);
        }
        void OnPotionConsumed(CharController charController, PotionNames potionName)
        {
           // if (potionName == PotionNames.st)
        }
        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "+3 Luck and -2 Haste";
            charStateModel.charStateCardStrs.Add(str0);

            str1 = "+2-3 Armor and -30 Air Res";
            charStateModel.charStateCardStrs.Add(str1);

            str2 = "Immune to <style=Poison>Poisoned</style>";
            charStateModel.charStateCardStrs.Add(str2);

            str3 = "Upon consume Elixir of Hardening:\nGain 30 Hp and 12 Fortitude";
            charStateModel.charStateCardStrs.Add(str3);
        }
    }
}
