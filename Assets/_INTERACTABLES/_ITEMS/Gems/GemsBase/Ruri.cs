using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Common;
using System.Linq;

namespace Interactables
{
    public class Ruri : GemBase, Iitems, IDivGem   
    {
        public override GemNames gemName => GemNames.Ruri;
        public ItemType itemType => ItemType.Gems;
        public int itemName => (int)GemNames.Ruri;
        public int itemId { get; set; }
        public int fxVal1 { get; set; }
        public int fxVal2 { get; set; }
        public float multFX { get; set; }
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();  

        public void ClearSocketBuffs()
        {
            foreach (int buffID in allBuffs.ToList())
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
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
        }
        public void OnSocketed()
        {
            charController = InvService.Instance.charSelectController;
            itemController = charController.itemController;
            itemController.OnSocketDivineGem(this);
            fxVal1 = (int)Random.Range(6f, 10f); 
        }

        public void SocketedFX(float multFx)
        {
            fxVal1 = (int)(fxVal1* multFX);
            //int buffID = charController.buffController.ApplyBuff(CauseType.Gems, (int)gemName, charID
            //        , StatsName.waterRes, fxVal1, TimeFrame.Infinity, 1, true);
            //allBuffs.Add(buffID);
            string str = $"+{fxVal1} Water Res";
            allDisplayStr.Add(str);
        }
    }
}
