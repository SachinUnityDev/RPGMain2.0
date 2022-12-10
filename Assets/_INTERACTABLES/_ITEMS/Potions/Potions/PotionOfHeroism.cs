using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class PotionOfHeroism : PotionsBase, IEquipAble, IConsumable, Iitems
    {
        public override PotionName potionName => PotionName.PotionOfHeroism;
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int)PotionName.PotionOfHeroism;
        public SlotType invSlotType { get; set; }
        public int maxInvStackSize { get ; set ; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
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
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);
            int buffID =     
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                       , StatsName.morale, Random.Range(2,5), TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);
            buffID =
            charController.buffController.ApplyBuffOnRange(CauseType.Potions, (int)potionName, charID
                     , StatsName.armor, -2f, -2f, TimeFrame.EndOfRound, castTime, true);

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

