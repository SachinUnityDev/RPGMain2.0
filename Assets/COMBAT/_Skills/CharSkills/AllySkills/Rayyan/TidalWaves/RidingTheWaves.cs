using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 


namespace Combat
{
    public class RidingTheWaves : PerkBase
    {
        public override PerkNames perkName => PerkNames.RidingTheWaves;
        public override PerkType perkType => PerkType.B3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() {PerkNames.CrazyWaves, PerkNames.FeelOfImpact };

        public override string desc => "Riding the waves.. .";

        public override CharNames charName => CharNames.Rayyan;

        public override SkillNames skillName => SkillNames.TidalWaves;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        //    public override List<DynamicPosData> targetDynas => new List<DynamicPosData>();
      
        public override void AddTargetPos()
        {
            if (skillModel == null) return;
            if (currDyna != null && skillModel != null)
            {
                CellPosData cell = new CellPosData(currDyna.charMode, currDyna.currentPos);
                skillModel.targetPos.Add(cell);
            }
            // should I mark my self as target 
            //myDyna.currentPos == i))
            //    {
            //    CellPosData cellPosData = new CellPosData(CharMode.Ally, i);
            //    DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);

        }
  
        public override void ApplyFX1()
        {
            CharStatesService.Instance
                .ApplyCharState(charGO, CharStateName.Soaked
                                     , charController, CauseType.CharSkill, (int)skillName);
        }

        public override void ApplyFX2()
        {
          
        }

        public override void ApplyFX3()
        {
            CombatService.Instance.mainTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>()
                    .buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , StatsName.morale, -3, TimeFrame.EndOfRound, skillModel.castTime, false));

            CombatService.Instance.mainTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>()
                    .buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , StatsName.luck, -3, TimeFrame.EndOfRound, skillModel.castTime, false ));
        }

        public override void SkillEnd()
        {
            base.SkillEnd();
            CharStatesService.Instance.RemoveCharState(charGO,  CharStateName.Soaked);
           // CombatService.Instance.mainTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>()
           //.ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.morale, 3));

           // CombatService.Instance.mainTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>()
           //  .ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.luck, 3));

        }


        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.MultiTargetRangeFX(PerkType.B3);

        }
        public override void ApplyMoveFX()
        {
            GridService.Instance.gridController.Move2Pos(currDyna, 7);
        }
        public override void DisplayFX1()
        {
            str1 = $"<style=Self> <style=States> Soaked </style>, 2 rds";
            SkillService.Instance.skillModel.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy> -3 Morale and Luck, 2 rds";
            SkillService.Instance.skillModel.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"<style=Move> Move </style>to pos 7";
            SkillService.Instance.skillModel.descLines.Add(str3);
        }

        public override void DisplayFX4()
        {
        }

    }


}


