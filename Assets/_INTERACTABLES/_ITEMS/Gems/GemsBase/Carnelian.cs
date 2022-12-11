using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Combat;

namespace Interactables
{
    public class Carnelian : GemBase, Iitems, IDivGem
   
    {
        public override GemNames gemName => GemNames.Carnelian;
        public ItemType itemType => ItemType.Gems;
        public int itemName => (int)GemNames.Carnelian;
        public int itemId { get; set; }
        public int fxVal1 { get; set; }
        public int fxVal2 { get; set; }
        public float multFX { get; set; }
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
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
    
        public void OnEnchantedFX()
        {

        }

 

        public void OnSocketed()
        {
            itemController.OnSocketDivineGem(this);
        }

        public void SocketedFX(float multFx)
        {
            fxVal1 = (int)(Random.Range(8f, 12f) * multFX);
            int buffID = charController.buffController.ApplyBuff(CauseType.Gems, (int)gemName, charID
                    , StatsName.fireRes, fxVal1, TimeFrame.Infinity, 1, true);
            allBuffs.Add(buffID);
        }

    }
}

