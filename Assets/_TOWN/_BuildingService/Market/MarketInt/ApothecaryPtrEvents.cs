using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{

    public class ApothecaryPtrEvents : BuildBaseEvents, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            // open Rest Panel
            MarketView marketView = buildView as MarketView;
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(marketView.craftPotionPanel.gameObject, true);
        }
    }
}