using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class SnowLeopardsBreath : PotionsBase, Iitems,IEquipAble, IConsumable, ISellable 
    {
        public override PotionName potionName => PotionName.SnowLeopardsBreath; 
        public override CharNames charName { get; set; }
        public override int charID { get; set; }
       // public override PotionModel potionModel { get ; set; }
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int)PotionName.SnowLeopardsBreath;        
        public int maxInvStackSize { get ; set ; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX()
        {

            PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionName)itemName);
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);
            int buffID=
            charController.GetComponent<CharStateController>().ApplyImmunityBuff(CauseType.Potions, (int)potionName, charID
                                , CharStateName.Soaked, TimeFrame.EndOfRound , castTime, true); // TO BE FILLED 
            allBuffs.Add(buffID);
            // immune to soaked 
            buffID=
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                      , StatsName.waterRes, Random.Range(24f, 37f), TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);
            buffID =
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                , StatsName.fireRes, Random.Range(-12f, -19f), TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);
        }
        public override void PotionEndFX()
        {

        }

        //   **************************CONSUMABLE ***************
     

        public void ApplyConsumableFX()
        {
        }
        //   **************************CONSUMABLE ***************

    
        //   ************************** EQUIPABLE ***************
        public bool IsEquipAble(GameState _state)
        {
            return false;
        }
        //   ************************** SELLABLE ***************
    

        public void ApplySellable()
        {

        }

 
    }


}

