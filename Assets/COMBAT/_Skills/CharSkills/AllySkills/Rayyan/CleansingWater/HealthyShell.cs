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
            skillModel.staminaReq = 7;
            skillModel.cd = 2;
            stackAmt = 0f;
        }
   
        public override void ApplyFX1()
        {
            if (IsTargetAlly())
            {
                StatData statData = targetController.GetStat(StatsName.armor);
                float armorMin = statData.minRange;
                float armorMax = statData.maxRange;
                 chgMin = statData.minRange * 0.6f;
                 chgMax = statData.maxRange * 0.6f;

                targetController.buffController.ApplyBuffOnRange(CauseType.CharSkill, (int)skillName, charID
                                                     , StatsName.armor, chgMin, chgMax, TimeFrame.EndOfRound
                                                     , skillModel.castTime, true);
            }
        }

        public override void ApplyFX2()
        {
            if (IsTargetAlly())
            {             
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , StatsName.fireRes, 20, TimeFrame.EndOfRound, skillModel.castTime, true);
            }
        }

        public override void ApplyFX3()
        {
            if (IsTargetAlly())
            {
                targetController.damageController.OnDamageApplied += EOCTick;
                CombatEventService.Instance.OnEOC += EOCEnd; 
            }
        }
        public override void ApplyMoveFX()
        {
        }
        void EOCTick(DmgAppliedData dmgAppliedData)
        {
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
            targetController.damageController.OnDamageApplied -= EOCTick;
            CombatEventService.Instance.OnEOC -= EOCEnd;
        }

        public override void SkillEnd()
        {
            //base.SkillEnd();
            //if (IsTargetAlly())
            //{               
            //    targetController.ChangeStatRange(CauseType.CharSkill, (int)skillName, charID
            //                                    , StatsName.armor, -chgMin, -chgMax);
            //    targetController.ChangeStat(CauseType.CharSkill, (int)skillName, charID
            //    , StatsName.fireRes, -20);
            //}

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
        }



        
    }



}

