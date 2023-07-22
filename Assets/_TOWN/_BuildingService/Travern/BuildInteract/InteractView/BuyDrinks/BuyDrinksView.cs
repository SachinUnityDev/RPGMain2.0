using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class BuyDrinksView : MonoBehaviour, IPanel
    {
        [Header("TBR")]
        public Transform buyDrinksMain;
        public Transform buySelf;
        public Transform buyEveryone;  

        public List<Transform> allPanels; 

        [Header("BUY SELF")]
        [SerializeField] Currency currency;

        [Header("Beer and Cider items")]
        public Iitems itemBeer;
        [SerializeField] bool hasBeer; 
        public Iitems itemCider;
        [SerializeField] bool hasCider;

        [Header("Can Drink")]
        public bool canSelfDrink;
        public bool canGroupDrink; 

        private void Start()
        {
            allPanels = new List<Transform>() { buyDrinksMain, buySelf, buyEveryone}; 
        }
        public void OnExitBtnPressed()
        {
            UnLoad();
        }
        public void Init()
        {          
            FillUpAlcoholStock(); 
            buyDrinksMain.GetComponent<MainPtrEvents>().InitMainPage(this); 
            buySelf.GetComponent<SelfPagePtrEvents>().InitSelfPage(this);  
            buyEveryone.GetComponent<EveryonePagePtrEvents>().InitBuyEveryOne(this);    
        }
        public void FillUpAlcoholStock()
        {
            if(itemBeer == null)
            {
                itemBeer = ItemService.Instance.itemFactory.GetNewItem(ItemType.Alcohol, (int)AlcoholNames.Beer); 
                hasBeer= true;
            }
            if(itemCider== null)
            {
                itemCider = ItemService.Instance.itemFactory.GetNewItem(ItemType.Alcohol, (int)AlcoholNames.Cider);
                hasCider= true;
            }
        }
        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(buyDrinksMain.gameObject, true);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    
    }
}