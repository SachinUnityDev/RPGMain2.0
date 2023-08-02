using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{
    public class KamilaPtrEvents : MarketBaseEvents, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            MarketView marketView = buildView as MarketView;
            marketView.TradePanel.GetComponent<TradeView>()
                        .InitTradeView(NPCNames.Kamila, BuildingNames.Marketplace);
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