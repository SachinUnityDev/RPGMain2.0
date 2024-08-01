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
        public GameState gameState;

        [Header("Select Saveslot and profile")]
        public SaveSlot saveSlot;
        public ProfileSlot profileSlot;


        [SerializeField] List<string> allGameJSONs = new List<string>();
        public List<GameModel> allGameModel;
        public ServicePath servicePath => ServicePath.GameService;

        private void Start()
        {
            //SceneManager.LoadScene((int)SceneName.INTRO, LoadSceneMode.Additive);  
            Debug.Log("CORE SCENE STARTED");
            SceneMgmtController sceneMgmtController =  SceneMgmtService.Instance.sceneMgmtController;
            StartCoroutine(sceneMgmtController.LoadScene(SceneName.INTRO)); 
        }

        public GameModel GetCurrentGameModel(int slot)
        {
            int index = allGameModel.FindIndex(t => t.profileSlot == (ProfileSlot)slot); 
            if(index != -1)
            {
                return allGameModel[index];
            }
            return null; 
        }
        public GameModel GetCurrentGameModel()
        {
            int index = allGameModel.FindIndex(t => t.isCurrGameModel == true);
            if (index != -1)
            {
                return allGameModel[index];
            }
            return null;
        }
        public GameModel GetGameModel(ProfileSlot profileSlot, SaveSlot saveSlot)
        {
            int index = allGameModel.FindIndex(t => t.profileSlot == profileSlot && t.saveSlot == saveSlot);
            if (index != -1)
            {
                return allGameModel[index];
            }
            return null;
        }
        public void AddGameModel(GameModel gameModel)
        {
            int index = allGameModel.FindIndex(t => t.profileSlot == gameModel.profileSlot && t.saveSlot == gameModel.saveSlot);
            if (index != -1)
            {
                 allGameModel.RemoveAt(index);  
            }
            allGameModel.Add(gameModel);
        }

        void OnEnable()
        {  
            sceneController = GetComponent<SceneController>();
            gameController= GetComponent<GameController>();
            SceneManager.activeSceneChanged += UpdateGameScene; 
        }
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= UpdateGameScene;
        }
        void UpdateGameScene(Scene oldScene, Scene newScene)
        {
            int index = newScene.buildIndex;
            
            if (index == (int)SceneName.TOWN)
            {
                GameSceneLoad(GameScene.InTown);
                currGameModel.gameScene = GameScene.InTown;
            }
            else if (index == (int)SceneName.QUEST)
            {
                GameSceneLoad(GameScene.InQuestRoom);
                currGameModel.gameScene = GameScene.InQuestRoom;
            }
            else if (index == (int)SceneName.COMBAT)
            {
                GameSceneLoad(GameScene.InCombat);
                currGameModel.gameScene = GameScene.InCombat;
            }
            else if (index == (int)SceneName.INTRO)
            {
                GameSceneLoad(GameScene.InIntro);
                currGameModel.gameScene = GameScene.InIntro;
            }
        }
       

        public void Init()
        {
            gameController = transform.GetComponent<GameController>();
           // string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            allGameModel = new List<GameModel>();
         
            LoadState();
            PostLoadActions();
        }
        public void CreateNewGame(int profileslot, string profileStr)  // On Set profile Continue btn
        {
            currGameModel = new GameModel(profileslot, profileStr);
            currGameModel.gameDifficulty = GameDifficulty.Easy;
            currGameModel.locationName = LocationName.Nekkisari;
            profileSlot = (ProfileSlot)profileslot; 
            if (currGameModel.abbasClassType == ClassType.None)
            {
                currGameModel.abbasClassType = ClassType.Skirmisher;
            }
            gameController.InitDiffGameController(currGameModel.gameDifficulty);
            allGameModel.Add(currGameModel);
            // Init Services related to profile
            DialogueService.Instance.InitDialogueService();             
            GameEventService.Instance.Set_GameState(GameState.OnNewGameStart);
        }        

        // call this after profile slot and saveslot is selected    
        public void LoadGame(GameModel gameModel)
        {
            currGameModel = gameModel;            
            gameController.InitDiffGameController(currGameModel.gameDifficulty);
            GameEventService.Instance.OnGameSceneChg?.Invoke(currGameModel.gameScene);
            GameEventService.Instance.OnGameStateChg?.Invoke(GameState.OnLoadGameStart);
        }

        public void OnProfileSet(ProfileSlot profileSlot)
        {
            this.profileSlot = profileSlot;
            // in this profile find the autosave slot
           // 1. Chk if the profile is empty
           // loop thru all game models and find the profile slot
           GameModel gameModel = GetGameModel(profileSlot, SaveSlot.AutoSave);
            if (gameModel != null)
            {
                saveSlot = SaveSlot.AutoSave;
            }   
        }
        public void GameSceneLoad(GameScene gameScene)
        {
            currGameModel.gameScene= gameScene;            
            GameEventService.Instance.OnGameSceneChg?.Invoke(gameScene);
            if (gameScene == GameScene.InIntro)
                OnIntroSceneLoad(); 
            if (gameScene == GameScene.InCombat)
                GameEventService.Instance.On_CombatEnter(); 
        }
        void OnIntroSceneLoad()
        {           
             Init(); // game Service init
            //DialogueService.Instance.InitDialogueService();
            GameEventService.Instance.On_IntroLoaded();// event
        }
        #region SAVE AND LOAD 

        public void LoadState()
        {
            // loop thru all save slots and load them
            foreach (ProfileSlot profileSlot in Enum.GetValues(typeof(ProfileSlot)))
            {
                foreach (SaveSlot saveSlot in Enum.GetValues(typeof(SaveSlot)))
                {                   
                    string path = SaveService.Instance.GetServicePath(saveSlot,servicePath, profileSlot );                   
                    if (SaveService.Instance.DirectoryExists(path))
                    { // loop thru all profiles and load them
                        string[] fileNames = Directory.GetFiles(path);
                        foreach (string fileName in fileNames)
                        {
                            // skip meta files
                            if (fileName.Contains(".meta")) continue;
                            string contents = File.ReadAllText(fileName);
                            Debug.Log("  " + contents);
                            GameModel gameModel = JsonUtility.FromJson<GameModel>(contents);
                            allGameModel.Add(gameModel); // load all game models   
                        }
                        // profileslot and save slot are set by loadView or Continue btn in main Menu

                    }
                    else
                    {
                        Debug.LogError("Service Directory missing" + path);
                    }
                }
            }                
            GameModel currGameModel = GetGameModel(profileSlot, saveSlot);// this is the game Model
            LoadGame(currGameModel);
        }     
        
        public void DelAGameProfile(GameModel gameModel)
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            string filePath = path + gameModel.GetProfileName() + ".txt";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine("File deleted successfully." + filePath);
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }
        void PostLoadActions() 
        {
            GameObject panelGO = IntroServices.Instance.GetPanel("MainMenu");
            bool isContinueBtnOn = (allGameModel.Count > 0);
            panelGO.GetComponent<MainMenuController>().ToggleContinueBtn(isContinueBtnOn);
        }
        public void ClearState()
        {
            // no public clear state as vital game profile information is stored
        }
        void ClearState_Private(string dirPath)
        {            
            DeleteAllFilesInDirectory(dirPath);
        }
        void UpdateCurrentGame()
        {
            foreach (GameModel gameModel in allGameModel)
            {
                if (gameModel.profileSlot == currGameModel.profileSlot && gameModel.saveSlot == currGameModel.saveSlot)
                {
                    gameModel.isCurrGameModel = true;
                }
                else
                {
                    gameModel.isCurrGameModel = false;
                }                
            }
        }
        public void SaveState()
        {
            if (allGameModel.Count <= 0)
            {
                Debug.LogError("no GameModel created"); return;
            }           
            UpdateCurrentGame();
            foreach (GameModel gameModel in allGameModel)
            {
                string gameModelJSON = JsonUtility.ToJson(gameModel);
                string path = SaveService.Instance.GetServicePath(gameModel.saveSlot, servicePath, gameModel.profileSlot);
                if(Directory.Exists(path))
                {
                    ClearState_Private(path);
                    string fileName =  path + gameModel.GetProfileName() + ".txt";                   
                    File.WriteAllText(fileName, gameModelJSON);
                }
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
                // empty
            }
        }
        #endregion
    }


}
