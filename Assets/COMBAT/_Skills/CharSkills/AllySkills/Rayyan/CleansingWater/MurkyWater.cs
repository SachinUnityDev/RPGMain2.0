using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Common; 

namespace Combat
{
    public class MurkyWater : PerkBase
    {
        public override CharNames charName => CharNames.Rayyan;
        public override SkillNames skillName => SkillNames.CleansingWater;
        public override SkillLvl skillLvl => SkillLvl.Level1;

        private PerkSelectState _state = PerkSelectState.Clickable; 
        public override PerkSelectState state { get => _state; set => _state = value; }

        public override string desc => "Can target any enemy /n Attack type: Ranged /n 80% Water dmg /n -2 Haste, 1 rd /n Soak";

        public override PerkNames perkName => PerkNames.MurkyWaters;
        public override PerkType perkType => PerkType.A1;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
      //  public override List<DynamicPosData> targetDynas => new List<DynamicPosData>();

        public override SkillModel skillModel { get ; set ; }
      
        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.damageMod = 80f; 
            if (skillController != null)
            {
               
                SkillService.Instance.SkillWipe += skillController.allSkillBases
                                               .Find(t => t.skillName == skillName).WipeFX4;   // remove only allies soak      
                skillModel.attackType = AttackType.Ranged;
            }               
        }
        public override void AddTargetPos()
        {
            if (skillModel == null) return;
            for (int i = 1; i < 8; i++)
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, i);
                DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                if (dyna != null)
                {
                    skillModel.targetPos.Add(cellPosData);
                    CombatService.Instance.mainTargetDynas.Add(dyna);
                }
            }
        }   

        public override void ApplyFX1()
        {
            Debug.Log("Murky water FX1 applied  ");

            if (IsTargetEnemy())
            {
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill
                            , (int)skillName, DamageType.Water, skillModel.damageMod, skillModel.skillInclination);
            }
        }
        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy> {skillModel.damageMod}% <style=Water>Water </style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
           
        }
        public override void ApplyFX2()
        {
            if (IsTargetEnemy())
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                                , AttribName.haste, -2f, skillModel.timeFrame, skillModel.castTime, false);
        }
        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy> -2 Haste, {skillModel.castTime} rd";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
          

        }
        public override void ApplyFX3()
        {
            
        }
        public override void DisplayFX3()
        {
            str3 = $"<style=Allies><style=Enemy> <style=States> Soaked, </style>{skillModel.castTime} rd";
            SkillService.Instance.skillModelHovered.descLines.Add(str3);
          

        }
        public override void ApplyVFx()
        {
         
        }
        public override void ApplyMoveFX()
        {
        }   
        public override void DisplayFX4()
        {

        }
  
    }

}

