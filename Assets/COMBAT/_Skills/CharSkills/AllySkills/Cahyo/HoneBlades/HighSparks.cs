﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class HighSparks : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.HoneBlades;
        public override SkillLvl skillLvl => SkillLvl.Level2;      
        public override PerkSelectState state { get; set; }
        public override PerkNames perkName => PerkNames.HighSparks;
        public override PerkType perkType => PerkType.B2;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.BlindingSparks };
        public override string desc => "High Sparks ";        
        public override float chance { get; set; }
     
    
        public override void ApplyFX1()
        {
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                , AttribName.dmgMax,2, skillModel.timeFrame, skillModel.castTime, true);
        }
        public override void ApplyFX2()
        {

        }

        public override void ApplyFX3()
        {

        }
        public override void DisplayFX1()
        {
            str1 = $"<style=Allies> max <style=Physical>Dmg</style> +2";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }
        public override void DisplayFX2()
        {

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
