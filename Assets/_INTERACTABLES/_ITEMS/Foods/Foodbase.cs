using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public abstract class Foodbase
    {
        public abstract FoodNames foodName { get; }
        public ItemController itemController { get; set; }
        public CharController charController { get; set; }
        public FoodSO foodSO { get; set; }
        public virtual void ApplyFX1()
        {
            foodSO = ItemService.Instance.GetFoodSO(foodName);

            int valHp = UnityEngine.Random.Range(foodSO.hpHealMin, foodSO.hpHealMax+1);

            charController = itemController.GetComponent<CharController>();
            charController.ChangeStat(CauseType.Food, (int)foodName,  charController.charModel.charID
                , StatsName.health, valHp);
          
            // hunger .. thirst 


        } 
        public abstract void ApplyFX();
    }

    public interface iShelfAble
    {     
        // start life ... tick and turn to rotten food
        // As soon as added to common , excess, and stash start tick
        // connect to invController add to above three inv

    }
}

