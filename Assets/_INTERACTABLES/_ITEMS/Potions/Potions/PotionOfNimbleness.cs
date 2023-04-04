using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class PotionOfNimbleness : PotionsBase, Iitems, IRecipe, IEquipAble, IConsumable
    {
        public override PotionNames potionName => PotionNames.PotionOfNimbleness; 
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int) PotionNames.PotionOfNimbleness;
        public SlotType invSlotType { get; set; }
        public int maxInvStackSize { get ; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public ItemData toolData { get; set; }
        public List<ItemDataWithQty> allIngredData { get; set; }
        public void RecipeInit()
        {
            toolData = new ItemData(ItemType.Tools, (int)ToolNames.Mortar);

            ItemData ingred1 = new ItemData(ItemType.Ingredients, (int)IngredNames.DragonflyWings);
            allIngredData.Add(new ItemDataWithQty(ingred1, 1));

            ItemData ingred2 = new ItemData(ItemType.Ingredients, (int)IngredNames.BatEar);
            allIngredData.Add(new ItemDataWithQty(ingred2, 1));

            ItemData ingred3 = new ItemData(ItemType.Herbs, (int)HerbNames.PurpleTeaLeaf);
            allIngredData.Add(new ItemDataWithQty(ingred3, 1));
        }
        public void OnHoverItem()
        {

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
                       , AttribName.dodge, +3, TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);

            buffID =
            charController.buffController.ApplyBuffOnRange(CauseType.Potions, (int)potionName, charID
                      , AttribName.armor, -1f, -1f, TimeFrame.EndOfRound, castTime, true);
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

      
    }


}

