using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
namespace Interactables
{
    public class Ametyst : GemBase, ICraftable, IPreciousGem, Iitems
    {
        public override GemName gemName => GemName.Ametyst;
        public override GemType gemType => GemType.Precious;
        public override CharNames charName { get; set; }
        public override int charID { get; set; }
        public override GemModel gemModel { get; set; }
        public override float singleBoost { get; set; }   // SUPPORT JOB SPEC BOOST
        public override float doubleBoost { get; set; }
        public ArmorType armorType { get; }
        public WeaponType weaponType { get; }
        public int currCharge { get; set; }
        public List<TgNames> supportedTradeGoods { get; set; }

        public ItemType itemType => ItemType.Gems;

        public int itemName => (int)GemName.Ametyst; 

        public int maxInvStackSize { get; set; }
        public SlotType invType { get; set; }

        public override GemModel GemInit(GemSO gemSO, CharController charController)
        {
            supportedTradeGoods = new List<TgNames>();
            supportedTradeGoods.Add(TgNames.SimpleRing);

            Iitems item = this as Iitems;
            item.maxInvStackSize = gemSO.inventoryStack;
            // CONTROLLERS AND MODELS
            this.charController = charController;
            charName = charController.charModel.charName;
            charID = charController.charModel.charID;

            gemModel = new GemModel(gemSO, charController);
            return gemModel;
        }

        public void OnHoverItem()
        {
           
        }
    }
}