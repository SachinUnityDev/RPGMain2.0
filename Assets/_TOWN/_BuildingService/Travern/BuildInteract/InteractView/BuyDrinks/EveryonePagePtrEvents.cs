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
    public class EveryonePagePtrEvents : MonoBehaviour, IPanel
    {
        BuyDrinksTavernView buyDrinksView;

        [Header("TBR")]
        [SerializeField] TickBtnPtrEvents tickBtnPtrEvents; 
        [SerializeField] Button returnBtn;
        [SerializeField] Button exitBtn;
        [SerializeField] DisplayCurrencyWithToggle currDsply;

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
            //displayStr = $"Wanna spend {silver} silver denari to buy everyone beer?";
            //displayTxt.text = displayStr;
        }
        public void InitBuyEveryOne(BuyDrinksTavernView buyDrinksView)
        {
            this.buyDrinksView = buyDrinksView;
            tavernModel = BuildingIntService.Instance.tavernController.tavernModel;
            currDsply.InitCurrencyToggle();
            Currency availAmt = EcoService.Instance.GetMoneyFrmCurrentPocket().DeepClone();
            TimeState timeState = CalendarService.Instance.currtimeState; 
            UpdateDsplyTxt(timeState);
            tickBtnPtrEvents.InitTickPtrEvents(this, availAmt, silver); 
            
        }
        void UpdateDsplyTxt(TimeState timeState)
        {
            if (timeState == TimeState.Day)
            {
                displayTxt.text = "Not many people around to buy drink to...";
            }
            else if (timeState == TimeState.Night)
            {
                displayTxt.text = $"Wanna spend {silver} silver denari to buy everyone beer?";
            }
        }

        void ResetOnTimeStateChg(TimeState timeState)
        {
            if(tavernModel!= null)
            {
                tavernModel.canOfferDrink = CanOfferDrink();
                tickBtnPtrEvents.ChgTickState(CanOfferDrink());
                UpdateDsplyTxt(timeState);
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

            EcoService.Instance.DebitMoneyFrmCurrentPocket(new Currency(silver, 0));            

            int fameGained = UnityEngine.Random.Range(5, 10);
            displayStr = $"You gained {fameGained} <style=fameSyblPos> Fame";
            FameService.Instance.fameController.fameModel.fameVal += fameGained;

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

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }

        public void UnLoad()
        {
           UIControlServiceGeneral.Instance.TogglePanel(gameObject, false); 
        }

        public void Init()
        {
           Load();
        }
    }
}