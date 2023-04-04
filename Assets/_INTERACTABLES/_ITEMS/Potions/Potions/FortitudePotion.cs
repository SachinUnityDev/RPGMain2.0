using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class FortitudePotion : PotionsBase,IRecipe,  Iitems, IEquipAble, IConsumable
    {
        public override PotionNames potionName => PotionNames.FortitudePotion;
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int)PotionNames.FortitudePotion;
        public SlotType invSlotType { get; set; }
        public int maxInvStackSize { get; set; }
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();
        public ItemData toolData { get; set; }
        public List<ItemDataWithQty> allIngredData { get; set; } = new List<ItemDataWithQty>();
        public void RecipeInit()
        {
            toolData = new ItemData(ItemType.Tools, (int)ToolNames.Mortar);

            ItemData ingred1 = new ItemData(ItemType.Ingredients, (int)IngredNames.FelineHeart);
            allIngredData.Add(new ItemDataWithQty(ingred1, 1));

            ItemData ingred2 = new ItemData(ItemType.Ingredients, (int)IngredNames.BatEar);
            allIngredData.Add(new ItemDataWithQty(ingred2, 1));

            ItemData ingred3 = new ItemData(ItemType.Herbs, (int)HerbNames.Hemp);
            allIngredData.Add(new ItemDataWithQty(ingred3, 1));
        }
        public void OnHoverItem()
        {

        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
            RecipeInit();
        }
        public override void PotionApplyFX()
        {
            float value = Random.Range(22f, 28f);

            int buffId =
                charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                        , AttribName.fortitude, value, TimeFrame.Infinity, -1, true);           
            allBuffs.Add(buffId);    

            buffId =
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                      , AttribName.fortOrg, -2, TimeFrame.EndOfQuest, 1, true);
            allBuffs.Add(buffId);
        }

        public void ApplyConsumableFX()
        {
            Debug.Log("Fortitude potion Consumed");
        }
        public void ApplyEquipableFX()
        {

        }

        public void RemoveEquipableFX()
        {

        }


    }


}

