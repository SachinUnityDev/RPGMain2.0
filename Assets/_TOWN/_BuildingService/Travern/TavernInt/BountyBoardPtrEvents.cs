using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{
    public class BountyBoardPtrEvents : TavernBaseEvents, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            TavernView tavernView = buildView as TavernView;
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(tavernView.bountyBoard.gameObject, true);
        }
    }
}