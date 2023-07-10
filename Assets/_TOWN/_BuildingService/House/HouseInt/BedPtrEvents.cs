using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using Common;

namespace Town
{
    public class BedPtrEvents : HouseBaseEvents, IPointerClickHandler
    {

        public void OnPointerClick(PointerEventData eventData)
        {
            // open Rest Panel
            HouseView houseView = buildView as HouseView;
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(houseView.restPanel.gameObject, true);
        }
    }


}


