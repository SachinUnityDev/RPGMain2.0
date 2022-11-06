using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class StaminaPotion : PotionsBase, Iitems, IEquipAble, IConsumable, IDisposable, ISellable 
    {
        public override PotionName potionName => PotionName.StaminaPotion; 

        public override CharNames charName { get; set; }

        public override int charID { get; set; }

        public override PotionModel potionModel { get ; set ; }

        public ItemType itemType => ItemType.Potions; 

        public int itemName => (int) PotionName.StaminaPotion;
        public SlotType invType { get; set; }

        public int maxInvStackSize { get; set; }
        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX1()
        {
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                            , StatsName.willpower, -1, TimeFrame.EndOfNight, potionModel.castTime, true);
        }

        public override void PotionApplyFX2()
        {
            StatData staminaData = charController.GetStat(StatsName.stamina);
            float val = (Random.Range(80f, 100f) * staminaData.maxLimit) / 100f;
            int charID = charController.charModel.charID;
            charController.ChangeStat(CauseType.Potions, (int)potionName, charID, StatsName.stamina, val); 
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
            if (_state == GameState.InCombat || _state == GameState.InQuest)
                return true;
            else
                return false; 
        }

        public void ApplyConsumableFX()
        {
            PotionApplyFX1();
            PotionApplyFX2();
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

