using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class BlindingSparks : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.HoneBlades;
        public override SkillLvl skillLvl => SkillLvl.Level1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override PerkNames perkName => PerkNames.BlindingSparks;
        public override PerkType perkType => PerkType.B1;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        public override string desc => "Bleed for Sure";
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        PerkBase perkBase1, perkBase2; 
  
        public override void BaseApply()
        {
            base.BaseApply();
            perkBase1 = skillController.allPerkBases.Find(t => t.perkName == PerkNames.FindTheWeakSpot);
            perkBase1.chance = 100f;

            perkBase2 = skillController.allPerkBases.Find(t => t.perkName == PerkNames.FasterThanEyeCanSee);
            perkBase2.chance = 60f;
        }

        public override void ApplyFX1()
        {
            
        }
        public override void ApplyFX2()
        {

        }

        public override void ApplyFX3()
        {

        }
        public override void DisplayFX1()
        {
            str0 = "Faster Than Eye Can See:";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = "60% <style=States>Blinded</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX3()
        {
            str2 = "Find the Weak Spot:";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX4()
        {
            str3 = "100% Regain AP";
            SkillService.Instance.skillModelHovered.AddDescLines(str3);
        }
        public override void ApplyVFx()
        {

        }
        
        public override void ApplyMoveFX()
        {
          
        }
        public override void SkillEnd()
        {
            base.SkillEnd();
            perkBase1.chance = 50f;
            perkBase2.chance = 30f;
        }
        public override void InvPerkDesc()
        {
            perkDesc = "Faster Than Eye Can See:";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "60% <style=States>Blinded</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Find the Weak Spot:";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "100% Regain AP";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }    
}

