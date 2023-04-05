using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class ElixirOfVigor : PotionsBase, Iitems, IEquipAble, IConsumable
    {
        public override PotionNames potionName => PotionNames.ElixirOfVigor; 
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int)PotionNames.ElixirOfVigor;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
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
            charController.ChangeAttrib(CauseType.Potions, (int)potionName, charID
                                        , AttribName.vigor, +1);  // Not a buff 

            PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionNames)itemName);
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);

            int buffID = -1;
            if (QuestService.Instance.questMode == QuestMode.Stealth)
            {
                     buffID = charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                   , AttribName.morale, -3, TimeFrame.EndOfNight, castTime, true);

                allBuffs.Add(buffID);   
            }
            buffID = charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
             , AttribName.vigor, -3, TimeFrame.EndOfNight, castTime, true);
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

