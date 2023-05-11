using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class NyalaTrophy : TGBase, Iitems, ITrophyable
    {
        public override TGNames tgName => TGNames.NyalaTrophy;
        public TavernSlotType tavernSlotType => TavernSlotType.Trophy;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.NyalaTrophy;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public int fameYield => -2; 
        public void OnHoverItem()
        {

        }
        //+2 WP and -1 Luck on Field
        //-2 Fame Yield"
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;

            allDisplayStr.Add("+2 Wp and -1 Luck on Field");
        }
  

        public void TrophyInit()
        {
        }

        public void OnTrophyWalled()
        {
            int index =
            charController.landscapeController.ApplyLandscapeBuff(CauseType.TradeGoods, (int)tgName, LandscapeNames.Field
                , AttribName.luck, -1);
            allLandscapeIndex.Add(index);

             index =
                charController.landscapeController.ApplyLandscapeBuff(CauseType.TradeGoods, (int)tgName, LandscapeNames.Field
                , AttribName.willpower, 2);
            allLandscapeIndex.Add(index);

            FameService.Instance.fameController
                .ApplyFameYieldChg(CauseType.TradeGoods, (int)tgName, fameYield);

        }

        public void OnTrophyRemoved()
        {
           
        }
    }
}