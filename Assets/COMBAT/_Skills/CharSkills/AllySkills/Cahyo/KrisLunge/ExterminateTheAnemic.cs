﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class ExterminateTheAnemic : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.KrisLunge;
        public override SkillLvl skillLvl => SkillLvl.Level3;       
        public override string desc => "EX the anemic";
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override PerkNames perkName => PerkNames.ExterminateTheAnemic;

        public override PerkType perkType => PerkType.B3; 

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None }; 

        //public override void SkillInit()
        //{
        //    skillModel = SkillService.Instance.allSkillModels
        //                                                 .Find(t => t.skillName == skillName);

        //    charController = CharacterService.Instance.GetCharCtrlWithName(charName);
        //    skillModel.staminaReq = 9;
        //}
        //public override void SkillHovered()
        //{
        //    SkillInit();
        //    SkillServiceView.Instance.skillCardData.skillModel = skillModel;

        //    SkillService.Instance.SkillHovered += DisplayFX1;
        //    SkillService.Instance.SkillHovered += DisplayFX2;

        //}
        //public override void SkillSelected()
        //{
        //    SkillService.Instance.SkillApply += BaseApply;
        //    SkillService.Instance.SkillApply += ApplyFX1;
        //    SkillService.Instance.SkillApply += ApplyFX2;
        //    SkillService.Instance.SkillApply += ApplyFX3;
        //}
        public override void BaseApply()
        {
            targetGO = SkillService.Instance.currentTargetDyna.charGO;
            targetController = targetGO.GetComponent<CharController>();
            CombatEventService.Instance.OnEOR += Tick;
            skillModel.lastUsedInRound = CombatService.Instance.currentRound;
        }
        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy>Kills Bleeding Enemy Health < 35%";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }
        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy> {skillModel.staminaReq} <style=Stamina> Stamina</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }
        public override void ApplyFX1()
        {
            if (CharStatesService.Instance.HasCharState(targetGO, CharStateName.BleedHighDOT)
                 || CharStatesService.Instance.HasCharState(targetGO, CharStateName.BleedMedDOT)
                 || CharStatesService.Instance.HasCharState(targetGO, CharStateName.BleedLowDOT))
            {
                if (targetController.GetStat(StatsName.health).baseValue < 35f)
                {
                    // kill TARGET  !!!!!... exceptions are Kongomato 
                }
            }
        }

        public override void ApplyFX2()
        {

        }

        public override void ApplyFX3()
        {

        }

  

        public override void Tick()
        {

        }
        public override void RemoveFX1()
        {
        }

        public override void RemoveFX2()
        {
        }

        public override void RemoveFX3()
        {
        }


        public override void SkillEnd()
        {
        }





        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }

        public override void WipeFX1()
        {
        }

        public override void WipeFX2()
        {
        }

        public override void WipeFX3()
        {
        }

        public override void WipeFX4()
        {
        }



        public override void ApplyVFx()
        {
        }

        public override void ApplyMoveFX()
        {
        }
    }

}

