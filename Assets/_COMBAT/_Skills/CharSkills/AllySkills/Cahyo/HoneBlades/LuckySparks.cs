﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class LuckySparks : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.HoneBlades;
        public override SkillLvl skillLvl => SkillLvl.Level3;      
        public override PerkSelectState state { get;set; }
        public override PerkNames perkName => PerkNames.LuckySparks;
        public override PerkType perkType => PerkType.B3;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.HighSparks, PerkNames.BlindingSparks };
        public override string desc => "Lucky Sparks";        
        public override float chance { get; set;  }

        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.staminaReq = 7; 
        }
        public override void ApplyFX1()
        {
            if(targetController)
               charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                             , AttribName.luck,+3, skillModel.timeFrame, skillModel.castTime, true);
        }
        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
        }
        public override void DisplayFX1()
        {
            str1 = "+3 Luck";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
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
            perkDesc = "Stm cost: 5 -> 7";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "+3 Luck";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }


}

