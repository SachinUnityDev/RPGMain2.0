﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;
namespace Combat
{

    public class HatchetSwing : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }

        public override SkillNames skillName => SkillNames.HatchetSwing;

        public override SkillLvl skillLvl => SkillLvl.Level0;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override string desc => "Hatchet Swing";

        private float _chance = 50f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void PopulateTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear();            
            for (int i = 1; i < 4; i++)
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, i); // Enemy
                DynamicPosData dyna = GridService.Instance.gridView
                                       .GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                if (dyna != null)
                {
                    skillModel.targetPos.Add(cellPosData);
                    CombatService.Instance.mainTargetDynas.Add(dyna);                       
                }
            }
        }

        public override void ApplyFX1()
        {
            if (targetController)
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                 , DamageType.Physical, skillModel.damageMod, skillModel.skillInclination);
        }

        public override void ApplyFX2()
        {
            if (targetController)
            {
                if (chance.GetChance())
                    targetController.damageController.ApplyLowBleed(CauseType.CharSkill, (int)skillName
                                                                        ,charController.charModel.charID);
            }
        }

        public override void ApplyFX3()
        {
        }

        public override void DisplayFX1()
        {
            str0 = $"{skillModel.damageMod}% <style=Physical>Physical</style> Dmg";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = $"{chance}% <style=Bleed>Low Bleed</style>";
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
            SkillService.Instance.skillFXMoveController.MeleeStrike(PerkType.None, skillModel);

        }

        public override void ApplyMoveFx()
        {
          
        }
     
    }



}
