using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using System.IO;
using System.Linq;

namespace Common
{
    public class BestiaryService : MonoSingletonGeneric<BestiaryService>, ISaveable 
    {
        [SerializeField] const int start_INT_FOR_BESTIARY_CHARID = 100; 
        
        [Header("Character Pos")]
        public Vector3 spawnPos = new Vector3(0, 0, 0);

        [Header("BestiarySO")]
        public List<CharacterSO> allBestiarySO = new List<CharacterSO>();

        [Header("CURRENT SELECTS")]
        public BestiaryViewController bestiaryViewController;
        public CharModel currbestiaryModel;
        public BestiaryController bestiaryController;
        public RaceType currSelectRace;

        [Header("Record of All Bestiary In the Game")]
        public List<CharModel> allRegBestiaryInGameModels = new List<CharModel>(); // on game INIT 

        [Header("Bestiary Currently in Combat/ Controllers active")]
        public List<CharController> allCurrBestiaryCtrl = new List<CharController>();

        public ServicePath servicePath => ServicePath.BestiaryService;

        void OnEnable()
        {
          currSelectRace = RaceType.None;
            if(GameService.Instance.currGameModel.gameScene == GameScene.InCombat)
            {
                bestiaryViewController = FindObjectOfType<BestiaryViewController>();
            }
        }
        private void OnDisable()
        {
            
        }
        public void Init()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {
                    allRegBestiaryInGameModels.Clear();
                    allCurrBestiaryCtrl.Clear();

                    // only bestiary met on Combat to be registered here
                    // Following is the code for testing purpose
                    //foreach (CharacterSO charSO in allBestiarySO)
                    //{
                    //    CharModel charModel = new CharModel(charSO);
                    //    allRegBestiaryInGameModels.Add(charModel);
                    //    // init all char controllers here
                    //}
                }
                else
                {
                    LoadState();// save all the files in the invetory
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }

        
           // CreateAllBestiaryCtrls();
         //   isNewGInitDone = true;
        }
        public void OnRaceSelect(RaceType raceType)
        {
            currSelectRace = raceType;
            bestiaryViewController.PopulateOnRaceSelect(raceType); 
        }
     
        public CharacterSO GetEnemySO(CharNames charName)
        {
            CharacterSO charSO = 
                    allBestiarySO.Find(t => t.charName == charName);
            return charSO;
        }

        public CharController SpawnBestiary1(CharNames charName)
        {

            CharacterSO charSO = GetEnemySO(charName);
            
            GameObject go = Instantiate(charSO.charPrefab, spawnPos, Quaternion.identity);
            CharController charController = go.AddComponent<CharController>();

            CharModel charModel = charController.InitController(charSO);
            CharService.Instance.allCharModels.Add(charModel);
            CharService.Instance.charsInPlayControllers.Add(charController);
            CharService.Instance.allCharInCombat.Add(charController);

            if(!allRegBestiaryInGameModels.Any(t=>t.charName == charName && t.charLvl == charModel.charLvl))
            {
                allRegBestiaryInGameModels.Add(charModel);  // models added                                        
            }
            // actual Spawn Control
            allCurrBestiaryCtrl.Add(charController); // ctrl added 
            

            charModel.availOfChar = AvailOfChar.Available;
            charModel.stateOfChar = StateOfChar.UnLocked; 
            charModel.charID = allRegBestiaryInGameModels.Count + start_INT_FOR_BESTIARY_CHARID + 1;
            //go.name += charModel.charID.ToString(); 
            return charController;
        }
        public CharController GetCharControllerWithName(CharNames enemyName)
        {
            int index = allCurrBestiaryCtrl.FindIndex(t=>t.charModel.charName == enemyName);
            if (index != -1)
                return allCurrBestiaryCtrl[index];
            else
                Debug.LogError("Enemy Controller not found !" + enemyName);
            return null;
        }

        public void SaveState()
        {
            if (allRegBestiaryInGameModels.Count <= 0)
            {
                Debug.Log("no Bestiary Registered uptill now in play"); return;
            }
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            ClearState();
            // save all char models

            foreach (CharModel charModel in allRegBestiaryInGameModels)
            {   
                string bestiaryModelJSON = JsonUtility.ToJson(charModel);                
                string fileName = path + charModel.charName.ToString() + ".txt";
                File.WriteAllText(fileName, bestiaryModelJSON);
            }
        }

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
                    CharModel charModel = JsonUtility.FromJson<CharModel>(contents);
                    allRegBestiaryInGameModels.Add(charModel);

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
