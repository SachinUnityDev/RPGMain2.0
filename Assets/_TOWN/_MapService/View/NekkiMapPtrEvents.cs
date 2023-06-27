using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Town
{
    public class NekkiMapPtrEvents : MonoBehaviour
    {
        public LocationName locationName;
        MapView mapView;
        Image btnImg;
        void Start()
        {
            btnImg = GetComponent<Image>();
            btnImg.alphaHitTestMinimumThreshold = 0.1f;
        }

        public void InitMapBtnEvents(MapView mapView)
        {
            this.mapView = mapView;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            mapView.LoadLocation(locationName);
        }
    }
}