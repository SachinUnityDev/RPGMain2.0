using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class FortitudePotion : PotionsBase, IEquipAble, IConsumable, IItemDisposable,ISellable,Iitems
    {
        public override PotionName potionName => PotionName.FortitudePotion;
        public override CharNames charName { get; set; }
        public override int charID { get; set; }

        public override PotionModel potionModel { get; set; }

        public ItemType itemType => ItemType.Potions;
        public int itemName => (int)PotionName.FortitudePotion;
        public SlotType invSlotType { get; set; }
        public int maxInvStackSize { get; set; }
        public ItemController itemController { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX1()
        {
            float value = Random.Range(22f, 28f); 

            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                        , StatsName.fortitude, value, TimeFrame.Infinity, -1, true);
        }

        public override void PotionApplyFX2()
        {
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                        , StatsName.fortOrg, -2, TimeFrame.EndOfQuest, 1, true); 
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

