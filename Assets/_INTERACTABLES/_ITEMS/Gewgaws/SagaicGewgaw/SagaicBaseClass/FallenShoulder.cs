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
        int cdInit;
        int stmReq;
        SkillBase skillbase;
        public Currency currency { get; set; }
        public override void GewGawSagaicInit()
        {
            CombatEventService.Instance.OnCombatFlee += OnCombatFleeNDead;
            CharService.Instance.OnCharDeath += OnCombatFleeNDead;

            string str = "When solo: +2 Stm regen";
            displayStrs.Add(str);
            str = "First 3 rds of Combat: +3-3 Armor";
            displayStrs.Add(str);
            str = "Intimidating Shout cd: 3 rds, Stm cost: 6";
            displayStrs.Add(str);
        }

        void OnCombatFleeNDead(CharController charController)
        {
            List<CharController> allAllies = new List<CharController>();
            allAllies = CharService.Instance.allCharInCombat.Where(t=>t.charModel.charMode == CharMode.Ally).ToList();
            if (allAllies.Count ==1)
            {
                if (allAllies[0].charModel.cultType == CultureType.Macalaki)
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
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                                 (int)sagaicGewgawName, AttribName.armorMin, 3,TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                           (int)sagaicGewgawName, AttribName.armorMax, 3, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            skillbase  = charController.skillController.GetSkillBase(SkillNames.IntimidatingShout);
            cdInit = skillbase.skillModel.cd;
            stmReq = skillbase.skillModel.staminaReq;

            skillbase.skillModel.cd = 3; 
            skillbase.skillModel.staminaReq= 6;
        }

        public override void UnEquipSagaic()
        {
            CombatEventService.Instance.OnCombatFlee -= OnCombatFleeNDead;
            CharService.Instance.OnCharDeath -= OnCombatFleeNDead;
            foreach (int i in buffIndex)
            {
                charController.buffController.RemoveBuff(i);
            }
            skillbase.skillModel.cd = cdInit;   
            skillbase.skillModel.staminaReq= stmReq;
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

