using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{

    public class ApothecaryPtrEvents : BuildBaseEvents, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.CraftPotion))
            {
                MarketView marketView = buildView as MarketView;
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(marketView.craftPotionPanel.gameObject, true);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.CraftPotion))
            {
                SetSpriteHL(); 
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.CraftPotion))
            {
                SetSpriteN();
            }
        }
    }
}