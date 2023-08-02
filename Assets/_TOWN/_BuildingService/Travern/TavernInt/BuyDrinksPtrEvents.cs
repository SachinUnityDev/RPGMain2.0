using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{


    public class BuyDrinksPtrEvents : TavernBaseEvents, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.BuyDrink))
            {
                TavernView tavernView = buildView as TavernView;
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(tavernView.buyDrink.gameObject, true);
            }
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (buildModel.IsBuildIntUnLocked(BuildInteractType.BuyDrink))
            {
                SetSpriteHL();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetSpriteN(); 
        }
    }
}