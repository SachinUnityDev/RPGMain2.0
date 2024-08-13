using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class SplintersOfEarth : PerkBase
    {
        public override PerkNames perkName => PerkNames.SplintersOfEarth;
        public override PerkType perkType => PerkType.A1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "splinters of earth";

        public override CharNames charName => CharNames.Baran;
        public override SkillNames skillName => SkillNames.EarthCracker;
        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void SkillHovered()
        {
            base.SkillHovered();
             SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName
                         ).WipeFX2;
        }
        public override void PerkSelected()
        {
            base.PerkSelected();
            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName
                     ).RemoveFX2;

        }
        public override void BaseApply()
        {
            base.BaseApply();
            DynamicPosData targetDyna = GridService.Instance.GetDyna4GO(targetGO); 

            if(targetDyna != null )
            CombatService.Instance.colTargetDynas = 
                GridService.Instance.gridController.GetAllAdjDynaOccupied(targetDyna);

        }

        public override void ApplyFX1()
        {
            for (int i = 0; i < CombatService.Instance.colTargetDynas.Count; i++)
            {
                CharController target = CombatService.Instance.colTargetDynas[i].charGO.GetComponent<CharController>();
                target.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                        , DamageType.Earth, 100, skillModel.skillInclination);
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
            str0 = $"75% -> 100% <style=Earth>Earth</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = $"Targets behind -> Adj targets";
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

            perkDesc = "75% -> 100%<style=Bleed> Earth </style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "Gain 3 <style=Fortitude>Fortitude</style> per pushed enemy";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }
}
