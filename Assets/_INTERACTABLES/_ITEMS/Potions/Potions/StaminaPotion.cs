using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class StaminaPotion : PotionsBase, Iitems,IRecipe, IEquipAble, IConsumable 
    {
        public override PotionNames potionName => PotionNames.StaminaPotion; 
        public ItemType itemType => ItemType.Potions; 
        public int itemName => (int) PotionNames.StaminaPotion;
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public int maxInvStackSize { get; set; }       
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();
        public ItemData toolData { get; set; }
        public List<ItemDataWithQty> allIngredData { get; set; } = new List<ItemDataWithQty>();
        public Currency currency { get; set; }
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
            PotionSO potionSO = ItemService.Instance.allItemSO.GetPotionSO((PotionNames)itemName);
            int charID = charController.charModel.charID;
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);

            int buffID = 
                    charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                            , AttribName.willpower, -1, TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);

            StatData staminaData = charController.GetStat(StatName.stamina);
            float val = (Random.Range(80f, 100f) * staminaData.maxLimit) / 100f;
           
            charController.ChangeStat(CauseType.Potions, (int)potionName, charID, StatName.stamina, val);
        }

        public void ApplyConsumableFX()
        {
            PotionApplyFX();
        }

        public void ApplyEquipableFX(CharController charController)
        {
            this.charController = charController;
        }

        public void RemoveEquipableFX()
        {
            charController = null;
        }

        public void RecipeInit()
        {
            toolData = new ItemData(ItemType.Tools, (int)ToolNames.Mortar);

            ItemData ingred1 = new ItemData(ItemType.Ingredients, (int)IngredNames.Hoof);
            allIngredData.Add(new ItemDataWithQty(ingred1, 1));

            ItemData ingred2 = new ItemData(ItemType.Ingredients, (int)IngredNames.HyenaEar);
            allIngredData.Add(new ItemDataWithQty(ingred2, 1));

            ItemData ingred3 = new ItemData(ItemType.Herbs, (int)HerbNames.Echinacea);
            allIngredData.Add(new ItemDataWithQty(ingred3, 1));
        }
    }
}

