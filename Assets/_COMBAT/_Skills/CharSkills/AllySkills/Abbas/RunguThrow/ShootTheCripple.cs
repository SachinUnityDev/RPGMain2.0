using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class ShootTheCripple : PerkBase
    {
        public override PerkNames perkName => PerkNames.ShootTheCripple;

        public override PerkType perkType => PerkType.A2;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.ConfuseThem };

        public override string desc => "this is shoot the cripple  ";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.RunguThrow;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        public override float chance { get; set; }

        DynamicPosData targetDyna; 
        public override void ApplyFX1()
        {
            if (targetController == null) return; 
            if(targetController.charStateController.HasCharState(CharStateName.Confused)
                || targetController.charStateController.HasCharState(CharStateName.Feebleminded)
                || targetController.charStateController.HasCharState(CharStateName.Despaired)
                || targetController.charStateController.HasCharState(CharStateName.Rooted))
            {
                ApplyDmg(true);
            }
            else
            {
                ApplyDmg(false);
            }
        }

        void ApplyDmg(bool isTrueStrike)
        {
            targetDyna = GridService.Instance.GetDyna4GO(targetController.gameObject);
            
            if (GridService.Instance.IsTargetInBackRow(targetDyna)) // if back row extra dmg 
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill,
                    (int)skillName, DamageType.Physical, (skillModel.damageMod + 40f), skillModel.skillInclination, false, isTrueStrike);
            else
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill
                    , (int)skillName, DamageType.Physical, (skillModel.damageMod), skillModel.skillInclination,false, isTrueStrike);
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
            str1 = "True Strike vs <style=States>Feebleminded, Despaired, Confused, Rooted</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "+40% Dmg vs Backrow";
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
            perkDesc = "True Strike vs <style=States>Feebleminded, Despaired, Confused, Rooted</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "+40% Dmg vs Backrow";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}