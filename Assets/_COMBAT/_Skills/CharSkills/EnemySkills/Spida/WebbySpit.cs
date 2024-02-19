using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class WebbySpit : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.WebbySpit;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override string desc => "this is xxx";

        private float _chance = 50f;
        public override float chance { get => _chance; set => _chance = value; }

        //earth choker
        //arachnid;
        public override void PopulateTargetPos()
        {
        skillModel.targetPos.Clear();
            for (int i = 1; i <= 7; i++)
            {
                    CellPosData cellPosData = new CellPosData(CharMode.Ally, i); // Allies
                    DynamicPosData dyna = GridService.Instance.gridView
                                           .GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                    if (dyna != null)
                    {
                        skillModel.targetPos.Add(cellPosData);
                    }                
            }
    
        }
        public override void ApplyFX1()
        {
         		if(targetController)
				 targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                    , charController.charModel.charID, CharStateName.Rooted, skillModel.timeFrame, skillModel.castTime);
      
		}

        public override void ApplyFX2()
        {
			   chance = 50f;
				if (chance.GetChance())
               targetController.damageController.ApplyLowPoison(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID);

                       
        }

        public override void ApplyFX3()
        {
          			
			 if(targetController)
                targetController.ChangeStat(CauseType.CharSkill
                             , (int)skillName, charController.charModel.charID, StatName.stamina,
                               -UnityEngine.Random.Range(3, 6));
				targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                            , AttribName.earthRes, -15, TimeFrame.EndOfRound, skillModel.castTime, false);

        }

        public override void DisplayFX1()
        {
            str1 = $"110% <style=Earth>Earth</style> Dmg";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str2 = "50% <style=Earth>Low Poison</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX3()
        {
        str3 = "Apply <style=states>Rooted</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX4()
        {
        }


        public override void ApplyVFx()
        {
			
			//change to range strik no movement
            if (targetController)
            {
                Sequence Seq = DOTween.Sequence();
                Seq.AppendCallback(() => SkillService.Instance.skillFXMoveController.RangedStrike(PerkType.None, skillModel))
                    .AppendInterval(0.5f)
                    .AppendCallback(() => GridService.Instance.gridMovement.MovebyRow(myDyna, MoveDir.Forward, 1));

                Seq.Play();
            }            
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
