using Common;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;


namespace Interactables
{
    public class Amber : GemBase , Iitems, ISupportGem
    {
        public override GemName gemName => GemName.Amber;
        public ItemType itemType => ItemType.Gems;

        public int itemName => (int)GemName.Amber;
        public int itemId { get; set; }
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }  

        public List<GemName> divineGemsSupported =>
                                new List<GemName> { GemName.Carnelian, GemName.Meranite, GemName.Rutele };

        public List<int> allBuffIDs { get; set; }


        public void ClearSocketBuffs()
        {
            foreach (int buffID in allBuffIDs)
            {
                charController.buffController.RemoveBuff(buffID);
            }
        }

        public void OnHoverItem()
        {

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
                   ApplyNInitBuffOnDay(CauseType.Gems, (int)itemName, charController.charModel.charID,
                               StatsName.hpRegen, 1, TimeFrame.Infinity, -1, true);
            allBuffIDs.Add(buffID);
        }


    }



}

