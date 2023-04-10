using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Combat
{
    public class Scratch : SkillBase
    {
        public override SkillModel skillModel { get; set; }
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.Scratch;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;

        private float _chance = 30f;
        public override float chance { get => _chance; set => _chance = value; }

        public override string desc => "This is the Scratch skill ";
        public Rodent rodent;
        public SenseTheWeak senseTheWeak;
        public override void PopulateTargetPos()
        {
            skillModel.targetPos.Clear();
            for (int i = 1; i < 4; i++)
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
            if (IsTargetMyEnemy())
            {
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill
                                                        , (int)skillName, DamageType.Physical, 100f);
            }
        }

        public override void ApplyFX2()
        {
            if (chance.GetChance())
                charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID, CharStateName.BleedLowDOT);

            //.SetCharState(targetGO, charController, CharStateName.BleedLowDOT);
        }

        public override void ApplyFX3()
        {          
            rodent = new Rodent();  // move to skill Init 
            //rodent.ApplyPassiveFX(targetController);
            //senseTheWeak = new SenseTheWeak();
            //senseTheWeak.ApplyPassiveFX(targetController);
        }

        //public override void SkillEnd()
        //{
        //    base.SkillEnd();
        //  //  if (rodent != null)
        //       // rodent.RemovePassiveFX(targetController);
        //    //if (senseTheWeak != null)
        //    //    senseTheWeak.RemovePassiveFX(targetController);
        //}
        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.MeleeSingleStrike(PerkType.None);

        }

        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy>60% <style=Bleed>Low Bleed</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);

        }

        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy>20% Rat Bite fever, {skillModel.castTime} rd ";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"<style=Enemy>35% more dmg,if target has Rat Bite Fever or nausea";
            SkillService.Instance.skillModelHovered.descLines.Add(str3);
        }

        public override void DisplayFX4()
        {
        }

        public override void PopulateAITarget()
        {
            // populate targets to skill Service 
            // main and collatral too... 
            PopulateTargetPos();
            DynamicPosData tempDyna = null;
            DynamicPosData randomDyna = null; 
            foreach (CellPosData cell in skillModel.targetPos)
            {
                DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);
                
                if (dyna != null)
                {
                    if (targetController.charStateController.HasCharDOTState(CharStateName.BleedLowDOT))
                    {
                        tempDyna = dyna; 
                    }
                    else if (targetController.tempTraitController.HasTempTrait(TempTraitName.Nausea) ||
                            targetController.tempTraitController.HasTempTrait(TempTraitName.RatBiteFever))
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
            if (tempDyna == null)
                SkillService.Instance.currentTargetDyna = randomDyna;


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