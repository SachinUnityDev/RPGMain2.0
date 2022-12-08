using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class ElixirOfVigor : PotionsBase, Iitems, IEquipAble, IConsumable
    {
        public override PotionName potionName => PotionName.ElixirOfVigor; 
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int)PotionName.ElixirOfVigor;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX()
        {
            charController.ChangeStat(CauseType.Potions, (int)potionName, charID
                                        , StatsName.vigor, +1);  // Not a buff 

            PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionName)itemName);
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);

            int buffID = -1;
            if (GameService.Instance.gameModel.gameMode == GameMode.Stealth)
            {
                     buffID = charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                   , StatsName.morale, -3, TimeFrame.EndOfNight, castTime, true);

                allBuffs.Add(buffID);   
            }
            buffID = charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
             , StatsName.vigor, -3, TimeFrame.EndOfNight, castTime, true);
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

