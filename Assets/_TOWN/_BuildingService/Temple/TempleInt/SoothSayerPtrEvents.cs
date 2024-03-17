using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{
    public class SoothSayerPtrEvents : BuildBaseEvents, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            TempleView  templeView= buildView as TempleView;
            templeView.TradePanel.GetComponent<TradeView>()
                        .InitTradeView(NPCNames.Minami, BuildingNames.Temple);
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