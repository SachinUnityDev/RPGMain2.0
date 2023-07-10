using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common; 

namespace Town
{


    public class AdalbertoPtrEvents : ShipBasePtrEvents, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            ShipView shipView = buildView as ShipView; 
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(shipView.smugglePanel.gameObject, true);

        }
    }
}