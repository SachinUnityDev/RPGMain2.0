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
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.Scratch;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;

        private float _chance = 30f;
        public override float chance { get => _chance; set => _chance = value; }

        public override string desc => "This is the Scratch skill ";
        //public Rodent rodent;
        //public SenseTheWeak senseTheWeak;
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
                if(targetController.tempTraitController.HasTempTrait(TempTraitName.Nausea))
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill
                                                            , (int)skillName, DamageType.Physical, skillModel.damageMod +40f);
                else
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill
                                                            , (int)skillName, DamageType.Physical, skillModel.damageMod);
            }
        }

        public override void ApplyFX2()
        {
            if (chance.GetChance())
                targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID, CharStateName.Bleeding);

            if (20f.GetChance())
                targetController.tempTraitController.ApplyTempTrait(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID, TempTraitName.RatBiteFever);


        }

        public override void ApplyFX3()
        {          
     
        }

        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.MeleeSingleStrike(PerkType.None);

        }

        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy>60% <style=Bleed>Low Bleed</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);

        }

        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy>20% Rat Bite fever, {skillModel.castTime} rd ";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"<style=Enemy>35% more dmg,if target has Rat Bite Fever or nausea";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }

        public override void DisplayFX4()
        {
        }

        public override void PopulateAITarget()
        {
            // populate targets to skill Service 
            // main and collatral too... 
            base.PopulateAITarget();
            if(SkillService.Instance.currentTargetDyna == null)
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