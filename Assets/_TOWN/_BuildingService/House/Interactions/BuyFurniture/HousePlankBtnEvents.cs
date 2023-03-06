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
        [SerializeField] Image plankBG;  

        [Header("Plank BG to be ref")]
        [SerializeField] Sprite spriteBG_Purchased;
        [SerializeField] Sprite spriteBG_Purchaseable;
        [SerializeField] Sprite spriteStatus;

        [Header("house Data")]
        [SerializeField] HousePurchaseOptsData houseData;
        [SerializeField] HouseModel houseModel;

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

            statusImg.gameObject.SetActive(false);

        }

        public void Init(HousePurchaseOptsData houseData, HouseModel houseModel, PurchaseFurnitureView purchaseFurnitureView)
        {
            this.houseModel= houseModel;    
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
            else
                OnDeSelect();
        }
    
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
                purchaseFurnitureView.OnSlotSelect(transform.GetSiblingIndex());
            plankBG.sprite = spriteBG_Purchaseable;
            plankBG.DOFade(1, 0.1f);
            
        }

        public void OnPurchase()
        {
            houseData.isPurchased = true;
            BuildingIntService.Instance.houseController.OnPurchase(houseData); 


            plankBG.sprite = spriteBG_Purchased;
            plankBG.DOFade(1, 0.1f);

            statusImg.gameObject.SetActive(true);  
            statusImg.sprite = spriteStatus ;

            
        }
        public void OnCancelPurchase()
        {
            if (houseData.isPurchased) return;
            plankBG.sprite = spriteBG_Purchaseable;
            plankBG.DOFade(0, 0.1f);

        }

        public void OnDeSelect()
        {
            if (houseData.isPurchased) return;
            plankBG.sprite = spriteBG_Purchaseable;
            plankBG.DOFade(0, 0.1f);
        }
    }


}




