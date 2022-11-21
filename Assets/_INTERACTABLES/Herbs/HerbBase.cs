using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Combat; 


namespace Interactables
{
    public interface IhealingHerb
    {

    }
    public interface IIngredient
    {

    }
    public interface IConsumableByAbzazulu
    {

    }

    public abstract class HerbBase 
    {
        public abstract HerbNames herbName { get;  } 

        protected  CharController charController;
        public virtual CharNames charName { get; set; }
        public  virtual int charID { get; set; }
        public  virtual HerbModel herbModel { get; set; }

        // item name and charController
        public virtual HerbModel HerbInit(HerbNames herbName, CharController charController)
        {
            Iitems item = this as Iitems;
            HerbSO herbSO = ItemService.Instance.GetHerbSO(herbName);
            item.maxInvStackSize = herbSO.maxInventoryStack;
            // CONTROLLERS AND MODELS
            this.charController = charController;
            charName = charController.charModel.charName;
            charID = charController.charModel.charID;

            herbModel = new HerbModel(herbSO, charController);
            return herbModel;

        }
        //public int maxInvStackSize { get; set; }
        //public SlotType invSlotType { get; set; }
        // apply fx method=> remove sickNess after a timedelay
        // herb model cost,inventory slot  and herb .. no max world instance
        // 
        public virtual void OnConsumedByAbzazuluFX()
        {
            charController.buffController.ApplyBuff(CauseType.Herb, (int)herbName, charID
            , StatsName.hpRegen, herbModel.HpRegenVal, TimeFrame.EndOfRound, herbModel.bufftimeInRds, true);
        } 
      
    }
}
