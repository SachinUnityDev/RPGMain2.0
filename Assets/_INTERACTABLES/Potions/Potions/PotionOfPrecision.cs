using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat; 

namespace Interactables
{
    public class PotionOfPrecision : PotionsBase, IEquipAble, IConsumable, IItemDisposable, ISellable , Iitems
    {
        public override PotionName potionName => PotionName.PotionOfPrecision;

        public override CharNames charName { get; set; }

        public override int charID { get; set; }

        public override PotionModel potionModel { get ; set ; }

        public ItemType itemType => ItemType.Potions;

        public int itemName => (int) PotionName.PotionOfPrecision;

        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public ItemController itemController { get; set; }
        public void InitItem() { }
        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX1()
        {
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                       , StatsName.acc, +3, TimeFrame.EndOfRound, potionModel.castTime, true);
        }

        public override void PotionApplyFX2()
        {
            charController.buffController.ApplyBuffOnRange(CauseType.Potions, (int)potionName, charID
                      , StatsName.damage, -1f,-1f, TimeFrame.EndOfRound, potionModel.castTime, true);
        }

        public override void PotionApplyFX3()
        {

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
