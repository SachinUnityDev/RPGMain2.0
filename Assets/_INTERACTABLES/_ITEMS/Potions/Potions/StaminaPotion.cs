using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class StaminaPotion : PotionsBase, Iitems, IEquipAble, IConsumable 
    {
        public override PotionName potionName => PotionName.StaminaPotion; 
        public ItemType itemType => ItemType.Potions; 
        public int itemName => (int) PotionName.StaminaPotion;
        public SlotType invSlotType { get; set; }
        public int maxInvStackSize { get; set; }       
        public int itemId { get; set; }
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
            PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionName)itemName);
            int charID = charController.charModel.charID;
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);

            int buffID = 
                    charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                            , StatsName.willpower, -1, TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);

            StatData staminaData = charController.GetStat(StatsName.stamina);
            float val = (Random.Range(80f, 100f) * staminaData.maxLimit) / 100f;
           
            charController.ChangeStat(CauseType.Potions, (int)potionName, charID, StatsName.stamina, val);
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

