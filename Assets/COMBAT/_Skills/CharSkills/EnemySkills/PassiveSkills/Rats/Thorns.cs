using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class Thorns : PassiveSkillBase
    {
        public override PassiveSkillNames passiveSkillName => PassiveSkillNames.Thorns;

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

        public override void ApplyFX()
        {   
            charController.strikeController.AddThornsBuff(DamageType.Physical, 3f, 6f
                                                                , TimeFrame.Infinity, 1);
        
            CombatEventService.Instance.OnDamageApplied -= RetaliateHPLessThan30f;
            CombatEventService.Instance.OnDamageApplied += RetaliateHPLessThan30f;
            CombatEventService.Instance.OnEOC += OnEOC; 
        }

        void RetaliateHPLessThan30f(DmgAppliedData dmgAppliedData)
        {
            if (dmgAppliedData.targetController.charModel.charID != charController.charModel.charID) return; 
            if (dmgAppliedData.attackType != AttackType.Melee) return;

            StatData hpStat = charController.GetStat(StatName.health); 
            if(hpStat.currValue < hpStat.maxLimit * 0.3)
            {
                charController.strikeController.ApplyRetaliate(dmgAppliedData.striker); 
            }
        }

        void OnEOC()
        {
            CombatEventService.Instance.OnDamageApplied -= RetaliateHPLessThan30f;
            CombatEventService.Instance.OnEOC -= OnEOC;
        }
    }
}