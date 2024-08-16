using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class CleverCleave : PerkBase
    {
        public override PerkNames perkName => PerkNames.CleverCleave;

        public override PerkType perkType => PerkType.A3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "If Edgy Axe is picked; 30% High Bleed --> 60% High Bleed /n If Pusher is picked; +1 Fortitude Org.until EOQ, per pushed enemy (Stacks up to 8)/n 6 --> 8 Stamina cost";

        public override CharNames charName => CharNames.Baran; 

        public override SkillNames skillName => SkillNames.Cleave;

        public override SkillLvl skillLvl => SkillLvl.Level3;


        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        int stack = 0;

        bool isEdgyAxeSelect = false;
        bool isPusherSelect = false; 
        public override void SkillHovered()
        {
            foreach (PerkData perkData in skillController.charSkillModel.allSkillPerkData)
            {
                if (perkData.state == PerkSelectState.Clicked && perkData.perkName == PerkNames.EdgyAxe)
                {
                    isEdgyAxeSelect = true;
                }
                if (perkData.state == PerkSelectState.Clicked && perkData.perkName == PerkNames.Pusher)
                {
                    isPusherSelect = true;
                }
            }

                //base.SkillHovered();
                //if(isEdgyAxeSelect)
                //    SkillService.Instance.SkillWipe += skillController.allSkillBases
                //                                .Find(t => t.skillName == skillName).WipeFX2;
                //if(isPusherSelect)
                //    SkillService.Instance.SkillWipe += skillController.allSkillBases
                //                                .Find(t => t.skillName == skillName).WipeFX1;
        }
        //public override void SkillSelected()
        //{
        //    base.SkillSelected();
        //    if(isPusherSelect)
        //        SkillService.Instance.SkillWipe += skillController.allSkillBases
        //                                      .Find(t => t.skillName == skillName).RemoveFX1;


        //}

        public override void BaseApply()
        {
            base.BaseApply();

            foreach (PerkData skillModelData in skillController.charSkillModel.allSkillPerkData)
            {
                if (isEdgyAxeSelect)
                {
                    skillController.allPerkBases.Find(t => t.skillName == skillName
                                                           && t.skillLvl == SkillLvl.Level1
                                                           && t.state == PerkSelectState.Clicked).chance = 60f;
                }
            }

            skillModel.staminaReq = 8; 
        }

        public override void ApplyFX1()
        {
            if(isPusherSelect)
                if (CombatService.Instance.mainTargetDynas.Count > 0)
                    CombatService.Instance.mainTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>().damageController
                        .ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Physical
                        , skillModel.damageMod, skillModel.skillInclination,false, true));
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
            if(isEdgyAxeSelect)
                str0 = $"60% <style=Bleed>High Bleed</style>";
                SkillService.Instance.skillModelHovered.AddDescLines(str0);
            if(isPusherSelect)
                str0 = $"True Strike";
                SkillService.Instance.skillModelHovered.AddDescLines(str0);

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
        public override void InvPerkDesc()
        {
            perkDesc = "If Edgy Axe is picked: <style=Bleed>High Bleed</style> chance: 30% -> 60%";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "If Pusher is picked: True Strike";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            
            perkDesc = "Stm cost: 6-> 8";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }
}
