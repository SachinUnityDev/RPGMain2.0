using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Cinnamon : IngredBase, IIngredient, Iitems
    {
        public override IngredNames ingredName => IngredNames.Cinnamon;
        public ItemType itemType => ItemType.Ingredients;
        public int itemName => (int)IngredNames.Cinnamon;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public ItemController itemController { get; set; }
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
    }
}
