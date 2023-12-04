using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class HealthyWater : PerkBase
    {
        public override CharNames charName => CharNames.Rayyan;

        public override SkillNames skillName => SkillNames.CleansingWater;
        public override SkillLvl skillLvl => SkillLvl.Level1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override string desc => "this is healthy water";

        public override PerkNames perkName => PerkNames.HealthyWater;
        public override PerkType perkType => PerkType.B1;       
        public override List<PerkNames> preReqList => new List<PerkNames>() {PerkNames.None };

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void SkillHovered()
        {
            base.SkillHovered();
            if (skillController != null)
            {
                SkillService.Instance.SkillWipe += skillController.allSkillBases
                                                    .Find(t => t.skillName == skillName).WipeFX1;  // heal change 
                skillModel.attackType = AttackType.Ranged;
            }
        }

        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.SkillFXRemove += skillController
                .allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;// heal change
        }

        public override void ApplyFX1()
        {
            if (IsTargetAlly())
            {
                targetController.damageController
                .ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Heal, Random.Range(6f, 12f));
            }
        }
        public override void ApplyFX2()
        {
            if (IsTargetAlly())
            {
                targetController.charStateController.ClearDOT(CharStateName.BurnHighDOT);
            }
        }

        public override void ApplyFX3()
        {
        }

        public override void ApplyVFx()
        {
           
        }
        public override void ApplyMoveFX()
        {
          
        }
        public override void DisplayFX1()
        {
            str1 = "<style=Heal>Heal </style>6-12";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Clear <style=Burn>Burn</style>";
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
            perkDesc = "Heal: 4-7 -> 6-12";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Clear <style=Burn>Burn</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }



}

