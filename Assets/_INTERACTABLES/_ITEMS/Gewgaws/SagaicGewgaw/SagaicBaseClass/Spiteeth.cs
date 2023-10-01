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
    public class Spiteeth : SagaicGewgawBase, Iitems
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.Spiteeth;

        public ItemType itemType => ItemType.SagaicGewgaws;
        public int itemName => (int)SagaicGewgawNames.Spiteeth;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public Currency currency { get; set; }

        // First 3 rds of combat: -2 Haste to enemy party	
        // +6-10 Dark Res and +6-10 Earth Res
        // If Ally: Spider: +4 Vigor and WP
        // If Ally: Spider: +1 Hp and Stamina Regen
        int valDark, valEarth; 
        public override void GewGawSagaicInit()  // to be changed Item Init
        {
            valDark = UnityEngine.Random.Range(6, 10);
            valEarth = UnityEngine.Random.Range(6, 10);
            string str = "First 3 rds of combat: -2 Haste to enemy party";
            displayStrs.Add(str);
            str = $"+{valDark} Dark Res, +{valEarth} Earth Res";
            displayStrs.Add(str);
            str = "If Ally: Spider: +4 Vigor and Wp";
            displayStrs.Add(str);
            str = "If Ally: Spider: +1 Hp and Stm Regen";
            displayStrs.Add(str);
        }
        void OnSpiderAddedToParty(CharController charController)
        {
            CharNames charName = charController.charModel.charName; 
            if (charName != CharNames.Spider_pet) return;

            CharController spiderController = 
            CharService.Instance.allCharsInPartyLocked.Find(t => t.charModel.charName == charName);
            if(spiderController != null)
                ApplyBuffOnSpider(spiderController); 
        }

        void ApplyBuffOnSpider(CharController spiderController)
        {
            // If Ally: Spider: +4 Vigor and WP
            // If Ally: Spider: +1 Hp and Stamina Regen
           int buffID = spiderController.buffController.ApplyBuff(CauseType.SagaicGewgaw, spiderController.charModel.charID,
                      (int)sagaicGewgawName, AttribName.vigor, 4, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
            buffID = spiderController.buffController.ApplyBuff(CauseType.SagaicGewgaw, spiderController.charModel.charID,
                      (int)sagaicGewgawName, AttribName.willpower, 4, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            buffID = spiderController.buffController.ApplyBuff(CauseType.SagaicGewgaw, spiderController.charModel.charID,
                      (int)sagaicGewgawName, AttribName.staminaRegen, 1, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
            buffID = spiderController.buffController.ApplyBuff(CauseType.SagaicGewgaw, spiderController.charModel.charID,
                      (int)sagaicGewgawName, AttribName.hpRegen, 1, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
        }
        void OnStartOfCombat()
        {
            foreach (CharController c in CombatService.Instance.allEnemiesInCombat)
            {
                int buffID=   c.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                        (int)sagaicGewgawName, AttribName.haste, -2, TimeFrame.EndOfRound, 3, false);
                        buffIndex.Add(buffID);
            }
        }





        public override void EquipGewgawSagaic()
        {
            charController = InvService.Instance.charSelectController;
            CombatEventService.Instance.OnSOC += OnStartOfCombat;

            // Dark and Earth 
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                        (int)sagaicGewgawName, AttribName.darkRes, valDark, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                        (int)sagaicGewgawName, AttribName.earthRes, valEarth, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            // if spider already there in party when gewgaw put in slot 
            if (CharService.Instance.allCharsInPartyLocked.Any(t => t.charModel.charName == CharNames.Spider_pet))
            {
                CharController spiderController = CharService.Instance.allCharsInPartyLocked
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
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
            GewGawSagaicInit();
        }

        public void OnHoverItem()
        {

        }

    }
}
