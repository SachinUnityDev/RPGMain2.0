using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using Common;

namespace Town
{
    public class ChestPtrEvents : HouseBaseEvents, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.Chest))
            {
                HouseView houseView = buildView as HouseView;
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(houseView.stashPanel.gameObject, true);
            }

        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.Chest))
            {
                SetSpriteHL();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.Chest))
            {
                SetSpriteN();
            }
        }
    }
}
