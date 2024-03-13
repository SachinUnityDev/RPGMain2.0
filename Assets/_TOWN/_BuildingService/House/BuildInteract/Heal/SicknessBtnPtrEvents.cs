using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Town
{
    public class SicknessBtnPtrEvents : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] HealView healView;
        [SerializeField] SicknessView sicknessView;
        [SerializeField] TempTraitBuffData tempTraitBuffData;
        [SerializeField] GameObject ptr;
        private void OnEnable()
        {
            ptr = transform.GetChild(0).gameObject;
            HidePtr();
        }
        public void InitPtrEvents(HealView healView, SicknessView sicknessView, TempTraitBuffData tempTraitBuffData)
        {
            this.healView = healView;
            this.sicknessView = sicknessView;   
            this.tempTraitBuffData= tempTraitBuffData;
           
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            healView.OnSicknessSelect(tempTraitBuffData.tempTraitName);
            sicknessView.SelectPtr(transform.GetSiblingIndex()); 
        }

        public void ShowPtr()
        {
            ptr.SetActive(true);
        }
        public void HidePtr()
        {
            ptr.SetActive(false);
        }
    }
}