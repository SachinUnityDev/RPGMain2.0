using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class Expectorate : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.Expectorate;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override string desc => "this is Expectorate ";

        private float _chance =0;
        public override float chance { get => _chance; set => _chance = value; }

    
        public override void PopulateTargetPos()
        {
            skillModel.targetPos.Clear();
            for (int i = 1; i < 8; i++)
            {
                CellPosData cellPosData = new CellPosData(CharMode.Ally, i); // other party
                DynamicPosData dyna = GridService.Instance.gridView
                                       .GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                if (dyna != null)
                {
                    skillModel.targetPos.Add(cellPosData);                    
                }
            }
        }
        public override void ApplyFX1()
        {
            if (IsTargetMyEnemy())
            {
                if (targetController.tempTraitController.HasTempTrait(TempTraitName.Nausea))
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill
                                                            , (int)skillName, DamageType.Earth, skillModel.damageMod + 20f);
                else
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill
                                                                , (int)skillName, DamageType.Earth, skillModel.damageMod);
            }
        }

        public override void ApplyFX2()
        {
            chance = 50f;
            if (chance.GetChance())
                charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                       , charController.charModel.charID, CharStateName.PoisonedLowDOT);

          
        }

        public override void ApplyFX3()
        {
            if (30f.GetChance())
                targetController.tempTraitController.ApplyTempTrait(CauseType.CharSkill, (int)skillName
                                        , charController.charModel.charID, TempTraitName.Nausea);
        }
        
        public override void ApplyVFx()
        {        
        
        }

        public override void PopulateAITarget()
        {
            PopulateTargetPos();
            SkillService.Instance.currentTargetDyna = null;
            DynamicPosData tempTarget = null;
            DynamicPosData randomTarget = null; 
            foreach (CellPosData cell in skillModel.targetPos)
            {
                DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);
                CharController targetCtrl = dyna.charGO.GetComponent<CharController>(); 

                if (dyna != null)
                {
                    if (targetCtrl.charStateController.HasCharDOTState(CharStateName.BleedLowDOT))
                    {
                        tempTarget= dyna;
                      
                    }
                    else if (targetCtrl.GetAttrib(AttribName.earthRes).currValue < 25f)
                    {
                        tempTarget = dyna;
                    }
                    else
                    {
                        randomTarget= dyna;
                    }
                    if (tempTarget != null)
                    {
                        SkillService.Instance.currentTargetDyna = tempTarget; break;
                    }
                }
            }
            if (SkillService.Instance.currentTargetDyna == null)
            {
                SkillService.Instance.currentTargetDyna = randomTarget; 
            }
        }

        public override void DisplayFX1()
        {
            str1 = $"110%<style=Earth> Earth</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"50%<style=Poison> Low Poison</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }

        public override void ApplyMoveFx()
        {
           

        }
    }


 


}
