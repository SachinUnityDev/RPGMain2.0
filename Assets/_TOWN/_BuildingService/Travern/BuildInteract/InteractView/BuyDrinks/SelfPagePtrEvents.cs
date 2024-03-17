using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Common;
using DG.Tweening;
using TMPro;

namespace Town
{
    public class SelfPagePtrEvents : MonoBehaviour, IPanel
    {
        [Header("TBR")]
        [SerializeField] Transform beerTrans;
        [SerializeField] Transform ciderTrans;
        [SerializeField] Button exitBtn;
        public DisplayCurrencyWithToggle currDsply;

        [Header("Global Var")]
        [SerializeField] BuyDrinksTavernView buyDrinksView;
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
        public void InitSelfPage(BuyDrinksTavernView buyDrinksView)
        {
            this.buyDrinksView = buyDrinksView;
            beerTrans.GetComponent<AlcoholBtnPtrEvents>().InitAlcoholPtrEvents(buyDrinksView, this);
            ciderTrans.GetComponent<AlcoholBtnPtrEvents>().InitAlcoholPtrEvents(buyDrinksView, this);
            currDsply.InitCurrencyToggle(); 
        }
        public void SetState(bool isNormal)
        {
            if(isNormal)
            {
                beerTrans.GetComponent<AlcoholBtnPtrEvents>().SetNSprite();
                ciderTrans.GetComponent<AlcoholBtnPtrEvents>().SetNSprite();
            }
            else
            {
                beerTrans.GetComponent<AlcoholBtnPtrEvents>().SetNASprite();
                ciderTrans.GetComponent<AlcoholBtnPtrEvents>().SetNASprite();
            }
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