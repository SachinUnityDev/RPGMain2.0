using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Interactables
{
    public class FallenShoulder : SagaicGewgawBase, Iitems
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.FallenShoulder;

        //+2 Stamina Regen when solo(solo: other heroes dead or fled)
        //+3-3 Armor first 3 rds of combat
        //Intimidating Shout is now 3 rds cd and cost 6 stamina

        public int itemId { get; set; }

        public ItemType itemType => ItemType.SagaicGewgaws;

        public int itemName => (int)SagaicGewgawNames.FallenShoulder;

        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();


        public override void GewGawSagaicInit()
        {
            CombatEventService.Instance.OnFleeInCombat += OnCombatFleeNDead;
            CombatEventService.Instance.OnCharDeath += OnCombatFleeNDead;

            string str = "When solo: +2 Stm regen";
            displayStrs.Add(str);
            str = "First 3 rds of Combat: +3-3 Armor";
            displayStrs.Add(str);
            str = "Intimidating Shout cd: 3 rds, Stm cost: 6";
            displayStrs.Add(str);
        }

        void OnCombatFleeNDead(CharController charController)
        {
            if(CombatService.Instance.allAlliesInCombat.Count == 1)
            {
                if(CombatService.Instance.allAlliesInCombat[0].charModel.charName 
                                    == charController.charModel.charName)
                {
                    int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw,
                       (int)sagaicGewgawName, charController.charModel.charID, AttribName.staminaRegen,
                       +2, TimeFrame.Infinity, -1, true);
                    buffIndex.Add(buffID);

                }
            }
        }

        public override void EquipGewgawSagaic()
        {
            charController = InvService.Instance.charSelectController;
            int buffID = charController.buffController.ApplyBuffOnRange(CauseType.SagaicGewgaw, charController.charModel.charID,
                                 (int)sagaicGewgawName, AttribName.armor, 3, 3, TimeFrame.Infinity, -1, true);
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

