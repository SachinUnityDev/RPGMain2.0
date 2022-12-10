using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Combat;

namespace Interactables
{
    public class BlueOnyx : GemBase, Iitems, IDivGem   
    {
        public override GemName gemName => GemName.BlueOnyx;
        public ItemType itemType => ItemType.Gems;
        public int itemName => (int)GemName.BlueOnyx;
        public int itemId { get; set; }
        public int fxVal1 { get; set; }
        public int fxVal2 { get; set; }
        public float multFX { get; set; }
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }


        public void OnHoverItem()
        {
            // talk to itemService... itemviewService .... item cards populate
            // using the data from the SO
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
            charController = ItemService.Instance.selectChar;
            itemController = charController.itemController;
           
            itemController.EnchantTheWeaponThruScroll(this);
        }



        public void OnSocketed()
        {
            charController = ItemService.Instance.selectChar;
            itemController = charController.itemController;
            itemController.OnSocketDivineGem(this);
        }

        public void SocketedFX(float multFx)
        {
            fxVal1 = (int)(Random.Range(5f, 10f) * multFX);
            int buffID = charController.buffController.ApplyBuff(CauseType.Gems, (int)gemName, charID
                    , StatsName.darkRes, fxVal1, TimeFrame.Infinity, 1, true);
            allBuffs.Add(buffID);
        }


    }
}
