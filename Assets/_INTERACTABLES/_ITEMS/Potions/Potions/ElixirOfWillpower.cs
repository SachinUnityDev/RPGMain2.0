using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class ElixirOfWillpower : PotionsBase, Iitems, IEquipAble, IConsumable
    {
        public override PotionNames potionName => PotionNames.ElixirOfWillpower;      
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int)PotionNames.ElixirOfWillpower;
        public SlotType invSlotType { get; set; }
        public int maxInvStackSize { get; set; }
        public ItemController itemController { get; set; }
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; }
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

            PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionNames)itemName);
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);

            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                     , AttribName.willpower, +1, TimeFrame.Infinity, -1, true);  // Not a buff 

            if (QuestMissionService.Instance.currQuestMode == QuestMode.Taunt)
            {
                charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                   , AttribName.morale, -3, TimeFrame.EndOfNight, castTime, true);
            }

              charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
              , AttribName.willpower, -3, TimeFrame.EndOfNight, castTime, true);

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

