using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{

    public class CamelLeatherBeltOfEvasion : GenGewgawBase ,Iitems, IEquipAble
    {
        public override GenGewgawNames genGewgawNames => GenGewgawNames.CamelLeatherBeltOfEvasion;
        public ItemType itemType => ItemType.GenGewgaws;
        public int itemName => (int)GenGewgawNames.CamelLeatherBeltOfEvasion;

        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }

        public int itemId { get; set; }
        public List<int> allBuffs { get; set; }
        public Currency currency { get; set; }

        public void OnHoverItem()
        {

        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
        }

        public override void EquipGenGewgawFX()
        {
            charController = CharService.Instance.GetCharCtrlWithName(InvService.Instance.charSelect);
            base.EquipGenGewgawFX();

        }
        public void ApplyEquipableFX()
        {
            EquipGenGewgawFX();
        }

        public void RemoveEquipableFX()
        {
            UnEquipGenGewgawFX();
        }
    }
}