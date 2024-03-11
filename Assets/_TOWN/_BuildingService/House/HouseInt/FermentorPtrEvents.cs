using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{
    public class FermentorPtrEvents : HouseBaseEvents, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.Fermentation))
            {
                HouseView houseView = buildView as HouseView;
                Transform panel = houseView.GetBuildInteractPanel(BuildInteractType.Fermentation); 
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(panel.gameObject, true);
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.Fermentation))
            {
                SetSpriteHL();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.Fermentation))
            {
                SetSpriteN();
            }
        }
    }
}