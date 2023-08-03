using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{
    public class TavernKeeperPtrEvents : TavernBaseEvents, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler  
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            TavernView tavernView = buildView as TavernView; 
            if(tavernView != null )
            {
                tavernView.TradePanel.GetComponent<TradeView>()
                        .InitTradeView(NPCNames.Greybrow, BuildingNames.Tavern);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetSpriteHL(); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetSpriteN();
        }
    }
}