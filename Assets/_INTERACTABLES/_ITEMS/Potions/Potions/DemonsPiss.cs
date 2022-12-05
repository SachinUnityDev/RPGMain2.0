using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class DemonsPiss : PotionsBase, Iitems, IEquipAble, IConsumable, ISellable
    {
        public ItemType itemType => ItemType.Potions; 
        public int itemName => (int)PotionName.DemonsPiss;
        public int maxInvStackSize { get; set; }
        public override PotionName potionName => PotionName.DemonsPiss; 
        public override CharNames charName { get; set; }
        public override int charID { get; set; }
        public SlotType invSlotType { get ; set; }     
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; }

        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX()
        {          
            
            PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionName)itemName);
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);

            int buffId =
           charController.GetComponent<CharStateController>().ApplyImmunityBuff(CauseType.Potions, (int)potionName, charID
                               , CharStateName.BurnHighDOT, TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffId);

            buffId = charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                     , StatsName.fireRes, Random.Range(24f, 37f), TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffId);
            buffId = charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                   , StatsName.waterRes, Random.Range(-12f, -19f), TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffId);
        }
   

        public override void PotionEndFX()
        {

        }

        //   **************************CONSUMABLE ***************
     
        public void ApplyConsumableFX()
        {
        }
        //   **************************CONSUMABLE ***************

        public void ApplyDisposable()
        {

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
