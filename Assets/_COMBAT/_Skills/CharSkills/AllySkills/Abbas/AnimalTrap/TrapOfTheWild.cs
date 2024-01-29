using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class TrapOfTheWild : PerkBase
    {
        public override PerkNames perkName => PerkNames.TrapOfTheWild;

        public override PerkType perkType => PerkType.A3;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.Flextrap };
        
        public override string desc => "this is trap of the wild";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.AnimalTrap;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        public override float chance { get; set; }

        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.dmgType[0] = DamageType.Earth;
            skillModel.skillInclination = SkillInclination.Magical;

            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX1;

        }
        public override void ApplyFX1()
        {
            if (targetController)
                targetController.damageController.ApplyLowPoison(CauseType.CharSkill, (int)skillName, charController.charModel.charID);
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
            str1 = $"{skillModel.damageMod}% <style=Earth>Earth</style> Dmg";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = $"100% <style=Earth>Low Poison</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX3()
        {
            str3 = "Ignore <style=Earth>Earth Res</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }
        public override void DisplayFX4()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = $"{skillModel.damageMod}% <style=Earth>Earth</style> Dmg";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = $"100% <style=Earth>Low Poison</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Ignore <style=Earth>Earth Res</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}
