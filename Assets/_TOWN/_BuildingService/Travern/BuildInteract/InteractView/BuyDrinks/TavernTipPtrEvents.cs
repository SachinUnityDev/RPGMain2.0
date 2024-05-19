using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Common;
using Interactables;
using TMPro;

namespace Town
{
    public enum TipState
    {
        None, 
        Tipable, 
        UnTipable, 
        Tipped, 
    }

    public class TavernTipPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        // use  limited to once per timestate

        [Header("tip txt TBR")]
        [SerializeField] TextMeshProUGUI tiptxt;
        [Header("Tip btn Img")]
        [SerializeField] Image tipBtnImg; 


        [Header("Global Var")]
        [SerializeField] string displayStr;  
        [SerializeField] TipState tipState = TipState.Tipable;



        AllBuildSO allBuildSO; 

        private void Start()
        {
            CalendarService.Instance.OnChangeTimeState += AllowTip;
            allBuildSO = 
                BuildingIntService.Instance.allBuildSO; 
            tipBtnImg = GetComponent<Image>();  
        }

        void AllowTip(TimeState timeState)
        {
            tipState = TipState.Tipable;    
            
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!(tipState == TipState.Tipable)) return;
                Currency curr = new Currency(0, 6);
                EcoService.Instance.DebitPlayerInvThenStash(curr);
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
                    displayStr = "Do you wanna throw in a bronze sixer?";
                    tipBtnImg.sprite = allBuildSO.OnTipable;
                    break;
                case TipState.UnTipable:
                    displayStr= string.Empty;
                    tipBtnImg.sprite = allBuildSO.OnUnTipable; 
                    break;
                case TipState.Tipped:
                    float chance = 100f;
                    if (chance.GetChance())
                    {
                        int count = allBuildSO.allTipStrs.Count; 
                        int index = UnityEngine.Random.Range(0, count);
                        displayStr = allBuildSO.allTipStrs[index];
                        tipBtnImg.sprite = allBuildSO.OnUnTipable;
                        //tipState = TipState.UnTipable;
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