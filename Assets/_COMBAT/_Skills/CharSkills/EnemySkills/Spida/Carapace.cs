using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class Carapace : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.Carapace;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override string desc => "this is xxx";
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        //earth choker
        //arachnid;
        public override void PopulateTargetPos()
        {
      
          SelfTarget();

        }
        public override void ApplyFX1()
        {
          
	//Trigger max Armor
        }

        public override void ApplyFX2()
        {
			
          	if(targetController)
               targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                            , AttribName.dodge, +2, TimeFrame.EndOfRound, skillModel.castTime, true);
			             
           
        }

        public override void ApplyFX3()
        {
          // check if true, 
            if(targetController)
                targetController.strikeController.AddThornsBuff(DamageType.Earth, 3, 6
                                                      , skillModel.timeFrame, skillModel.castTime);
			
			
		

        }

        public override void DisplayFX1()
        {
            str1 = $"Trigger Max Armor";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"+2 Dodge";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"Thorns: 3-5 <style=Earth>Earth</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }

        public override void DisplayFX4()
        {
        }


		public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.RangedStrike(PerkType.None, skillModel);
        }



        public override void PopulateAITarget()
        {
            base.PopulateAITarget();
            if (SkillService.Instance.currentTargetDyna == null)
            {
                CellPosData cell = skillModel.targetPos[0];
                DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);
                if (dyna != null)
                    SkillService.Instance.currentTargetDyna = dyna;
                else
                    Debug.Log("target DYNA found null ");
            } 
        }

        public override void ApplyMoveFx()
        {
           
        }
    }



}
