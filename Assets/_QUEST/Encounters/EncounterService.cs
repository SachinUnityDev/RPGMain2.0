using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

namespace Quest
{
    public class EncounterService : MonoSingletonGeneric<EncounterService>, ISaveable
    {
        [Header("City Encounter NTBR")]
        public CityEController cityEController;
        public CityEncounterFactory cityEFactory;

        [Header("City Encounter TBR")]
        public AllCityESO allCityESO;

        [Header("Map Encounter TBR")]       
        public AllMapESO allMapESO;

        [Header("Map Encounter NTBR")]
        public MapEController mapEController;
        public MapEFactory mapEFactory;

        [Header("Quest Encounter NTBR")]
        public QuestEFactory questEFactory;
        public QuestEController questEController;

        [Header("Quest Encounter TBR")]
        public AllQuestESO allQuestESO;

        public ServicePath servicePath => ServicePath.EncounterService;

        void Start()
        {     
            questEController= gameObject.GetComponent<QuestEController>();
            questEFactory= gameObject.GetComponent<QuestEFactory>();    
        }
        void GetRef()
        {
            cityEController = gameObject.GetComponent<CityEController>();
            mapEController = gameObject.GetComponent<MapEController>();
            questEController = gameObject.GetComponent<QuestEController>();
        }
        public void EncounterInit()
        {
            GetRef(); 
            cityEFactory = gameObject.GetComponent<CityEncounterFactory>();            

            mapEFactory = gameObject.GetComponent<MapEFactory>();
            
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path = path + "/QuestE/";
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {
                    cityEController.InitCityE(allCityESO);
                    mapEController.InitMapE(allMapESO);
                    questEController.InitQuestE(allQuestESO);
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
            if (Input.GetKeyDown(KeyCode.F6))
            {
                ClearState();
            }
        }

        public void SaveState()
        {
            SaveStateQE();
            SaveStateMapE();
            SaveStateCityE();
        }

        public void LoadState()
        {
            GetRef(); 
            LoadStateQE();
            LoadStateMapE();
            LoadStateCityE();
        }
        public void ClearStateQE()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "/QuestE/";
            DeleteAllFilesInDirectory(path);
        }
        public void SaveStateQE()
        {            
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "/QuestE/";
            // Check if the folder does not exist and create it if necessary
            if (!Directory.Exists(path))
                CreateAFolder(path); 

            ClearStateQE();
            foreach (QuestEModel questEModel in questEController.allQuestEModels)
            {                
                string questEModelJson = JsonUtility.ToJson(questEModel);
                Debug.Log(questEModelJson);
                string fileName = path + questEModel.questEName.ToString() + ".txt";
                File.WriteAllText(fileName, questEModelJson);
            }
        }

        public void SaveStateMapE()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "/MapE/";
            if (!Directory.Exists(path))
                CreateAFolder(path);
            ClearStateMapE();
            foreach (MapEModel mapEModel in mapEController.allMapEModels)
            {
                string mapEModelJson = JsonUtility.ToJson(mapEModel);
                Debug.Log(mapEModelJson);
                string fileName = path + mapEModel.mapEName.ToString() + ".txt";
                File.WriteAllText(fileName, mapEModelJson);
            }
        }
        public void ClearStateMapE()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "/MapE/";

            DeleteAllFilesInDirectory(path);
        }

        public void SaveStateCityE()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "/CityE/";
            if (!Directory.Exists(path))
                CreateAFolder(path);
            ClearStateCityE();
            foreach (CityEModel cityEModel in cityEController.allCityEModels)
            {
                string cityEModelJson = JsonUtility.ToJson(cityEModel);
                Debug.Log(cityEModelJson);
                string fileName = path + cityEModel.cityEName.ToString() + ".txt";
                File.WriteAllText(fileName, cityEModelJson);
            }
        }
        public void ClearStateCityE()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "/CityE/";
            DeleteAllFilesInDirectory(path);
        }

        void LoadStateQE()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            string questEPath = path + "/QuestE/";
       
            if (SaveService.Instance.DirectoryExists(questEPath))
            {
                string[] fileNames = Directory.GetFiles(questEPath);
                List<QuestEModel> allQuestEModels = new List<QuestEModel>();
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;

                    string contents = File.ReadAllText(fileName);
                    QuestEModel qEModel = JsonUtility.FromJson<QuestEModel>(contents);

                    allQuestEModels.Add(qEModel);
                }
                questEController.InitQuestE(allQuestEModels);
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }
        void LoadStateMapE()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            string mapEPath = path + "/MapE/";
            if (SaveService.Instance.DirectoryExists(mapEPath))
            {
                string[] fileNames = Directory.GetFiles(mapEPath);
                List<MapEModel> allMapEModel = new List<MapEModel>();
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;

                    string contents = File.ReadAllText(fileName);
                    MapEModel mapEModel = JsonUtility.FromJson<MapEModel>(contents);

                    allMapEModel.Add(mapEModel);
                }
                mapEController.InitMapE(allMapEModel);
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }

        void LoadStateCityE()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            string cityEPath = path + "/CityE/";
            if (SaveService.Instance.DirectoryExists(cityEPath))
            {
                string[] fileNames = Directory.GetFiles(cityEPath);
                List<CityEModel> allCityEModel = new List<CityEModel>();
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;

                    string contents = File.ReadAllText(fileName);
                    CityEModel cityEModel = JsonUtility.FromJson<CityEModel>(contents);

                    allCityEModel.Add(cityEModel);
                }
                cityEController.InitCityE(allCityEModel);
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }
        public void ClearState()
        {
            ClearStateCityE();
            ClearStateMapE();
            ClearStateQE();
        }
    }
}