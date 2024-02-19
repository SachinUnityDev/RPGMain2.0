using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Combat
{
    public class JawOfSteel : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.JawOfSteel;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        private float _chance = 30f;
        public override float chance { get => _chance; set => _chance = value; }

        public override string desc => "This is the Scratch skill ";
        //public Rodent rodent;
        //public SenseTheWeak senseTheWeak;
        public override void PopulateTargetPos()
        {
    FirstOnSamelane();
        }
        public override void ApplyFX1()
        {
         			
				if (60f.GetChance())
               targetController.damageController.ApplyHighBleed(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID);
        }

        public override void ApplyFX2()
        {
           
				    if (20f.GetChance())
                targetController.tempTraitController.ApplyTempTrait(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID, TempTraitName.Rabies);
					
					if(targetController)
				 targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                    , charController.charModel.charID, CharStateName.Rooted, skillModel.timeFrame, skillModel.castTime);
										

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
            str1 = $"60% <style=Bleed>High Bleed</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);

        }

        public override void DisplayFX2()
        {
       str2 = $"Apply <style=Bleed>Rooted</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
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


//"1- bleeding target
//2 - Rat Bite Fever or Nausea traits"	

//Damage	100%
//Use	No cd
//Attk Type	Melee
//Dmg Type	Physical, DoT
//Cast Position	1/2/3/4
//Target	1/2/3/4
//4 Stamina	Level 0
//effects	30% low bleed
//cast time	
//base weight	10
//CSI 	physical attack