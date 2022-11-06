using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class ScorpionRepeller : PotionsBase, Iitems, IEquipAble, IConsumable, IDisposable, ISellable
    {
        public override PotionName potionName => PotionName.ScorpionRepeller; 

        public override CharNames charName { get; set; }

        public override int charID { get; set; }

        public override PotionModel potionModel { get ; set; }

        public ItemType itemType => ItemType.Potions;

        public int itemName => (int)PotionName.ScorpionRepeller;
        public SlotType invType { get; set; }

        public int maxInvStackSize { get ; set ; }
        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX1()
        {
            charController.GetComponent<CharStateController>().ApplyImmunityBuff(); 
            // Immune  poison 
        }

        public override void PotionApplyFX2()
        {
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                       , StatsName.earthRes, Random.Range(24f,36f), TimeFrame.EndOfRound, potionModel.castTime, true);
        }

        public override void PotionApplyFX3()
        {
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                    , StatsName.airRes, Random.Range(-12f, -18f), TimeFrame.EndOfRound, potionModel.castTime, true);
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

