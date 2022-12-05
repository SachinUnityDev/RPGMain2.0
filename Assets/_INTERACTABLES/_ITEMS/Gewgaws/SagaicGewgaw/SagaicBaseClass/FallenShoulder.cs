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
        public override SagaicGewgawNames sagaicgewgawName => SagaicGewgawNames.FallenShoulder;
        public override CharController charController { get; set; } 
        public override List<int> buffIndex  { get; set; }
        public override List<string> displayStrs { get; set; }
        //+2 Stamina Regen when solo(solo: other heroes dead or fled)
        //+3-3 Armor first 3 rds of combat
        //Intimidating Shout is now 3 rds cd and cost 6 stamina
        public override void GewGawInit()
        {
            CombatEventService.Instance.OnFleeInCombat += OnCombatFleeNDead;
            CombatEventService.Instance.OnCharDeath += OnCombatFleeNDead;
        }
        public override void ApplyGewGawFX(CharController charController)
        {
            int buffID = charController.buffController.ApplyBuffOnRange(CauseType.SagaicGewgaw, charController.charModel.charID,
                                    (int)sagaicgewgawName, StatsName.armor, 3, 3, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);



        }
        void OnCombatFleeNDead(CharController charController)
        {
            if(CombatService.Instance.allAlliesInCombat.Count == 1)
            {
                if(CombatService.Instance.allAlliesInCombat[0].charModel.charName 
                                    == charController.charModel.charName)
                {
                    int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw,
                       (int)sagaicgewgawName, charController.charModel.charID, StatsName.staminaRegen,
                       +2, TimeFrame.Infinity, -1, true);
                    buffIndex.Add(buffID);

                }
            }
        }
        public override List<string> DisplayStrings()
        {
            return null;
        }

    

        public override void RemoveFX()
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

