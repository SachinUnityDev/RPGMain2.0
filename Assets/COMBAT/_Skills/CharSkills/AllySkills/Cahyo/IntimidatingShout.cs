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
        public override string desc => "This is intimading shout";
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void PopulateTargetPos()
        {
            SelfTarget(); 
            AnyWithCharMode(CharMode.Enemy); 
        }
        public override void ApplyFX1()
        {
            CombatService.Instance.mainTargetDynas.AddRange(GridService.Instance.GetAllByCharMode(CharMode.Enemy));
            foreach (var dyna in CombatService.Instance.mainTargetDynas)
            {
                dyna.charGO.GetComponent<CharController>().buffController
                    .ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.morale, -2, skillModel.timeFrame, skillModel.castTime, false);
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
            str0 = "-2 Morale";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
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

    
        public override void PopulateAITarget()
        {
          
        }

        public override void ApplyMoveFx()
        {
        }
    }

}
