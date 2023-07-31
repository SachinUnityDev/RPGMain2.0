using Common;
using DG.Tweening;
using Interactables;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Town
{
    public class CraftRecipeView : MonoBehaviour
    {
        [Header("slot TBR")]
        [SerializeField] Transform slot1;
        [SerializeField] Transform slot2;
        [SerializeField] Transform slot3;

        List<Transform> slots;

        [Header("Global var")]
        CraftView craftView;
        IRecipe recipe;
        public bool hasAllItems; 

        private void Awake()
        {
           
        }
        public void InitRecipeSlotView(CraftView craftView, Iitems item)
        {
            slots = new List<Transform>() { slot1, slot2, slot3 };
            hasAllItems = true; 
            recipe = item as IRecipe;
            this.craftView = craftView;
            for (int i = 0; i < slots.Count; i++)
            {
                if(i< recipe.allIngredData.Count)
                {
                    slots[i].GetComponent<RecipeSlotController>().InitSlot(craftView, this, recipe.allIngredData[i]);
                }
            }
            craftView.PotionBtnStateOnQty(hasAllItems);
        }
        public void RemoveIngredients(Iitems item)
        {
            foreach (ItemDataWithQty itemQty in recipe.allIngredData) 
            {
                for (int i = 0; i < itemQty.quantity; i++)
                {
                    InvService.Instance.invMainModel.RemoveItemFrmCommInv(itemQty.itemData); 
                }
            }
            InitRecipeSlotView(craftView, item);
        }
    }
}