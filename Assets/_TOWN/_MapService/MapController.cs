
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Town;
using Common;
using UnityEngine.SceneManagement;

namespace Quest
{



    public class MapController : MonoBehaviour
    {
        public MapView mapView;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void InitMapController()
        {
            mapView = FindObjectOfType<MapView>(true);
            mapView.InitMapView();
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "TOWN")
            {
                mapView = FindObjectOfType<MapView>(true);

                InitMapController();
            }

        }
    }
}