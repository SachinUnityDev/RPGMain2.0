using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Interactables
{
    public class CraftData
    {
        public ItemData itemSelf ; 
        public List<ItemData> otherComponent = new List<ItemData>(); 
        public ItemData craftedResult;

        public CraftData(ItemData itemSelf)
        {
            this.itemSelf = itemSelf;
        }
    }

   

    public interface ISocketable  //BLACKSMITH ONLY 
    {
        GemName gemName { get; }
        ArmorType armorType {get; }
        bool IsSocketAble();
        void OnSocket(); //Can DO IN town+ quest prep scene + camp ...option not available in other states as in combat etc
        void OnUnSocket(); // gem to be Destroyed on UnSocket.. will not to common Inv....
                           // ONLY BLACKSMIRTH CAN UNSOCKET A GEM 
        void SocketGemFX();   // three sockets 
    
    }

    public interface IEnchantable    // ONLY IN THE TEMPLE // ONLY DIVINE GEM ENCHANT 
    {
        GemName gemName { get; }
        WeaponType weaponType { get; }
        void EnchantGemFX();   // only one socket... unlocks a skills 
        bool IsEnchantable();      
        int currCharge { get; set; } // 12 
    }

    public interface ICraftable  // ONLY PRECIOUS GEMS ARE CRAFTABLE 
    {
        
    }


    public interface IDivineGem
    {

    }
    public interface ISupportable
    {
        void SupportGemFX(float multiplier); 

    }

    public interface ISupportGem
    {
        List<GemName> supportedDivineGems { get; set; }

        void GemFX(); 
       
    }
    public interface IPreciousGem
    {
        List<TgNames> supportedTradeGoods { get; set; }         



    }
    public abstract class GemBase 
    {
        public abstract GemName gemName { get; }
        public abstract GemType gemType { get; }

        protected CharController charController;
        public abstract CharNames charName { get; set; }
        public abstract int charID { get; set; }
        public abstract GemModel gemModel { get; set; }
        public virtual GemModel GemInit(GemSO gemSO, CharController charController) 
        {
            Iitems item = this as Iitems;
            item.maxInvStackSize = gemSO.inventoryStack;
            // CONTROLLERS AND MODELS
            this.charController = charController;
            charName = charController.charModel.charName;
            charID = charController.charModel.charID;

            gemModel = new GemModel(gemSO, charController);
            return gemModel;

        } // depending on the name copy and Init the params         

        public bool IsDisposable(GameState _State)
        {
            return true; 
        }

       
        public abstract float singleBoost { get; set; }  // if divine gem support gem will provide a boost
        public abstract float doubleBoost { get; set; }        

    }

    //Divine, // enchanted(Weapon) and socketed(Armor)
    //    Precious, // only craftable ... precious gem on ring => emerald 
    //    Support,// only socketed


}

