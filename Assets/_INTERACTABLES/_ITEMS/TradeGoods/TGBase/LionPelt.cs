using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{

    public class LionPelt : TGBase, Iitems, ITrophyable 
    {
        public override TGNames tgName => TGNames.LionPelt;
        public  TavernSlotType tavernSlotType => TavernSlotType.Pelt;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.LionPelt;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public int fameYield => 2;
        public Currency currency { get; set; }
        public void OnHoverItem()
        {

        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
            allDisplayStr.Add("+1 Vigor on Savannah");
        }
        public void TrophyInit()
        {

        }
            
            //+1 Vigor on Savannah
            
        public void OnTrophyWalled()
        {
            int index =
               charController.landscapeController.ApplyLandscapeBuff(CauseType.TradeGoods, (int)tgName, LandscapeNames.Savannah
               , AttribName.vigor, 1);
            allLandscapeIndex.Add(index);

            FameService.Instance.fameController.ApplyFameYieldChg(CauseType.TradeGoods, (int)tgName, fameYield);
            
        }

        public void OnTrophyRemoved()
        {
            FameService.Instance.fameController.ApplyFameYieldChg(CauseType.TradeGoods, (int)tgName, -fameYield);

        }
    }
}