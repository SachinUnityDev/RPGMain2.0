using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class BreathTaker : PerkBase
    {
        public override PerkNames perkName => PerkNames.Breathtaker; 

        public override PerkType perkType => PerkType.B2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.EdgyAxe };

        public override string desc => "(PR: B1) /n Hits first row regardless of cast pos /n 3-5 Stamina dmg"; 

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.Cleave;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override void AddTargetPos()
        {

            if (skillModel == null) return; 
            skillModel.targetPos.Clear();
           // CombatService.Instance.mainTargetDynas.Clear();

            List<DynamicPosData> firstRowChar = GridService.Instance.GetFirstRowChar(CharMode.Enemy);
            Debug.Log("SkillModel is FINE " + skillModel.skillName);
            if (firstRowChar != null)
            {
                foreach (var dynaChar in firstRowChar)
                {
                    skillModel.targetPos.Add(new CellPosData(dynaChar.charMode, dynaChar.currentPos));
                    CombatService.Instance.mainTargetDynas.Add(dynaChar);
                }
            }
        }
        public override void SkillHovered()
        {
            base.SkillHovered();
            SkillService.Instance.SkillWipe += skillController.allSkillBases
                                                .Find(t => t.skillName == skillName).WipeFX3;

        }
     
        public override void ApplyFX1()
        {
            foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
            {
                dyna.charGO.GetComponent<CharController>().damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                    , DamageType.StaminaDmg, UnityEngine.Random.Range(3, 6)); 
            }
        }

        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
        }

        public override void ApplyMoveFX()
        {
        }

        public override void ApplyVFx()
        {
        }
        public override void DisplayFX1()
        {
            str0 = "Target First Row";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        //wipe str 2 of cleave
        public override void DisplayFX2()
        {
            str1 = "Drain <style=Stamina>Stamina</style> 3-5";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
 
        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "Target First Row regardless of cast pos";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Drain 3-5 Stamina";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }

}
