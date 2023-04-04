using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class RatBite : SkillBase
    {
        public override SkillModel skillModel { get; set; }

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.RatBite;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override string desc => "this is Rat bite";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        Rodent rodent;
        SenseTheWeak senseTheWeak;
        public override void PopulateTargetPos()
        {
            skillModel.targetPos.Clear();
            int pos = GridService.Instance.GetDyna4GO(charGO).currentPos;
            CharMode charMode = charController.charModel.charMode;
            CellPosData cellPos = new CellPosData(charMode, pos);
            List<DynamicPosData> InSameLane =  GridService.Instance.GetInSameLaneOppParty(cellPos);
            
            if (InSameLane.Count > 0)
            {
                skillModel.targetPos.Add(new CellPosData(InSameLane[0].charMode, InSameLane[0].currentPos));
                Debug.Log("RAT BITE TARGET SET" + skillModel.targetPos  + InSameLane[0].charGO); 
            }
            

        }
        public override void ApplyFX1()
        {
            if (IsTargetMyEnemy())
            {
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                                                            , DamageType.Physical, 125f);
                
            }
        }

        public override void ApplyFX2()
        {
            if (IsTargetMyEnemy())
            {
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                            , AttribName.haste, -3, TimeFrame.EndOfRound, skillModel.castTime, false);
                GridService.Instance.gridMovement.MovebyRow(myDyna, MoveDir.Forward, 1);
            }
        }

        public override void ApplyFX3()
        {
            rodent = new Rodent();
           // rodent.ApplyPassiveFX(targetController);
            senseTheWeak = new SenseTheWeak();
           // senseTheWeak.ApplyPassiveFX(targetController);

        }

        //public override void SkillEnd()
        //{
        //    base.SkillEnd();
        //    if (IsTargetMyEnemy())
        //    {
        //        //targetController.ChangeStat(CauseType.CharSkill, (int)skillName, charController
        //        //    , StatsName.haste, +3);
        //      //  if (rodent != null)
        //          //  rodent.RemovePassiveFX(targetController);
        //       // if (senseTheWeak != null)
        //          //  senseTheWeak.RemovePassiveFX(targetController);
        //    }
        //}

        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy>125% <style=Physical>Phyical</style> dmg";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy>-3 Haste";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"<style=Enemy><style=States>Pull self</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str3);
        }

        public override void DisplayFX4()
        {
        }


        public override void ApplyVFx()
        {
        }



        public override void PopulateAITarget()
        {
            PopulateTargetPos();
            CellPosData cell = skillModel.targetPos[0];
            DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);
            if (dyna != null)
                SkillService.Instance.currentTargetDyna = dyna;
            else
                Debug.Log("target DYNA found null "); 
        }

        public override void ApplyMoveFx()
        {
        }
    }



}
