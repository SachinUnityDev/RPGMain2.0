using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Town
{
    public class FountainPtrEvents : BuildBaseEvents, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            TempleView templeView = buildView as TempleView; 
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(templeView.clearMindPanel.gameObject, true);
        }
    }
}