using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;


namespace Common
{
    public class BestiaryService : MonoSingletonGeneric<BestiaryService>
    {
        [Header("Character Pos")]
        public Vector3 spawnPos = new Vector3(0, 0, 0);

        [Header("BestiarySO")]
        public List<CharacterSO> allBestiarySO = new List<CharacterSO>();

        [Header("CURRENT SELECTS")]
        public BestiaryViewController bestiaryViewController;
        public CharModel bestiaryModel;
        public BestiaryController bestiaryController;
        public RaceType currSelectRace;

        [Header("All Bestiary")]
        public List<CharModel> allModel4BestiaryInGame = new List<CharModel>(); // on game INIT 
        public List<CharController> allBestiaryInGame = new List<CharController>(); 

        [Header("UNLOCKED Bestiary")]
        public List<CharController> allRegBestiaryCtrl = new List<CharController>();
        public List<CharModel> allRegBestiaryModels = new List<CharModel>(); // Unlocks here when u meet them 
     
        // UNLOCKS => REGISTER after u meet a enemy in combat they register in scroll list
        void Start()
        {
          currSelectRace = RaceType.None;
        }

        public void Init()
        {
            foreach (CharacterSO charSO in allBestiarySO)
            {
                bestiaryModel = new CharModel(charSO);
                allModel4BestiaryInGame.Add(bestiaryModel);
                // init all char controllers here
            }
            CreateAllBestiaryCtrls();
        }

        public void OnRaceSelect(RaceType raceType)
        {
            currSelectRace = raceType;
            bestiaryViewController.PopulateOnRaceSelect(raceType); 
        }

        public void CreateAllBestiaryCtrls()
        {
            CharController charCtrl = new CharController();
            foreach (CharacterSO c in allBestiarySO)
            {
                charCtrl.InitiatizeController(c);            
                allBestiaryInGame.Add(charCtrl);
                CharService.Instance.charsInPlayControllers.Add(charCtrl);
                LevelService.Instance.LevelUpInit(charCtrl);
            }
        }

        public CharacterSO GetEnemySO(CharNames charName)
        {
            CharacterSO charSO = 
                    allBestiarySO.Find(t => t.charName == charName);
            return charSO;
        }

        public CharController SpawnBestiary(CharNames enemyName, int charID)
        {
            CharController charController = null;
            CharacterSO charSO = GetEnemySO(enemyName);
            if ( charID > (int)enemyName)
            {
                charController = CreateEnemyCtrl(enemyName);
                charController.charModel.charID = charID;

            }
            else
            {
                charController = GetCharControllerWithID(charID);
                CharNames charName = charController.charModel.charName;
               
                if (charSO == null)
                {
                    charSO = BestiaryService.Instance.GetEnemySO(charName);// this one is pet
                }
            }
            GameObject go = Instantiate(charSO.charPrefab, spawnPos, Quaternion.identity);
            go.AddComponent<CharController>();

            return charController;
        }

        public CharController GetCharControllerWithID(int charID)
        {
            CharController charController = allBestiaryInGame.Find(t=>t.charModel.charID == charID);
            if (charController != null)
                return charController;
            else
                Debug.LogError("Enemy Controller not found");
            return null;
        }
        public CharController CreateEnemyCtrl(CharNames charName)  // create for duplicate Chars 
        {
            CharController charCtrl = new CharController();
            foreach (CharacterSO c in allBestiarySO)
            {
                if (charName == c.charName)
                {
                    charCtrl.InitiatizeController(c);
                    allBestiaryInGame.Add(charCtrl);
                    CharService.Instance.charsInPlayControllers.Add(charCtrl);                    
                    LevelService.Instance.LevelUpInit(charCtrl);

                }
            }
            return charCtrl;
        }

    }


}

