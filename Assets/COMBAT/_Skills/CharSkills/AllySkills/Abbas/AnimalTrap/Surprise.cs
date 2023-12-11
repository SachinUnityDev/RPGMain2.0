using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class Surprise : PerkBase
    {
        public override PerkNames perkName => PerkNames.Surprise;

        public override PerkType perkType => PerkType.B3;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() {PerkNames.EnjoyTheTrap, PerkNames.HuntingSeason };

        public override string desc => "this is enjoy the etrap";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.AnimalTrap;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        public override float chance { get; set; }
        //public override void SkillHovered()
        //{
        //    base.SkillHovered();
        //    skillModel.staminaReq = 4; 
        //}
        public override void ApplyFX1()
        {
            if (targetController == null) return;
            if (targetController.charModel.raceType == RaceType.Animal)
                targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                     , charController.charModel.charID, CharStateName.Feebleminded, skillModel.timeFrame, skillModel.castTime);
            else
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName,
                 charController.charModel.charID, AttribName.luck, -3, skillModel.timeFrame, skillModel.castTime, false);
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
            str1 = "Vs Animal: <style=States>Feebleminded</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Vs else: -3 Luck";
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
            perkDesc = "Vs Animal: Apply<style=States>Feebleminded</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Vs else: -3 Luck";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Stm cost: 5 -> 4";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}