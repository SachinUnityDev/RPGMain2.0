using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class BraveMansRetortion : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.HoneBlades;

        public override SkillLvl skillLvl => SkillLvl.Level3;
       
        public override PerkSelectState state { get;set; }

        public override PerkNames perkName => PerkNames.BraveMansRetortion;

        public override PerkType perkType => PerkType.A3;

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.ReturnedRegards };

        public override string desc => "Brave Mans retoration";
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        DynamicPosData charDyna;

        public override void BaseApply()
        {
            base.BaseApply();
            CombatEventService.Instance.OnDamageApplied -= RetaliatedAdjHitWithMelee;
            CombatEventService.Instance.OnDamageApplied += RetaliatedAdjHitWithMelee;

            if (skillController.IsPerkClicked(skillName, PerkNames.QuickToReact))
            {
                skillController.allPerkBases.Find(t => t.skillName == skillName).chance = 60f;
            }
        }
        void RetaliatedAdjHitWithMelee(DmgAppliedData dmgAppliedData)
        {
            if (dmgAppliedData.attackType != AttackType.Melee) return;
            // get adjacent from gridService
            List<DynamicPosData> adjDynas = GridService.Instance.gridController.GetAllAdjDynaOccupied(charDyna);

            if (adjDynas.Count == 0) return;
            foreach (DynamicPosData adjDyna in adjDynas)
            {
                if (adjDyna.charGO.GetComponent<CharController>() == dmgAppliedData.targetController)
                {
                    charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID
                                                            , StatName.fortitude, +3, true);
                    break;
                }
            }
        }
        public override void SkillEnd()
        {
            CombatEventService.Instance.OnDamageApplied -= RetaliatedAdjHitWithMelee;           
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
            str1 = "Gain 3 Fortitude when Retaliate";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Regain AP 20% -> 60%";
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
        }
        public override void ApplyMoveFX()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "Gain 3 Fortitude when Retaliate";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = "Regain AP 20% -> 60%";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }
    }



}

