using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace Combat
{
    public class NoPatience : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.NoPatience;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        public override string desc => "No patience";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        float  StackAmt = 0; 
        public override void PopulateTargetPos()
        {            
            if (skillModel == null) return;
            skillModel.targetPos.Clear();
            skillModel.targetPos.Add(new CellPosData(myDyna.charMode, myDyna.currentPos));
        }

        public override void BaseApply()
        {
            base.BaseApply();                     
            CombatEventService.Instance.OnEOC += NoPatienceWpIncrEnd; 
        }
      
        void NoPatienceWpIncrEnd()
        {
            Debug.Log("THIS MEHTHOD NAME" + MethodBase.GetCurrentMethod().Name);
            if (StackAmt >0 )
            {
                charController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID
                                  , AttribName.willpower, -1 * StackAmt, false);
                StackAmt = 0; 
            }    
            CombatEventService.Instance.OnEOC -= NoPatienceWpIncrEnd;
        }
        public override void ApplyFX1()
        {
            //Gain 5 Fortitude
            charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatName.fortitude, +5, false);

        }

        public override void ApplyFX2()
        {
            DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(CharMode.Ally, 1); 
            if(dyna != null)
            {
                GridService.Instance.gridController.SwapPos(myDyna, dyna); 

            }else
            {
                GridService.Instance.gridController.Move2Pos(myDyna, 1);
            }
        }

        public override void ApplyFX3()
        {

            if (StackAmt < 3)
            {
                charController.ChangeAttrib(CauseType.CharSkill, (int)skillName, charID, AttribName.willpower, +1, false);
                StackAmt++;
            }
        }

        public override void ApplyVFx()
        {
        }

        public override void PopulateAITarget()
        {
        }

        public override void DisplayFX1()
        {
            str0 = $"+1<style=Attributes> Willpower </style>until eoc, stacks up to 3";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);
        }

        public override void DisplayFX2()
        {
            str1 = $"+5 <style=Attributes>Fortitude</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);           
        }

        public override void DisplayFX3()
        {
            str2 = $"<style=Move> Move </style>to pos 1";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }

        public override void DisplayFX4()
        {
        }

        public override void ApplyMoveFx()
        {
        }
    }



}
