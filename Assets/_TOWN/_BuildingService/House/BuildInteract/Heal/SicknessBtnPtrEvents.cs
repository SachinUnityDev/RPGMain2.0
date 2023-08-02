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
        public void InitPtrEvents(HealView healView, SicknessView sicknessView, TempTraitBuffData tempTraitBuffData)
        {
            this.healView = healView;
            this.sicknessView = sicknessView;   
            this.tempTraitBuffData= tempTraitBuffData;
            Image ptrImg = transform.GetChild(0).GetComponent<Image>(); 
            sicknessView.ptrImgs.Add(ptrImg); 
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            healView.OnSicknessSelect(tempTraitBuffData.tempTraitName);
            sicknessView.SelectPtr(transform.GetSiblingIndex()); 
        }
    }
}