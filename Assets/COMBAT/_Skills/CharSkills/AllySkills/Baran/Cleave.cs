using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Common;

namespace Combat
{
    public class Cleave : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.Cleave;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Multiple;
        public override string desc => "";
        private float _chance = 30f;
        public override float chance { get => _chance; set => _chance = value; }

        //List<DynamicPosData> targetDynas = new List<DynamicPosData>();

        public override void PopulateTargetPos()
        {
            if (skillModel == null) return;

            skillModel.targetPos.Clear(); CombatService.Instance.mainTargetDynas.Clear();
            //  targetDynas.Clear();
            if (myDyna.currentPos == 1)
            {
                AddTargetInRange(1, 3, CharMode.Enemy); 
            }
            else if (myDyna.currentPos == 4)
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, 1);
                AddTarget(cellPosData);
            }
            else if (myDyna.currentPos == 2)
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, 1);
                AddTarget(cellPosData); 
                CellPosData cellPosData2 = new CellPosData(CharMode.Enemy, 2);
                AddTarget(cellPosData2);
            }
            else if (myDyna.currentPos == 3)
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, 1);
                AddTarget(cellPosData); 
                CellPosData cellPosData2 = new CellPosData(CharMode.Enemy, 3);
                AddTarget(cellPosData2);
            }
        }
     

        public override void ApplyFX1()
        {
            Debug.Log("Apply FX reached"); 
          if(CombatService.Instance.mainTargetDynas.Count>0)
                CombatService.Instance.mainTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>().damageController
                    .ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Physical, skillModel.damageMod
                    , skillModel.skillInclination));
        }

        public override void ApplyFX2()
        {          
            if (chance.GetChance())            
                    CombatService.Instance.mainTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>()
                    .charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                            , charController.charModel.charID, CharStateName.BleedLowDOT));
        }

        public override void ApplyFX3()
        {

        }

 
        public override void DisplayFX1()
        {
            //str0 = "<margin=1.2em>Ranged";
            //SkillService.Instance.skillModelHovered.descLines.Add(str0);

            str1 = $"{skillModel.damageMod}% <style=Physical>Physical</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }


        public override void DisplayFX2()
        {
            str2 = $"Target Front Row if cast from 1, 1+2 if cast from 2, 1+3 if cast from 3";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"{chance}% <style=Bleed>Low Bleed </style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
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

        public override void ApplyMoveFx()
        {
        }
    }


}

