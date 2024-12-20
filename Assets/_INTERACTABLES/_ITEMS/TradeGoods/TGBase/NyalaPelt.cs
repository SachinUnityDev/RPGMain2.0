using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


namespace Interactables
{
    public class NyalaPelt : TGBase, Iitems, ITrophyable
    {
        public override TGNames tgName => TGNames.NyalaPelt;
        public TavernSlotType tavernSlotType => TavernSlotType.Pelt;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int) TGNames.NyalaPelt;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public int fameYield => -1;
        public Currency currency { get; set; }
        public void OnHoverItem()
        {

        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
            allDisplayStr.Add("+1 Haste on Field");

        }
  
//        "Abbas only: +1 Haste on Field
//-1 Fame Yield"
        public void TrophyInit()
        {
            // define charController as abbas
        }

        public void OnTrophyWalled()
        {
            int index = 
            charController.landscapeController.ApplyLandscapeBuff(CauseType.TradeGoods, (int)tgName, LandscapeNames.Field
                , AttribName.haste, 1);
            allLandscapeIndex.Add(index);

            FameService.Instance.fameController.ApplyFameYieldChg(CauseType.TradeGoods, (int)tgName, fameYield);           
        }

        public void OnTrophyRemoved()
        {
            FameService.Instance.fameController.ApplyFameYieldChg(CauseType.TradeGoods, (int)tgName, -fameYield);
        }
    }
}

