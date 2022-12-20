using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


namespace Interactables
{
    public class NyalaTrophy : TGBase, Iitems, ITrophyable
    {
        public override TGNames tgName => TGNames.NyalaTrophy;
        public TavernSlotType tgSlotType => TavernSlotType.Trophy;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.NyalaTrophy;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {

        }
        //+2 WP and -1 Luck on Field
        //-2 Fame Yield"
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
        }
  

        public void TrophyInit()
        {
        }

        public void OnTrophyWalled()
        {
            int index =
            charController.landscapeController.ApplyLandscapeBuff(CauseType.TradeGoods, (int)tgName, LandscapeNames.Field
                , StatsName.luck, -1);
            allLandscapeIndex.Add(index);

             index =
                charController.landscapeController.ApplyLandscapeBuff(CauseType.TradeGoods, (int)tgName, LandscapeNames.Field
                , StatsName.willpower, 2);
            allLandscapeIndex.Add(index);

            index = FameService.Instance.fameController.ApplyFameModBuff(CauseType.TradeGoods, (int)tgName
                , -2, TimeFrame.Infinity, 1);
            allFameIndex.Add(index);
        }

        public void OnTrophyRemoved()
        {
           
        }
    }
}