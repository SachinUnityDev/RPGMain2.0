using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class TheEntangler : PerkBase
    {
        public override PerkNames perkName => PerkNames.TheEntangler;

        public override PerkType perkType => PerkType.B3;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.ThePuller };

        public override string desc => "this is the Wapper ";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.Telekinesis;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        public override float chance { get; set; }

        public override void ApplyFX1()
        {
            if (targetController)
            {
                targetController.charStateController.RemoveCharState(CharStateName.LuckyDuck);
                targetController.charStateController.RemoveCharState(CharStateName.Inspired);
                targetController.charStateController.RemoveCharState(CharStateName.Concentrated);
                targetController.charStateController.RemoveCharState(CharStateName.Lissome);
            }
                
        }

        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
            if (targetController)
                targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                                    , charController.charModel.charID, CharStateName.Rooted, skillModel.timeFrame
                                    , skillModel.castTime);
        }

        public override void ApplyMoveFX()
        {
        }

        public override void ApplyVFx()
        {
        }

        public override void DisplayFX1()
        {
            str1 = "Apply <style=States>Rooted</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Clear <style=States>Lucky Duck, Inspired, Concentrated, Lissome</style>";
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
            perkDesc = "Target -> All enemies";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "<style=Move>Push</style> -> <style=Move>Shuffle</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}