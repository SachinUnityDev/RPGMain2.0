using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Town
{
    public class TipAtShipPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("tip txt TBR")]
        [SerializeField] TextMeshProUGUI tiptxt;
        [Header("Tip btn Img")]
        [SerializeField] Image tipBtnImg;


        [Header("Global Var")]
        [SerializeField] string displayStr;
        [SerializeField] TipState tipState = TipState.Tipable;

        AllBuildSO allBuildSO;
        [SerializeField]ShipModel shipModel; 
        private void Start()
        {
            CalendarService.Instance.OnChangeTimeState += AllowTip;
            TimeState timeState = CalendarService.Instance.currtimeState; 
            AllowTip(timeState);
            allBuildSO = BuildingIntService.Instance.allBuildSO;
            tipBtnImg = GetComponent<Image>();
        }        
        void AllowTip(TimeState timeState)
        {
            tipState = TipState.Tipable;
            shipModel = BuildingIntService.Instance.shipController.shipModel;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!(tipState == TipState.Tipable)) return;
            Currency curr = new Currency(0, 3);
            EcoService.Instance.DebitMoneyFrmCurrentPocket(curr);
            ChgState(TipState.Tipped);
        }
        void ChgState(TipState tipState)
        {
            this.tipState = tipState;
            switch (tipState)
            {
                case TipState.None:
                    break;
                case TipState.Tipable:
                    displayStr = "Do you wanna throw in three bronze coins?";
                    tipBtnImg.sprite = allBuildSO.OnTipable;
                    break;
                case TipState.UnTipable:
                    displayStr = string.Empty;
                    tipBtnImg.sprite = allBuildSO.OnUnTipable;
                    break;
                case TipState.Tipped:
                    float chance = 100f;
                    if (chance.GetChance())
                    {  
                        displayStr = shipModel.GetTipTxt();
                        tipBtnImg.sprite = allBuildSO.OnUnTipable;
                    }
                    else
                    {
                        displayStr = string.Empty;
                    }
                    break;
                default:
                    break;
            }
            tiptxt.text = displayStr;

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (tipState != TipState.Tipable) return;
            tipBtnImg.sprite = allBuildSO.OnTipableHL;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (tipState != TipState.Tipable) return;
            tipBtnImg.sprite = allBuildSO.OnTipable;
        }
    }
}