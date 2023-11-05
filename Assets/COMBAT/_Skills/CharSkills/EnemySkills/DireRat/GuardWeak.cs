﻿using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Combat
{
    public class GuardWeak : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.GuardWeak;

        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override string desc => "guard weak";
        float minArmorChg = 0; 
        float maxArmorChg = 0f;
        public override void PopulateTargetPos()
        {
            skillModel.targetPos.Clear();
            for (int i = 1; i < 8; i++)
            {              
                CellPosData cellPosData = new CellPosData(charController.charModel.charMode, i); // Allies
                DynamicPosData dyna = GridService.Instance.gridView
                    .GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                if (dyna != null)
                {
                    if(dyna.charGO.GetComponent<CharController>().charModel.charName
                                                            == CharNames.CrestedRat)
                      skillModel.targetPos.Add(cellPosData);
                }                
            }
        }
   
        public override void ApplyFX1()
        {
            if (IsTargetMyAlly())
            {
                AttribData attribDataMin = charController.GetAttrib(AttribName.armorMin);
                AttribData attribDataMax = charController.GetAttrib(AttribName.armorMax);

                minArmorChg = attribDataMin.currValue * 0.5f;
                maxArmorChg = attribDataMax.currValue * 0.5f;

                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                  , AttribName.armorMin, minArmorChg,skillModel.timeFrame, skillModel.castTime, true);

                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                 , AttribName.armorMax, maxArmorChg, skillModel.timeFrame, skillModel.castTime, true);

                targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName, charID,
                        CharStateName.Guarded, skillModel.timeFrame, skillModel.castTime); 

            }
        }

        public override void ApplyFX2()
        {
            if (IsTargetMyAlly())
            {   
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.waterRes, 20, skillModel.timeFrame, skillModel.castTime, true);

                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                   , AttribName.earthRes, 20, skillModel.timeFrame, skillModel.castTime, true);
            }
        }

        public override void ApplyFX3()
        {
        }

 
        public override void DisplayFX1()
        {
            str0 = $"<style=Allies> Guard Crested Rat, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);
            str1 = $"<style=self> Gain +40% Armor, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
  
        }

        public override void DisplayFX2()
        {
            str2 = $"<style=self> +20 <style=Water>Water</style> and <style=Earth>Earth</style> Res, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }


  
        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.MultiTargetRangeFX(PerkType.None);
        }

        public override void PopulateAITarget()
        {

            base.PopulateAITarget();
            if (SkillService.Instance.currentTargetDyna != null) return; 
            foreach (CellPosData cell in skillModel.targetPos)
            {
                DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);
                if (dyna != null)
                {
                    CharController targetCharCtrl = dyna.charGO.GetComponent<CharController>();
                   
                    StatData hp = targetCharCtrl.GetStat(StatName.health); 

                    if(hp.currValue/hp.maxLimit < 0.4f)
                    {
                        SkillService.Instance.currentTargetDyna = dyna; break;
                    }
                    else
                    {
                        randomDyna = dyna; 
                    } 
                }       
            }
            if(SkillService.Instance.currentTargetDyna == null)
            {
                SkillService.Instance.currentTargetDyna = randomDyna;
            }
        }

        public override void ApplyMoveFx()
        {
        }
    }
}