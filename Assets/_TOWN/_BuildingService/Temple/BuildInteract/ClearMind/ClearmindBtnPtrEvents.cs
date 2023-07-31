using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Common
{
    public class ClearmindBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] Sprite btnN;
        [SerializeField] Sprite btnHL;
        [SerializeField] Sprite btnNA;

        ClearmindView clearMindView; 
        TempTraitController tempTraitController;
        [SerializeField] CharNames charName;

        [SerializeField] Transform clearMindtxtTrans;

        [Header("Global var")]
        [SerializeField] bool isClickable;
        [SerializeField] Image img;

        public void InitBtnEvents(CharNames charName, TempTraitController tempTraitController, ClearmindView clearMindView)
        {
            img = GetComponent<Image>();
            this.clearMindView = clearMindView; 
            this.tempTraitController = tempTraitController;
            this.charName = charName;

        }
        public void SetState(bool isClickable)
        {
            this.isClickable = isClickable;
            SetImg();
        }
        void SetImg()
        {
            img = GetComponent<Image>();
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
            tempTraitController.OnClearMindPressed();
            clearMindView.OnClearMindPressed(); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
          clearMindtxtTrans.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            clearMindtxtTrans.gameObject.SetActive(false); 
        }
    }
}

//[SerializeField] Sprite btnN;
//[SerializeField] Sprite btnHL;
//[SerializeField] Sprite btnNA;
//// [SerializeField] Sprite btnDisabled;
//[SerializeField] Transform costDisplay;



//[SerializeField] CharNames charSelect;
//[SerializeField] ArmorModel armorModel;
//[SerializeField] FortifyView fortifyView;
//Currency fortifyCost;
//[SerializeField] bool isClickable;
//[SerializeField] Image img;
//private void Start()
//{
//    EcoServices.Instance.OnPocketSelected += btnStateOnPocketChg;
//}
//public void InitFortifyBtn(CharNames charSelect, ArmorModel armorModel, FortifyView fortifyView)
//{
//    img = GetComponent<Image>();
//    this.charSelect = charSelect;
//    this.armorModel = armorModel;
//    this.fortifyView = fortifyView;
//    LocationName locName = TownService.Instance.townModel.currTown;
//    fortifyCost = armorModel.GetFortifyCost(locName).DeepClone(); // get build upgrading
//    costDisplay.GetChild(1).GetComponent<DisplayCurrency>().Display(fortifyCost);
//}
//// pocket change 

//void btnStateOnPocketChg(PocketType pocketType)
//{
//    Currency amt = EcoServices.Instance.GetMoneyFrmCurrentPocket();
//    if (amt.BronzifyCurrency() >= fortifyCost.BronzifyCurrency())
//    {
//        SetState(true);
//    }
//    else
//    {
//        SetState(false);
//    }
//}
//public void SetState(bool isClickable)
//{
//    this.isClickable = isClickable;
//    SetImg();

//}
//void SetImg()
//{
//    if (isClickable)
//    {
//        img.sprite = btnN;
//    }
//    else
//    {
//        img.sprite = btnNA;
//    }
//}
//public void OnPointerClick(PointerEventData eventData)
//{
//    ArmorService.Instance.OnArmorFortifyPressed(charSelect, armorModel);
//}

//public void OnPointerEnter(PointerEventData eventData)
//{
//    costDisplay.gameObject.SetActive(true);
//    if (isClickable)
//        img.sprite = btnHL;
//}

//public void OnPointerExit(PointerEventData eventData)
//{
//    costDisplay.gameObject.SetActive(false);
//    if (isClickable)
//        img.sprite = btnNA;
//}