using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq; 

namespace Combat
{
    public class PressurizedWater : PerkBase
    {
        public override CharNames charName => CharNames.Rayyan;
        public override SkillNames skillName => SkillNames.CleansingWater;
        public override SkillLvl skillLvl => SkillLvl.Level2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override string desc => "80% --> %120 Water /n Push target /n 15% confuse, 1 rd";
        public override List<PerkNames> preReqList => new List<PerkNames>() {PerkNames.MurkyWaters };
        public override PerkNames perkName => PerkNames.PressurizedWater;
        public override PerkType perkType => PerkType.A2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
     //   public override List<DynamicPosData> targetDynas => new List<DynamicPosData>();

        public override void SkillHovered()
        { // only for enemies
            base.SkillHovered();
            skillModel.damageMod = 120f;
            SkillService.Instance.SkillWipe += skillController.allPerkBases.Find(t => t.skillName == skillName
                     && t.skillLvl == SkillLvl.Level1 && t.state == PerkSelectState.Clicked).WipeFX1;         
        }

        public override void SkillSelected()
        { 
            base.SkillSelected();
            SkillService.Instance.SkillFXRemove += skillController.allPerkBases.Find(t => t.skillName == skillName
                                                  && t.skillLvl == SkillLvl.Level1
                                                  && t.state == PerkSelectState.Clicked).RemoveFX1;

            //skillController.allPerkBases.Find(t => t.skillName == skillName && t.skillLvl == SkillLvl.Level1
            //&& t.state == PerkSelectState.Clicked).RemoveFX1();
           
        }
 
        public override void ApplyFX1()
        {
            if (IsTargetEnemy())
            {
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName,
                         DamageType.Water, skillModel.damageMod, skillModel.skillInclination);
            } 
        }
        public override void ApplyFX2()
        {

            if (IsTargetEnemy())
            {
                float percent = 15f;
                if (percent.GetChance())
                    charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName, charController.charModel.charID
                                                                        , CharStateName.Feebleminded, skillModel.timeFrame, skillModel.castTime);
            }       
        }
        public override void ApplyFX3()
        {
           
        }

        public override void ApplyVFx()
        {
            
        }
        public override void ApplyMoveFX()
        {
            if (IsTargetEnemy())
            {
                if (GridService.Instance.targetSelected != null)
                {
                    GridService.Instance.gridMovement.MovebyRow(GridService.Instance.targetSelected
                        , MoveDir.Backward, 1);
                }
            }
        }
        
        //public override void SkillEnd()
        //{
        //    if (IsTargetEnemy())
        //    {
        //        targetController.charStateController.RemoveCharState(CharStateName.Confused);
        //    }           
        //}

        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy>{skillModel.damageMod}% <style=Water>Water</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy><style=Move>Push</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"<style=Enemy>15%<style=States> Feebleminded</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str3);
        }

        public override void DisplayFX4()
        {
            
        }

 

     
    }


}

