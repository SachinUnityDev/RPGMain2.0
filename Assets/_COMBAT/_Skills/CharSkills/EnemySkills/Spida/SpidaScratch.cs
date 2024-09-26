using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class SpidaScratch : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.SpidaScratch;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override string desc => "this is xxx";

        private float _chance = 0f;
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
           // FirstOnSamelane();

        }
        public override void ApplyFX1()
        {
          
    //ignore armor vs Poisoned 
        }

        public override void ApplyFX2()
        {
            if (IsTargetMyEnemy())
            {
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                            , AttribName.haste, -2, TimeFrame.EndOfRound, skillModel.castTime, false);
				targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                            , AttribName.earthRes, -15, TimeFrame.EndOfRound, skillModel.castTime, false);

             
            }
        }

        public override void ApplyFX3()
        {
          
			//if (30f.GetChance() && targetController)
               // targetController.tempTraitController.ApplyTempTrait(CauseType.CharSkill, (int)skillName
                                        //, charController.charModel.charID, TempTraitName.RatBiteFever);

        }

        public override void DisplayFX1()
        {
            str1 = $"90% <style=Physical>Phyical</style> Dmg";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str2 = "-2 Haste";
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
            if (targetController)
            {
                Sequence Seq = DOTween.Sequence();
                Seq.AppendCallback(() => SkillService.Instance.skillFXMoveController.MeleeStrike(PerkType.None, skillModel))
                    .AppendInterval(0.5f)
                    .AppendCallback(() => GridService.Instance.gridMovement.MovebyRow(myDyna, MoveDir.Forward, 1));

                Seq.Play();
            }            
        }



        public override void PopulateAITarget()
        {
            base.PopulateAITarget();
            if (SkillService.Instance.currentTargetDyna == null && skillModel.targetPos.Count >0)
            {
                CellPosData cell = skillModel.targetPos[0];
                DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);
                if (dyna != null)
                    SkillService.Instance.currentTargetDyna = dyna;
                else
                    Debug.Log("target DYNA found null ");  // case taken care off in skillController
            } 
        }

        public override void ApplyMoveFx()
        {
           
        }
    }



}
