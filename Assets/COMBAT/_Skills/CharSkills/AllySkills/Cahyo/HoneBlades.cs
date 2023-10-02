using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



namespace Combat
{
    public class HoneBlades : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.HoneBlades;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override string desc => "Hone blades";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void PopulateTargetPos()
        {
            //skillModel.targetPos.Clear();

            //for (int i = 1; i < 5; i++)
            //{
            //    if (!(myDyna.currentPos == i))
            //    {
            //        CellPosData cellPosData = new CellPosData(CharMode.Enemy, i);
            //        DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
            //        if (dyna != null)
            //        {
            //            skillModel.targetPos.Add(cellPosData);
            //        }
            //    }
            //}
        }
         
    
        public override void ApplyFX1()
        {
            //charController.ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.damage, skillModel.damageMod, 0f);
        }
        public override void DisplayFX1()
        {
            //str1 = $"-2 <style=Attributes>Focus,</style> {skillModel.castTime} rds";
            //SkillServiceView.Instance.skillCardData.descLines.Add(str1);
        }
        public override void DisplayFX2()
        {
        }
        public override void ApplyFX2()
        {
        }
        public override void ApplyFX3()
        {
        }
  
        public override void RemoveFX1()
        {
            SkillService.Instance.OnSkillApply -= ApplyFX1;
        }

        public override void RemoveFX2()
        {
        }

        public override void RemoveFX3()
        {
        }

        public override void RemoveVFX()
        {
        }
        public override void SkillEnd()
        {
            CombatEventService.Instance.OnEOR -= Tick; RemoveFX1();
          //  charController.ChangeStatRange(CauseType.CharSkill, (int)skillName, charController,StatsName.damage, -2, 0);
           
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }

      

        public override void PreApplyFX()
        {
          
        }

        public override void PostApplyFX()
        {
          
        }

        public override void ApplyVFx()
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

