using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{

    public class FortifyBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Sprite btnN;
        [SerializeField] Sprite btnHL;
        [SerializeField] Sprite btnNA; 
       // [SerializeField] Sprite btnDisabled;
        [SerializeField] Transform costDisplay;



        [SerializeField] CharController charController;
        [SerializeField] ArmorModel armorModel;
        [SerializeField] FortifyView fortifyView;
        Currency fortifyCost;
        [SerializeField] bool isClickable;
        [SerializeField] Image img;
        private void Start()
        {
            EcoServices.Instance.OnPocketSelected -= (PocketType p) => BtnStateUpdate();
            EcoServices.Instance.OnPocketSelected +=(PocketType p)=> BtnStateUpdate(); 
        }
        public void  InitFortifyBtn(Currency fortifyCost, CharController charController, ArmorModel armorModel, FortifyView fortifyView)
        {
            img = transform.GetChild(0).GetComponent<Image>();
            this.charController = charController;
            this.armorModel = armorModel;   
            this.fortifyView= fortifyView;
            this.fortifyCost = fortifyCost; 
            costDisplay.GetChild(1).GetComponent<DisplayCurrency>().Display(fortifyCost);
            BtnStateUpdate();
        }
        // pocket change 

        void BtnStateUpdate()
        {
            Currency amt = EcoServices.Instance.GetMoneyFrmCurrentPocket();
            if (armorModel.armorState == ArmorState.Fortifiable && amt.BronzifyCurrency() >= fortifyCost.BronzifyCurrency()) 
                SetState(true); 
            else
                SetState(false);
        }

  
        public void SetState(bool isClickable)
        {
            this.isClickable = isClickable;
            SetImg(); 
            
        }
        void SetImg()
        {
            if (isClickable)
            {
                img.sprite = btnN;
            }
            else
            {
                img.sprite = btnNA;
            }
        }
 

        public void OnPointerClick(PointerEventData eventData)
        {
            if(isClickable)
            {
                fortifyView.OnFortifyBtnPressed(charController, armorModel);            
                BtnStateUpdate();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {            
            if (isClickable)
            {
                costDisplay.gameObject.SetActive(true);
                img.sprite = btnHL;
            }                
        }

        public void OnPointerExit(PointerEventData eventData)
        {            
            if (isClickable)
            {
                costDisplay.gameObject.SetActive(false);
                img.sprite = btnN;
            }                
        }
    }
}