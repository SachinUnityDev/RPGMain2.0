using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class CraftView : MonoBehaviour, IPanel
    {
        [SerializeField] Currency currForCraft = new Currency(0, 9); 
        [Header("TBR")]
        [SerializeField] PotionOptsBtnView potionOptsBtnView;
        [SerializeField] CraftRecipeView craftRecipeView;
        [SerializeField] Transform currencyTrans;
        [SerializeField] Transform costCurrtrans;
        [SerializeField] Button exitBtn;
        [SerializeField] CraftBtnPtrEvents craftBtnPtrEvents;
        [SerializeField] TextMeshProUGUI OnPotionCraftedTxt; 

        [Header("Current potion Select")]
        [SerializeField] PotionNames potionSelect;
        Iitems healthPotion;
        Iitems staminaPotion;
        Iitems fortPotion;

        bool hasItems;
        bool hasMoney; 
        private void Awake()
        {
            exitBtn.onClick.AddListener(OnExitBtnPressed);
           
        }
        private void Start()
        {
            EcoServices.Instance.OnInvMoneyChg += (Currency c) => PotionBtnStateOnMoney();
            EcoServices.Instance.OnStashMoneyChg += (Currency c) => PotionBtnStateOnMoney();
        }

        void OnExitBtnPressed()
        {
            UnLoad();
        }

        void InitCraftView()
        {
         
            potionOptsBtnView.InitPotionPtrEvents(this);
            potionOptsBtnView.OnHealthPotionPressed();

            costCurrtrans.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                      BuildingIntService.Instance.marketController.marketModel.costOfCraftInBronze.ToString();
            currencyTrans.GetComponent<DisplayCurrencyWithToggle>().InitCurrencyToggle(); 
            craftBtnPtrEvents.InitCraftBtnPtrEvents(this);
            OnPotionCraftedTxt.gameObject.SetActive(false);
        }

        public void PotionSelect(int i)
        {
            hasItems = true;           
            PotionBtnStateOnMoney();
            Iitems item = GetPotionItem(i);
            potionSelect = (PotionNames)item.itemName; 
            // get ingred// 
            // pass ingred to the recipeSlot container 
            craftRecipeView.GetComponent<CraftRecipeView>().InitRecipeSlotView(this, item);
        }

        public void PotionBtnStateOnQty(bool hasItems)
        {
            this.hasItems= hasItems;    
            if (hasMoney && hasItems)
                craftBtnPtrEvents.SetState(true);
            else
                craftBtnPtrEvents.SetState(false);
        }
        public void PotionBtnStateOnMoney()
        {
            Currency money = EcoServices.Instance.GetMoneyFrmCurrentPocket();
            if(money.BronzifyCurrency() < currForCraft.BronzifyCurrency())
            {
                hasMoney = false; 
            }
            else
            {
                hasMoney= true; 
            }
            if (hasMoney && hasItems)
                craftBtnPtrEvents.SetState(true);
            else
                craftBtnPtrEvents.SetState(false);
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

        public void OnCraftPressed()
        {
            // add item to common Inv 
            bool InvHasSpace = false; 
            switch (potionSelect)
            {
                case PotionNames.None:
                    break;
                case PotionNames.HealthPotion:
                    InvHasSpace= InvService.Instance.invMainModel.AddItem2CommORStash(healthPotion);
                    craftRecipeView.RemoveIngredients(healthPotion);                    
                    break;
                case PotionNames.StaminaPotion:
                    InvHasSpace= InvService.Instance.invMainModel.AddItem2CommORStash(staminaPotion);
                    craftRecipeView.RemoveIngredients(staminaPotion);
                    break;
                case PotionNames.FortitudePotion:
                    InvHasSpace = InvService.Instance.invMainModel.AddItem2CommORStash(fortPotion);
                    craftRecipeView.RemoveIngredients(fortPotion);
                    break;                
                default:
                    break;
            }
            EcoServices.Instance.DebitMoneyFrmCurrentPocket(currForCraft); 
            OnPotionCraftedTxt.gameObject.SetActive(true);
            if(InvHasSpace)
            OnPotionCraftedTxt.text = $"{potionSelect.ToString().CreateSpace()} added to Inventory"; 
            else
                OnPotionCraftedTxt.text = $"Inventory is full";

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
          
        }

        public void Load()
        {
            CreatePotion();
            InitCraftView();
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    }
}