using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


namespace Interactables
{
    public class DeerSkin : TGBase, Iitems, ITrophyable
    {
        public override TGNames tgName => TGNames.DeerSkin;
        public TavernSlotType tavernSlotType => TavernSlotType.Pelt;
        public ItemType itemType => ItemType.TradeGoods;
        public int itemName => (int)TGNames.DeerSkin;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public int fameYield => 1;

        public void OnHoverItem()
        {

        }

        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
            //+1 Dodge on Field
            //+1 Fame Yield"

            allDisplayStr.Add("+1 Dodge on Field");
        }
        public void TrophyInit()
        {
            
        }
        //+1 Dodge on Field
        //+1 Fame Yield"
        public void OnTrophyWalled()
        {
            int index =
            charController.landscapeController.ApplyLandscapeBuff(CauseType.TradeGoods, (int)tgName
             , LandscapeNames.Field, AttribName.dodge, 1);
            allLandscapeIndex.Add(index);

             FameService.Instance.fameController.ApplyFameYieldChg(CauseType.TradeGoods, (int)tgName, fameYield);
        }

        public void OnTrophyRemoved()
        {
           
        }
    }
}