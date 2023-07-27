using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Town
{
    public class BuyDrinksShipView : MonoBehaviour, IPanel
    {
        [Header("TBR")]
        public MainDrinksView mainDrinksView;
        public SelfDrinksView selfDrinksView;
        public CrewDrinkView crewDrinkView;

        public List<Transform> allPanels;

        [Header("BUY SELF")]
        [SerializeField] Currency currency;

        [Header("Beer and Cider items")]
        public Iitems itemBeer;
        [SerializeField] bool hasBeer;
        public Iitems itemRum;
        [SerializeField] bool hasRum;

        [Header("Can Drink")]
        public bool canSelfDrink;
        public bool canGroupDrink;

        private void Start()
        {
           /* allPanels = new List<Transform>() { mainDrinksView, selfDrinksView, everyoneDrinksView }*/;
        }
        public void OnExitBtnPressed()
        {
            UnLoad();
        }
        public void Init()
        {
            FillUpAlcoholStock();
            mainDrinksView.InitMainPage(this);
            selfDrinksView.InitSelfPage(this);
            crewDrinkView.InitBuyEveryOne(this);

        }
        public void FillUpAlcoholStock()
        {
            if (itemBeer == null)
            {
                itemBeer = ItemService.Instance.itemFactory.GetNewItem(ItemType.Alcohol, (int)AlcoholNames.Beer);
                hasBeer = true;
            }
            if (itemRum == null)
            {
                itemRum = ItemService.Instance.itemFactory.GetNewItem(ItemType.Alcohol, (int)AlcoholNames.Rum);
                hasRum = true;
            }
        }
        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(mainDrinksView.gameObject, true);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }

    }
}