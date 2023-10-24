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
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get ; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            // +3 Luck and -2 Haste
            // +2-3 Armor and -30 Air Res
            // Immune to Poisoned
            // Upon consume Elixir of Hardening: Gain 30 Hp and 12 Fortitude
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.luck, +3, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.haste, -2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.armorMin, 2,timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.armorMax, 3, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.airRes, -30, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            List<int> immuneBuffIDs = charController.charStateController
                  .ApplyDOTImmunityBuff(CauseType.CharState, (int)charStateName
                     , charID, CharStateName.PoisonedHighDOT, timeFrame, castTime);
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
            charStateCardStrs.Add(str0);

            str1 = "+2-3 Armor and -30 Air Res";
            charStateCardStrs.Add(str1);

            str2 = "Immune to <style=Poison>Poisoned</style>";
            charStateCardStrs.Add(str2);

            str3 = "Upon consume Elixir of Hardening:\nGain 30 Hp and 12 Fortitude";
            charStateCardStrs.Add(str3);
        }
    }
}
