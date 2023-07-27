using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class SelfDrinksView : MonoBehaviour, IPanel
    {
        [Header("TBR")]
        [SerializeField] Transform beerTrans;
        [SerializeField] Transform rumTrans;
        [SerializeField] Button exitBtn;
        [SerializeField] DisplayCurrencyWithToggle currDsply;

        [Header("Global Var")]
        [SerializeField] BuyDrinksShipView buyDrinksView;
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
        public void InitSelfPage(BuyDrinksShipView buyDrinksView)
        {
            this.buyDrinksView = buyDrinksView;
            beerTrans.GetComponent<ShipAlcoholBtnPtrEvents>().InitAlcoholPtrEvents(buyDrinksView, this);
            rumTrans.GetComponent<ShipAlcoholBtnPtrEvents>().InitAlcoholPtrEvents(buyDrinksView, this);
            currDsply.InitCurrencyToggle();
        }
        public void SetState(bool isNormal)
        {
            if (isNormal)
            {
                beerTrans.GetComponent<ShipAlcoholBtnPtrEvents>().SetNSprite();
                rumTrans.GetComponent<ShipAlcoholBtnPtrEvents>().SetNSprite();
            }
            else
            {
                beerTrans.GetComponent<ShipAlcoholBtnPtrEvents>().SetNASprite();
                rumTrans.GetComponent<ShipAlcoholBtnPtrEvents>().SetNASprite();
            }
        }
        void OnReturnBtnPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(buyDrinksView.mainDrinksView.gameObject, true);
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
           // UIControlServiceGeneral.Instance.TogglePanelOnInGrp(buyDrinksView.selfDrinksView.gameObject, true);

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