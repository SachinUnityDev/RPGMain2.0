using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat; 

namespace Interactables
{
    public class PotionOfPrecision : PotionsBase,IRecipe, Iitems, IEquipAble, IConsumable
    {
        public override PotionNames potionName => PotionNames.PotionOfPrecision;
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int) PotionNames.PotionOfPrecision;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public ItemData toolData { get; set; }
        public List<IngredData> allIngredData { get; set; }

        public void OnHoverItem()
        {
            // hoverrr
        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
        }
        public override void PotionApplyFX()
        {
            PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionNames)itemName);
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);
            int buffID =
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                       , StatsName.acc, +3, TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);
            buffID = 
            charController.buffController.ApplyBuffOnRange(CauseType.Potions, (int)potionName, charID
                      , StatsName.damage, -1f, -1f, TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);
        }
        public void ApplyConsumableFX()
        {
        }

        public void ApplyEquipableFX()
        {

        }

        public void RemoveEquipableFX()
        {

        }

        public void RecipeInit()
        {
            toolData = new ItemData(ItemType.Tools, (int)ToolNames.Mortar);

            ItemData ingred1 = new ItemData(ItemType.Ingredients, (int)IngredNames.RatFang);
            allIngredData.Add(new IngredData(ingred1, 1));

            ItemData ingred2 = new ItemData(ItemType.Ingredients, (int)IngredNames.HyenaEar);
            allIngredData.Add(new IngredData(ingred2, 1));

            ItemData ingred3 = new ItemData(ItemType.Herbs, (int)HerbNames.Buchu);
            allIngredData.Add(new IngredData(ingred3, 1));
        }
    }



}
