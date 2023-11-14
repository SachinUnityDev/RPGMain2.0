using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Combat
{
    public class IntimidatingShout : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.IntimidatingShout;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        public override StrikeTargetNos strikeNos => StrikeTargetNos.Multiple;
        public override string desc => "This is intimading shout";


        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void PopulateTargetPos()
        {
            AllInCharMode(CharMode.Enemy); 
        }
        public override void ApplyFX1()
        {
           
        }
        public override void ApplyFX2()
        {        
            foreach (var dyna in CombatService.Instance.mainTargetDynas)
            {
                dyna.charGO.GetComponent<CharController>().buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.morale, -2
                    , skillModel.timeFrame, skillModel.castTime, false);
            }
        }
        public override void ApplyFX3()
        {

        }
        public override void DisplayFX1()
        {
            str0 = "<margin=1.2em>Buff, Debuff";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);

            str1 = $"<style=Performer>+3 Acc and Dodge";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy> -2 Morale";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
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

    
        public override void PopulateAITarget()
        {
          
        }

        public override void ApplyMoveFx()
        {
        }
    }

}
