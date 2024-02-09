using Common;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;


namespace Interactables
{
    public class Amber : GemBase , Iitems, ISupportGem
    {
        public override GemNames gemName => GemNames.Amber;
        public ItemType itemType => ItemType.Gems;

        public int itemName => (int)GemNames.Amber;
        public int itemId { get; set; }
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }

        public List<GemNames> divineGemsSupported =>
                                new List<GemNames> { GemNames.Carnelian, GemNames.Meranite, GemNames.Rutele };

        public List<int> allBuffs { get; set; } = new List<int>();
        public Currency currency { get; set; }
        

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
            charController = InvService.Instance.charSelectController;
            itemController = charController.itemController;
            itemController.OnSocketSupportGem(this);
           // SocketedFX();
        }
        public void SocketedFX()
        {
            int buffID =
              charController.buffController.
                     ApplyNInitBuffOnDayNNight(CauseType.Gems, (int)itemName, charController.charModel.charID,
                                 AttribName.hpRegen, 1, TimeFrame.Infinity, -1, true, TimeState.Day);
            allBuffs.Add(buffID);
            string str = $"+1 Hp Regen at day";
            allDisplayStr.Add(str);
        }


    }



}

