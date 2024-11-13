using Common;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Town;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Quest
{
    public class MapEController : MonoBehaviour
    {
        public Action<MapENames, bool> OnMapEComplete;

        public bool mapEOnDsply = false; 
        [Header("Map E view")]
        public MapEView mapEView;

        public List<MapEModel> allMapEModels = new List<MapEModel>();
        public List<MapEbase> allMapEBases= new List<MapEbase>();
        [SerializeField] int mapBaseCount = 0;

        MapEFactory mapEFactory;

        [Header("Inter Scene ref for Current mapEbase")]
        public MapEbase currMapEBase;  


        void Awake()
        {
            mapEFactory= GetComponent<MapEFactory>();   
        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "TOWN")
            {
                mapEView = FindObjectOfType<MapEView>(true);                
            }
        }

        public void ShowMapE(MapENames mapEName, PathModel pathModel)
        {
            if(mapEOnDsply)
                return;
            MapEModel mapEModel = GetMapEModel(mapEName); 
            mapEModel.isDsplyed = true; 
            mapEView.GetComponent<MapEView>().InitEncounter(mapEModel, pathModel);
            mapEOnDsply= true;
        }
        public MapEbase GetCurrentDsplyedMapEBase()
        {
           return allMapEBases.Find(t => t.mapEModel.isDsplyed);
        }
        public void DsplyResults() // called from the result MAPEBase
        {
            MapEModel mapEModel = currMapEBase.mapEModel; 
            mapEModel.isDsplyed = true;
            mapEView.GetComponent<MapEView>().LoadEncounterResult(currMapEBase);
            mapEOnDsply = true;
        }


        public void On_MapEComplete(MapENames mapEName, bool isSuccess)
        {
            mapEOnDsply = false;            
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
        public void InitMapE(List<MapEModel> allMapEModels)
        {
            this.allMapEModels = allMapEModels.DeepClone();
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

        public void SetMapEBaseAsCurrent(MapEbase mapEbase)
        {
            currMapEBase = mapEbase;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
               // ShowMapE(MapENames.BuffaloStampede);
            }
        }


    }
}