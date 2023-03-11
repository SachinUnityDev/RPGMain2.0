using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{

    public class Cider :AlcoholBase, IRecipe, Iitems
    {
        public override AlcoholNames alcoholName => AlcoholNames.Cider;
        public int itemId { get; set; }
        public ItemType itemType => ItemType.Alcohol;
        public int itemName => (int)AlcoholNames.Cider;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public ItemData toolData { get; set; }
        public List<ItemDataWithQty> allIngredData { get; set; } = new List<ItemDataWithQty>();
        public void RecipeInit()
        {
            toolData = new ItemData(ItemType.Tools, (int)ToolNames.Fermentor);

            ItemData ingred1 = new ItemData(ItemType.Fruits, (int)FruitNames.Apple);
            allIngredData.Add(new ItemDataWithQty(ingred1, 1));

            ItemData ingred2 = new ItemData(ItemType.Ingredients, (int)IngredNames.Yeast);
            allIngredData.Add(new ItemDataWithQty(ingred2, 1));

            ItemData ingred3 = new ItemData(ItemType.Ingredients, (int)IngredNames.Cardamom);
            allIngredData.Add(new ItemDataWithQty(ingred3, 1));
        }

        public void InitItem(int itemId, int maxInvStackSize)
        {
            RecipeInit();
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;

        }
        public void OnHoverItem()
        {

        }

        public override void OnDrink()
        {
            float chance = 24f;
            charController = CharService.Instance.GetCharCtrlWithName(CharNames.Abbas_Skirmisher);
            if (chance.GetChance())
                charController.buffController.ApplyBuff(CauseType.Items, (int)itemName, charController.charModel.charID
                    , StatsName.focus, 1, TimeFrame.EndOfDay, 1, true);
        }
    }
}