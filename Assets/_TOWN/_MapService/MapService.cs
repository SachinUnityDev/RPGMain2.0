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
        public PathExpView pathExpView;

        [Header("Controllers")]
        public PathController pathController;
      //  public PathFactory pathFactory;

        [Header("Map Controllers")]
        public MapController mapController;


        [Header(" All Path Nodes")]
        public AllPathSO allPathSO;
        public PathSO currPathSO;

        void Start()
        {
            pathController= GetComponent<PathController>();
          //  pathFactory = GetComponent<PathFactory>();
            mapController = GetComponent<MapController>();
        }
        
        public void InitMapService()
        {          
            pathController.InitPath(allPathSO);
            pathExpView.PathExpInit();
            mapController.InitMapController();
        }


    }
}



