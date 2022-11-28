using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class HumanEar : IngredBase, IIngredient, Iitems
    {
        public override IngredNames ingredName => IngredNames.HumanEar; 

        public ItemType itemType => ItemType.Ingredients;

        public int itemName => (int)IngredNames.HumanEar;

        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public void InitItem() { }

        public void OnHoverItem()
        {

        }

    }
}

