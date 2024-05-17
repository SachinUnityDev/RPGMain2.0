using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using Town;

namespace Common
{
    public class BestiaryService : MonoSingletonGeneric<BestiaryService>
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
        public List<CharModel> allModel4BestiaryInGame = new List<CharModel>(); // on game INIT 
        public List<CharController> allBestiaryInGame = new List<CharController>(); 

        [Header("UNLOCKED Bestiary")]
        public List<CharController> allRegBestiaryCtrl = new List<CharController>();
       // public List<CharModel> allRegBestiaryModels = new List<CharModel>(); // Unlocks here when u meet them 

        // UNLOCKS => REGISTER after u meet a enemy in combat they register in scroll list
        [Header("Game Init")]
        public bool isNewGInitDone = false;

      

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
            foreach (CharacterSO charSO in allBestiarySO)
            {
               CharModel charModel = new CharModel(charSO);
                allModel4BestiaryInGame.Add(charModel);
                // init all char controllers here
            }
           // CreateAllBestiaryCtrls();
            isNewGInitDone = true;
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

          //  CharService.Instance.charsInPlay.Add(go);
        
            allRegBestiaryCtrl.Add(charController);

            // update char Level too here depending on ally levels FORMULA
            // find the model and update  its level here 
           // allRegBestiaryModels.Add(charModel);
            
            charModel.availOfChar = AvailOfChar.Available;
            charModel.stateOfChar = StateOfChar.UnLocked; 
            charModel.charID = allRegBestiaryCtrl.Count + start_INT_FOR_BESTIARY_CHARID + 1;
            //go.name += charModel.charID.ToString(); 
            
            return charController;
        }
        public CharController GetCharControllerWithName(CharNames enemyName)
        {
            int index = allBestiaryInGame.FindIndex(t=>t.charModel.charName == enemyName);
            if (index != -1)
                return allBestiaryInGame[index];
            else
                Debug.LogError("Enemy Controller not found !" + enemyName);
            return null;
        }

    }


}

//public void CreateAllBestiaryCtrls()
//{
//    CharController charCtrl = new CharController();
//    foreach (CharacterSO c in allBestiarySO)
//    {
//        charCtrl.InitiatizeController(c);            
//        allBestiaryInGame.Add(charCtrl);
//        CharService.Instance.charsInPlayControllers.Add(charCtrl);
//       // LevelService.Instance.LevelUpInitAlly(charCtrl);
//    }
//}

//public CharController CreateEnemyCtrl(CharNames charName)  // create for duplicate Chars 
//{
//    CharController charCtrl = new CharController();
//    foreach (CharacterSO c in allBestiarySO)
//    {
//        if (charName == c.charName)
//        {
//            charCtrl.charModel =  charCtrl.InitiatizeController(c);
//            allBestiaryInGame.Add(charCtrl);
//            CharService.Instance.charsInPlayControllers.Add(charCtrl);

//            //LevelService.Instance.LevelUpInitBeastiary(charCtrl);
//        }
//    }
//    return charCtrl;
//}



//public CharController SpawnBestiary(CharNames enemyName)
//    //, int charID)
//{
//    CharController charController = null;
//    CharacterSO charSO = GetEnemySO(enemyName);
//    if ( charSO.orgCharMode == CharMode.Enemy)
//    {
//        charController = CreateEnemyCtrl(enemyName);                
//    }
//    else
//    {
//        charController = GetCharControllerWithName(enemyName);
//        CharNames charName = charController.charModel.charName;

//        if (charSO == null)
//        {
//            charSO = GetEnemySO(charName);// this one is pet
//        }
//    }            
//    GameObject go = Instantiate(charSO.charPrefab, spawnPos, Quaternion.identity);
//    go.AddComponent<CharController>();
//    CharService.Instance.charsInPlay.Add(go);
//    return charController;
//}