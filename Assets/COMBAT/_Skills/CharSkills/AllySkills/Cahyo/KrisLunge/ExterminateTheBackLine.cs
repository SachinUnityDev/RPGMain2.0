using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Combat
{
    public class ExterminateTheBackLine : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.KrisLunge;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private PerkSelectState _state = PerkSelectState.Clickable;

        public override PerkNames perkName => PerkNames.ExterminateTheBackline;
        public override PerkType perkType => PerkType.B2;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.RevealTheBackline };

        public override string desc => "Extedrminate the backline";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override PerkSelectState state { get; set; }

        public override void ApplyFX1()
        {
            if(targetController.charStateController.HasCharState(CharStateName.Sneaky))
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill,
                             (int)skillName, DamageType.Physical, (skillModel.damageMod + 65f)
                             , skillModel.skillInclination,false, true);
        }
        public override void ApplyFX2()
        {
            
        }

        public override void ApplyFX3()
        {
        }
        public override void DisplayFX1()
        {

        }

        public override void DisplayFX2()
        {
            str1 = $"<style=Allies> <style=States>Confuse </style> backrow1 rd";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
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
            DynamicPosData targetDyna = GridService.Instance.GetDyna4GO(targetGO);
            if (GridService.Instance.IsTargetInBackRow(targetDyna))
            {
                GridService.Instance.gridMovement.MovebyRow(targetDyna, MoveDir.Forward, 1);
            }
        }
    }
}

