using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class StrengthOfTheEarth : PerkBase
    {
        public override PerkNames perkName => PerkNames.StrengthOfTheEarth; 
        public override PerkType perkType => PerkType.A3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.GravityForce, PerkNames.SplintersOfEarth };

        public override string desc => "(PR: A1 + A2) /n Ignores armor on initial target /n Collateral targets receive -25 ER, 4 rds";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.EarthCracker;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        List<DynamicPosData> colTargetDyna = new List<DynamicPosData>();

        public override void BaseApply()
        {
            base.BaseApply();
            DynamicPosData targetDyna = GridService.Instance.GetDyna4GO(targetGO);
            colTargetDyna.Clear();
            if (targetDyna != null)
            {
                CombatService.Instance.colTargetDynas =
                   GridService.Instance.gridController.GetAllAdjDynaOccupied(targetDyna);

                colTargetDyna.AddRange(CombatService.Instance.colTargetDynas);
            }
        }
        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.staminaReq = 11; 
            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX1;
        }

        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;

        }

        public override void ApplyFX1()
        {
            if (targetController != null)
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                     , DamageType.Physical, skillModel.damageMod, 
                                                      skillModel.skillInclination, true, false);
        }

        public override void ApplyFX2()
        {
            if(targetController != null)
            CombatService.Instance.colTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>()
            .buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.earthRes, -40
            ,skillModel.timeFrame, skillModel.castTime, false ));

        }    
        public override void ApplyFX3()
        {
            charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName, charController.charModel.charID,
                        CharStateName.Vanguard, skillModel.timeFrame, skillModel.castTime); 
        }

        public override void ApplyMoveFX()
        {
        }

        public override void ApplyVFx()
        {
        }
        public override void DisplayFX1()
        {
            str0 = "Ignore Armor on initial target";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = "-40<style=Earth>Earth Res</style>on col targets";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX3()
        {
            str2 = "Gain <style=States>Vanguard</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX4()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "Ignore Armor on initial target";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "-40<style=Earth>Earth Res</style>on col targets";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "Gain <style=States>Vanguard</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = $"Stm cost: 9 -> 11";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }
}
