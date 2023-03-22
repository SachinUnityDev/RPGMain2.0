using Interactables;
using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Town
{
    public class RecipeContainerSlotView : MonoBehaviour
    {
        [Header("slot TBR")]
        [SerializeField] Transform slot1;
        [SerializeField] Transform slot2;
        [SerializeField] Transform slot3;

        List<Transform> slots;

        [Header("Global var")]
        CraftView craftView;
        IRecipe recipe;
        private void Awake()
        {
            slots = new List<Transform>() { slot1, slot2, slot3 }; 
        }
        public void InitRecipeSlotView(CraftView craftView, Iitems item)
        {
            recipe = item as IRecipe;
            this.craftView = craftView;
            for (int i = 0; i < slots.Count; i++)
            {
                if(i< recipe.allIngredData.Count)
                {
                    slots[i].GetComponent<RecipeSlotController>().InitSlot(item, recipe.allIngredData[i]);
                }
                else
                {

                }
            }
        }
    }
}