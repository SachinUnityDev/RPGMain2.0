using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class StaminaPotion : PotionsBase, Iitems, IEquipAble, IConsumable, IItemDisposable, ISellable 
    {
        public override PotionName potionName => PotionName.StaminaPotion; 
        public override CharNames charName { get; set; }
        public override int charID { get; set; }
      //  public override PotionModel potionModel { get ; set ; }
        public ItemType itemType => ItemType.Potions; 
        public int itemName => (int) PotionName.StaminaPotion;
        public SlotType invSlotType { get; set; }
        public int maxInvStackSize { get; set; }       
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; }
        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX()
        {
            PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionName)itemName);
            int charID = charController.charModel.charID;
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);

            int buffID = 
                    charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                            , StatsName.willpower, -1, TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);

            StatData staminaData = charController.GetStat(StatsName.stamina);
            float val = (Random.Range(80f, 100f) * staminaData.maxLimit) / 100f;
           
            charController.ChangeStat(CauseType.Potions, (int)potionName, charID, StatsName.stamina, val);
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
            PotionApplyFX();
          
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

        //   ************************** SELLABLE ***************
  

        public void ApplySellable()
        {

        }

        
    }


}

