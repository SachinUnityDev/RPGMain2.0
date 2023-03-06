using Combat;
using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class PurchaseFurnitureView : MonoBehaviour, IPanel
    {

        [SerializeField] Transform plankContainer;
        [SerializeField] Button closeBtn;

        [Header("To be ref")]
        [SerializeField] Button yesBtn;
        [SerializeField] Button noBtn;
        [SerializeField] Transform descTxt;
        [SerializeField] Transform currencyTrans;


        [Header("global Variable")]
        public int selectInt =-1;        
        HouseModel houseModel;
        Currency stashCurr;
        Currency purchaseValue; 
        private void Awake()
        {
            yesBtn.onClick.AddListener(OnYesPressed); 
            noBtn.onClick.AddListener(OnNoPressed);
            closeBtn.onClick.AddListener(() => UIControlServiceGeneral.Instance.TogglePanel(gameObject,false));   
        }

        public void Init()
        {
          
        }

        public void Load()
        {
            FillSlots();
            FillStashMoney();   
        }

        public void UnLoad()
        {
            
        }
        public void OnSlotSelect(int index)
        {
            selectInt = index;
            int i = 0;
            foreach (Transform child in plankContainer)
            {
                if(i != index)
                child.GetComponent<HousePlankBtnEvents>().OnDeSelect(); 
                i++;
            }
            AppearTxtNBtns();
        }
        void FillStashMoney()
        {
            stashCurr = EcoServices.Instance.GetMoneyAmtInPlayerStash();
            currencyTrans.GetComponent<DisplayCurrency>().Display(stashCurr);
        }
        void FillSlots()
        {
            houseModel = BuildingIntService.Instance.houseController.houseModel;
            int i = 0; 
            foreach(Transform child in plankContainer)
            {
                child.GetComponent<HousePlankBtnEvents>(). Init(houseModel.purchaseOpts[i], houseModel, this);
                i++; 
            }
        }
        void AppearTxtNBtns()
        {
            yesBtn.gameObject.SetActive(true);
            noBtn.gameObject.SetActive(true); 
            descTxt.gameObject.SetActive(true); 
        }
        void DisappearTxtnBtns()
        {
            yesBtn.gameObject.SetActive(false);
            noBtn.gameObject.SetActive(false);
            descTxt.gameObject.SetActive(false);
        }
        void OnYesPressed()  // purchase confirmed
        {
            if (selectInt == -1) return; 
            HousePurchaseOpts opts = (HousePurchaseOpts)selectInt;
            houseModel.purchaseOpts.Find(t => t.houseOpts == opts).isPurchased = true;

            purchaseValue = houseModel.purchaseOpts.Find(t => t.houseOpts == opts).currency;
            if (purchaseValue.BronzifyCurrency() < stashCurr.BronzifyCurrency())
                EcoServices.Instance.DebitPlayerStash(purchaseValue);
            else
            {
                //change txt
                return;
            }
            plankContainer.GetChild(selectInt).GetComponent<HousePlankBtnEvents>().OnPurchase();// modified status on plank 

            FillStashMoney();
            selectInt = -1;
            DisappearTxtnBtns();
     
        }
        //void CompleteSale()
        //{
        //    HousePurchaseOpts opts = (HousePurchaseOpts)selectInt;
         

        //}

        void OnNoPressed()
        {
            if(selectInt == -1) return;
            plankContainer.GetChild(selectInt).GetComponent<HousePlankBtnEvents>().OnCancelPurchase();
            selectInt = -1;
        }
    }
}