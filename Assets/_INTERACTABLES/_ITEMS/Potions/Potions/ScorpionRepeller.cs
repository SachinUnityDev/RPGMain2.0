using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class ScorpionRepeller : PotionsBase, Iitems, IEquipAble, IConsumable, IItemDisposable, ISellable
    {
        public override PotionName potionName => PotionName.ScorpionRepeller;
        public override CharNames charName { get; set; }
        public override int charID { get; set; }
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int)PotionName.ScorpionRepeller;
        public SlotType invSlotType { get; set; }
        public int maxInvStackSize { get ; set ; }
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; }
        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX()
        {
            
            PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionName)itemName);
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);

            int buffID = 
            charController.GetComponent<CharStateController>().ApplyImmunityBuff(CauseType.Potions, (int)potionName, charID
                                , CharStateName.PoisonedHighDOT, TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);
           

            buffID = charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                   , StatsName.earthRes, Random.Range(24f, 37f), TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);

            buffID= charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                    , StatsName.airRes, Random.Range(-12f, -19f), TimeFrame.EndOfRound, castTime, true);
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

