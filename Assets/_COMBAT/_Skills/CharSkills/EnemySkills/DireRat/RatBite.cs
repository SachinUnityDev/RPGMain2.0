using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class RatBite : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.RatBite;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override string desc => "this is Rat bite";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        //Rodent rodent;
        //SenseTheWeak senseTheWeak;
        public override void PopulateTargetPos()
        {
            //skillModel.targetPos.Clear();
            //int pos = GridService.Instance.GetDyna4GO(charGO).currentPos;
            //CharMode charMode = charController.charModel.charMode;
            //CellPosData cellPos = new CellPosData(charMode, pos);
            //List<DynamicPosData> InSameLane =  GridService.Instance.GetInSameLaneOppParty(cellPos);

            //if (InSameLane.Count > 0)
            //{
            //    skillModel.targetPos.Add(new CellPosData(InSameLane[0].charMode, InSameLane[0].currentPos));
            //    Debug.Log("RAT BITE TARGET SET" + skillModel.targetPos  + InSameLane[0].charGO); 
            //}
            FirstOnSamelane();

        }
        public override void ApplyFX1()
        {
          
            if (IsTargetMyEnemy())
            {
                if (targetController.tempTraitController.HasTempTrait(TempTraitName.RatBiteFever))
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                                                            , DamageType.Physical, skillModel.damageMod + 40f, skillModel.skillInclination);
                else
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                                                             , DamageType.Physical, skillModel.damageMod, skillModel.skillInclination);
            }
        }

        public override void ApplyFX2()
        {
            if (IsTargetMyEnemy())
            {
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                            , AttribName.haste, -3, TimeFrame.EndOfRound, skillModel.castTime, false);

             
            }
        }

        public override void ApplyFX3()
        {
          
            if (30f.GetChance() && targetController)
                targetController.tempTraitController.ApplyTempTrait(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID, TempTraitName.RatBiteFever);

        }

        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy>125% <style=Physical>Phyical</style> dmg";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy>-3 Haste";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"<style=Enemy><style=States>Pull self</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }

        public override void DisplayFX4()
        {
        }


        public override void ApplyVFx()
        {
            if (targetController)
            {
                Sequence Seq = DOTween.Sequence();
                Seq.AppendCallback(() => SkillService.Instance.skillFXMoveController.JumpStrike(PerkType.None, skillModel))
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
                    Debug.Log("target DYNA found null ");
            } 
        }

        public override void ApplyMoveFx()
        {
           
        }
    }



}
