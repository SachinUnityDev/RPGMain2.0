using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 

namespace Combat
{
    public class HealthyShell : PerkBase
    {
        public override CharNames charName => CharNames.Rayyan;
        public override SkillNames skillName => SkillNames.CleansingWater;
        public override SkillLvl skillLvl => SkillLvl.Level3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override string desc => "this is Healthy Shell";
        public override PerkNames perkName => PerkNames.HealthyShell;
        public override List<PerkNames> preReqList => new List<PerkNames>()
                                     { PerkNames.HealthyWater, PerkNames.HealthySplash };
        public override PerkType perkType => PerkType.B3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        float stackAmt=0;
        float chgMin;
        float chgMax;

        bool isAPRewardGained = false;  
        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.cd = 2;            
        }
        void RemoveReward(int rd)
        {
            //SkillService.Instance.OnSkillUsed -= WaterShellRegainAP;
          //  CombatEventService.Instance.OnEOR1 -= RemoveReward;
            isAPRewardGained = false;
        }

        public override void BaseApply()
        {
            base.BaseApply();
            SkillService.Instance.OnSkillUsed -= WaterShellRegainAP;
            SkillService.Instance.OnSkillUsed += WaterShellRegainAP;
            CombatEventService.Instance.OnEOR1 -= RemoveReward;
            CombatEventService.Instance.OnEOR1 += RemoveReward;
        }
        void WaterShellRegainAP(SkillEventData skilleventData)
        {
            if (50f.GetChance()) return; 
            if (skilleventData.skillName == SkillNames.WaterShell && !isAPRewardGained)
            {
                charController.combatController.IncrementAP();
                isAPRewardGained = true;
                SkillService.Instance.OnSkillUsed -= WaterShellRegainAP;
            }
        }
    
        public override void ApplyFX1()
        {
            if (IsTargetAlly())
            {             
                float armorMin = targetController.GetAttrib(AttribName.armorMin).currValue;
                float armorMax = targetController.GetAttrib(AttribName.armorMax).currValue;
                chgMin = armorMin * 0.6f;
                 chgMax = armorMax * 0.6f;

                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                                                     , AttribName.armorMin, chgMin, skillModel.timeFrame
                                                     , skillModel.castTime, true);
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                                                   , AttribName.armorMin, chgMax, skillModel.timeFrame
                                                   , skillModel.castTime, true);

            }
        }

        public override void ApplyFX2()
        {
            if (IsTargetAlly())
            {             
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.fireRes, 20, skillModel.timeFrame, skillModel.castTime, true);
            }
        }

        public override void ApplyFX3()
        {
            if (IsTargetAlly())
            {
                targetController.statBuffController.ApplyStatRecAltBuff(12f, StatName.health, CauseType.CharSkill, (int)skillName
                                                    , charController.charModel.charID, TimeFrame.EndOfCombat, 1, true, true);        
             }
        }
        public override void ApplyMoveFX()
        {
        }
 

        public override void ApplyVFx()
        {
        }
        public override void DisplayFX1()
        {
            str1 = "+60% Armor and +20 <style=Fire>Fire Res</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "+12% <style=Heal>Healing</style> Received until eoc";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX3()
        {
            str3 = "On use Water Shell this turn: 50% Regain AP";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }
        public override void DisplayFX4()
        {
            
        }

        public override void InvPerkDesc()
        {
            perkDesc = "+60% Armor and +20 <style=Fire>Fire Res</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "+12% <style=Heal>Healing</style> Received until eoc";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "On use Water Shell this turn: 50% Regain AP";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Cd: 0 -> 2";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }



}

