using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public abstract class FruitBase
    {
        public abstract FruitNames fruitName { get; }
        public FruitSO fruitSO { get; set;  }
        public CharController charController { get; set; }  
        public int charID { get; set; }
        public virtual void ApplyInitNHPStaminaRegenFX() 
        {
            charController = ItemService.Instance.selectChar;
            charID = charController.charModel.charID;
            fruitSO = ItemService.Instance.GetFruitSO(fruitName); 

            float val = fruitSO.hpRegen;
            if(val > 0) 
            charController.buffController.ApplyBuff(CauseType.Fruit, (int)fruitName
                          , charID, StatsName.hpRegen, val, fruitSO.timeFrameRegen, fruitSO.regenCastTime, true);
            
            val = fruitSO.staminaRegen;
            if(val > 0)
            charController.buffController.ApplyBuff(CauseType.Fruit, (int)fruitName
                          , charID, StatsName.staminaRegen, val, fruitSO.timeFrameRegen, fruitSO.regenCastTime, true);

        }
        public virtual void ApplyHungerThirstRegenFX() 
        {
            
        }    
        public abstract void ApplyBuffFX();
    }
}