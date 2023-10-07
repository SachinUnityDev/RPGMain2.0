using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;
namespace Combat
{

    public class HatchetSwing : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }

        public override SkillNames skillName => SkillNames.HatchetSwing;

        public override SkillLvl skillLvl => SkillLvl.Level0;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override string desc => "Hatchet Swing";

        private float _chance = 40f;
        public override float chance { get => _chance; set => _chance = value; }

     

        public override StrikeTargetNos strikeNos { get; }
        public override void PopulateTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear();
            for (int i = 1; i <= 4; i++) //1/2/3/4/
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, i);
                skillModel.targetPos.Add(cellPosData);
            }

        }

        public override void ApplyFX1()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyEnemy()) return;
            
            if (targetController)
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                                    , DamageType.Physical, skillModel.damageMod, false);
        }

        public override void ApplyFX2()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyEnemy()) return;

            //if (targetController)
            //{
            //    if (chance.GetChance())
            //        CharStatesService.Instance.SetCharState(targetGO, CharStateName.BleedLowDOT);
            //}
        }

        public override void ApplyFX3()
        {
        }

        public override void DisplayFX1()
        {
            str0 = "<margin=1.2em>Ranged";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);

            str1 = $"{skillModel.damageMod}%<style=Physical> Physical,</style> dmg";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"{chance}%<style=Bleed> Low Bleed</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {

        }

        public override void DisplayFX4()
        {
        }

    
    
        public override void SkillEnd()
        {
        }



        public override void Tick(int roundNo)
        {
        }

    

        public override void ApplyVFx()
        {
          
        }

        public override void ApplyMoveFx()
        {
          
        }

        public override void PopulateAITarget()
        {
           
        }
    }



}
