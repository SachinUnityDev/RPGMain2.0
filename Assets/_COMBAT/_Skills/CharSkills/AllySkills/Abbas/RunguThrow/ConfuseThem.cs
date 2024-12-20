﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class ConfuseThem : PerkBase
    {
        public override PerkNames perkName => PerkNames.ConfuseThem;

        public override PerkType perkType => PerkType.A1;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is Confuse them ";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.RunguThrow;

        public override SkillLvl skillLvl => SkillLvl.Level1;
        private float _chance = 50f;    
        public override float chance { get=> _chance; set { _chance = 50f;  } }
        
        public override void SkillHovered()
        {
            base.SkillHovered();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX2;
        }
        public override void PerkSelected()
        {
            base.PerkSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX2;
        }
        public override void ApplyFX1()
        {
            if (targetController && !IsDodged())
            {
                if (chance.GetChance())
                    targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                    , charController.charModel.charID, CharStateName.Confused, skillModel.timeFrame, skillModel.castTime);

            }
          
        }

        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
        }

        public override void ApplyMoveFX()
        {
        }

        public override void ApplyVFx()
        {
        }

        public override void DisplayFX1()
        {
            str1 = $"{chance}% apply <style=States>Confused</style>";
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
        public override void InvPerkDesc()
        {
            perkDesc = "50% apply <style=States>Confused</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Remove -2 Focus";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }

}
