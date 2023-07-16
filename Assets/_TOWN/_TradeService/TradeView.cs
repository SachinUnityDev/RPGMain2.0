using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class TradeView : MonoBehaviour
    {

        [SerializeField] Button tradeBtn;
        [SerializeField] Button buyBtn;
        [SerializeField] Button sellBtn; 

        [SerializeField] Button exitBtn;

        [SerializeField] TradeScrollView tradeScrollView;

        [Header(" Global var")]
        public TradeModel tradeModel;
        public NPCNames npcName;
        public BuildingNames buildName; 

        private void Start()
        {
            tradeBtn.onClick.AddListener(OnTradeBtnPressed); 
            buyBtn.onClick.AddListener(OnBuyBtnPressed);
            sellBtn.onClick.AddListener(OnSellBtnPressed);  
           // exitBtn.onClick.AddListener(OnExitBtnPressed);
        }

        #region BUTTONS RESPONSES

        void OnTradeBtnPressed()
        {
           // complete transactions in trade slots
          
        }
        void OnBuyBtnPressed()
        {
          // buy btn pressed 
          // clear items in tradebox , Fill stock inv in panel below 
          FillBuySlots();
        }
        void OnSellBtnPressed()
        {
            FillSellSlots();
            // clear items in tradebox , Fill stash and Main ...items which are accepted by NPC
            // inv in panel below 
        }
        void OnExitBtnPressed()
        {
            // clear trade box 
            // reset to buy
        }
        #endregion

        public void InitTradeView(NPCNames npcName, BuildingNames buildName)
        {
            // get stock Inv npcModel, 
            // get stock of the NPC for buy(below) panel slots
            // get Abbas.. stash and main Inv items that are accepted by the NPC
            // fill to sell Panel slots
            this.npcName = npcName; 
            gameObject.SetActive(true);
            FillSellSlots(); 
        }

        void FillBuySlots()
        {
            // get  Slots 
            tradeModel = TradeService.Instance.tradeController.GetTradeModel(npcName);

            if (tradeScrollView != null && tradeModel != null)
            {
                tradeScrollView.ClearSlotView();
                tradeScrollView.InitSlotView(tradeModel.allItems);
            }   
            else
                Debug.Log("Trade Scroll view n trade Model not FOUND"); 

        }
        void FillSellSlots()
        {
            List<Iitems> commInvItems = InvService.Instance.invMainModel.commonInvItems;
            // sort out items that do not confirm to the NPC
            
            if (tradeScrollView != null)
            {
                tradeScrollView.ClearSlotView();
                tradeScrollView.InitSlotView(commInvItems);
            }
                
        }

        void FillTradeBox()
        {

        }
     
        void OnItemsPurchased()
        {
           // check items space availablity in the main inv and stash inv
           // if available then completed purchase
           // subtract money in selected money bag (Stash or mainInv)
           // 
        }
        void OnItemsSold()
        {

        }

    }

    public enum TradeState
    {
        None, 
        BuySelect, 
        SellSelect, 
        TradeCompleted, 
        TradeCancelled, 
    }


}