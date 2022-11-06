﻿using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro; 
 

namespace Combat
{
    public class CleansingWater : SkillBase
    {     
        public override SkillModel skillModel { get; set; }
        
        private CharNames _charName; 
        public override CharNames charName { get => _charName; set => _charName = value;  }
        public override SkillNames skillName => SkillNames.CleansingWater;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single; 
        public override string desc => "this is cleansing water";       

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

       
        public override void PopulateTargetPos()
        {
            skillModel.targetPos.Clear();            

            for (int i = 1; i < 8; i++)
            {
                if (!(myDyna.currentPos == i))
                {
                    CellPosData cellPosData = new CellPosData(CharMode.Ally, i);
                    DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                    if (dyna != null)
                    {                      
                        skillModel.targetPos.Add(cellPosData);
                        CombatService.Instance.mainTargetDynas.Add(dyna);
                    }
                }
            }         
        }    
        public override void ApplyFX1()
        {
            Debug.Log("this is cleansing water Apply FX1 ");

            if (targetController && IsTargetMyAlly())
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Heal
                                                                    , UnityEngine.Random.Range(4f, 7f), false);           
        }

        public override void DisplayFX1()
        {

        }
        public override void ApplyFX2()
        {           
           if(IsTargetMyAlly())
            targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                                    , StatsName.haste, 2, TimeFrame.EndOfRound, skillModel.castTime,true);
        }

        public override void DisplayFX2()
        {
            str1 = $"<style=Allies> <style=Heal>Heal,</style> 4-7";
            SkillServiceView.Instance.skillCardData.descLines.Add(str1);       
        }

        public override void ApplyFX3()
        {          
            CharStatesService.Instance
                 .ApplyCharState(targetGO, CharStateName.Soaked
                                     , charController, CauseType.CharSkill, (int)skillName);
        }
        public override void DisplayFX3()
        {
            str2 = $"<style=Allies> +2 <style=Attributes>Haste</style>, {skillModel.castTime} rds ";
            SkillServiceView.Instance.skillCardData.descLines.Add(str2);      
        }
        public override void DisplayFX4()
        {
            str3 = $"<style=Allies> <style=States>Soaked</style>, {skillModel.castTime} rds ";
            SkillServiceView.Instance.skillCardData.descLines.Add(str3);
        }

        public override void ApplyVFx()
        {          
            SkillService.Instance.skillFXMoveController.SingleTargetRangeStrike(PerkType.None); 

        }
        public override void SkillEnd()
        {
            base.SkillEnd(); 
            //if (IsTargetMyEnemy()) return;
            //targetController.ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.haste, -2);
            CharStatesService.Instance.RemoveCharState(targetGO, CharStateName.Soaked);
        }
        public override void PopulateAITarget()
        {
           
        }

        public override void ApplyMoveFx()
        {

        }
    }
}


// move sequence general ..
// movement controller .. style 1 move and strike a target 
// style 2 move N strike multiple targets 


//..... FX 
// FX.. get FX ..style 1 .. just instnatiate at once point
// style 2 start at a point and end at a point 
// change color 
// change scale

