using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using Common;

namespace Town
{
    public class ChestPtrEvents : HouseBaseEvents, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(houseView.stashPanel.gameObject, true);

        }
    }
}
