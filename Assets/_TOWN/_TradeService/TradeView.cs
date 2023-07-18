using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class TradeView : MonoBehaviour, IPanel
    {
        [Header("buy button/ Sell Btn")]
        public bool isBuyBtnPressed = false;

        [Header("Trade Btns")]
        public TradeBtnPtrEvents tradeBtnPtrEvents;
        [SerializeField] Button buyBtn;
        [SerializeField] Button sellBtn;
        [SerializeField] Button exitBtn;


        [Header(" Trade Scroll")]
        public TradeScrollView tradeScrollView;

        [Header(" Trade Select")]
        public TradeSelectView tradeSelectView;

        [Header("Trade N Talk Btn View")]
        public TalkNTradeBtnPtrEvents talkNTradeBtnPtrEvents; 

        [Header(" Global var")]
        public TradeModel tradeModel;
        public NPCNames npcName;
        public BuildingNames buildName;


        private void Start()
        { 
            buyBtn.onClick.AddListener(OnBuyBtnPressed);
            sellBtn.onClick.AddListener(OnSellBtnPressed);
            // exitBtn.onClick.AddListener(OnExitBtnPressed);
        }
        public void InitTradeView(NPCNames npcName, BuildingNames buildName, TalkNTradeBtnPtrEvents talkNTradeBtnPtrEvents)
        {
            this.npcName = npcName;
            this.talkNTradeBtnPtrEvents= talkNTradeBtnPtrEvents; 
            this.buildName = buildName;
            gameObject.SetActive(true);

            tradeSelectView.InitSelectView(npcName, this);
            FillSellSlots();
            isBuyBtnPressed = false;
            tradeBtnPtrEvents.InitTradeBtnEvents(this, tradeSelectView);
            Load(); 
        }

        #region BUTTONS RESPONSES

        void OnBuyBtnPressed()
        {
            isBuyBtnPressed = true;
            FillBuySlots();
        }
        void OnSellBtnPressed()
        {
            isBuyBtnPressed = false;
            FillSellSlots();
        }
        void OnExitBtnPressed()
        {
            // clear trade box 
            // reset to buy
        }

        public void OnTradePressed()
        {
            if(isBuyBtnPressed)
            {
                // subtract money from player inv 
                EcoServices.Instance.DebitPlayerInv(tradeSelectView.netVal);    
            }
            else
            {
                EcoServices.Instance.AddMoney2PlayerInv(tradeSelectView.netVal);
            }
            tradeSelectView.InitInvMoney();
            tradeSelectView.InitTransactCurrViews();
            tradeSelectView.OnTradePressed();
        }

        #endregion

        void FillBuySlots()
        {
            // get  Slots 
            tradeModel = TradeService.Instance.tradeController.GetTradeModel(npcName);
            tradeSelectView.ClearSlotView();
            if (tradeScrollView != null && tradeModel != null)
            {
                tradeScrollView.ClearSlotView();
                tradeScrollView.InitSlotView(tradeModel.allItems, this);
            }
            else
                Debug.Log("Trade Scroll view n trade Model not FOUND");

        }
        void FillSellSlots()
        {
            List<Iitems> commInvItems = InvService.Instance.invMainModel.commonInvItems;
            // sort out items that do not confirm to the NPC
            tradeSelectView.ClearSlotView();
            if (tradeScrollView != null)
            {
                tradeScrollView.ClearSlotView();
                tradeScrollView.InitSlotView(commInvItems, this);
            }

        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
            talkNTradeBtnPtrEvents.OnDeSelect();
        }

        public void Init()
        {
           
        }
    }
}