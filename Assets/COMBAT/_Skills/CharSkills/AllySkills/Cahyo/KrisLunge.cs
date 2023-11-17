﻿using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class KrisLunge : SkillBase
    {
        public override SkillNames skillName => SkillNames.KrisLunge;
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override string desc => "This is Kris Lunge";

        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;

        private float _chance = 30f;
        public override float chance { get => _chance; set => _chance = value; }

        public List<int> possibleTargetLoc = new List<int>();

        public override void PopulateTargetPos()
        {
            //if (skillModel == null) return; 

            // skillModel.targetPos.Clear(); CombatService.Instance.mainTargetDynas.Clear();   

            //List<DynamicPosData> sameLaneOccupiedPos = GridService.Instance.GetInSameLaneOppParty
            //             (new CellPosData(CharMode.Ally, GridService.Instance.GetDyna4GO(charGO).currentPos));
            //if (sameLaneOccupiedPos.Count > 0)
            //{
            //    CellPosData Pos = new CellPosData(CharMode.Enemy, sameLaneOccupiedPos[0].currentPos);
            //    skillModel.targetPos.Add(Pos);
            //    CombatService.Instance.mainTargetDynas
            //        .Add(GridService.Instance.GetDynaAtCellPos(CharMode.Enemy, sameLaneOccupiedPos[0].currentPos)); 
            //}
            FirstOnSamelane();
        }

        public override void ApplyFX1()
        {
            if(targetController)
               targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                ,DamageType.Physical, skillModel.damageMod, skillModel.skillInclination);
        }
        public override void ApplyFX2()
        {   
            if (chance.GetChance())
                charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID, CharStateName.BleedHighDOT);
        }
        public override void ApplyFX3()
        {
            if (targetController)            
                GridService.Instance.gridMovement.MovebyRow(GridService.Instance.targetSelected
                                                                             , MoveDir.Forward, 1);            
        }
        public override void DisplayFX1()
        {
            str1 = $"{skillModel.damageMod}% <style=Physical>Physical </style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = $" <style=Bleed>High Bleed</style>, 30%";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX3()
        {
            str3 = $"<style=Move>Pull </style> self";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }

        public override void DisplayFX4()
        {

        }
      
        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.SingleTargetRangeStrike(PerkType.None);
        }
        public override void PopulateAITarget()
        {
           
        }
        public override void ApplyMoveFx()
        {
        }
    }


}
