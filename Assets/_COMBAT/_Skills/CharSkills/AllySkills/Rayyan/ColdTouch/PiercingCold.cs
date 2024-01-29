using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class PiercingCold : PerkBase
    {
        public override PerkNames perkName => PerkNames.PiercingCold;
        public override PerkType perkType => PerkType.A3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>()
                                            {PerkNames.InspiringTouch, PerkNames.ColderThanBlade }; 

        public override string desc => "+4-6 --> 8-12 Water dmg /nIgnores Water Res/n 4 -> 6 Stm cost";

        public override CharNames charName => CharNames.Rayyan;

        public override SkillNames skillName => SkillNames.ColdTouch;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.staminaReq = 6;
            skillController.allSkillBases.Find(t => t.skillName == skillName).chance = 70f;
        }
   
        public override void ApplyFX1()
        {
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
            perkDesc = "Dmg buff: 40% -> +70%<style=Water> Water</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Stm cost: 4 -> 6";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}
