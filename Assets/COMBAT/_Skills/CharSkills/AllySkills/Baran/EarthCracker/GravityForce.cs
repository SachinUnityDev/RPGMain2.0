using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class GravityForce : PerkBase
    {
        public override PerkNames perkName => PerkNames.GravityForce;
        public override PerkType perkType => PerkType.A2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.SplintersOfEarth };

        public override string desc => "(PR: A1)/n  Rooted on initial target/n On collateral targets, -1 luck, 2 rds /n 50% dmg on initial target if rooted";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.EarthCracker;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 60f;
        public override float chance { get => _chance; set => _chance = value; }

        List<DynamicPosData> colDynaCopy = new List<DynamicPosData>(); 
  
        public override void BaseApply()
        {
            base.BaseApply();            
            DynamicPosData targetDyna = GridService.Instance.GetDyna4GO(targetGO);
            colDynaCopy.Clear(); 
            if (targetDyna != null)
            {
                CombatService.Instance.colTargetDynas =
                   GridService.Instance.gridController.GetAllAdjDynaOccupied(targetDyna);

                colDynaCopy.AddRange(CombatService.Instance.colTargetDynas);                   
            }               
        }
        public override void ApplyFX1()
        {
            if (charController.charStateController.HasCharState(CharStateName.Rooted))
                targetController
                    .ChangeAttrib(CauseType.CharSkill, (int)skillName, charID, AttribName.luck, 2);

            if (chance.GetChance())
                charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName,
                charController.charModel.charID, CharStateName.Rooted);


        }
        public override void SkillEnd()
        {
            base.SkillEnd();
            //colDynaCopy.ForEach(t => t.charGO.GetComponent<CharController>()
            //  .buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
            //  , StatsName.luck, 1, TimeFrame.EndOfRound, skillModel.castTime, true));
            charController.charStateController.RemoveCharState(CharStateName.Rooted);
        }
        public override void ApplyFX2()
        {
            colDynaCopy.ForEach(t => t.charGO.GetComponent<CharController>()
              .buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
              , AttribName.luck, 1, TimeFrame.EndOfRound, skillModel.castTime, true));
        }

        public override void ApplyFX3()
        {
        }

        public override void ApplyMoveFX()
        {
        }

        public override void ApplyVFx()
        {
        }

        public override void DisplayFX1()
        {
            str0 = $"<style=States> Rooted </style>on main target, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);
        }

        public override void DisplayFX2()
        {
            str1 = $"+50%<style=Physical> Physical </style>on main target if already rooted";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX3()
        {
            str2 = $"-1<style=Attributes> Luck </style>on col targets, {skillModel.castTime} rds";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }

        public override void DisplayFX4()
        {
        }
  
    }
}

