using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{

    public class Skull : IngredBase, IIngredient, Iitems
    {
        public override IngredNames ingredName => IngredNames.Skull;
        public ItemType itemType => ItemType.Ingredients;
        public int itemName => (int)IngredNames.Skull;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; }

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