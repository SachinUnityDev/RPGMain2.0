using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class RunAway : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.RunAway;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override string desc => "this is run away";   

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
  

        public override void PopulateTargetPos()
        {
            skillModel.targetPos.Clear();   
            CellPosData selfCellPos = new CellPosData(myDyna.charMode, myDyna.currentPos);
            skillModel.targetPos.Add(selfCellPos);                
        }
        public override void ApplyFX1()
        {
            //charController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID
            //        , AttribName.dodge, 3);                 
        }
        public override void ApplyFX2()
        {
          // GridService.Instance.gridMovement.MovebyRow(myDyna, MoveDir.Backward, 2);
        }

        public override void ApplyFX3()
        {
        }
     

        public override void DisplayFX1()
        {
            str1 = $"Move back 2";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"+3 Dodge, {skillModel.castTime} rd";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
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
            base.PopulateAITarget();
            if (SkillService.Instance.currentTargetDyna != null) return;
            if(myDyna == null)
            {
                Debug.LogError("mydyna null" + myDyna.charGO.name); return; 
            }
            SkillService.Instance.currentTargetDyna = myDyna; 
        }

        public override void ApplyMoveFx()
        {
        }
    }

}


