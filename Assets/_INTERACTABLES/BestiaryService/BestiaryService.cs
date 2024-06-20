using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using Town;

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

        [Header("All Bestiary")]
        public List<CharModel> allRegBestiaryInGameModels = new List<CharModel>(); // on game INIT 
       // public List<CharController> allBestiaryInGame = new List<CharController>(); 

        [Header("UNLOCKED Bestiary")]
        public List<CharController> allCurrBestiaryCtrl = new List<CharController>();
       // public List<CharModel> allRegBestiaryModels = new List<CharModel>(); // Unlocks here when u meet them 

        // UNLOCKS => REGISTER after u meet a enemy in combat they register in scroll list
        [Header("Game Init")]
        public bool isNewGInitDone = false;

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

            allRegBestiaryInGameModels.Add(charModel);  // models added        
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
                Debug.LogError("no chars in play"); return;
            }
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            ClearState();
            // save all char models


            foreach (CharController charCtrl in allRegBestiaryInGameModels)
            {
                CharModel charModel = charCtrl.charModel;
                string charModelJSON = JsonUtility.ToJson(charModel);
                Debug.Log(charModelJSON);
                string fileName = path + charModel.charName.ToString() + ".txt";
                File.WriteAllText(fileName, charModelJSON);
            }
        }

        public void LoadState()
        {

        }

        public void ClearState()
        {

        }
    }
}
