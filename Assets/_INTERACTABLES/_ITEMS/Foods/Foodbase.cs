using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public abstract class Foodbase
    {
        public abstract FoodNames foodName { get; }   
        public FoodSO foodSO { get; set; }
        public CharController charController { get; set; }
        public int charID { get; set; }
        /// <summary>
        ///  Always apply FX1 before 2 as its also init for the food
        /// </summary>
        public virtual void ApplyFX1()  
        {
            foodSO = ItemService.Instance.GetFoodSO(foodName);

         //   int valHp = UnityEngine.Random.Range(foodSO.hpHealMin, foodSO.hpHealMax+1);
         //   Iitems item = this as Iitems;
         ////   charController=    item.itemController.GetComponent<CharController>(); 
         //   charID= charController.charModel.charID;
         //   charController.ChangeStat(CauseType.Food, (int)foodName,  charID
         //                                          , StatsName.health, valHp);
          
            // hunger .. thirst 


        } 
        public abstract void ApplyFX2();
    }

    public interface iShelfAble
    {     
        // start life ... tick and turn to rotten food
        // As soon as added to common , excess, and stash start tick
        // connect to invController add to above three inv

    }
}

