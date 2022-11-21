using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Ruby :GemBase, Iitems
    {
        public override GemName gemName => GemName.Ruby;
        public override GemType gemType => GemType.Precious;
        public override CharNames charName { get; set; }
        public override int charID { get; set; }
        public override GemModel gemModel { get; set; }
        public override float singleBoost { get; set; }   // SUPPORT JOB SPEC BOOST
        public override float doubleBoost { get; set; }
        public ArmorType armorType { get; }
        public WeaponType weaponType { get; }
        public int currCharge { get; set; }

        public ItemType itemType => ItemType.Gems;
        public int itemName => (int)GemName.Ruby;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public void OnHoverItem()
        {

        }

    }
}

