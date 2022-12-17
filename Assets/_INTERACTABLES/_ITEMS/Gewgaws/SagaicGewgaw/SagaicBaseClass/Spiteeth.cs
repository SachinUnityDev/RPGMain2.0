using Combat;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using UnityEngine;


namespace Interactables
{
    public class Spiteeth : SagaicGewgawBase
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.Spiteeth;


        // First 3 rds of combat: -2 Haste to enemy party	
        // +6-10 Dark Res and +6-10 Earth Res
        // If Ally: Spider: +4 Vigor and WP
        // If Ally: Spider: +1 Hp and Stamina Regen
        int valDark, valEarth; 
        public override void GewGawSagaicInit()  // to be changed Item Init
        {
            valDark = UnityEngine.Random.Range(6, 10);
            valEarth = UnityEngine.Random.Range(6, 10);
            // 


        }
        void OnSpiderAddedToParty(CharNames charNames)
        {
            if (charNames != CharNames.Spider_pet) return;

            CharController spiderController = 
            CharService.Instance.allCharsInParty.Find(t => t.charModel.charName == charNames);
            if(spiderController != null)
                ApplyBuffOnSpider(spiderController); 
        }

        void ApplyBuffOnSpider(CharController spiderController)
        {
            // If Ally: Spider: +4 Vigor and WP
            // If Ally: Spider: +1 Hp and Stamina Regen
           int buffID = spiderController.buffController.ApplyBuff(CauseType.SagaicGewgaw, spiderController.charModel.charID,
                      (int)sagaicGewgawName, StatsName.vigor, 4, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
            buffID = spiderController.buffController.ApplyBuff(CauseType.SagaicGewgaw, spiderController.charModel.charID,
                      (int)sagaicGewgawName, StatsName.willpower, 4, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            buffID = spiderController.buffController.ApplyBuff(CauseType.SagaicGewgaw, spiderController.charModel.charID,
                      (int)sagaicGewgawName, StatsName.staminaRegen, 1, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
            buffID = spiderController.buffController.ApplyBuff(CauseType.SagaicGewgaw, spiderController.charModel.charID,
                      (int)sagaicGewgawName, StatsName.hpRegen, 1, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
        }
        void OnStartOfCombat()
        {
            foreach (CharController c in CombatService.Instance.allEnemiesInCombat)
            {
                int buffID=   c.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                        (int)sagaicGewgawName, StatsName.haste, -2, TimeFrame.EndOfRound, 3, false);
                        buffIndex.Add(buffID);
            }
        }





        public override void EquipGewgawSagaic()
        {
            charController = InvService.Instance.charSelectController;
            CombatEventService.Instance.OnSOC += OnStartOfCombat;

            // Dark and Earth 
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                        (int)sagaicGewgawName, StatsName.darkRes, valDark, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                        (int)sagaicGewgawName, StatsName.earthRes, valEarth, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            // if spider already there in party when gewgaw put in slot 
            if (CharService.Instance.allCharsInParty.Any(t => t.charModel.charName == CharNames.Spider_pet))
            {
                CharController spiderController = CharService.Instance.allCharsInParty
                                                    .Find(t => t.charModel.charName == CharNames.Spider_pet);

                ApplyBuffOnSpider(spiderController);
            }
            else
            {
                CharService.Instance.OnCharAddedToParty += OnSpiderAddedToParty;
            }
        }

        public override void UnEquipSagaic()
        {
            CombatEventService.Instance.OnSOC -= OnStartOfCombat;
            foreach (int i in buffIndex)
            {
                charController.buffController.RemoveBuff(i);
            }
        }
    }
}
