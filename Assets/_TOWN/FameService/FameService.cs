using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using Town;

namespace Common
{
    public class FameService : MonoSingletonGeneric<FameService>, ISaveable
    {
        public FameSO fameSO;
        public FameController fameController;
        public FameViewController fameViewController;

        public event Action <int> OnFameYieldChg;
        public event Action<int> OnFameChg;

        public ServicePath servicePath => ServicePath.FameService;
        void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "TOWN")
            {
                fameViewController = FindObjectOfType<FameViewController>(true);
            }
        }


        public void InitOnLoad(FameModel fameModel)
        {
            fameController = gameObject.GetComponent<FameController>();

            if (fameController.fameModel == null)
                fameController.fameModel = new FameModel();
            fameController.fameModel = fameModel.DeepClone();
        }

        public void Init()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {                 
                    fameController = gameObject.GetComponent<FameController>();
                    fameController.InitFameController(fameSO);
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
        public FameType GetFameType()
        {
            float currentFame = FameService.Instance.GetFameValue();
            if (currentFame >= 30 && currentFame < 60) return FameType.Respectable;
            else if (currentFame >= 60 && currentFame < 120) return FameType.Honorable;
            else if (currentFame >= 120) return FameType.Hero;
            else if (currentFame > -60 && currentFame <= -30) return FameType.Despicable;
            else if (currentFame > -120 && currentFame <= -60) return FameType.Notorious;
            else if (currentFame <= -120) return FameType.Villain;
            else if (currentFame > -30 && currentFame < 30) return FameType.Unknown;
            else return FameType.None;
        }
        public int GetFameValue()
        {
            return (int)fameController.fameModel.fameVal;
        }
        public int GetFameYieldValue()
        {
            return (int)fameController.fameModel.fameYield;          
        }

        public void On_FameChg(int val)
        {
            OnFameChg?.Invoke(val); 
        }
        public void On_FameYieldChg(int val)
        {
            OnFameYieldChg?.Invoke(val);
        }
        public List<FameChgData> GetFameChgList()
        {
            return fameController.fameModel.allFameData;        
        }
        public void LoadState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);

            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                FameModel fameModel = new FameModel();
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                   fameModel = JsonUtility.FromJson<FameModel>(contents);

                }
                InitOnLoad(fameModel);
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
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            ClearState();             
            string fameModelJSON = JsonUtility.ToJson(fameController.fameModel);
            string fileName = path + "FameModel" + ".txt";
            File.WriteAllText(fileName, fameModelJSON);
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
    }




}
