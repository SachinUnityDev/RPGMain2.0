using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Combat; 

namespace Interactables
{
    public class Malachite : GemBase, Iitems, IDivGem
    {
        public override GemName gemName => GemName.Malachite;
        public ItemType itemType => ItemType.Gems;
        public int itemName => (int)GemName.Malachite;
        public int itemId { get; set; }
        public int fxVal1 { get; set; }
        public int fxVal2 { get; set; }
        public float multFX { get; set; }
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffIDs { get; set; }

        public void ClearSocketBuffs()
        {
            foreach (int buffID in allBuffIDs)
            {
                charController.buffController.RemoveBuff(buffID);
            }
        }

        public void OnEnchantedFX()
        {
            
        }

        public void OnHoverItem()
        {
            
        }

        public void OnSocketed()
        {
            charController = ItemService.Instance.selectChar;
            itemController = charController.itemController;
            itemController.OnSocketDivineGem(this);
        }

        public void SocketedFX(float multFx)
        {
            fxVal1 = (int)(Random.Range(8f, 12f) * multFX);
            int buffID = charController.buffController.ApplyBuff(CauseType.Gems, (int)gemName, charID
                    , StatsName.earthRes, fxVal1, TimeFrame.Infinity, 1, true);
            allBuffIDs.Add(buffID);
        }
    }

}
