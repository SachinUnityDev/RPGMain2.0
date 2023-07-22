using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Common;
using Interactables;
using TMPro;

namespace Town
{
    public class EveryonePagePtrEvents : MonoBehaviour
    {
        BuyDrinksView buyDrinksView;

        [Header("TBR")]
        [SerializeField] TickBtnPtrEvents tickBtnPtrEvents; 
        [SerializeField] Button returnBtn;
        [SerializeField] Button exitBtn;
        [SerializeField] Transform currTransform;

        [SerializeField] TextMeshProUGUI displayTxt;

        [Header(" Global Var")]
        [SerializeField] int silver;
        [SerializeField]string displayStr = "";
        TavernModel tavernModel;
        private void Awake()
        {
            CalendarService.Instance.OnChangeTimeState += ResetOnTimeStateChg;
        }
        private void Start()
        {
            returnBtn.onClick.AddListener(OnReturnBtnPressed);
            exitBtn.onClick.AddListener(OnExitBtnPressed);
          
            silver = UnityEngine.Random.Range(5, 10);
            displayStr = $"Wanna spend {silver} silver denari to buy everyone beer?";
            displayTxt.text = displayStr;
        }
        public void InitBuyEveryOne(BuyDrinksView buyDrinksView)
        {
            this.buyDrinksView = buyDrinksView;
            tavernModel = BuildingIntService.Instance.tavernController.tavernModel;
            currTransform.GetComponent<DisplayCurrencyWithToggle>().InitCurrencyToggle();
            Currency availAmt = EcoServices.Instance.GetMoneyAmtInPlayerInv().DeepClone();
            TimeState timeState = CalendarService.Instance.currtimeState; 
            if(timeState == TimeState.Day)
            {
                displayTxt.text = "Not many people around to buy drink to..."; 
            }
            else if(timeState == TimeState.Night)
            {
                displayTxt.text = $"Wanna spend {silver} silver denari to buy everyone beer?";
            }
            tickBtnPtrEvents.InitTickPtrEvents(this, availAmt, silver);            
        }
        void ResetOnTimeStateChg(TimeState timeState)
        {
            if(tavernModel!= null)
            {
                tavernModel.canOfferDrink = true;
                tickBtnPtrEvents.isClickable= true;
            }                
        }
        public bool CanOfferDrink()
        {
            if(CalendarService.Instance.currtimeState == TimeState.Night)
            {
                if(tavernModel.canOfferDrink)
                    return true;
                else
                    return false;
            }   
            return false;
        }
        public void OnTickBtnPressed()
        {
            if (!CanOfferDrink()) return; 

            EcoServices.Instance.DebitPlayerInvThenStash(new Currency(silver, 0));            
            currTransform.GetComponent<DisplayCurrencyWithToggle>().InitCurrencyToggle();
            
            int fameGained = UnityEngine.Random.Range(5, 10);
            displayStr = $"You gained {fameGained} <style=fameSyblPos> Fame";
            displayTxt.text = displayStr;
            tavernModel.canOfferDrink = false; 
        }
      

        void OnExitBtnPressed()
        {
            buyDrinksView.OnExitBtnPressed();
        }
        void OnReturnBtnPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(buyDrinksView.buyDrinksMain.gameObject, true);
        }
    }
}