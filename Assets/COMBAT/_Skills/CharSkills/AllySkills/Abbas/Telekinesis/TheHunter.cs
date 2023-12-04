using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{


    public class TheHunter : PerkBase
    {
        public override PerkNames perkName => PerkNames.TheHunter;

        public override PerkType perkType => PerkType.A1;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is the hunter";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.Telekinesis;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        public override float chance { get; set; }

        public override void ApplyFX1()
        {
            if(targetController)
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                 , AttribName.dodge, -2, skillModel.timeFrame, skillModel.castTime, false);
        }

        public override void ApplyFX2()
        {
            if(targetController)
                charController.charStateController.RemoveCharState(CharStateName.Sneaky);
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
            str1 = "-2 Dodge";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Clear <style=States>Sneaky</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

      

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "-2 Dodge";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Clear <style=States>Sneaky</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}