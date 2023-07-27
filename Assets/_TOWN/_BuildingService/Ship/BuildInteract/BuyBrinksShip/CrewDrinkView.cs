using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class CrewDrinkView : MonoBehaviour, IPanel
    {
        BuyDrinksShipView shipDrinksView;

        [Header("TBR")]
        [SerializeField] CrewTickPtrEvents crewTickPtrEvents;
        [SerializeField] Button returnBtn;
        [SerializeField] Button exitBtn;
        [SerializeField] DisplayCurrencyWithToggle currDsply;

        [SerializeField] TextMeshProUGUI displayTxt;

        [Header(" Global Var")]
        [SerializeField] int silver;
        [SerializeField] string displayStr = "";
        [SerializeField] ShipModel shipModel;
        private void Awake()
        {
            CalendarService.Instance.OnChangeTimeState += ResetOnTimeStateChg;
        }
        private void Start()
        {
            returnBtn.onClick.AddListener(OnReturnBtnPressed);
            exitBtn.onClick.AddListener(OnExitBtnPressed);

            silver = UnityEngine.Random.Range(3, 13);
            displayStr = $"Crew members are sleeping right now...?";
            displayTxt.text = displayStr;
        }
        public void InitBuyEveryOne(BuyDrinksShipView shipDrinksView)
        {
            this.shipDrinksView = shipDrinksView;
            shipModel = BuildingIntService.Instance.shipController.shipModel;
            currDsply.InitCurrencyToggle();
            Currency availAmt = EcoServices.Instance.GetMoneyFrmCurrentPocket().DeepClone();
            TimeState timeState = CalendarService.Instance.currtimeState;
            if (timeState == TimeState.Day)
            {
                displayTxt.text = "Crew members are sleeping right now...";               
            }
            else if (timeState == TimeState.Night)
            {              
                displayTxt.text = $"Share drinks with crew members so they can tell nasty stories about you.\n" +
                        $"It will cost you just {silver} silver denari.";
            }
            crewTickPtrEvents.InitTickPtrEvents(this, availAmt, silver);
        }
        void ResetOnTimeStateChg(TimeState timeState)
        {
            if (shipModel != null)
            {
                shipModel.canOfferDrink = CanOfferDrink();
                crewTickPtrEvents.ChgTickState(CanOfferDrink());
            }
        }
        public bool CanOfferDrink()
        {
            if (CalendarService.Instance.currtimeState == TimeState.Night)
            {
                if (shipModel.canOfferDrink)
                    return true;
                else
                    return false;
            }
            return false;
        }
        public void OnTickBtnPressed()
        {
            if (!CanOfferDrink()) return;

            EcoServices.Instance.DebitMoneyFrmCurrentPocket(new Currency(silver, 0));          

            int fameLoss = UnityEngine.Random.Range(6, 15);
            displayStr = $"You lost {fameLoss} <style=fameSyblNeg> Fame";
            FameService.Instance.fameController.fameModel.fameVal -= fameLoss;

            displayTxt.text = displayStr;
            shipModel.canOfferDrink = false;
        }
        void OnExitBtnPressed()
        {
            shipDrinksView.OnExitBtnPressed();
        }
        void OnReturnBtnPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(shipDrinksView.mainDrinksView.gameObject, true);
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