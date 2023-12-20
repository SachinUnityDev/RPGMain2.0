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
                  , charID, AttribName.haste, -2, timeFrame, castTime, false);
            allBuffIds.Add(buffID);


            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.airRes, -20, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            List<int> immuneBuffIDs = charController.charStateController.ApplyDOTImmunityBuff(CauseType.CharState, (int)charStateName
                                                    , charID, CharStateName.Poisoned, timeFrame, castTime);
            allImmunityBuffs.AddRange(immuneBuffIDs);

            CombatEventService.Instance.OnPotionConsumedInCombat += OnPotionConsumed; 

        }
        void OnPotionConsumed(CharController charController, PotionNames potionName)
        {
            if (charController.charModel.charID != charID) return; 
            if(potionName == PotionNames.ElixirOfVigor)
            {
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
                                                               , StatName.health, +30);

                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
                                                               , StatName.fortitude, +12);
            }
        }
        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "+3 Luck and -2 Haste";
            allStateFxStrs.Add(str0);

            str1 = "-30 Air Res";
            allStateFxStrs.Add(str1);

            str2 = "Immune to <style=Poison>Poisoned</style>";
            allStateFxStrs.Add(str2);

            str3 = "Upon consume Elixir of Hardening:\nGain 30 Hp and 12 Fortitude";
            allStateFxStrs.Add(str3);
        }
    }
}
