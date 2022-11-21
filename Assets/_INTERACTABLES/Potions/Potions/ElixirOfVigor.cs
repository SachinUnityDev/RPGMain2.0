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

        public override PotionModel potionModel { get ; set; }

        public ItemType itemType => ItemType.Potions;

        public int itemName => (int)PotionName.ElixirOfVigor;

        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }

        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX1()
        {
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                     , StatsName.vigor, +1, TimeFrame.Infinity, -1, true);  // Not a buff 
        }

        public override void PotionApplyFX2()
        {
            if(GameService.Instance.gameModel.gameMode == GameMode.Stealth)
            {
                charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                   , StatsName.morale, -3, TimeFrame.EndOfNight, potionModel.castTime, true);
            }
        }

        public override void PotionApplyFX3()
        {
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                  , StatsName.vigor, -3, TimeFrame.EndOfNight, potionModel.castTime, true);
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

