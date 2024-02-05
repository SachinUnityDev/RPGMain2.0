using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class PotionOfHeroism : PotionsBase, IEquipAble, IConsumable, Iitems
    {
        public override PotionNames potionName => PotionNames.PotionOfHeroism;
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int)PotionNames.PotionOfHeroism;
        public SlotType invSlotType { get; set; }
        public int maxInvStackSize { get ; set ; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public Currency currency { get; set; }
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
            int buffID =     
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                       , AttribName.morale, Random.Range(2,5), TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);

            buffID =
                charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                     , AttribName.armorMin, -2f, TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);

            buffID =
                charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                     , AttribName.armorMax, -2f, TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);
        }
        public void ApplyConsumableFX()
        {
            PotionApplyFX();
        }

        public void ApplyEquipableFX(CharController charController)
        {
            this.charController= charController;
        }

        public void RemoveEquipableFX()
        {
            charController = null; 
        }

    }


}

