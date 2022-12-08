using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class Banana : FruitBase, Iitems, IConsumable, IOverConsume
    {
        public override FruitNames fruitName => FruitNames.Banana;
        public ItemType itemType => ItemType.Fruits;
        public int itemName => (int)FruitNames.Banana;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public TempTraitName tempTraitName  { get; set; }
        public float OcWt { get; set; }

        public void OnHoverItem()
        {

        }
        public override void ApplyBuffFX()
        {
            
        }
      

        public void CheckNApplyOC()
        {
            tempTraitName = fruitSO.tempTraitName;
            OcWt = fruitSO.weightOfSickeness;
            OCData ocData = new OCData(itemType, itemName, OcWt);
            charController.itemController.ChecknApplyOC(ocData, tempTraitName, this);
        }

        public void ApplyOC_FX()
        {
            charController.tempTraitController.ApplyTempTraitBuff(CauseType.Fruit, (int)itemName, charID
                                , tempTraitName, TimeFrame.Infinity, 1, true);
                
        }
        public void ApplyConsumableFX()
        {
            ApplyInitNHPStaminaRegenFX();
            ApplyHungerThirstRegenFX();
            CheckNApplyOC(); 

        }
    }
}