using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common; 

namespace Town
{


    public class AdalbertoPtrEvents : ShipBasePtrEvents, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {            
            BuildingNames buildName = buildView.BuildingName;
            buildView.TradePanel.GetComponent<TradeView>()
                        .InitTradeView(NPCNames.Adalberto, buildName);
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