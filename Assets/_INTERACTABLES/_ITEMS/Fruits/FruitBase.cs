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
        public virtual void ApplyHPStaminaRegenFX() 
        {   
            float val = fruitSO.hpRegen;

            charController.buffController.ApplyBuff(CauseType.Fruit, (int)fruitName
                          , charID, StatsName.hpRegen, val, fruitSO.timeFrameRegen, fruitSO.regenCastTime, true); 

        }
        public virtual void ApplyHungerThirstRegenFX() 
        {
            float val = fruitSO.staminaRegen;

            charController.buffController.ApplyBuff(CauseType.Fruit, (int)fruitName
                          , charID, StatsName.staminaRegen, val, fruitSO.timeFrameRegen, fruitSO.regenCastTime, true);


        }
        public virtual void ApplySicknessFX() 
        {
           
        
        } 
        public abstract void ApplyBuffFX();
    }
}