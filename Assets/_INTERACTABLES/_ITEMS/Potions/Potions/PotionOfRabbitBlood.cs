using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class PotionOfRabbitBlood : PotionsBase, Iitems, IEquipAble, IConsumable
    {
        public override PotionName potionName => PotionName.PotionOfRabbitblood;  
        public ItemType itemType => ItemType.Potions; 
        public int itemName => (int) PotionName.PotionOfRabbitblood;
        public SlotType invSlotType { get; set; }
        public int maxInvStackSize { get; set ; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }

        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX()
        {
            PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionName)itemName);
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime); 

            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                       , StatsName.luck, Random.Range(2,5), TimeFrame.EndOfRound, castTime, true);
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                      , StatsName.dodge, -2f, TimeFrame.EndOfRound, castTime, true);
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


