using Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Interactables
{
    public class Rutele : GemBase, Iitems, IDivGem
    {
        public override GemNames gemName => GemNames.Rutele;
        public ItemType itemType => ItemType.Gems;
        public int itemName => (int)GemNames.Rutele;
        public int itemId { get; set; }
        public int fxVal1 { get; set; }
        public int fxVal2 { get; set; }
        public float multFX { get; set; }
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();
        public Currency currency { get; set; }
        public void ClearSocketBuffs()
        {
            foreach (int buffID in allBuffs)
            {
                charController.buffController.RemoveBuff(buffID);
            }
            allDisplayStr.Clear();
        }

        public void OnEnchantedFX()
        {

        }

        public void OnHoverItem()
        {

        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
            multFX = 1;
        }
        public void OnSocketed()
        {
            charController = InvService.Instance.charSelectController;
            itemController = charController.itemController;
            itemController.OnSocketDivineGem(this);
        }

        public void SocketedFX(float multFx)
        {
            fxVal1 = (int)(Random.Range(5f, 8f) * multFX);
            int buffID = charController.buffController.ApplyBuff(CauseType.Gems, (int)gemName, charID
                    , AttribName.lightRes, fxVal1, TimeFrame.Infinity, 1, true);
            allBuffs.Add(buffID);
            string str = $"+{fxVal1} Light Res";
            allDisplayStr.Add(str);
        }
    }
}

