using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.UI;


public class MainPtrEvents : MonoBehaviour
{
    BuyDrinksView buyDrinksView;

    [SerializeField] Button selfBtn;
    [SerializeField] Button everyoneBtn;

    [SerializeField] Button exitBtn; 

    private void Start()
    {
        selfBtn.onClick.AddListener(OnSelfBtnPressed);
        everyoneBtn.onClick.AddListener(OnEveryOneBtnPressed);
        exitBtn.onClick.AddListener(OnExitBtnPressed); 
    }
    void OnExitBtnPressed()
    {
        buyDrinksView.OnExitBtnPressed();
    }
    public void InitMainPage(BuyDrinksView buyDrinksView)
    {
        this.buyDrinksView = buyDrinksView;
    }
    void OnEveryOneBtnPressed()
    {
        EveryonePagePtrEvents ptrEvents = buyDrinksView.buyEveryone.GetComponent<EveryonePagePtrEvents>(); 
        ptrEvents.InitBuyEveryOne(buyDrinksView);    
        UIControlServiceGeneral.Instance.TogglePanelOnInGrp(buyDrinksView.buyEveryone.gameObject, true); 
    }
    void OnSelfBtnPressed()
    {
        //BuyDrinksPtrEvents ptrEvents = buyDrinksView.buyEveryone.GetComponent<BuyDrinksPtrEvents>();
        //ptrEvents.Init(ptrEvents);
        //buySelf.GetComponent<SelfPagePtrEvents>().InitSelfPage(this);
        UIControlServiceGeneral.Instance.TogglePanelOnInGrp(buyDrinksView.buySelf.gameObject, true);
    }
}
