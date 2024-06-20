using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.SceneManagement;
using System.IO;
namespace Town
{
    public class TownService : MonoSingletonGeneric<TownService>, ISaveable
    {        
        public FameViewController fameController;    
       
        public TownModel townModel;
        public TownController townController;
        public TownViewController townViewController;

        [Header("BUILDING CONTROLLERS")]
        public TempleController templeController;

        [Header("TOWN LOCATION")]
        public BuildingNames selectBuildingName;

        public AllBuildSO allbuildSO;
        //[Header("Game Init")]
        //public bool isNewGInitDone = false;
        public ServicePath servicePath => ServicePath.TownService;
        void OnEnable()
        {
            townController = GetComponent<TownController>();
            fameController = GetComponent<FameViewController>();
            //templeController = buildingIntViewController.templePanel.GetComponent<TempleController>();
            SceneManager.sceneLoaded += OnSceneLoaded; 
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "TOWN")
            {
                townViewController=  FindObjectOfType<TownViewController>(true);

                TimeState timeState = CalendarService.Instance.calendarModel.currtimeState;
                townViewController.TownViewInit(timeState);
            }
        }
        public void Init()
        {

            string path = SaveService.Instance.GetCurrSlotServicePath(ServicePath.TownService);
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {
                    townModel = new TownModel(); // to be linke d to save Panels
                    SetTownNGetChars();
                }
                else
                {
                    LoadState();
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }

        
            
           //// townViewController.TownViewInit(CalendarService.Instance.currtimeState);
           // isNewGInitDone = true;
        }
        void SetTownNGetChars()
        {
            townModel.currTown = LocationName.Nekkisari;
            townController.allCharInTown
                = RosterService.Instance.rosterController.GetCharAvailableInTown(LocationName.Nekkisari);
        }

        public void LoadState()
        {
            // browse thru all files in the folder and load them
            // as char Models 
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);

            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    Debug.Log("  " + contents);
                    townModel = JsonUtility.FromJson<TownModel>(contents);      
                    SetTownNGetChars();
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }


        public void ClearState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            DeleteAllFilesInDirectory(path);
        }
        public void SaveState()
        {
            if (this.townModel == null)
            {
                Debug.LogError("no town model"); return;
            }
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            ClearState();
            // save Town model
           
            string townModelJSON = JsonUtility.ToJson(townModel);            
            string fileName = path + "townModel" + ".txt";
            File.WriteAllText(fileName, townModelJSON);

        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                SaveState();
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                LoadState();
            }
        }

    }


}

