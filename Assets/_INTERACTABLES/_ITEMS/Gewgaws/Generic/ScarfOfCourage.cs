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
        public override GenGewgawNames genGewgawNames => GenGewgawNames.ScarfOfCourage;

        public override SuffixBase suffixBase { get; set; }
        public override PrefixBase prefixBase { get; set; }

        public override GenGewgawModel GenGewgawInit(GenGewgawSO genericGewgawSO
                                                         , GenGewgawQ genGewgawQ)
        {

            GenGewgawModel genGewgawModel = new GenGewgawModel(genericGewgawSO, genGewgawQ);
            genGewgawModel.invType = SlotType.CommonInv;
            invSlotType = genGewgawModel.invType;

            return genGewgawModel;
        }

        public override void EquipGenGewgawFX(CharController charController)
        {
            genGewgawModel.charName = charController.charModel.charName;
            suffixBase?.ApplySuffixFX(charController); 
        }

     

        public override void DisposeGenGewgawFX()
        {
            //GewgawService.Instance.removeGenGewgaw(this); 
            // remove charModel too from the service 
            //Destroy(this);    
        }

        public override void UnEquipGenGewgawFX()
        {
            suffixBase?.RemoveFX();
        }

        
    }


}


