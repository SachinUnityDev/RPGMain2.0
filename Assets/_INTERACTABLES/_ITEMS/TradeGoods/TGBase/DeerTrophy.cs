using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class DeerTrophy : TGBase, Iitems, ITrophyable
    {
        public override TGNames tgName => TGNames.DeerTrophy;
        public TavernSlotType tavernSlotType => TavernSlotType.Trophy;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.DeerTrophy;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public int fameYield => 1;

        // +1 Accuracy on Field
        //+1 Fame Yield"
        public void OnHoverItem()
        {

        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
            allDisplayStr.Add("+1 Acc on Field");
        }
  
        public void TrophyInit()
        {
    
        }

        public void OnTrophyWalled()
        {
            int index =
            charController.landscapeController.ApplyLandscapeBuff(CauseType.TradeGoods, (int)tgName
                     , LandscapeNames.Field, AttribName.acc, 1);
            allLandscapeIndex.Add(index);

            FameService.Instance.fameController.ApplyFameYieldChg(CauseType.TradeGoods, (int)tgName, fameYield);
            
        }

        public void OnTrophyRemoved()
        {
         
        }
    }
}

