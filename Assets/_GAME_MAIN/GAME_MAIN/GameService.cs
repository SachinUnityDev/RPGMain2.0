using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Linq;
using Intro;
using Town;
using Combat;
using Interactables;


namespace Common
{

    public class GameService : MonoSingletonGeneric<GameService>, ISaveable
    {
        [Header("CONTROLLERS")]
        public GameController gameController; // centralised service
        public GameModeController gameModeController; // centralised service

        [Header("Scene Controller")]
        public SceneController sceneController;

        [Header("Global Var")]
        public GameModel currGameModel;
        public GameProgress gameProgress; 


        public bool isGameOn = false;
        [SerializeField] List<string> allGameJSONs = new List<string>();
        [SerializeField] List<GameModel> allGameModel = new List<GameModel>();


        public bool isNewGInitDone = false;
        public ServicePath servicePath => ServicePath.GameService;
        public GameModel GetGameModel(int slot)
        {
            int index = allGameModel.FindIndex(t => t.profileSlot == (ProfileSlot)slot); 
            if(index != -1)
            {
                return allGameModel[index];
            }
            return null; 
        }


        void OnEnable()
        {  
            sceneController = GetComponent<SceneController>();
            gameController= GetComponent<GameController>();
            SceneManager.sceneLoaded += OnSceneLoad; 
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoad;
        }

        void OnSceneLoad(Scene scene, LoadSceneMode loadMode)
        {   
            int index = scene.buildIndex;
            if(index == (int)SceneSeq.Town)
            {
                GameSceneLoad(GameScene.InTown); 
            }
            else if (index == (int)SceneSeq.Quest)
            {
                GameSceneLoad(GameScene.InQuestRoom);
            }
            else if (index == (int)SceneSeq.Combat)
            {
                GameSceneLoad(GameScene.InCombat);
            }
            else if (index == (int)SceneSeq.Intro)
            {
                GameSceneLoad(GameScene.InIntro);
            }
        }
        public void CreateNewGame(int profileId, string profileStr)  // On Set profile Continue btn
        {
            currGameModel = new GameModel(profileId, profileStr);
            currGameModel.gameDifficulty = GameDifficulty.Easy;
            currGameModel.locationName = LocationName.Nekkisari;
            gameController.InitDiffGameController(currGameModel.gameDifficulty);
            allGameModel.Add(currGameModel); 
        }
        
        public void GameSceneLoad(GameScene gameScene)
        {
            currGameModel.gameScene= gameScene;            
            GameEventService.Instance.OnGameSceneChg?.Invoke(gameScene);
            if (gameScene == GameScene.InIntro)
                OnIntroSceneLoad(); 
        }

        void OnIntroSceneLoad()
        {
            // game Start state
            LoadState(); 

        }


        #region SAVE AND LOAD 
        public void LoadState()
        {
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
                    GameModel gameModel = JsonUtility.FromJson<GameModel>(contents);
                    allGameModel.Add(gameModel);
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
            if (allGameModel.Count <= 0)
            {
                Debug.LogError("no GameModel created"); return;
            }
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            ClearState();

            foreach (GameModel gameModel in allGameModel)
            {
                string gameModelJSON = JsonUtility.ToJson(gameModel);               
                string fileName = path + gameModel.GetProfileName() + ".txt";
                File.WriteAllText(fileName, gameModelJSON);
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
        #endregion
    }


}


//string mydataPath = "/SAVE_SYSTEM/savedFiles/" + SaveService.Instance.slotSelected.ToString()
//         + "/Char/gameModels.txt";

//if (File.Exists(Application.dataPath + mydataPath))
//{
//    Debug.Log("File found!");
//    string str = File.ReadAllText(Application.dataPath + mydataPath);

//    allGameJSONs = str.Split('|').ToList();

//    foreach (string modelStr in allGameJSONs)
//    {
//        Debug.Log($"CHAR: {modelStr}");
//        if (String.IsNullOrEmpty(modelStr)) continue; // eliminate blank string
//        GameModel gameModel = JsonUtility.FromJson<GameModel>(modelStr);

//        Debug.Log(gameModel.gameScene);
//    }
//}
//else
//{
//    Debug.Log("File Does not Exist");
//}