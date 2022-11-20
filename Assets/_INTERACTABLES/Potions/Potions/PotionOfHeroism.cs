using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class PotionOfHeroism : PotionsBase, IEquipAble, IConsumable, IItemDisposable, ISellable , Iitems
    {
        public override PotionName potionName => PotionName.PotionOfHeroism;
        public override CharNames charName { get; set; }
        public override int charID { get; set; }
        public override PotionModel potionModel { get; set ; }
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int)PotionName.PotionOfHeroism;
        public SlotType invType { get; set; }
        public int maxInvStackSize { get ; set ; }
        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX1()
        {
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                       , StatsName.morale, Random.Range(2,4), TimeFrame.EndOfRound, potionModel.castTime, true);
        }

        public override void PotionApplyFX2()
        {
            charController.buffController.ApplyBuffOnRange(CauseType.Potions, (int)potionName, charID
                      , StatsName.armor, -2f, -2f, TimeFrame.EndOfRound, potionModel.castTime, true);
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

