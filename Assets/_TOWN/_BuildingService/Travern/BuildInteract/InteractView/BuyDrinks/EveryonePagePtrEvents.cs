using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Common;
using Interactables;
using TMPro;
using UnityEngine.Rendering;

namespace Town
{
    public class EveryonePagePtrEvents : MonoBehaviour
    {
        BuyDrinksView buyDrinksView;

        [Header("TBR")]
        [SerializeField] Button tickBtn; 
        [SerializeField] Button returnBtn;
        [SerializeField] Button exitBtn;
        [SerializeField] Transform currTransform;

        [SerializeField] TextMeshProUGUI displayTxt;
        [SerializeField] int silver;
        [SerializeField]string displayStr = "";
        TavernModel tavernModel;
        private void Awake()
        {
            returnBtn.onClick.AddListener(OnReturnBtnPressed);
            exitBtn.onClick.AddListener(OnExitBtnPressed);
            tickBtn.onClick.AddListener(OnTickBtnPressed);
            silver = UnityEngine.Random.Range(5, 11);
            displayStr = $"Wanna spend {silver} silver denari to buy everyone beer?";
            displayTxt.text = displayStr;
            CalendarService.Instance.OnChangeTimeState += ResetOnTimeStateChg;
        }
   
        void ResetOnTimeStateChg(TimeState timeState)
        {
            if(tavernModel!= null) 
            tavernModel.canOfferDrink = true;
        }
        bool CanOfferDrink()
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
        void OnTickBtnPressed()
        {
            if (!CanOfferDrink()) return; 
            EcoServices.Instance.DebitPlayerInvThenStash(new Currency(silver, 0));
            int fameGained = UnityEngine.Random.Range(3, 9);
            displayStr = $"You gained {fameGained} <style=fameSyblPos> Fame";
            displayTxt.text = displayStr;
            tavernModel.canOfferDrink = false; 
        }
        public void InitBuyEveryOne(BuyDrinksView buyDrinksView)
        {
            this.buyDrinksView= buyDrinksView;
            tavernModel = BuildingIntService.Instance.tavernController.tavernModel;
            currTransform.GetComponent<DisplayCurrencyWithToggle>().InitCurrencyToggle();
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