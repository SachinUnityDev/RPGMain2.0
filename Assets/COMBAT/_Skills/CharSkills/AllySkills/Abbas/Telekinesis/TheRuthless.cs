using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{


    public class TheRuthless : PerkBase
    {
        public override PerkNames perkName => PerkNames.TheRuthless;

        public override PerkType perkType => PerkType.A2;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.ThePuller, PerkNames.TheEntangler };

        public override string desc => "this is the grabber";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.Telekinesis;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        public override float chance { get; set; }

        public override void SkillHovered()
        {
            base.SkillHovered();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX1;

        }
        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;

        }

        public override void ApplyFX1()
        {
            StatData hpStatData = targetController.GetStat(StatName.health);
            float hpPercent = hpStatData.currValue / hpStatData.maxLimit; 

            if(hpPercent >= 0.2f)
            {             // pure dmg
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                , skillModel.dmgType[0], UnityEngine.Random.Range(10, 13), skillModel.skillInclination);
            
            }
            else
            {
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                        , skillModel.dmgType[0], UnityEngine.Random.Range(14, 19), skillModel.skillInclination);
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
            str1 = "<style=Pure>10-12 Pure</style> Dmg";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "If enemy Hp < 20%: <style=Pure>14-18</style> Dmg";
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
            perkDesc = "10-12 <style=Pure>Pure</style> Dmg";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "If enemy Hp < 20%: <style=Pure>14-18</style> Dmg";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}