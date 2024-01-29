using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class ReturnRegards : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.HoneBlades;
        public override SkillLvl skillLvl => SkillLvl.Level2;        
        public override PerkSelectState state { get; set; }
        public override PerkNames perkName => PerkNames.ReturnedRegards;
        public override PerkType perkType => PerkType.A2;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        public override string desc => "Return regards";        
        public override float chance { get; set; }
        DynamicPosData charDyna;

        public override void BaseApply()
        {
            base.BaseApply();
            CombatEventService.Instance.OnDamageApplied -= RetaliateAdjHitWithMelee;
            CombatEventService.Instance.OnDamageApplied += RetaliateAdjHitWithMelee;

        }
        void RetaliateAdjHitWithMelee(DmgAppliedData dmgAppliedData)
        {
           if (dmgAppliedData.attackType != AttackType.Melee) return;

            List<DynamicPosData> adjDynas = GridService.Instance.gridController.GetAllAdjDynaOccupied(charDyna);

            if (adjDynas.Count == 0) return;
            foreach (DynamicPosData adjDyna in adjDynas)
            {
                if (adjDyna.charGO.GetComponent<CharController>() == dmgAppliedData.targetController)
                {
                    charController.strikeController.ApplyRetaliate(dmgAppliedData.striker);
                    break; 
                }
            }
        }
        public override void DisplayFX1()
        {
            str1 = "Retaliates if any adj allies are hit";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void SkillEnd()
        {
            CombatEventService.Instance.OnDamageApplied -= RetaliateAdjHitWithMelee;            
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
        public override void DisplayFX2()
        {
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
            perkDesc = "Retaliates if any adj allies are hit";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);        
        }
    }



}
