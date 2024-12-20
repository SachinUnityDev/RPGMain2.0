using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class MainDrinksView : MonoBehaviour, IPanel
    {
        BuyDrinksShipView buyDrinksView;

        [SerializeField] Button selfBtn;
        [SerializeField] Button crewBtn;

        [SerializeField] Button exitBtn;

        private void Start()
        {
            selfBtn.onClick.AddListener(OnSelfBtnPressed);
            crewBtn.onClick.AddListener(OnCrewBtnPressed);
            exitBtn.onClick.AddListener(OnExitBtnPressed);
        }
        void OnExitBtnPressed()
        {
            buyDrinksView.OnExitBtnPressed();
        }
        public void InitMainPage(BuyDrinksShipView buyDrinksView)
        {
            this.buyDrinksView = buyDrinksView;
        }
        void OnCrewBtnPressed()
        {
            //EveryonePagePtrEvents ptrEvents = buyDrinksView.buyEveryone.GetComponent<EveryonePagePtrEvents>();
            //ptrEvents.InitBuyEveryOne(buyDrinksView);
            buyDrinksView.crewDrinkView.InitBuyEveryOne(buyDrinksView); 
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(buyDrinksView.crewDrinkView.gameObject, true);
        }
        void OnSelfBtnPressed()
        {
            //BuyDrinksPtrEvents ptrEvents = buyDrinksView.buyEveryone.GetComponent<BuyDrinksPtrEvents>();
            //ptrEvents.Init(ptrEvents);
            buyDrinksView.selfDrinksView.InitSelfPage(buyDrinksView);
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(buyDrinksView.selfDrinksView.gameObject, true);
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