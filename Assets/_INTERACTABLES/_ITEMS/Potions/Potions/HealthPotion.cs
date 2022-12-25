using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat; 

namespace Interactables
{
    public class HealthPotion : PotionsBase,IRecipe,  Iitems,IConsumable, IEquipAble
    {
        public override PotionNames potionName => PotionNames.HealthPotion; 
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int)PotionNames.HealthPotion;
        public SlotType invSlotType { get; set; }
        public int maxInvStackSize { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public ItemData toolData { get; set; }
        public List<IngredData> allIngredData { get; set; }

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
            int buffID= 
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                                , StatsName.vigor, -2, TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);   

            StatData hpData = charController.GetStat(StatsName.health);
            float val = (Random.Range(40f, 60f) * hpData.maxLimit) / 100f;
            charController.damageController
                .ApplyDamage(charController, CauseType.Potions, (int)potionName, DamageType.Heal, val, false);

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

            ItemData ingred1 = new ItemData(ItemType.Ingredients, (int)IngredNames.FelineHeart);
            allIngredData.Add(new IngredData(ingred1, 1));

            ItemData ingred2 = new ItemData(ItemType.Ingredients, (int)IngredNames.HumanEar);
            allIngredData.Add(new IngredData(ingred2, 1));

            ItemData ingred3 = new ItemData(ItemType.Herbs, (int)HerbNames.Aloe);
            allIngredData.Add(new IngredData(ingred3, 1));
        }
    }



}
