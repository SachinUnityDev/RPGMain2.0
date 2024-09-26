using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Town
{
    public class MapService : MonoSingletonGeneric<MapService>, ISaveable 
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


        public ServicePath servicePath => ServicePath.MapService; 

        void Start()
        {
            GetControllerRef();
        }
        
        public void InitMapService()
        {
            GetControllerRef(); 

            pathController.InitPath(allPathSO);
            pathView.PathViewInit(pathController);
            mapController.InitMapController();

        }
        void GetControllerRef() {
            pathController = GetComponent<PathController>();
            mapController = GetComponent<MapController>();
        }   
        public void SaveState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            ClearState();
            // save all char models

            foreach (PathModel pathModel in pathController.allPathModel)
            {
                string pathJSON = JsonUtility.ToJson(pathModel);
                string fileName = path + pathModel.questName.ToString()+"_"+ pathModel.objName.ToString() + ".txt";
                File.WriteAllText(fileName, pathJSON);
            }
        }

        public void LoadState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            GetControllerRef();  // controller references
            List<PathModel> allpathModel = new List<PathModel>();
            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    Debug.Log("pathModel" + contents);
                    PathModel pathModel = JsonUtility.FromJson<PathModel>(contents);
                    //ClassifyAndInitBuildModel(pathModel);
                    allpathModel.Add(pathModel);
                }
              //  pathController.Load(allpathModel); 
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }

        public void ClearState()
        {

        }
    }
}



