using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{
    public class WorkShopPtrEvents : MarketBaseEvents, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(marketView.fortifyPanel.gameObject, true);
        }
    }
}