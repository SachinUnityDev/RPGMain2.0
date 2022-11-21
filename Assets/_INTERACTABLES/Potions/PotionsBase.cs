using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Combat;


namespace Interactables
{
  
    public interface Iitems  // Apply this to derived class of all ITEMS
    {
        ItemType itemType { get; }
        int itemName { get; }
        int maxInvStackSize { get; set; }
        SlotType invSlotType { get; set; }

      //public abstract void InitItem();
        public abstract void OnHoverItem();
        
    }


    public interface IConsumable
    {
        bool IsConsumable(GameState _state);
        void ApplyConsumableFX(); 
    }

    public interface IEquipAble   // stored in active inventory in combat 
    {
        bool IsEquipAble(GameState _state); 
    }

    public interface IItemDisposable
    {
        bool IsDisposable(GameState _State);  // probably state can be removed
        void ApplyDisposable(); 
    }

    public interface ISellable
    {
        bool IsSellable(GameState _state);  // may be add NPC Name to it

        void ApplySellable(); 
    }

    public interface IPurchaseable
    {
        void ApplyPurchaseable();
    }



    public abstract class PotionsBase 
    {
        public abstract PotionName potionName { get; }
        public int potionInstanceCount = 0; 

        protected CharController charController;
        protected PotionController potionController; 
        public abstract CharNames charName { get; set; }
        public abstract int charID { get; set; }
        public abstract PotionModel potionModel { get; set; }
        public virtual PotionModel PotionInit(PotionSO potionSO) 
        {
            // IiTEMS ....
            potionModel = new PotionModel(potionSO);            
            Iitems item = this as Iitems;
            item.maxInvStackSize = potionSO.inventoryStack;
           
            return potionModel; 

            //potionController = charController.gameObject.GetComponent<PotionController>();
            //if (potionController != null)
            //    potionController.allPotionModelsInInv.Add(potionModel);
                
        } // depending on the name copy and Init the params 

        public virtual PotionModel PotionEquip(CharController charController)
        {
            this.charController = charController;
            charName = charController.charModel.charName;
            charID = charController.charModel.charID;

            // check if potion Model auto updates 
            potionModel.charName = charName;
            return potionModel;
        }

        public abstract void PotionApplyFX1();
        public abstract void PotionApplyFX2();
        public abstract void PotionApplyFX3();
        public abstract void PotionEndFX(); 

    }
}

