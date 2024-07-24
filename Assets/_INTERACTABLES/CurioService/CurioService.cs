using Common;
using Intro;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement; 

namespace Quest
{
    public class CurioService : MonoSingletonGeneric<CurioService>, ISaveable
    {
        public AllCurioSO allCurioSO;
        public CurioController curioController;
        [Header("Curio canvas View")]
        public CurioView curioView;

        [Header("Curio Factory")]
        public CurioFactory curioFactory;

        public List<CurioModel> allCurioModel = new List<CurioModel>();
        public List<CurioBase> allCurioBases = new List<CurioBase>();

        public ServicePath servicePath => ServicePath.CurioService; 

        private void OnEnable()
        {
            curioFactory = GetComponent<CurioFactory>();
            curioController= GetComponent<CurioController>();
            SceneManager.activeSceneChanged += OnSceneLoaded;
        }
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene oldScene, Scene newScene)
        {
            if (newScene.name == "QUEST")
            {
                curioView = FindObjectOfType<CurioView>(true);
            }
        }


        public void InitCurioService()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {
                    curioController.Init(allCurioSO);
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

        public void SaveState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            ClearState();
            foreach (CurioModel curioModel in curioController.allCurioModel)
            {
                string curioModelJSON = JsonUtility.ToJson(curioModel);
                Debug.Log(curioModelJSON);
                string fileName = path + curioModel.curioName.ToString() + ".txt";
                File.WriteAllText(fileName, curioModelJSON);
            }
        }

        public void LoadState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            
            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                List<CurioModel> allCurioModel = new List<CurioModel>();
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;

                    string contents = File.ReadAllText(fileName);
                    CurioModel curioModel = JsonUtility.FromJson<CurioModel>(contents);

                    allCurioModel.Add(curioModel);
                }
                curioController.InitModelOnLoad(allCurioModel); 
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


