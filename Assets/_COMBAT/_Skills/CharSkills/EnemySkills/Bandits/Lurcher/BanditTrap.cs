using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

namespace Combat
{

    public class BanditTrap : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.BanditTrap;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        private float _chance = 30f;
        public override float chance { get => _chance; set => _chance = value; }

        public override string desc => "This is the Bandit trap";
     
        public override void PopulateTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear();
            CombatService.Instance.mainTargetDynas.Clear();

            for (int i = 1; i < 8; i++)
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, i);
                DynamicPosData dyna = GridService.Instance.gridView
                                        .GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                if (dyna == null)  // null pos targetted 
                {
                    skillModel.targetPos.Add(cellPosData);
                }
            }
        }

        public override void ApplyFX1()
        {
            if (targetController == null) return;            
                targetController.ChangeStat(CauseType.CharSkill, (int)skillName, charController.charModel.charID
                               , StatName.fortitude, -UnityEngine.Random.Range(6, 9));
        }

        public override void ApplyFX2()
        {
            if (targetController == null) return;
            targetController.ChangeStat(CauseType.CharSkill, (int)skillName, charController.charModel.charID
                           , StatName.stamina, -UnityEngine.Random.Range(4, 7));           
        }
        public override void ApplyFX3()
        {
            if (targetController == null) return;
            targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName,
                 charController.charModel.charID, AttribName.haste, -2, TimeFrame.EndOfCombat, 1, false);
        }

        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.RangedStrike(PerkType.None, skillModel);

        }

        public override void DisplayFX1()
        {
            str0 = "Drains <style=Fortitude>6-8 Fortitude</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);

        }

        public override void DisplayFX2()
        {
            str1 = "Drains <style=Stamina>4-6 Stm</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX3()
        {
            str2 = $"-2 Haste until eoc";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }

        public override void DisplayFX4()
        {
        }

        public override void PopulateAITarget()
        {
            // populate targets to skill Service 
            // main and collatral too... 
            base.PopulateAITarget();
            if (SkillService.Instance.currentTargetDyna != null) return;
            foreach (CellPosData cell in skillModel.targetPos)
            {
                DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);

                if (dyna != null)
                {
                    //if (targetController == null)
                    //    Debug.Log(" Traget controller is null"); 
                    //if(targetController.charStateController== null)
                    //    Debug.Log("CharState controller");

                    CharController targetCtrl = dyna.charGO.GetComponent<CharController>();
                    if (targetCtrl.charStateController.HasCharState(CharStateName.Bleeding))
                    {
                        tempDyna = dyna;
                    }
                    else if (targetCtrl.tempTraitController.HasTempTrait(TempTraitName.Nausea) ||
                            targetCtrl.tempTraitController.HasTempTrait(TempTraitName.RatBiteFever))
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

        public override void ApplyMoveFx()
        {

        }
    }
}
