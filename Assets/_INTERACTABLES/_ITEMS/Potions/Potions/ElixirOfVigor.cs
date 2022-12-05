using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class ElixirOfVigor : PotionsBase, IEquipAble, IConsumable, IItemDisposable, ISellable, Iitems
    {
        public override PotionName potionName => PotionName.ElixirOfVigor; 

        public override CharNames charName { get; set; }
        public override int charID { get; set; }
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

        public override void PotionEndFX()
        {

        }

        //   **************************CONSUMABLE ***************
        public bool IsConsumable(GameState _state)
        {
            return false;
        }

        public void ApplyConsumableFX()
        {
        }
        //   **************************CONSUMABLE ***************

        public bool IsDisposable(GameState _State)
        {
            return false;
        }

        public void ApplyDisposable()
        {

        }
        //   ************************** EQUIPABLE ***************
        public bool IsEquipAble(GameState _state)
        {
            return false;
        }
        //   ************************** SELLABLE ***************
        public bool IsSellable(GameState _state)
        {
            return false;
        }

        public void ApplySellable()
        {

        }


    }
}

