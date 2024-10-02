using Combat;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Aquaborne : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Aquaborne;
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            // +3 Luck and -2 Haste
            // +2-3 Armor and -30 Air Res
            // Immune to Poisoned
            // Upon consume Elixir of Hardening: Gain 30 Hp and 12 Fortitude
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.focus, +2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyDmgArmorByPercent(CauseType.CharState, (int)charStateName
             , charID, AttribName.armorMax, 12f, timeFrame, castTime, true);
            allBuffIds.Add(buffID);
            buffID = charController.buffController.ApplyDmgArmorByPercent(CauseType.CharState, (int)charStateName
             , charID, AttribName.armorMin, 12f, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                  , charID, AttribName.fireRes, -20, timeFrame, castTime, false);
            allBuffIds.Add(buffID);

            CombatEventService.Instance.OnPotionConsumedInCombat += OnPotionConsumed;
            SkillService.Instance.OnSkillUsed += OnPatienceSkill; 
        }

        // 50% chance to regain Ap upon using patience skill 
        void OnPatienceSkill(SkillEventData skillEventData)
        {
            if (skillEventData.strikerController.charModel.charID != charID) return;
            if(skillEventData.skillModel.skillInclination == SkillInclination.Patience)
            {
                if (50f.GetChance())
                    charController.combatController.IncrementAP(); 
            }
            return;
        }



        void OnPotionConsumed(CharController charController, PotionNames potionName)
        {
            if (charController.charModel.charID != charID) return;
            if (potionName == PotionNames.PotionOfHeroism)
            {
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
                                                               , StatName.health, +20);
            }
            if (potionName == PotionNames.SnowLeopardsBreath)
            {
                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
                                                               , StatName.stamina, +15);
            }
        }

        public override void EndState()
        {
            base.EndState();
            SkillService.Instance.OnSkillUsed -= OnPatienceSkill;
            CombatEventService.Instance.OnPotionConsumedInCombat -= OnPotionConsumed;
        }
        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "+2 Focus and +12% Armor";
            allStateFxStrs.Add(str0);
            str1 = "-20 Fire Res";
            allStateFxStrs.Add(str1);
            
            str2 = "Upon consume Potion of Concentration:\nGain 20 Hp";
            allStateFxStrs.Add(str2);
            str3 = "Upon consume Snow Leopard's Breath:\nGain 15 Stamina";
            allStateFxStrs.Add(str3);
            str4 = "Upon use Patience Skills: 50% Regain AP";
            allStateFxStrs.Add(str4);
           

        }
    }
}