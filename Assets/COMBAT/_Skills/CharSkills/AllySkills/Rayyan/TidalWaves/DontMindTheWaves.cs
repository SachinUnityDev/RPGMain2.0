using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class DontMindTheWaves : PerkBase
    {
        public override PerkNames perkName => PerkNames.DontMindTheWaves;
        public override PerkType perkType => PerkType.A2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.SweepingWaves };
        public override string desc => "this is Don t mind the waves ";
        public override CharNames charName => CharNames.Rayyan;
        public override SkillNames skillName => SkillNames.TidalWaves;
        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        List<DynamicPosData> allAlliesINFront = new List<DynamicPosData>();
        List<DynamicPosData> allEnemies = new List<DynamicPosData>();
        public override void AddTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear(); CombatService.Instance.mainTargetDynas.Clear(); allEnemies.Clear();
            if (currDyna != null && skillModel !=null)
            {
                allAlliesINFront = GridService.Instance?.GetAllInFrontINSameParty(currDyna);
                allEnemies = GridService.Instance.GetAllByCharMode(CharMode.Enemy);
                CombatService.Instance.colTargetDynas.AddRange(allAlliesINFront);
                CombatService.Instance.mainTargetDynas.AddRange(allEnemies);
                if (allAlliesINFront.Count > 0)
                {
                    foreach (DynamicPosData dyna in allAlliesINFront)
                    {
                        CellPosData cell = new CellPosData(dyna.charMode, dyna.currentPos);
                        skillModel.targetPos.Add(cell);
                    }
                }

                if (allEnemies.Count > 0)
                {
                    foreach (DynamicPosData dyna in allEnemies)
                    {
                        CellPosData cell = new CellPosData(dyna.charMode, dyna.currentPos);
                        skillModel.targetPos.Add(cell);
                    }
                }
            }
           
        }

        public override void SkillHovered()
        {
            base.SkillHovered();
            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX3;
        }

        public override void ApplyFX1()  // Soak all in front enemy n allies 
        {
            allEnemies.ForEach(t => CharStatesService.Instance
                                .ApplyCharState(t.charGO, CharStateName.Soaked
                                     , charController, CauseType.CharSkill, (int)skillName));
            
            allAlliesINFront.ForEach(t => CharStatesService.Instance
                                    .ApplyCharState(t.charGO, CharStateName.Soaked
                                     , charController, CauseType.CharSkill, (int)skillName));
        }

        public override void ApplyFX2()  
        {
            foreach (DynamicPosData dyna in allAlliesINFront)
            {
                dyna.charGO.GetComponent<CharController>().buffController.ApplyBuff(CauseType.CharSkill
                    , (int)skillName, charID, StatsName.haste, 2, TimeFrame.EndOfRound, skillModel.castTime, true);
            }
            foreach (DynamicPosData dyna in allEnemies)
            {
                dyna.charGO.GetComponent<CharController>().buffController.ApplyBuff(CauseType.CharSkill, (int)skillName
                    , charID, StatsName.haste, -2, TimeFrame.EndOfRound, skillModel.castTime, false);
            }
        }

        public override void ApplyFX3()
        {
           
        }

      
        public override void SkillEnd()
        {
            base.SkillEnd();
            //foreach (DynamicPosData dyna in allAlliesINFront)
            //{
            //    dyna.charGO.GetComponent<CharController>()
            //        .ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.haste, -2);
            //}
            //foreach (DynamicPosData dyna in allEnemies)
            //{
            //    dyna.charGO.GetComponent<CharController>()
            //        .ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.haste, 2);
            //}
        }
   
        public override void ApplyVFx()
        {
        }
        public override void ApplyMoveFX()
        {
            foreach (DynamicPosData dyna in allAlliesINFront)
            {
                GridService.Instance.gridMovement.MovebyRow(dyna, MoveDir.Forward, 1);
            }

            foreach (DynamicPosData dyna in allEnemies)
            {
                GridService.Instance.gridMovement.MovebyRow(dyna, MoveDir.Backward, 1);
            }
        }
        public override void DisplayFX1()
        {
            str0 = $"<style=Allies> <style=Enemy> <style=States> Soaked </style>, {skillModel.castTime} rd";
            SkillService.Instance.skillCardData.descLines.Add(str0);
        }

        public override void DisplayFX2()
        {
            str1 = $"<style=Allies> <style=Move> Push </style>";
            SkillService.Instance.skillCardData.descLines.Add(str1);
        }

        public override void DisplayFX3()
        {
            str3 = $"<style=Allies> Init +2, 2 rds";
            SkillService.Instance.skillCardData.descLines.Add(str3);
        }

        public override void DisplayFX4()
        {
        }

       
    
    }




}
