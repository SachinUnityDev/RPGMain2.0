using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class CraftView : MonoBehaviour, IPanel
    {
        [Header("TBR")]
        [SerializeField] Transform potionOptsBtnContainer;
        [SerializeField] Transform recipeSlotContainer;
        [SerializeField] Transform currencyTrans;
        [SerializeField] Transform costCurrtrans;
        [SerializeField] Button exitBtn;


        [Header("Current potion Select")]
        [SerializeField] PotionNames potionSelect;
        Iitems healthPotion;
        Iitems staminaPotion;
        Iitems fortPotion; 
        private void Awake()
        {
            exitBtn.onClick.AddListener(OnExitBtnPressed); 
        }
        void OnExitBtnPressed()
        {
            UnLoad();
        }

        void InitCraftView()
        {
            potionOptsBtnContainer.GetComponent<PotionOptsBtnView>().InitPotionPtrEvents(this);

        }

        public void PotionSelect(int i)
        {
            Iitems item = GetPotionItem(i);
            // get ingred// 
            // pass ingred to the recipeSlot container 

            recipeSlotContainer.GetComponent<RecipeContainerSlotView>().InitRecipeSlotView(this, item);
        }
        Iitems GetPotionItem(int i)
        {
            switch (i)
            {
                case 1:
                    return healthPotion;                     
                case 2:
                    return staminaPotion;
                case 3:
                    return fortPotion;  
            }
            return null;
        }

        void CreatePotion()
        {
            if(healthPotion == null)
            {
                healthPotion = ItemService.Instance.GetNewItem(new ItemData(ItemType.Potions
                                                                        , (int)PotionNames.HealthPotion));
                healthPotion.invSlotType = SlotType.CraftSlot; 
            }
            
            if(staminaPotion == null)
            {
                staminaPotion = ItemService.Instance.GetNewItem(new ItemData(ItemType.Potions
                                                                        , (int)PotionNames.StaminaPotion));
                staminaPotion.invSlotType = SlotType.CraftSlot;
            }
            if(fortPotion == null)
            {
                fortPotion = ItemService.Instance.GetNewItem(new ItemData(ItemType.Potions
                                                                       , (int)PotionNames.FortitudePotion));
                fortPotion.invSlotType = SlotType.CraftSlot;
            }
        }
    
        public void Init()
        {
            Load(); 
        }

        public void Load()
        {
            CreatePotion();
            potionSelect = PotionNames.None;
            InitCraftView();
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    }
}