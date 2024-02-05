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

        public HerbSO herbSO;
        // item name and charController
        public virtual void HerbInit(HerbNames herbName, CharController charController)
        {
            Iitems item = this as Iitems;
             herbSO = ItemService.Instance.allItemSO.GetHerbSO(herbName);
            item.maxInvStackSize = herbSO.maxInvStackSize;
            // CONTROLLERS AND MODELS
            this.charController = charController;
            charName = charController.charModel.charName;
            charID = charController.charModel.charID;

        }
  
        public virtual void OnConsumedByAbzazuluFX()
        {
            charController.buffController.ApplyBuff(CauseType.Herb, (int)herbName, charID
            , AttribName.hpRegen, herbSO.HpRegenVal, TimeFrame.EndOfRound, herbSO.bufftimeInRds, true);
        } 
      
    }
}
