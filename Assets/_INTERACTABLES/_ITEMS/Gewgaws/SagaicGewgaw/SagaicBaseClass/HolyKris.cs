using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class HolyKris : SagaicGewgawBase, Iitems
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.HolyKris;

        // "In the name of usmu" skill has no CD	
        // +2-4 willpower
        // when Stamina below 30%, gain +30% dmg
        public int itemId { get; set; }
        public ItemType itemType => ItemType.SagaicGewgaws;
        public int itemName => (int)SagaicGewgawNames.HolyKris;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();
        public Currency currency { get; set; }
        int valWP;
        public override void GewGawSagaicInit()
        {
            valWP = UnityEngine.Random.Range(2, 5);
            string str = $"+{valWP} Willpower";
            displayStrs.Add(str);
            str = "When Stamina < 30%: +30% Dmg";
            displayStrs.Add(str);
            str = "In the Name of Usmu cd: 1";
            displayStrs.Add(str);
        }
        
        public override void EquipGewgawSagaic()
        {
            charController = InvService.Instance.charSelectController;
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                          (int)sagaicGewgawName, AttribName.willpower, valWP, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
        }


        public override void UnEquipSagaic()
        {
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
