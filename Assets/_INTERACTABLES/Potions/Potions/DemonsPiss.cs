using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class DemonsPiss : PotionsBase, IEquipAble, IConsumable, IItemDisposable, ISellable, Iitems
    {
        public ItemType itemType => ItemType.Potions; 

        public int itemName => (int)PotionName.DemonsPiss;

        public int maxInvStackSize { get; set; }

        public override PotionName potionName => PotionName.DemonsPiss; 

        public override CharNames charName { get; set; }

        public override int charID { get; set; }

        public override PotionModel potionModel { get ; set ; }
        public SlotType invSlotType { get ; set; }
        public ItemController itemController { get; set; }
        public void InitItem() { }
        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX1()
        {

            charController.GetComponent<CharStateController>().ApplyImmunityBuff(CauseType.Potions, (int)potionName, charID
                                , CharStateName.BurnHighDOT, TimeFrame.Infinity, -1, true); 

            charController.GetComponent<CharStateController>().ApplyImmunityBuff(CauseType.Potions, (int)potionName, charID
                                , CharStateName.BurnLowDOT, TimeFrame.Infinity, -1, true);

            charController.GetComponent<CharStateController>().ApplyImmunityBuff(CauseType.Potions, (int)potionName, charID
                                , CharStateName.BurnMedDOT, TimeFrame.Infinity, -1, true);
        }

        public override void PotionApplyFX2()
        {
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                       , StatsName.fireRes, Random.Range(24f, 37f), TimeFrame.EndOfRound, potionModel.castTime, true);
        }

        public override void PotionApplyFX3()
        {
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                    , StatsName.waterRes, Random.Range(-12f, -19f), TimeFrame.EndOfRound, potionModel.castTime, true);
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
