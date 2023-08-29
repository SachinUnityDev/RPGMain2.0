using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{
    public class MapEController : MonoBehaviour
    {
        public Action<MapENames, bool> OnMapEComplete; 


        [Header("Map E view")]
        [SerializeField] MapEView mapEView;
       // public MapENodePtrEvents mapENodePtrEvents; // ref to node on map that triggered the MapE 

        public List<MapEModel> allMapEModels = new List<MapEModel>();
        public List<MapEbase> allMapEBases= new List<MapEbase>();
        [SerializeField] int mapBaseCount = 0;

        MapEFactory mapEFactory; 

        void Awake()
        {
            mapEFactory= GetComponent<MapEFactory>();   
        }


        public void ShowMapE(MapENames mapEName)
        {
            MapEModel mapEModel = GetMapEModel(mapEName); 
            mapEView.GetComponent<MapEView>().InitEncounter(mapEModel);
        }

        public void On_MapEComplete(MapENames mapEName, bool isSuccess)
        {
            mapEView.GetComponent<IPanel>().UnLoad();    
            OnMapEComplete?.Invoke(mapEName, isSuccess);
        }
        public void InitMapE(AllMapESO allMapSO)
        {
            foreach (MapESO mapESO in allMapSO.allMapESO)
            {
                MapEModel mapEModel = new MapEModel(mapESO);
                allMapEModels.Add(mapEModel);
            }
            InitAllMapEBase();
        }

        void InitAllMapEBase()
        {
            foreach (MapEModel mapModel in allMapEModels)
            {
                MapEbase mapEBase = mapEFactory.GetMapEBase(mapModel.mapEName);
                mapEBase.MapEInit(mapModel);
                allMapEBases.Add(mapEBase);
            }
            mapBaseCount = allMapEBases.Count;
        }

        public MapEModel GetMapEModel(MapENames mapEName)
        {
           int index = allMapEModels.FindIndex(t => t.mapEName == mapEName);
            if(index != -1)
                return allMapEModels[index];
            else
                Debug.Log("Map E not found" + mapEName);
            return null;
        }

        public MapEbase GetMapEBase(MapENames mapEName)
        {
            int index = allMapEBases.FindIndex(t => t.mapEName == mapEName);
            if (index != -1)
                return allMapEBases[index];
            else
                Debug.Log("city encounterBase not found" + mapEName);
            return null;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                ShowMapE(MapENames.BuffaloStampede);
            }
        }


    }
}