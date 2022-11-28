using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class BronzePauldrons : GenGewgawBase, Iitems
    {
        public ItemType itemType => ItemType.GenGewgaws;

        public int itemName => (int)GenGewgawNames.BronzePauldrons;

        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public override GenGewgawNames genGewgawNames => GenGewgawNames.BronzePauldrons;

        public override SuffixBase suffixBase { get; set; }
        public override PrefixBase prefixBase { get; set; }
        public ItemController itemController { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {
            // get So from the item service using item name 
            // populate the card using just the SO 
            // as string s are not dynamic
        }    

        public override GenGewgawModel GenGewgawInit(GenGewgawSO genericGewgawSO, GenGewgawQ genGewgawQ)
        {
            GenGewgawModel genGewgawModel = new GenGewgawModel(genericGewgawSO, genGewgawQ);
            genGewgawModel.invType = SlotType.CommonInv;
            invSlotType = genGewgawModel.invType;
            return genGewgawModel;
        }
        public override void EquipGenGewgawFX(CharController charController)
        {
            genGewgawModel.charName = charController.charModel.charName;
            prefixBase?.ApplyPrefix(charController);
        }

        public override void DisposeGenGewgawFX()
        {

        }

        public override void UnEquipGenGewgawFX()
        {
            prefixBase?.RemoveFX(); 
           
        }
    }
}

