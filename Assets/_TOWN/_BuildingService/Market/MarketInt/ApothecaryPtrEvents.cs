using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{

    public class ApothecaryPtrEvents : MarketBaseEvents, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            // open Rest Panel

            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(marketView.craftPotionPanel.gameObject, true);
        }
    }
}