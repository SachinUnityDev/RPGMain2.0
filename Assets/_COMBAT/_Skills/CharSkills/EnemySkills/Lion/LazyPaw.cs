using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Combat
{
    public class LazyPaw : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.LazyPaw;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        private float _chance = 30f;
        public override float chance { get => _chance; set => _chance = value; }

        public override string desc => "This is the Scratch skill ";
        //public Rodent rodent;
        //public SenseTheWeak senseTheWeak;
        public override void PopulateTargetPos()
        {
            skillModel.targetPos.Clear();
            for (int i = 1; i <= 7; i++)
            {
                    CellPosData cellPosData = new CellPosData(CharMode.Ally, i); // Allies
                    DynamicPosData dyna = GridService.Instance.gridView
                                           .GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                    if (dyna != null)
                    {
                        skillModel.targetPos.Add(cellPosData);
                    }                
            }
        }

        public override void ApplyFX1()
        {
         			chance = 60f;
				if (chance.GetChance())
               targetController.damageController.ApplyLowBleed(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID);
        }

    public override void ApplyFX2()
        {
       				
					chance = 10f;
					if(chance.GetChance())
				 targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                    , charController.charModel.charID, CharStateName.Horrified, skillModel.timeFrame, skillModel.castTime);
        }

        public override void ApplyFX3()
        {          
     
        }

        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.MeleeStrike(PerkType.None, skillModel);

        }

        public override void DisplayFX1()
        {
            str0 = $"{skillModel.damageMod}% <style=Physical>Physical</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);

        }

        public override void DisplayFX2()
        {
            str1 = $"<style=Enemy>60% <style=Bleed>Low Bleed</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX3()
        {
            
        }

        public override void DisplayFX4()
        {
        }

        public override void PopulateAITarget()
        {
            // populate targets to skill Service 
            // main and collatral too... 
            base.PopulateAITarget();
            if (SkillService.Instance.currentTargetDyna != null) return; 
            foreach (CellPosData cell in skillModel.targetPos)
            {
                DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);
                
                if (dyna != null)
                {
                    //if (targetController == null)
                    //    Debug.Log(" Traget controller is null"); 
                    //if(targetController.charStateController== null)
                    //    Debug.Log("CharState controller");

                    CharController targetCtrl = dyna.charGO.GetComponent<CharController>();
                    if (targetCtrl.charStateController.HasCharState(CharStateName.Bleeding))
                    {
                        tempDyna = dyna; 
                    }
                    else if (targetCtrl.tempTraitController.HasTempTrait(TempTraitName.Nausea) ||
                            targetCtrl.tempTraitController.HasTempTrait(TempTraitName.RatBiteFever))
                    {
                        tempDyna = dyna;
                    }else
                    {
                        randomDyna = dyna; 
                    }
                    if(tempDyna != null)
                    {
                        SkillService.Instance.currentTargetDyna = tempDyna; break; 
                    }
                }
            }
            if (SkillService.Instance.currentTargetDyna == null)
            {
                SkillService.Instance.currentTargetDyna = randomDyna;
            }

        }

        public override void ApplyMoveFx()
        {

        }
    }



   



}

