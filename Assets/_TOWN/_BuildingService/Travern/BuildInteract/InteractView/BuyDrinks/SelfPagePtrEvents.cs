using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Common;
using DG.Tweening;

namespace Town
{
    public class SelfPagePtrEvents : MonoBehaviour
    {
        [Header("TBR")]
        [SerializeField] Transform beerTrans;
        [SerializeField] Transform ciderTrans;
        [SerializeField] Button exitBtn;
        [SerializeField] Transform currTransform; 

        [Header("Global Var")]
        [SerializeField] BuyDrinksView buyDrinksView;
        [SerializeField] Button returnBtn;
 
        

        private void Awake()
        {
            returnBtn.onClick.AddListener(OnReturnBtnPressed);
            exitBtn.onClick.AddListener(OnExitBtnPressed);

        }
        void OnExitBtnPressed()
        {
            buyDrinksView.UnLoad();
        }
        public void InitSelfPage(BuyDrinksView buyDrinksView)
        {
            this.buyDrinksView = buyDrinksView;
            beerTrans.GetComponent<AlcoholBtnPtrEvents>().InitAlcoholPtrEvents(buyDrinksView);
            ciderTrans.GetComponent<AlcoholBtnPtrEvents>().InitAlcoholPtrEvents(buyDrinksView);
            currTransform.GetComponent<DisplayCurrencyWithToggle>().InitCurrencyToggle(); 
        }
        void OnReturnBtnPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(buyDrinksView.buyDrinksMain.gameObject, true); 
        }

    }
}