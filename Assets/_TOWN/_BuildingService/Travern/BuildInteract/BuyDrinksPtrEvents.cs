using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{


    public class BuyDrinksPtrEvents : TavernBaseEvents, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(tavernView.buyDrink.gameObject, true);
        }
    }
}