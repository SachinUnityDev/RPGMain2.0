using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class ColderThanBlade : PerkBase
    {
        public override PerkNames perkName => PerkNames.ColderThanBlade;
        public override PerkType perkType => PerkType.A2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.InspiringTouch };

        public override string desc => "+20% Physical skills";

        public override CharNames charName => CharNames.Rayyan;

        public override SkillNames skillName => SkillNames.ColdTouch;
        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
  

        //public override void BaseApply()
        //{
        //    base.BaseApply();
        //    CombatEventService.Instance.OnStrikeFired += ExtraPhysicalDmg;
        //}

        //public void ExtraPhysicalDmg(StrikeData strikeData)
        //{
        //    if (targetController == strikeData.striker)
        //    {
        //        if (strikeData.skillInclination == SkillInclination.Physical)
        //        {
        //            strikeData.targets.ForEach(t => t.damageController.ApplyDamage(strikeData.striker, CauseType.CharSkill
        //                                                               , (int)skillName, DamageType.Physical, +20f, false));
        //        }
        //    }
        //}
        public override void ApplyFX1()
        {
            targetController.skillController.ApplySkillDmgModBuff(CauseType.CharSkill, (int)skillName, SkillInclination.Physical
               , 20f, skillModel.timeFrame, skillModel.castTime);                
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
            str1 = $"+20% Physical Skills, {skillModel.castTime} rds";

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
    }
}

