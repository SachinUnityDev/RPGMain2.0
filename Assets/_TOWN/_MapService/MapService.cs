using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class MapService : MonoSingletonGeneric<MapService>
    {
        [Header("view")]
        public GameObject mapIntViewPanel;
        public PathView pathView;

        [Header("Controllers")]
        public PathController pathController;
       // public PathFactory pathFactory;
        public AllPathSO allPathSO; 



        [Header("Map Controllers")]
        public MapController mapController;


        [Header(" All Path Nodes")]
      
        public PathSO currPathSO;

        [Header("Game Init")]
        public bool isNewGInitDone = false;

        void Start()
        {
            pathController= GetComponent<PathController>();
          //  pathFactory = GetComponent<PathFactory>();
            mapController = GetComponent<MapController>();
        }
        
        public void InitMapService()
        {
            pathController = GetComponent<PathController>();
            mapController = GetComponent<MapController>();

            pathController.InitPath(allPathSO);
            pathView.PathViewInit(pathController);
            mapController.InitMapController();

            isNewGInitDone = true;
        }


    }
}



