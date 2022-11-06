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

    public class GameService : MonoSingletonGeneric<GameService>, ISaveableService
    {
        [Header("CONTROLLERS")]
        public GameController gameController; // centralised service
        public GameModeController gameModeController; // centralised service

        public GameModel gameModel;
        public bool isGameOn = false;
        [SerializeField] List<string> allGameJSONs = new List<string>();

        void Start()
        {
            gameController = GetComponent<GameController>();
            gameModeController = GetComponent<GameModeController>(); 
            Init(); 
        }

        public void Init()
        {
            gameModel = new GameModel(GameState.InTown, GameDifficulty.Easy, GameMode.Stealth
                        , LocationName.Nekkisari);


            GameEventService.Instance.On_TownEnter(LocationName.Nekkisari); 
            // Load scene say here we start with the town scene

            // Init all the services in the scene 
            // ensure all the service has save service "Isaveable integrated"



            // list of Common Service to init 
            // charService
            // beastiary service 
         

        }

        public void RestoreState()
        {
            string mydataPath = "/SAVE_SYSTEM/savedFiles/" + SaveService.Instance.slotSelect.ToString()
                     + "/Char/gameModels.txt";

            if (File.Exists(Application.dataPath + mydataPath))
            {
                Debug.Log("File found!");
                string str = File.ReadAllText(Application.dataPath + mydataPath);

                allGameJSONs = str.Split('|').ToList();

                foreach (string modelStr in allGameJSONs)
                {
                    Debug.Log($"CHAR: {modelStr}");
                    if (String.IsNullOrEmpty(modelStr)) continue; // eliminate blank string
                    GameModel gameModel = JsonUtility.FromJson<GameModel>(modelStr);
                   
                    Debug.Log(gameModel.gameState);
                }
            }
            else
            {
                Debug.Log("File Does not Exist");
            }

        }
        public void ClearState()
        {
            string mydataPath = "/SAVE_SYSTEM/savedFiles/" + SaveService.Instance.slotSelect.ToString()
             + "/Char/charModels.txt";
            File.WriteAllText(Application.dataPath + mydataPath, "");

        }
        public void SaveState()
        {            
            ClearState();
        }

     
        bool isPressedA = false;
        bool isPressedZ = false;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) && !isPressedA)
            {
                //isPressedA = true;
                //Init();
            }
            if (Input.GetKeyDown(KeyCode.Z) && !isPressedZ)
            {
                //isPressedZ = true; 
                //List<CharNames> char2beCreated = new List<CharNames>() {CharNames.Abbas_Skirmisher
                //    ,CharNames.Baran, CharNames.Cahyo, CharNames.Rayyan };

                //char2beCreated.ForEach(t => CharService.Instance.CreateCharsCtrl(t));
            }
        }


    }


}

