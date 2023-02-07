using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Interactables;

namespace Town
{
    public class HousePlankBtnEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] Image plankBG;//get it from the SO
        //public bool isPlankClicked;

        //[Header("To be ref")]
        //[SerializeField] TextMeshProUGUI itemNameTxt;
        //[SerializeField] Button tickButton;
        //[SerializeField] Transform currTrans;



        [Header("Plank BG to be ref")]
        [SerializeField] Sprite spriteBG_Purchased;
        [SerializeField] Sprite spriteBG_Purchaseable;
        [SerializeField] Sprite spriteStatus;

        [Header("house Data")]
        [SerializeField] HousePurchaseOptsData houseData;

        [Header("Parent")]
        [SerializeField] PurchaseFurnitureView purchaseFurnitureView;

        [Header("Transform")]
        [SerializeField] TextMeshProUGUI plankName;
        [SerializeField] Image statusImg;
        [SerializeField] Transform currencyTrans; 

        void Awake()
        {
            plankName = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            statusImg = transform.GetChild(1).GetComponent<Image>();
            currencyTrans = transform.GetChild(2);
            plankBG = GetComponent<Image>();


            //isPlankClicked = false;

            statusImg.gameObject.SetActive(false);

            //tickButton = GetComponentInChildren<Button>();
            //tickButton.gameObject.SetActive(false);
            //tickButton.onClick.AddListener(OnTickButtonPressed);
            
            
        }

        public void Init(HousePurchaseOptsData houseData, PurchaseFurnitureView purchaseFurnitureView)
        {
            this.houseData = houseData;
            this.purchaseFurnitureView = purchaseFurnitureView; 
            FillPlank(); 

        }
        void FillPlank()
        {
            plankName.text = houseData.houseOpts.ToString().CreateSpace();
            currencyTrans.GetComponent<CurrencyView>().Init(houseData.currency);
            statusImg.gameObject.SetActive(false);
            if (houseData.isPurchased)
                OnPurchase(); 
        }

        //public void SetUnclickedState()
        //{
        //    if (houseData.isPurchased) return; 
        //    isPlankClicked = false; 
        //    plankBG.DOFade(0, 0.05f);
        //   // tickButton.gameObject.SetActive(false);

        //}

        //void OnTickButtonPressed()
        //{
        //    Debug.Log("Purchase made");
        //}

        public void OnPointerEnter(PointerEventData eventData)
        {
            // if some is selected other will not be hoverable 
            if (purchaseFurnitureView.selectInt != -1) return; 
            if(houseData.isPurchased) return;
            plankBG.sprite = spriteBG_Purchaseable;
            plankBG.DOFade(1, 0.1f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (houseData.isPurchased) return;
            if (purchaseFurnitureView.selectInt != -1) return;
            plankBG.DOFade(0, 0.1f); 
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // update the parent
            if (houseData.isPurchased) return;
            purchaseFurnitureView.selectInt = transform.GetSiblingIndex();
            plankBG.sprite = spriteBG_Purchased;
            plankBG.DOFade(1, 0.1f);
        }

        public void OnPurchase()
        {
            houseData.isPurchased = true;
            
            plankBG.sprite = spriteBG_Purchased;
            plankBG.DOFade(1, 0.1f);

            statusImg.gameObject.SetActive(true);  
            statusImg.sprite = spriteStatus ;

            
        }
        public void OnCancelPurchase()
        {
            plankBG.sprite = spriteBG_Purchaseable;
            plankBG.DOFade(0, 0.1f);

        }

        //void ShowTick()
        //{
        //    if (isPlankClicked)  // Less money to purchase condition to be added later 
        //    {
        //        tickButton.gameObject.SetActive(true);
        //    }
        //}
        //bool ChkOtherClickedStatus()
        //{
        //    Transform parentTrans = transform.parent;
        //    foreach (Transform child in parentTrans)
        //    {
        //        if (child.gameObject == this.gameObject) continue;
        //        HousePlankBtnEvents HPBEvent = child.GetComponent<HousePlankBtnEvents>();
        //        if (HPBEvent.isPlankClicked)
        //        {
        //            return true; 
        //        }
        //    }
        //    return false; 
        //}

        //void ChgOtherPlankStatus()
        //{
        //    //get parent .. loop thru all the scripts see if any oneelse is clicked
        //    Transform parentTrans = transform.parent;
        //    foreach (Transform child in parentTrans)
        //    {
        //        if (child.gameObject == this.gameObject) continue;
        //        HousePlankBtnEvents HPBEvent = child.GetComponent<HousePlankBtnEvents>(); 
        //        if (HPBEvent.isPlankClicked)
        //        {   
        //            HPBEvent.SetUnclickedState();
        //        }
        //    }
        //}

        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    if (!isPlankClicked)
        //    {
        //        ChgOtherPlankStatus(); 
        //        plankBG.DOFade(1, 0.05f);
        //        isPlankClicked = true;
        //        ShowTick();
        //    }
        //    else
        //    {
        //        SetUnclickedState();
        //    }
        //}

        //public void OnPointerEnter(PointerEventData eventData)
        //{
        //    if(!ChkOtherClickedStatus())
        //        plankBG.DOFade(1, 0.05f); 
        //}

        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    if(!isPlankClicked)
        //        plankBG.DOFade(0, 0.05f);
        //}


    }


}




