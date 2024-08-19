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
        [Header(" START DAY OF NEW GAME")]
        public int START_DAY_OF_GAME = 24;
        public  MonthName START_MONTH_OF_THE_GAME = MonthName.FeatherOfThePeafowl;

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
            SceneName sceneName = (SceneName)index;
            // covert scene name to game scene
            GameScene gameScene = GameScene.None;

            switch (sceneName)
            {
                case SceneName.INTRO:
                    gameScene = GameScene.InIntro;
                    break;
                case SceneName.TOWN:
                    gameScene = GameScene.InTown; 
                    break;
                case SceneName.QUEST:
                    gameScene = GameScene.InQuestRoom;
                    break;
                case SceneName.COMBAT:
                    gameScene = GameScene.InCombat;
                    break;
                case SceneName.CORE:
                    gameScene = GameScene.InCore;
                    break;
                default:
                    gameScene = GameScene.None;
                    break;
            }
            //if (sceneName== (int)SceneName.TOWN)
            //    gameScene = GameScene.InTown;
            //if ((GameScene)index == GameScene.InCombat)
            //    gameScene = GameScene.InCombat;
            //if ((GameScene)index == GameScene.InQuestRoom)
            //    gameScene = GameScene.InQuestRoom;
            //if ((GameScene)index == GameScene.InIntro)
            //    gameScene = GameScene.InIntro;
            //if ((GameScene)index == GameScene.InCore)
            //    gameScene = GameScene.InCore;

            currGameModel.gameScene = gameScene;
            GameSceneLoad(gameScene);
        }
        public void Init()
        {
            gameController = transform.GetComponent<GameController>();
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
            saveSlot = gameModel.saveSlot;
            profileSlot = gameModel.profileSlot;
            gameController.InitDiffGameController(currGameModel.gameDifficulty);
            GameEventService.Instance.Set_GameState(GameState.OnLoadGameStart);            
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

        public void SetProfileNSaveSlot(ProfileSlot profileSlot, SaveSlot saveSlot)
        {
            this.saveSlot = saveSlot;
            this.profileSlot = profileSlot;
        }
        public void GameSceneLoad(GameScene gameScene)
        {
            if (gameScene == GameScene.InCore)
                GameEventService.Instance.On_CoreSceneLoaded();
            if (gameScene == GameScene.InIntro)
                GameEventService.Instance.On_IntroLoaded(); 
            if (gameScene == GameScene.InCombat)
                GameEventService.Instance.On_CombatEnter();
            if (gameScene == GameScene.InTown)
                GameEventService.Instance.OnTownLoaded();
            currGameModel.gameScene = gameScene;
            GameEventService.Instance.OnGameSceneChg?.Invoke(gameScene);
        }

        #region SAVE AND LOAD 

        public void LoadState()
        {
            // loop thru all save slots and load them
            foreach (ProfileSlot profileSlot in Enum.GetValues(typeof(ProfileSlot)))
            {
                foreach (SaveSlot saveSlot in Enum.GetValues(typeof(SaveSlot)))
                {                   
                    string path = SaveService.Instance.GetServicePath(saveSlot,servicePath, profileSlot);                   
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
                        // profileslot and save slot are set by LoadGame()

                    }
                    else
                    {
                        Debug.LogError("Service Directory missing" + path);
                    }
                }
            }                
            GameModel currGameModel = GetGameModel(profileSlot, saveSlot);// this is the game Model
           //  LoadGame(currGameModel);
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
            ClearState_Private(SaveService.Instance.GetCurrSlotServicePath(servicePath));   
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
