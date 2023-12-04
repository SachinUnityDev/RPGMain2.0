using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



namespace Combat
{
    public class HoneBlades : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.HoneBlades;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override string desc => "Hone blades";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void PopulateTargetPos()
        {
            skillModel.targetPos.Clear();
            int pos = GridService.Instance.GetDyna4GO(charGO).currentPos;
            skillModel.targetPos.Add(new CellPosData(myDyna.charMode, pos));
        }
        public override void ApplyFX1()
        {
            if(targetController)
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                            , AttribName.dmgMin,2 , skillModel.timeFrame, skillModel.castTime, true);
        }
        public override void ApplyFX2()
        {
        }
        public override void ApplyFX3()
        {
        }
        public override void DisplayFX1()
        {
            str1 = $"+2 Min Dmg";
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
        public override void PopulateAITarget()
        {
           
        }
        public override void ApplyMoveFx()
        {
        }
    }
}

