using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class MapController : MonoBehaviour
    {
        public MapView mapView; 

        void Start()
        {

        }

        public void InitMapController()
        {
            mapView.InitMapView();
        }
    }
}