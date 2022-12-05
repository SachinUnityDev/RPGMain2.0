using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class ElixirOfWillpower : PotionsBase, Iitems, IEquipAble, IConsumable, IItemDisposable, ISellable
    {
        public override PotionName potionName => PotionName.ElixirOfWillpower;

        public override CharNames charName { get; set; }
        public override int charID { get; set; }

        //public override PotionModel potionModel { get ; set ; }

        public ItemType itemType => ItemType.Potions;

        public int itemName => (int)PotionName.ElixirOfWillpower;
        public SlotType invSlotType { get; set; }
        public int maxInvStackSize { get; set; }
        public ItemController itemController { get; set; }
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; }

        public override void PotionApplyFX()
        {

            PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionName)itemName);
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);

            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                     , StatsName.willpower, +1, TimeFrame.Infinity, -1, true);  // Not a buff 

            if (GameService.Instance.gameModel.gameMode == GameMode.Taunt)
            {
                charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                   , StatsName.morale, -3, TimeFrame.EndOfNight, castTime, true);
            }

              charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
              , StatsName.willpower, -3, TimeFrame.EndOfNight, castTime, true);

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

        public void OnHoverItem()
        {
           
        }
    }
}

