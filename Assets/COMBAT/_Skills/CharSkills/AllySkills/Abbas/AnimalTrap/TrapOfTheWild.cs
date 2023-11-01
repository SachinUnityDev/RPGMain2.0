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
        }
        public override void ApplyFX1()
        {
            targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                            , skillModel.dmgType[0], skillModel.damageMod, skillModel.skillInclination, true);
        }

        public override void ApplyFX2()
        {
            if(targetController)
            targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID, CharStateName.PoisonedLowDOT);
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
    }
}
