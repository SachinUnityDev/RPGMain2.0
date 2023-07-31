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



        [SerializeField] CharNames charSelect;
        [SerializeField] ArmorModel armorModel;
        [SerializeField] FortifyView fortifyView;
        Currency fortifyCost;
        [SerializeField] bool isClickable;
        [SerializeField] Image img;
        private void Start()
        {
            EcoServices.Instance.OnPocketSelected += btnStateOnPocketChg; 
        }
        public void InitFortifyBtn(CharNames charSelect, ArmorModel armorModel, FortifyView fortifyView)
        {
            img = GetComponent<Image>();
            this.charSelect = charSelect;
            this.armorModel = armorModel;   
            this.fortifyView= fortifyView;
            LocationName locName = TownService.Instance.townModel.currTown;
            fortifyCost = armorModel.GetFortifyCost(locName).DeepClone(); // get build upgrading
            costDisplay.GetChild(1).GetComponent<DisplayCurrency>().Display(fortifyCost); 
        }
        // pocket change 

        void btnStateOnPocketChg(PocketType pocketType)
        {
            Currency amt = EcoServices.Instance.GetMoneyFrmCurrentPocket(); 
            if(amt.BronzifyCurrency() >= fortifyCost.BronzifyCurrency())
            {
                SetState(true);
            }
            else
            {
                SetState(false);
            }
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
            ArmorService.Instance.OnArmorFortifyPressed(charSelect, armorModel); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            costDisplay.gameObject.SetActive(true);
            if (isClickable)
                img.sprite = btnHL; 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            costDisplay.gameObject.SetActive(false);
            if (isClickable)
                img.sprite = btnNA; 
        }
    }
}