﻿using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace Combat
{
    public class FistOfWater : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.FistOfWater;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override string desc => "this is fist of water";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

       // List<DynamicPosData> targetDynasCopy = new List<DynamicPosData>(); // COPY for Skill EnD
        public override void PopulateTargetPos()
        {
            //if (skillModel == null) return;
            //targetDynasCopy.Clear();
            //skillModel.targetPos.Clear();
            //CombatService.Instance.mainTargetDynas.Clear(); 

            //for (int i = 1; i < 8; i++)
            //{
            //    CellPosData cellPosData = new CellPosData(CharMode.Enemy, i);
            //    DynamicPosData dyna = GridService.Instance.gridView
            //                            .GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
            //    if(dyna != null)
            //    {
            //        skillModel.targetPos.Add(cellPosData);
            //        CombatService.Instance.mainTargetDynas.Add(dyna);
            //        targetDynasCopy.Add(dyna); 
            //    }            
            //}
            AnyWithCharMode(CharMode.Enemy);
        }

        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.skillFXMoveController.ApplyOnSkillSelect();
        }

        public override void ApplyFX1()
        {
            if (skillModel.targetPos.Count > 0)
            { 
                CombatService.Instance.mainTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>().damageController
                                        .ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Water
                                                            , skillModel.damageMod, skillModel.skillInclination, true));            
            }
        }

        public override void ApplyFX2()
        {
            if (skillModel.targetPos.Count > 0)            
                CombatService.Instance.mainTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>()
                    .charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                    , charController.charModel.charID, CharStateName.Soaked, skillModel.timeFrame, skillModel.castTime));
        }
        public override void ApplyFX3()
        {


        }
        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.RangedStrike(PerkType.None, skillModel);             
        }

        public override void DisplayFX1()
        {
            str0 = $"{skillModel.damageMod}%<style=Water> Water</style> Dmg";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = "<style=States>Soaked</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX3()
        {
            str2 = "Ignore <style=Water>Water Res</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX4()
        {
        }

        public override void PopulateAITarget()
        {
           
        }

        public override void ApplyMoveFx()
        {
        }
    }


}

