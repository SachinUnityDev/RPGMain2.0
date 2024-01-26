using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Combat
{
    public class WristSpin : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.WristSpin;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override string desc => "this is wristSpin";
        
        private float _chance = 50f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void PopulateTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear(); CombatService.Instance.mainTargetDynas.Clear();
            //  targetDynas.Clear();
            if (myDyna.currentPos == 1)
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, 1);
                AddTarget(cellPosData);
            }      
            else if (myDyna.currentPos == 2)
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, 1);
                AddTarget(cellPosData);
                CellPosData cellPosData2 = new CellPosData(CharMode.Enemy, 2);
                AddTarget(cellPosData2);
            }
            else if (myDyna.currentPos == 3)
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, 1);
                AddTarget(cellPosData);
                CellPosData cellPosData2 = new CellPosData(CharMode.Enemy, 3);
                AddTarget(cellPosData2);
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
           
            if (targetController && chance.GetChance())
                targetController.damageController.ApplyLowBleed(CauseType.CharSkill, (int)skillName
                                                                , charController.charModel.charID);
        }
  
        public override void DisplayFX1()
        {
            str1 = $"{skillModel.damageMod}% <style=Physical>Physical</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = $"{chance}% <style=Bleed>Low Bleed</style> ";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
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
            SkillService.Instance.skillFXMoveController.MeleeStrike(PerkType.None, skillModel);
        }

        public override void PopulateAITarget()
        {
        }

        public override void ApplyMoveFx()
        {
        }
   
    }



}
