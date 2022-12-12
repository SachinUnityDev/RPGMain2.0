using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Interactables
{
    public class ScarfOfCourage : GenGewgawBase, Iitems 
    {
        public ItemType itemType => ItemType.GenGewgaws;
        public int itemName => (int)GenGewgawNames.ScarfOfCourage;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {
            // get item data here.. 
            // get hold of item cards here 

        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
        }
        public override GenGewgawNames genGewgawNames => GenGewgawNames.ScarfOfCourage;

        public override SuffixBase suffixBase { get; set; }
        public override PrefixBase prefixBase { get; set; }

        //public override GenGewgawModel GenGewgawInit(GenGewgawSO genericGewgawSO
        //                                                 , GenGewgawQ genGewgawQ)
        //{

        //    GenGewgawModel genGewgawModel = new GenGewgawModel(genericGewgawSO, genGewgawQ);
        //    genGewgawModel.invType = SlotType.CommonInv;
        //    invSlotType = genGewgawModel.invType;

        //    return genGewgawModel;
        //}

        public override void EquipGenGewgawFX()
        {
            charController = CharService.Instance.GetCharCtrlWithName(InvService.Instance.charSelect);
            suffixBase?.ApplySuffixFX(charController); 
        }

        public override void UnEquipGenGewgawFX()
        {
            suffixBase?.RemoveFX();
        }

        
    }


}


