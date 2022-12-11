using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Common; 

namespace Interactables
{
    public class Oltu : GemBase, Iitems, ISupportGem    
    {
        public override GemNames gemName => GemNames.Oltu;
        public ItemType itemType => ItemType.Gems;

        public int itemName => (int)GemNames.Oltu;
        public int itemId { get; set; }
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<GemNames> divineGemsSupported =>
                                new List<GemNames> { GemNames.Ruri, GemNames.Malachite, GemNames.BlueOnyx };

        public List<int> allBuffs { get; set; }

        public void OnHoverItem()
        {

        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
        }
        public void ClearSocketBuffs()
        {
            foreach (int buffID in allBuffs)
            {
                charController.buffController.RemoveBuff(buffID);
            }
        }
        public void OnSocketed()
        {
            charController = ItemService.Instance.selectChar;
            itemController = charController.itemController;
            itemController.OnSocketSupportGem(this);
            SocketedFX(); 
        }
        public void SocketedFX()
        {         

            int buffID =
            charController.buffController.
                   ApplyBuffOnNight(CauseType.Gems, (int)itemName, charController.charModel.charID,
                               StatsName.hpRegen, 1, TimeFrame.Infinity, -1, true);
            allBuffs.Add(buffID);
        }


    }
}

