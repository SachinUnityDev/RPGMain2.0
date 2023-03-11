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
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }

        public int fameYield => -1;

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
                , StatsName.haste, 1);
            allLandscapeIndex.Add(index);

            index = FameService.Instance.fameController.ApplyFameModBuff(CauseType.TradeGoods, (int)tgName
                , fameYield, TimeFrame.Infinity, 1);
            allFameIndex.Add(index); 
        }

        public void OnTrophyRemoved()
        {
          
        }
    }
}

