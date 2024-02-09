using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class DemonsPiss : PotionsBase, Iitems, IEquipAble, IConsumable
    {
        public ItemType itemType => ItemType.Potions; 
        public int itemName => (int)PotionNames.DemonsPiss;
        public int maxInvStackSize { get; set; }
        public override PotionNames potionName => PotionNames.DemonsPiss; 
        public SlotType invSlotType { get ; set; }
        public int slotID { get; set; }
        public int itemId { get; set; }
        public Currency currency { get; set; }
        public List<int> allBuffs { get; set; }

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
            
            PotionSO potionSO = ItemService.Instance.allItemSO.GetPotionSO((PotionNames)itemName);
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);
            
           charController.GetComponent<CharStateController>().ApplyImmunityBuff(CauseType.Potions, (int)potionName, charID
                               , CharStateName.Burning, TimeFrame.EndOfRound, castTime);            

            int buffId = charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                     , AttribName.fireRes, Random.Range(24f, 37f), TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffId);
            buffId = charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                   , AttribName.waterRes, Random.Range(-12f, -19f), TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffId);
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

  
    }

}
