using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class NoMatterWhoYouAre : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.IntimidatingShout;
        public override SkillLvl skillLvl => SkillLvl.Level1;
        public override PerkSelectState state { get; set; }
        public override PerkNames perkName => PerkNames.NoMatterWhoYouAre;
        public override PerkType perkType => PerkType.B1;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        public override string desc => "No matter who you are !";        
        public override float chance { get; set; }
      

   
        public override void ApplyFX1()
        {
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.acc, +3
                , skillModel.timeFrame, skillModel.castTime, true);

            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.dodge, +3
                , skillModel.timeFrame, skillModel.castTime, true);
        }

        public override void ApplyFX2()
        {
           if(CombatService.Instance.mainTargetDynas.Count ==1)
                targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                       , charController.charModel.charID, CharStateName.Despaired);
        }
        public override void ApplyFX3()
        {
            AttribData moraleAttrib = charController.GetAttrib(AttribName.morale);
            if (moraleAttrib.currValue == 12)        
            {
                RegainAP();
            }
        }
        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy><style=States> Despair</style> solo enemy";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
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

