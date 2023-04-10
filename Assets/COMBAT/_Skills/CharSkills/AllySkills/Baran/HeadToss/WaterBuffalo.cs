using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class WaterBuffalo : PerkBase
    {
        public override PerkNames perkName => PerkNames.WaterBuffalo;
        public override PerkType perkType => PerkType.B3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.CrackTheNose };

        public override string desc => "(PR: B1 + B2) /n + 40 Self Earth Res and Water Res, 2 rds /n  Clear any DoT on self";

        public override CharNames charName => CharNames.Baran;
        public override SkillNames skillName => SkillNames.HeadToss;
        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        
        public override void ApplyFX1()
        {
            if (targetController)
            {
                charController.buffController.ApplyBuff(CauseType.CharSkill,(int)skillName, charID, AttribName.waterRes, +40f
                    , TimeFrame.EndOfRound, skillModel.castTime, true);
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.earthRes, +40f
                   , TimeFrame.EndOfRound, skillModel.castTime, true);

            }
        }

        public override void SkillEnd()
        {
            base.SkillEnd();
             //charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.waterRes, -40f, false);
             //charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.earthRes, -40f, false);

        }
        public override void ApplyFX2()
        {
            charController.charStateController.ClearDOT(CharStateName.BleedLowDOT);
            charController.charStateController.ClearDOT(CharStateName.PoisonedLowDOT);
            charController.charStateController.ClearDOT(CharStateName.BurnLowDOT);
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
            str0 = $"<style=Performer> +40<style=Water> Water Res </style>and<style=Earth> Earth Res </style>, 2 rds";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);
        }

        public override void DisplayFX2()
        {
            str1 = $"<style=Performer> Clear DoT";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX3()
        {
     
        }

        public override void DisplayFX4()
        {
        }

       
    }
}
