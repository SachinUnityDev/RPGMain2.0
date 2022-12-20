using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


namespace Interactables
{

    public class LionPelt : TGBase, Iitems, ITrophyable 
    {
        public override TGNames tgName => TGNames.LionPelt;
        public  TavernSlotType tgSlotType => TavernSlotType.Pelt;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.LionPelt;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {

        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
        }
        public void TrophyInit()
        {

        }
            //+1 Morale and +1 Vigor on Savannah
            //+1 Fame Yield"
        public void OnTrophyWalled()
        {
            int index =
           charController.landscapeController.ApplyLandscapeBuff(CauseType.TradeGoods, (int)tgName, LandscapeNames.Savannah
               , StatsName.morale, 1);
            allLandscapeIndex.Add(index);

            index =
               charController.landscapeController.ApplyLandscapeBuff(CauseType.TradeGoods, (int)tgName, LandscapeNames.Savannah
               , StatsName.vigor, 1);
            allLandscapeIndex.Add(index);

            index = FameService.Instance.fameController.ApplyFameModBuff(CauseType.TradeGoods, (int)tgName
                , 1, TimeFrame.Infinity, 1);
            allFameIndex.Add(index);
        }

        public void OnTrophyRemoved()
        {
          
        }
    }
}