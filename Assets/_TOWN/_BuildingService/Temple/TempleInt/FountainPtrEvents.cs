using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Town
{
    public class FountainPtrEvents : BuildBaseEvents, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler   
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.ClearMind))
            {
                TempleView templeView = buildView as TempleView;
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(templeView.clearMindPanel.gameObject, true);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.ClearMind))
            {
                SetSpriteHL(); 
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.ClearMind))
            {
                SetSpriteN(); 
            }
        }
    }
}