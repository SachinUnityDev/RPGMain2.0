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
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; }
        public ItemData toolData { get; set; }
        public List<ItemDataWithQty> allIngredData { get; set; } = new List<ItemDataWithQty>();
        public Currency currency { get; set; }
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
            string str = $"Chance to gain Focus buff";
            allDisplayStr.Add(str);
        }
        public void OnHoverItem()
        {
        }

        public override string OnDrink()
        {
            onDrinkBuffStr = "Keep Drinking..."; 
            float chance = 36f;
            charController = CharService.Instance.GetAllyController(CharNames.Abbas);
            if (chance.GetChance())
            {
                charController.buffController.ApplyBuff(CauseType.Items, (int)itemName, charController.charModel.charID
                    , AttribName.focus, 1, TimeFrame.EndOfDay, 1, true);
                onDrinkBuffStr = "+1 Focus gained for one day"; 
            }
            return onDrinkBuffStr; 

        }
    }
}