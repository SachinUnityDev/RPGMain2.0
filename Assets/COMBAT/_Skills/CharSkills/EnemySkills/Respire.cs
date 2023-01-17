using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 

namespace Combat
{
    public class Respire : SkillBase
    {
        public override SkillModel skillModel { get; set; }

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.Respire;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override string desc => "Respire and sleep";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void PopulateTargetPos()
        {
            skillModel.targetPos.Clear();
            int pos = GridService.Instance.GetDyna4GO(charGO).currentPos;
            CharMode charMode = charController.charModel.charMode;
            CellPosData cellPos = new CellPosData(charMode, pos);

            skillModel.targetPos.Add(cellPos);
        }

        public override void ApplyFX1()
        {
            StatData willPowerSD = charController.GetStat(StatsName.willpower);
            float maxStamina = willPowerSD.baseValue; 

            float value = 0.25f * maxStamina;
            charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID
                                                            , StatsName.stamina, +value); 
        }

        public override void ApplyFX2()
        {
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , StatsName.stamina, -2, skillModel.timeFrame, skillModel.castTime, false );
        }

        public override void ApplyFX3()
        {

        }

        public override void ApplyMoveFx()
        {
        }

        public override void ApplyVFx()
        {
        }

        public override void DisplayFX1()
        {
            str1 = $"Regain 25% of<style=Stamina> Max Stamina </style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"-2<style=Attributes> Haste </style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
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
            CellPosData cell = skillModel.targetPos[0];
            DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);
            if (dyna != null)
                SkillService.Instance.currentTargetDyna = dyna;
            else
                Debug.Log("target DYNA found null ");

        }
    }
}
