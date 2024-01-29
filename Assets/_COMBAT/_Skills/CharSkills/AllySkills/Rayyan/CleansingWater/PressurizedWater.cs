using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq; 

namespace Combat
{
    public class PressurizedWater : PerkBase
    {
        public override CharNames charName => CharNames.Rayyan;
        public override SkillNames skillName => SkillNames.CleansingWater;
        public override SkillLvl skillLvl => SkillLvl.Level2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override string desc => "80% --> %120 Water /n Push target /n 15% confuse, 1 rd";
        public override List<PerkNames> preReqList => new List<PerkNames>() {PerkNames.MurkyWaters };
        public override PerkNames perkName => PerkNames.PressurizedWater;
        public override PerkType perkType => PerkType.A2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void SkillHovered()
        { // only for enemies
            base.SkillHovered();
            skillModel.damageMod = 120f;
               
        }

     
        public override void ApplyFX1()
        {
            if (IsTargetEnemy())
            {
                float percent = 15f;
                if (percent.GetChance())
                    charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName, charController.charModel.charID
                                                        , CharStateName.Feebleminded, skillModel.timeFrame, skillModel.castTime);
            }
        }
        public override void ApplyFX2()
        {

            
        }
        public override void ApplyFX3()
        {
           
        }

        public override void ApplyVFx()
        {
            
        }
        public override void ApplyMoveFX()
        {
            if (IsTargetEnemy())
            {
                if (GridService.Instance.targetSelected != null)
                {
                    GridService.Instance.gridMovement.MovebyRow(GridService.Instance.targetSelected
                        , MoveDir.Backward, 1);
                }
            }
        }
        public override void DisplayFX1()
        {
            str1 = "<style=Move>Push</style> enemy 1";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "15% <style=States> Feebleminded</style> on enemy";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX3()
        {
         
        }

        public override void DisplayFX4()
        {
            
        }
        public override void InvPerkDesc()
        {
            perkDesc = "<style=Water>Water</style> Dmg: 80% -> 120%";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "<style=Move>Push</style> enemy 1";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "15% <style=States> Feebleminded</style> on enemy";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }



    }


}

