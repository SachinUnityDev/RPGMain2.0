using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

        [Header("Portraits")]
        [SerializeField] Transform portLeft;
        [SerializeField] Transform portRight;

        [Header("heading Txt")]
        [SerializeField] TextMeshProUGUI headingTxt;

        [Header(" Trade Scroll")]
        public TradeScrollView tradeScrollView;

        [Header(" Trade Select")]
        public TradeSelectView tradeSelectView;

        
        [Header(" Global var")]
        public TradeModel tradeModel;
        public NPCNames npcName;
        public BuildingNames buildName;
        NPCSO npcSO;
        List<Iitems> invitems = new List<Iitems>();
        private void Start()
        { 
            buyBtn.onClick.AddListener(OnBuyBtnPressed);
            sellBtn.onClick.AddListener(OnSellBtnPressed);           
        }
        public void InitTradeView(NPCNames npcName, BuildingNames buildName)
        {
            this.npcName = npcName;
            this.buildName = buildName;
            gameObject.SetActive(true);
            npcSO = TradeService.Instance.tradeController.allNPCSO.GetNPCSO(npcName);
            tradeSelectView.InitSelectView(npcName, this);
            FillBuySlots();
            isBuyBtnPressed = true;
            tradeBtnPtrEvents.InitTradeBtnEvents(this, tradeSelectView);
            Load();
            FillPortraits(); 
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
   
        public void OnTradePressed()
        {
            if(isBuyBtnPressed)
            {               
                if(tradeModel.allSelectItems.Count > 0)
                EcoService.Instance.DebitPlayerInv(tradeSelectView.netVal);
             
            }
            else
            {
                EcoService.Instance.AddMoney2PlayerInv(tradeSelectView.netVal);
            }
            tradeSelectView.InitInvMoney();
            tradeSelectView.InitTransactCurrViews();
            tradeSelectView.OnTradePressed();
            tradeBtnPtrEvents.InitTradeBtnEvents(this, tradeSelectView); 
        }

        #endregion

        void FillPortraits()
        {
            CharacterSO charSO = CharService.Instance.allCharSO.GetAllySO(CharNames.Abbas);
            portLeft.GetChild(0).GetComponent<Image>().sprite = charSO.charSprite;
            portRight.GetChild(0).GetComponent<Image>().sprite = npcSO.npcSprite; 
        }

        void FillBuySlots()
        {
           
            headingTxt.text =  npcSO.classTypes.ToString().CreateSpace()+ " Stock";            
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
            headingTxt.text = "Inventory"; 
            List<Iitems> commInvItems = InvService.Instance.invMainModel.commonInvItems;            
            invitems.Clear();
            foreach (Iitems item in commInvItems)
            {
                if(npcSO.itemTypesAccepted.Any(t=>t== item.itemType))
                {
                    invitems.Add(item);
                }
            }
            tradeSelectView.ClearSlotView();
            if (tradeScrollView != null)
            {
                tradeScrollView.ClearSlotView();
                tradeScrollView.InitSlotView(invitems, this);
            }
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }

        public void UnLoad()
        {
            TradeService.Instance.On_TradeEnds(); 
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
            
        }

        public void Init()
        {
           
        }
    }
}