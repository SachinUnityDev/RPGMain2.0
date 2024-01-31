using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class HighSparks : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.HoneBlades;
        public override SkillLvl skillLvl => SkillLvl.Level2;      
        public override PerkSelectState state { get; set; }
        public override PerkNames perkName => PerkNames.HighSparks;
        public override PerkType perkType => PerkType.B2;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.BlindingSparks };
        public override string desc => "High Sparks ";        
        public override float chance { get; set; }
     
    
        public override void ApplyFX1()
        {
           charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                , AttribName.dmgMax,3, skillModel.timeFrame, skillModel.castTime, true);
            
        }
        public override void ApplyFX2()
        {
            if(50f.GetChance())
            charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                                                , charController.charModel.charID, CharStateName.Lissome);
        }

        public override void ApplyFX3()
        {

        }
        public override void DisplayFX1()
        {
            str1 = "+3 Max Dmg";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
            str2 = "Apply 50% <style=States>Lissome</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
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
            perkDesc = "+3 Max Dmg";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "Apply 50% <style=States>Lissome</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}
