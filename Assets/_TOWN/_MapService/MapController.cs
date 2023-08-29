
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Town;

namespace Quest
{

   

    public class MapController : MonoBehaviour
    {
        public MapView mapView; 

      

        public void InitMapController()
        {
            mapView.InitMapView();
        }
    }
}