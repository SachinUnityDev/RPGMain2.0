using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class Expectorate : SkillBase
    {
        public override SkillModel skillModel { get ; set; }

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.Expectorate;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override string desc => "this is Expectorate ";

        private float _chance =0;
        public override float chance { get => _chance; set => _chance = value; }

        Rodent rodent;
        Vermin vermin; 
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
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                             , DamageType.Earth, skillModel.damageMod);
            }
        }

        public override void ApplyFX2()
        {
            chance = 50f;
            if (chance.GetChance())
                CharStatesService.Instance
                    .ApplyCharState(targetGO, CharStateName.PoisonedLowDOT
                                     , charController, CauseType.CharSkill, (int)skillName);

        }

        public override void ApplyFX3()
        {
            rodent = new Rodent();
            //rodent.ApplyPassiveFX(targetController);
            vermin = new Vermin();
           // vermin.ApplyPassiveFX(targetController);
        }
        public override void SkillEnd()
        {
            base.SkillEnd();
            //if (rodent != null)
            //   // rodent.RemovePassiveFX(targetController);
            //if (vermin != null)
            //    vermin.RemovePassiveFX(targetController);
        }
        public override void ApplyVFx()
        {        
            SkillService.Instance.skillFXMoveController.MultiTargetRangeFX(PerkType.None);            
        }

        public override void PopulateAITarget()
        {
            PopulateTargetPos();
            DynamicPosData tempTarget = null; 
            foreach (CellPosData cell in skillModel.targetPos)
            {
                DynamicPosData dyna = GridService.Instance.GetDynaAtCellPos(cell.charMode, cell.pos);
                CharController targetController = dyna.charGO.GetComponent<CharController>(); 

                if (dyna != null)
                {
                    if (CharStatesService.Instance.HasCharDOTState(charGO, CharStateName.BleedLowDOT))
                    {
                        SkillService.Instance.currentTargetDyna = dyna;
                        return; 
                    }
                    else if (targetController.GetStat(StatsName.earthRes).currValue < 25f)
                    {
                        SkillService.Instance.currentTargetDyna = dyna;
                        return; 
                    }
                    tempTarget = dyna;
                }
            }
            SkillService.Instance.currentTargetDyna = tempTarget;
        }

        public override void DisplayFX1()
        {
            str1 = $"110%<style=Earth> Earth</style>";
            SkillService.Instance.skillModel.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"50%<style=Poison> Low Poison</style>";
            SkillService.Instance.skillModel.descLines.Add(str2);
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
