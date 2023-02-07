using Combat;
using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class PurchaseFurnitureView : MonoBehaviour, IPanel
    {

        [SerializeField] Transform plankContainer;
        [SerializeField] Button closeBtn;


        [SerializeField] Button yesBtn;
        [SerializeField] Button noBtn; 

        public int selectInt =-1;

        HouseModel houseModel; 

        private void Awake()
        {
            yesBtn.onClick.AddListener(OnYesPressed); 
            noBtn.onClick.AddListener(OnNoPressed);
            closeBtn.onClick.AddListener(() => UIControlServiceGeneral.Instance.TogglePanel(gameObject,false));   
            
        }


        private void Start()
        {
            Init();
        }
        public void Init()
        {
            FillSlots();
        }

        public void Load()
        {
            
        }

        public void UnLoad()
        {
            
        }

        void FillSlots()
        {
            houseModel = BuildingService.Instance.houseController.houseModel;
            int i = 0; 
            foreach(Transform child in plankContainer)
            {
                child.GetComponent<HousePlankBtnEvents>().Init(houseModel.purchaseOpts[i], this);
                i++; 
            }
        }

        void OnYesPressed()  // purchase confirmed
        {
            if (selectInt == -1) return; 
            HousePurchaseOpts opts = (HousePurchaseOpts)selectInt;
            houseModel.purchaseOpts.Find(t => t.houseOpts == opts).isPurchased = true;

            plankContainer.GetChild(selectInt).GetComponent<HousePlankBtnEvents>().OnPurchase();
            selectInt = -1;    
        }
        void OnNoPressed()
        {
            plankContainer.GetChild(selectInt).GetComponent<HousePlankBtnEvents>().OnCancelPurchase();
            selectInt = -1;
        }
    }
}