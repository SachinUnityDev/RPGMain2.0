using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{


    public class Intimidate : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }

        public override SkillNames skillName => SkillNames.Intimidate;

        public override SkillLvl skillLvl => SkillLvl.Level0;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override string desc => "Initimate";

        private float _chance = 40f;
        public override float chance { get => _chance; set => _chance = value; }

        public override StrikeNos strikeNos { get; }
        List<DynamicPosData> firstRowChar = new List<DynamicPosData>();

        public override void PopulateTargetPos()
        {
            AnyWithCharMode(CharMode.Ally); 
        }

        public override void ApplyFX1()
        {
            if (chance.GetChance())
                targetController.ChangeStat(CauseType.CharSkill, (int)skillName, charController.charModel.charID
                               , StatName.fortitude, -UnityEngine.Random.Range(10, 19));

        }

        public override void ApplyFX2()
        {
            if (10f.GetChance())
                targetController.tempTraitController.ApplyTempTrait(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID, TempTraitName.RatBiteFever);
        }

        public override void ApplyFX3()
        {
            if (50f.GetChance())
                targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID, CharStateName.Confused);
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
            base.PopulateAITarget();
            if (SkillService.Instance.currentTargetDyna != null) return;

            foreach (CellPosData cell in skillModel.targetPos)
            {
                DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);

                if (dyna != null)
                {
                    CharController targetCtrl = dyna.charGO.GetComponent<CharController>();

                    AttribData focusData = targetCtrl.GetAttrib(AttribName.focus);

                    if (targetCtrl.charStateController.HasCharState(CharStateName.Fearful))
                    {
                        tempDyna = dyna;
                    }
                    else if (targetCtrl.GetAttrib(AttribName.focus).currValue > 
                        tempDyna.charGO.GetComponent<CharController>().GetAttrib(AttribName.focus).currValue)
                    {
                        tempDyna = dyna;
                    }                   
                    else
                    {
                        randomDyna = dyna;
                    }
                    if (tempDyna != null)
                    {
                        SkillService.Instance.currentTargetDyna = tempDyna; break;
                    }
                }
            }
            if (SkillService.Instance.currentTargetDyna == null)
            {
                SkillService.Instance.currentTargetDyna = randomDyna;
            }
        }
    }
}