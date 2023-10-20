using System.Collections;
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
        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.staminaReq = 10; 
        }
        public override void ApplyFX1()
        {
            if (targetController.charModel.charName == CharNames.RatKing ||
                targetController.charModel.charName == CharNames.Kongamato)
                return; 
           if(targetController.charStateController.HasCharState(CharStateName.BleedHighDOT))
           {
                StatData statData = targetController.GetStat(StatName.health);
                float hpPercent = statData.currValue / statData.maxLimit; 

               if (hpPercent < 30f)
               {
                    // kill TARGET  !!!!
                 
               }
           }
        }
        public override void ApplyFX2()
        {

        }

        public override void ApplyFX3()
        {

        }
        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy>Kills Bleeding Enemy Health < 30%";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }
        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy> {skillModel.staminaReq} <style=Stamina> Stamina</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }
        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
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

