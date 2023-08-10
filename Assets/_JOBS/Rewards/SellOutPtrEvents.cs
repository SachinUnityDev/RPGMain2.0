using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{
    public class SellOutPtrEvents : MonoBehaviour, IPointerClickHandler
    {
        WoodGameData woodGameData;
        WoodGameController1 woodController;
        WoodGameView1 woodGameView;
        public void Init(WoodGameData woodGameData, WoodGameController1 woodController, WoodGameView1 woodGameView)
        {
            this.woodGameView = woodGameView;
            this.woodGameData = woodGameData;
            this.woodController = woodController;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            woodGameView.OnQuickSell();
        }
    }
}