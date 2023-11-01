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

      //  public override List<DynamicPosData> targetDynas => new List<DynamicPosData>();  

        float stackAmt;

        float chgMin;
        float chgMax;
        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.cd = 2;
            stackAmt = 0f;
        }
        public override void BaseApply()
        {
            base.BaseApply();
            SkillService.Instance.OnSkillUsed += WaterShellRegainAP;
            CombatEventService.Instance.OnEOT += OnEOT;
        }
        bool WaterShellRegainAP(SkillEventData skilleventData)
        {
            if (50f.GetChance()) return true; 
            if (skilleventData.skillName == SkillNames.WaterShell)
            {
                RegainAP();
            }
            return true;
        }
        void OnEOT()
        {
            SkillService.Instance.OnSkillUsed -= WaterShellRegainAP;
            CombatEventService.Instance.OnEOT -= OnEOT;
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
                CombatEventService.Instance.OnDamageApplied += EOCTick;
                CombatEventService.Instance.OnEOC += EOCEnd; 
            }
        }
        public override void ApplyMoveFX()
        {
        }
        void EOCTick(DmgAppliedData dmgAppliedData)
        {
            if (dmgAppliedData.targetController.charModel.charID != targetController.charModel.charID) return;
            if (dmgAppliedData.dmgType != DamageType.Heal) return;
            if (stackAmt <= 36)
            {
                stackAmt += 12;
                float addedVal = dmgAppliedData.dmgValue * stackAmt/100f;
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Heal, addedVal);                
            }       
        }
        void EOCEnd()
        {
            CombatEventService.Instance.OnDamageApplied -= EOCTick;
            CombatEventService.Instance.OnEOC -= EOCEnd;
        }

        public override void ApplyVFx()
        {
        }

        public override void DisplayFX1()
        {
            str1 = $"+60% Armor, {skillModel.castTime} rd";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);

        }

        public override void DisplayFX2()
        {
            str2 = $"+20 <style=Fire>Fire res</style>, {skillModel.castTime} rd";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"+12% <style=Heal>Heal</style> recieved until EOC, stack up to 36%";
            SkillService.Instance.skillModelHovered.descLines.Add(str3);
        }

        public override void DisplayFX4()
        {
            str0 = $"regain to 50% ";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);
        }
        
    }



}

