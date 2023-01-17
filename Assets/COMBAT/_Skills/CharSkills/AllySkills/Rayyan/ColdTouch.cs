using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Combat
{
    public class ColdTouch : SkillBase
    {
        public override SkillModel skillModel { get; set; }

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.ColdTouch;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override string desc => "the is cold touch";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        List<GameObject> targetGOs = new List<GameObject>();
        List<CharController> targetControllers = new List<CharController>();
        public override void PopulateTargetPos()
        {
            if (skillModel == null) return; 
            CombatService.Instance.mainTargetDynas.Clear();
            skillModel.targetPos.Clear();
            for (int i = 1; i < 8; i++)
            {
                if (!(GridService.Instance.GetDyna4GO(charGO).currentPos == i))
                {
                    CellPosData cellPosData = new CellPosData(CharMode.Ally, i); // Allies
                    DynamicPosData dyna = GridService.Instance.gridView
                                             .GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                    if (dyna != null)
                    {
                        skillModel.targetPos.Add(cellPosData);
                        CombatService.Instance.mainTargetDynas.Add(dyna);
                    }
                }
            }
        }
        public override void BaseApply()
        {
            base.BaseApply();
            CombatEventService.Instance.OnStrikeFired += ExtraWaterDmg;
        }

        public void ExtraWaterDmg(StrikeData strikeData)
        {                    
            if (targetController== strikeData.striker)
            {
                if (strikeData.attackType == AttackType.Melee && strikeData.skillInclination == SkillInclination.Physical)
                {
                    strikeData.targets.ForEach(t => Debug.Log("STRIKE DATA " + t.name)); 
                    strikeData.targets.ForEach(t => t.damageController.ApplyDamage(strikeData.striker,
                                                 CauseType.CharSkill, (int)skillName, DamageType.Water, 40f, false));
                }                       
            }          
        }     
        public override void SkillEnd()
        {
            base.SkillEnd(); 
            CombatEventService.Instance.OnStrikeFired -= ExtraWaterDmg;
        }
        public override void DisplayFX1()
        {
            str1 = $"+50%<style=Water> Water </style>added on Ally Physical Melee attacks, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
     
        }

        public override void ApplyFX1()
        {   
        }

        public override void ApplyFX2()
        {


        }

        public override void ApplyFX3()
        {

        }

        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.SingleTargetRangeStrike(PerkType.None);

        }

        public override void DisplayFX3()
        {
          
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


