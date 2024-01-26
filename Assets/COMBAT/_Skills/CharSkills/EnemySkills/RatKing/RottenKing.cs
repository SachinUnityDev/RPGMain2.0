using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class RottenKing : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }

        public override SkillNames skillName => SkillNames.RottenKing;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override string desc => "Rotten King";

        private float _chance = 40f;
        public override float chance { get => _chance; set => _chance = value; }

        List<DynamicPosData> firstRowChar = new List<DynamicPosData>();

        public override void PopulateTargetPos()
        {
            AnyWithCharMode(CharMode.Enemy);
        }

        public override void ApplyFX1()
        {

            foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
            {
                dyna.charGO.GetComponent<CharController>().buffController.ApplyBuff(CauseType.CharSkill, (int)skillName,
                   charController.charModel.charID , AttribName.morale, +3, skillModel.timeFrame, skillModel.castTime, true); 
            }
        }

        public override void ApplyFX2()
        {
            AttribData dmgMaxData = charController.GetAttrib(AttribName.dmgMax);
            float dmgInc = dmgMaxData.currValue * 0.5f;

            AttribData dmgMinData = charController.GetAttrib(AttribName.dmgMin);
            float dmgMinInc = dmgMinData.currValue * 0.5f;

            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName,
                  charController.charModel.charID, AttribName.dmgMax, dmgInc, skillModel.timeFrame, skillModel.castTime, true);

            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName,
                 charController.charModel.charID, AttribName.dmgMin, dmgMinInc, skillModel.timeFrame, skillModel.castTime, true);
        }

        public override void ApplyFX3()
        {
       
        }

        public override void ApplyVFx()
        {

        }

        public override void ApplyMoveFx()
        {

        }

        public override void DisplayFX1()
        {

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

        public override void PopulateAITarget()
        {
            PopulateTargetPos();
            SkillService.Instance.currentTargetDyna = null;

            DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(skillModel.targetPos[0].charMode, skillModel.targetPos[0].pos);
            SkillService.Instance.currentTargetDyna = dyna;
        }
    }
}