using Combat;
using Common;
using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Interactables
{
    public class FallenShoulder : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.FallenShoulder;

        //+2 Stamina Regen when solo(solo: other heroes dead or fled)
        //+3-3 Armor first 3 rds of combat
        //Intimidating Shout is now 3 rds cd and cost 6 stamina
        public override void GewGawSagaicInit()
        {
            CombatEventService.Instance.OnFleeInCombat += OnCombatFleeNDead;
            CombatEventService.Instance.OnCharDeath += OnCombatFleeNDead;
        }

        void OnCombatFleeNDead(CharController charController)
        {
            if(CombatService.Instance.allAlliesInCombat.Count == 1)
            {
                if(CombatService.Instance.allAlliesInCombat[0].charModel.charName 
                                    == charController.charModel.charName)
                {
                    int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw,
                       (int)sagaicGewgawName, charController.charModel.charID, StatsName.staminaRegen,
                       +2, TimeFrame.Infinity, -1, true);
                    buffIndex.Add(buffID);

                }
            }
        }

        public override void EquipGewgawSagaic()
        {
            charController = InvService.Instance.charSelectController;
            int buffID = charController.buffController.ApplyBuffOnRange(CauseType.SagaicGewgaw, charController.charModel.charID,
                                 (int)sagaicGewgawName, StatsName.armor, 3, 3, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
        }

        public override void UnEquipSagaic()
        {
            CombatEventService.Instance.OnFleeInCombat += OnCombatFleeNDead;
            CombatEventService.Instance.OnCharDeath += OnCombatFleeNDead;
            foreach (int i in buffIndex)
            {
                charController.buffController.RemoveBuff(i);
            }
        }
    }
}

