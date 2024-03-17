using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{

    public class Beer : AlcoholBase, IRecipe, Iitems
    {
        public override AlcoholNames alcoholName => AlcoholNames.Beer;
        public int itemId { get; set; }
        public ItemType itemType => ItemType.Alcohol;
        public int itemName => (int)AlcoholNames.Beer;
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

            ItemData ingred1 = new ItemData(ItemType.Ingredients, (int)IngredNames.Wheat);
            allIngredData.Add(new ItemDataWithQty(ingred1, 2));

            ItemData ingred2 = new ItemData(ItemType.Ingredients, (int)IngredNames.Yeast);
            allIngredData.Add(new ItemDataWithQty(ingred2, 1));
        }

        public void InitItem(int itemId, int maxInvStackSize)
        {
            RecipeInit(); 
            this.itemId= itemId;    
            this.maxInvStackSize = maxInvStackSize;
            string str = $"Chance to gain Morale buff";            
            allDisplayStr.Add(str);
         
        }
        public void OnHoverItem()
        {
        }
        public override string OnDrink()
        {
            // +1 morale for one day 
            onDrinkBuffStr = "Keep Drinking..."; 
            float chance = 30f;
            charController = CharService.Instance.GetAbbasController(CharNames.Abbas);
            if (chance.GetChance())
            {
                charController.buffController.ApplyBuff(CauseType.Items, (int)itemName, charController.charModel.charID
                    , AttribName.morale, 1, TimeFrame.EndOfDay, 1, true);
                onDrinkBuffStr = "+1 Morale gained for a day";                
            }
            return onDrinkBuffStr;

        }
    }
}