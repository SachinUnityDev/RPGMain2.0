using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

namespace Combat
{


    public class GuardLurcher : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.GuardLurcher;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override string desc => "guard Lurcher";

        public override void PopulateTargetPos()
        {
            skillModel.targetPos.Clear();
            for (int i = 1; i < 8; i++)
            {
                CellPosData cellPosData = new CellPosData(charController.charModel.charMode, i); // Allies
                DynamicPosData dyna = GridService.Instance.gridView
                    .GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                if (dyna != null)
                {
                    if (dyna.charGO.GetComponent<CharController>().charModel.charName
                                                            == CharNames.Lurcher_pet)
                        skillModel.targetPos.Add(cellPosData);
                }
            }
        }

        public override void ApplyFX1()
        {
            if (IsTargetMyAlly())
            {
                AttribData attribDataMin = charController.GetAttrib(AttribName.armorMin);
                AttribData attribDataMax = charController.GetAttrib(AttribName.armorMax);

                float minArmorChg = attribDataMin.currValue * 0.6f;
                float maxArmorChg = attribDataMax.currValue * 0.6f;

                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                  , AttribName.armorMin, minArmorChg, skillModel.timeFrame, skillModel.castTime, true);

                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                 , AttribName.armorMax, maxArmorChg, skillModel.timeFrame, skillModel.castTime, true);

                targetController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName, charID,
                        CharStateName.Guarded, skillModel.timeFrame, skillModel.castTime);

            }
        }

        public override void ApplyFX2()
        {
            if (IsTargetMyAlly())
            {
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.morale, 2, skillModel.timeFrame, skillModel.castTime, true);
            }
        }

        public override void ApplyFX3()
        {
        }


        public override void DisplayFX1()
        {
            str0 = $"Guard Lurcher";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
            str1 = $"+60% Armor on ally";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);

        }

        public override void DisplayFX2()
        {
            str2 = $"Gain +2 Morale";
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
            SkillService.Instance.skillFXMoveController.RangedStrike(PerkType.None, skillModel);
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
                    CharController targetCharCtrl = dyna.charGO.GetComponent<CharController>();

                    StatData hp = targetCharCtrl.GetStat(StatName.health);
                    if (!targetCharCtrl.charStateController.HasCharState(CharStateName.Guarded))
                    {
                        SkillService.Instance.currentTargetDyna = dyna; break;
                    }
                    else if (hp.currValue / hp.maxLimit < 0.4f)
                    {
                        SkillService.Instance.currentTargetDyna = dyna; break;
                    }
                    else
                    {
                        randomDyna = dyna;
                    }
                }

            }
            if (SkillService.Instance.currentTargetDyna == null)
            {
                SkillService.Instance.currentTargetDyna = randomDyna;
            }
        }

        public override void ApplyMoveFx()
        {
        }
    }
}

