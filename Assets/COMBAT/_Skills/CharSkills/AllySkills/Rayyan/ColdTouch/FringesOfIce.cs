using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class FringesOfIce : PerkBase
    {
        public override PerkNames perkName => PerkNames.FringesOfIce;
        public override PerkType perkType => PerkType.B1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "Melee attackers against targeted ally takes 4-6 water dmg(cast time 2 rds)(Retaliation)";

        public override CharNames charName => CharNames.Rayyan;

        public override SkillNames skillName => SkillNames.ColdTouch;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void ApplyFX1()
        {
            if(targetController && IsTargetAlly())
                targetController.strikeController.AddThornsBuff(DamageType.Water, 4, 6
                                                      , skillModel.timeFrame, skillModel.castTime);
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
            str1 = "Apply Thorns 4-6<style=Water> Water</style>";
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
            perkDesc = "Apply Thorns 4-6<style=Water> Water</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
    
}