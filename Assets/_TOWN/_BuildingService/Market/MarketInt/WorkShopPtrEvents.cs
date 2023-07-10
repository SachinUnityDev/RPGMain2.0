using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{
    public class WorkShopPtrEvents : BuildBaseEvents, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            MarketView marketView = buildView as MarketView;
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(marketView.fortifyPanel.gameObject, true);
        }
    }
}