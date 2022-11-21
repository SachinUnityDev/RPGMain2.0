using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat; 

namespace Interactables
{
    public class HealthPotion : PotionsBase, Iitems,IConsumable, IItemDisposable, IEquipAble, ISellable
    {
        public override PotionName potionName => PotionName.HealthPotion; 

        public override CharNames charName { get; set; }
        public override int charID { get; set; }

        public override PotionModel potionModel { get; set; }

        public ItemType itemType => ItemType.Potions;

        public int itemName => (int)PotionName.HealthPotion;
        public SlotType invSlotType { get; set; }

        public int maxInvStackSize { get; set; }
        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX1()
        {
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                                , StatsName.vigor, -2, TimeFrame.EndOfRound, potionModel.castTime, true);
        }

        public override void PotionApplyFX2()
        {
            StatData hpData = charController.GetStat(StatsName.health);
            float val = (Random.Range(40f, 60f) * hpData.maxLimit)/100f; 
            charController.damageController
                .ApplyDamage(charController, CauseType.Potions, (int)potionName, DamageType.Heal, val, false);

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
            return true;
        }

        public void ApplyConsumableFX()
        {
            PotionApplyFX1();
            PotionApplyFX2(); 

        }
        //   **************************CONSUMABLE ***************

        public bool IsDisposable(GameState _State)
        {
            return true; 
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
