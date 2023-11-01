using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class FindTheWeakSpot : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.KrisLunge;
        public override SkillLvl skillLvl => SkillLvl.Level2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get; set; }
        public override PerkNames perkName => PerkNames.FindTheWeakSpot;
        public override PerkType perkType => PerkType.A2;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is find the weak spot";
        private float _chance = 50f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void SkillHovered()
        {
            base.SkillHovered();
            skillController.allSkillBases.Find(t => t.skillName == skillName).chance = 60f;
        }
        public override void BaseApply()
        {
            base.BaseApply();
            if (targetController.charStateController.HasCharDOTState(CharStateName.BleedLowDOT))
                if (chance.GetChance())
                    RegainAP();
        }
        public override void ApplyFX1()
        {
            if(targetController)
                targetController.damageController.ApplyDamage(charController,
                                CauseType.CharSkill, (int)skillName, DamageType.StaminaDmg,
                                UnityEngine.Random.Range(3,6), skillModel.skillInclination);
        }

        public override void ApplyFX2()
        {
          
        }
        public override void DisplayFX1()
        {

        }
        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy>3 5 stm dmg";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }
        public override void ApplyFX3()
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
    }
}

