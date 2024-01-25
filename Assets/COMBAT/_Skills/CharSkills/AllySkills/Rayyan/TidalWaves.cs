using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Combat
{
    public class TidalWaves : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.TidalWaves;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeNos strikeNos => StrikeNos.Multiple;
        public override string desc => " tidal waves base";
     
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        List<DynamicPosData> firstRowChar = new List<DynamicPosData>(); 

        public override void PopulateTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear(); CombatService.Instance.mainTargetDynas.Clear();firstRowChar.Clear(); 
            firstRowChar =   GridService.Instance.GetFirstRowChar(CharMode.Enemy);

            if (firstRowChar.Count > 0)
            {
                foreach (var dynaChar in firstRowChar)
                {
                    skillModel.targetPos.Add(new CellPosData(dynaChar.charMode, dynaChar.currentPos));
                    CombatService.Instance.mainTargetDynas.Add(dynaChar);
                }
            }          
        }
        public override void ApplyFX1()  // DAMAGE 
        {
            foreach (DynamicPosData targetDyna in CombatService.Instance.mainTargetDynas)
            {
                targetDyna.charGO.GetComponent<CharController>()
                    .damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                            , DamageType.Water, skillModel.damageMod, skillModel.skillInclination);
            }           
        }

   

        public override void ApplyMoveFx()
        {
            foreach (DynamicPosData targetDyna in CombatService.Instance.mainTargetDynas)
            {
                GridService.Instance.gridMovement.MovebyRow(targetDyna, MoveDir.Backward, 1);
            }
        }
        public override void ApplyFX2() 
        {
           
        }

        public override void ApplyFX3()
        {
            foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
            {
               dyna.charGO.GetComponent<CharController>().charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                       , charController.charModel.charID, CharStateName.Soaked, skillModel.timeFrame, skillModel.castTime);
            }

        }

        public override void DisplayFX1()
        {
            str0 = $"{skillModel.damageMod}%<style=Water> Water </style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = "Apply <style=States>Soaked</style> and<style=Move> Push </style>1";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
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
        }

    
    }



}

