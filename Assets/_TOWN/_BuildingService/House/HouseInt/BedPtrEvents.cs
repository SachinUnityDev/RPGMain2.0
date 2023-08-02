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
    public class BedPtrEvents : HouseBaseEvents, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.EndDay))
            {
                HouseView houseView = buildView as HouseView;
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(houseView.restPanel.gameObject, true);
            }
            // open Rest Panel
            
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.EndDay))
            {
                SetSpriteHL();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.EndDay))
            {
                SetSpriteN();
            }
        }
    }


}


