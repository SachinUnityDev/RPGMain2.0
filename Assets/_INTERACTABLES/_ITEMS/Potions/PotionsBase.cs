using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Combat;


namespace Interactables
{
  
    public interface Iitems  // Apply this to derived class of all ITEMS
    {
        int itemId { get; set; } 
        ItemType itemType { get; }
        int itemName { get;  }
        int maxInvStackSize { get; set; }
        //above is import for direct inv interation
        // SO ref hard to find for every click 
        SlotType invSlotType { get; set; }
        abstract void InitItem(int itemId, int maxInvStackSize);      
        abstract void OnHoverItem();
        List<int> allBuffs  { get; set; }
    }


    public interface IConsumable
    {      
        void ApplyConsumableFX(); 
    }

    public interface IEquipAble   // stored in active inventory in combat 
    {
        void ApplyEquipableFX();
        void RemoveEquipableFX();

    }
    public interface IPurchaseable
    {
        void ApplyPurchaseable();
    }
    public abstract class PotionsBase 
    {
        public abstract PotionNames potionName { get; }
        public int potionInstanceCount = 0; 
        protected CharController charController;        
        protected CharNames charName;
        protected int charID;
        public virtual void  PotionEquip()  
        {
            charController = CharService.Instance.GetCharCtrlWithName(InvService.Instance.charSelect); 
            charName = charController.charModel.charName;
            charID = charController.charModel.charID;           
        }
        public abstract void PotionApplyFX();  
    }

    public interface IOverConsume
    {
        //data
        float OcWt { get; set; }
        TempTraitName tempTraitName { get; set; }        
        void CheckNApplyOC();// this write to OCData in item Model 
        // tap in itemController and check data in itemModel
        void ApplyOC_FX(); 
    }

}

