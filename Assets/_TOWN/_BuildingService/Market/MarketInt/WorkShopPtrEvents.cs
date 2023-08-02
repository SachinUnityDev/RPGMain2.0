using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{
    public class WorkShopPtrEvents : BuildBaseEvents, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.Fortify))
            {
                MarketView marketView = buildView as MarketView;
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(marketView.fortifyPanel.gameObject, true);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.Fortify))
            {
                SetSpriteHL(); 
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.Fortify))
            {
                SetSpriteN();
            }
        }
    }
}