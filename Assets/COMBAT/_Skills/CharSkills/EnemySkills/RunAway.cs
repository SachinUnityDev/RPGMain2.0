using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class RunAway : SkillBase
    {
        public override SkillModel skillModel { get; set; }
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.RunAway;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override string desc => "this is run away";   

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        Rodent rodent;
        Vermin vermin;

        public override void PopulateTargetPos()
        {
            CellPosData selfCellPos = new CellPosData(myDyna.charMode, myDyna.currentPos);
            skillModel.targetPos.Add(selfCellPos);                
        }
        public override void ApplyFX1()
        {
            charController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.dodge, 3);                 
        }
        public override void ApplyFX2()
        {
           GridService.Instance.gridMovement.MovebyRow(myDyna, MoveDir.Backward, 2);
        }

        public override void ApplyFX3()
        {
        }
        public override void SkillEnd()
        {
            base.SkillEnd();

            //targetController.ChangeStat(CauseType.CharSkill, (int)skillName, charController
            //    , StatsName.dodge, -3);
            // if (rodent != null)
            //  rodent.RemovePassiveFX(targetController);
            //if (vermin != null)
            //    vermin.RemovePassiveFX(targetController);

        }

        public override void DisplayFX1()
        {
            str1 = $"Move back 2";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"+3 Dodge, {skillModel.castTime} rd";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }


        public override void ApplyVFx()
        {
        }

        public override void PopulateAITarget()
        {
            SkillService.Instance.currentTargetDyna = myDyna;
        }

        public override void ApplyMoveFx()
        {
        }
    }

}


