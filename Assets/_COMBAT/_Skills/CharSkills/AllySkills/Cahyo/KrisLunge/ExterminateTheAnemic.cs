﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class ExterminateTheAnemic : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.KrisLunge;
        public override SkillLvl skillLvl => SkillLvl.Level3;       
        public override string desc => "EX the anemic";
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override PerkNames perkName => PerkNames.ExterminateTheAnemic;

        public override PerkType perkType => PerkType.B3; 

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.staminaReq = 10; 
        }
        public override void ApplyFX1()
        {
            if (targetController.charModel.charName == CharNames.RatKing ||
                targetController.charModel.charName == CharNames.Kongamato)
                return; 
           if(targetController.charStateController.HasCharState(CharStateName.Bleeding))
           {
                StatData statData = targetController.GetStat(StatName.health);
                float hpPercent = statData.currValue / statData.maxLimit; 

               if (hpPercent < 30f)
               {
                    // kill TARGET  !!!!
                 
               }
           }
        }
        public override void ApplyFX2()
        {

        }

        public override void ApplyFX3()
        {

        }
        public override void DisplayFX1()
        {
            str1 = "Instant kill if target Hp < 30% & <style=Bleed>Bleeding</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Doesn't work on Bosses";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }
        public override void ApplyVFx()
        {
        }

        public override void ApplyMoveFX()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "Instant kill if target Hp < 30% & <style=Bleed>Bleeding</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(perkDesc);

            perkDesc = "Doesn't work on Bosses";
            SkillService.Instance.skillModelHovered.AddDescLines(perkDesc);

            perkDesc = "Stm cost: 5 -> 7";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }

}

