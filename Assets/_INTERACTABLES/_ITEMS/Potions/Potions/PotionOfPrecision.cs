using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat; 

namespace Interactables
{
    public class PotionOfPrecision : PotionsBase,Iitems, IEquipAble, IConsumable
    {
        public override PotionName potionName => PotionName.PotionOfPrecision;
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int) PotionName.PotionOfPrecision;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX()
        {
            PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionName)itemName);
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);
            int buffID =
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                       , StatsName.acc, +3, TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);
            buffID = 
            charController.buffController.ApplyBuffOnRange(CauseType.Potions, (int)potionName, charID
                      , StatsName.damage, -1f, -1f, TimeFrame.EndOfRound, castTime, true);
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
