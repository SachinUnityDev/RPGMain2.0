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
      
  
        public override void ApplyFX1()
        {
            if(targetController)
                charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                    , charController.charModel.charID, CharStateName.Soaked,skillModel.timeFrame, skillModel.castTime); 
        }

        public override void ApplyFX2()
        {
            if (targetController)
            {
                List<DynamicPosData> allAdjOccupied = GridService.Instance.gridController
                                                                  .GetAllAdjDynaOccupied(currDyna);

                allAdjOccupied.ForEach(t=>t.charGO.GetComponent<CharController>().charStateController.
               ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                    , charController.charModel.charID, CharStateName.Sneaky, skillModel.timeFrame, skillModel.castTime));

            }
        }

        public override void ApplyFX3()
        {
      
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
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy> -3 Morale and Luck, 2 rds";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"<style=Move> Move </style>to pos 7";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }

        public override void DisplayFX4()
        {
            str0 = $"<style=States> Add sneaky state to adj targets </style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }

    }


}


