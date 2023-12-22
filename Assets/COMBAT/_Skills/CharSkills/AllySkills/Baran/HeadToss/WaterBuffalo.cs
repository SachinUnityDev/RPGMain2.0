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
            charController.buffController.ApplyBuff(CauseType.CharSkill,(int)skillName, charID, AttribName.waterRes, +24f
                , skillModel.timeFrame, skillModel.castTime, true);
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.earthRes, +24f
                , skillModel.timeFrame, skillModel.castTime, true);
        }
        public override void ApplyFX2()
        {
            charController.charStateController.RemoveCharState(CharStateName.Bleeding);
            charController.charStateController.RemoveCharState(CharStateName.Poisoned);
            charController.charStateController.RemoveCharState(CharStateName.Burning);
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
            str0 = "Self: +24<style=Water> Water</style>and<style=Earth> Earth Res</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = "Self: Clear DoT";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX3()
        {
     
        }

        public override void DisplayFX4()
        {
        }

        public override void InvPerkDesc()
        {
            perkDesc = "Self: +24<style=Water> Water</style>and<style=Earth> Earth Res</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "Self: Clear DoT";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}
