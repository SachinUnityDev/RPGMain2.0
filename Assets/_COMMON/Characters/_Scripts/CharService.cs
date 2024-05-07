using Combat;
using Interactables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Town; 
namespace Common
{
    public class CharService : MonoSingletonGeneric<CharService>, ISaveable
    {
        public event Action<CharController> OnCharDeath;
        public event Action<CharController> OnCharSpawn;
        public event Action<CharController> OnCharAddedToParty;
        public event Action OnPartyLocked; 
        public event Action OnPartyDisbanded;

        public event Action<CharController> OnCharFleeCombat;
        public event Action<CharController> OnCharFleeQuest;

        public bool isPartyLocked = false; 

        [Header("Character SO")]
        public AllCharSO allCharSO; 
        public List<CharacterSO> allAllySO = new List<CharacterSO>();
        public AllNPCSO allNpcSO;
        public FleeChancesSO fleeChancesSO;

        [Header("Load Strings ")]
        public List<string> allCharsJSONs = new List<string>();

        [Header("Sopporting SO")]
        public StatsVsChanceSO statChanceSO;
        public LvlNExpSO lvlNExpSO;
        public CharComplimentarySO charComplimentarySO;

        [Header("ALL Loaded char models")]
        public List <CharModel> allCharModels = new List<CharModel>();// to be populated by charCtrl
        public CharMainModel charMainModel; 

        [Header("GO list")]
        public List<GameObject> allyInPlay;       
        public List<GameObject> charsInPlay; // 

       // only for Quest  and Combat
        
        [Header("represents all the chars in the game")]
        // all Char In Game this service + bestiary 
        public List<CharController> charsInPlayControllers;

        [Header("Specific Sub list of Chars/ Allies")]
        public List<CharController> allyInPlayControllers = new List<CharController>();
        public List<CharModel> allyUnLockedCompModels= new List<CharModel>();// char Unlocked in the game
        public List<CharController> allAvailComp = new List<CharController>();


        [Header("Char List Related to Combat")]
        public List<CharController> allCharsInPartyLocked = new List<CharController>(); // on party Locked and Set 
        public List<CharController> allCharInCombat =new List<CharController>(); 
        public List<CharController> charDiedinLastTurn = new List<CharController>();

        [Header(" Char in Graveyard")]
        public List<CharController> charInGraveyard = new List<CharController>();   
        [Header(" Fled list")]
        public List<CharController> allCharfledQ = new List<CharController>();

        [Header("Character Pos")]
        public Vector3 spawnPos = new Vector3(-100, 0, 0);

        [Header(" Game Init ")]
        public bool isNewGInitDone = false;

        void Start()
        {
            //lastAllyCharID = 0;       
            isPartyLocked= false;
            CombatEventService.Instance.OnEOT += UpdateOnDeath;       
            DontDestroyOnLoad(this.gameObject);
        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnEOT -= UpdateOnDeath;
        }



        

        public void Init()  // on Scene enter 
        {
            // get all so and Popyulate the list of controllers
            // if save slot is defined take from save slot other SO from here pass in charSO    

            //  if(SaveService.Instance.)
            List<CharNames> chars2Spawn = new List<CharNames> { CharNames.Abbas
                                 , CharNames.Baran, CharNames.Rayyan, CharNames.Cahyo };
            charMainModel= new CharMainModel(); 
            foreach (CharNames charName in chars2Spawn)
            {
                SpawnCompanions(charName);
            }
            SetAbbasClassOnQuickStart();
            //CharController abbas = charsInPlayControllers.Find(t => t.charModel.charName == CharNames.Abbas);
            //foreach (CharController charCtrl in allyInPlayControllers)
            //{
            //    On_CharInit();
            //    On_CharAddToParty(GetCharCtrlWithName(charCtrl.charModel.charName));
            //}

            isNewGInitDone = true;

            //CreateAllAlliesCtrls();
        }

        #region GETTERS
        public CharacterSO GetCharSO( CharNames charName)
        {            
            return allCharSO.GetCharSO(charName);                
        }
        public CharacterSO GetCharSO(CharModel charModel)
        {
            CharNames charName = charModel.charName;
            CharacterSO charSO = allAllySO.Find(x => x.charName == charName);
            return charSO;
        }
        public CharacterSO GetAllySO(CharNames _charName)
        {           
            CharacterSO charSO = allAllySO.Find(x => x.charName == _charName);
            return charSO;
        }      
        public List<CharController> GetAllAvailChars()
        {
            List<CharController> availChars = new List<CharController>();
            availChars = allyInPlayControllers.Where(t=>(t.charModel.availOfChar == AvailOfChar.Available ||
                                   t.charModel.availOfChar == AvailOfChar.UnAvailable_Fame  ||
                                   t.charModel.availOfChar == AvailOfChar.UnAvailable_InParty ||
                                   t.charModel.availOfChar == AvailOfChar.UnAvailable_Prereq) 
                                    && t.charModel.stateOfChar == StateOfChar.UnLocked
                                   ).ToList();
            return availChars;
        }
        public CharController GetCharCtrlWithCharID(int  _charID)
        {
            CharController charCtrl = charsInPlayControllers.Find(x => x.charModel.charID == _charID ); 
            return charCtrl; 
        }
        public CharController GetAbbasController(CharNames charName)
        {
            CharController charCtrl = charsInPlayControllers.Find(x => x.charModel.charName == charName);
            if (charCtrl != null)
                return charCtrl;
            else
                Debug.Log("Abbas CharController not found" + charName);
            return null;
        }
        #endregion

        #region LOCK and UNLOCK and FLEE

        public void On_PartyLocked()
        {
            isPartyLocked= true;
            OnPartyLocked?.Invoke(); 
        }
        public void On_PartyDisbanded()
        {
            isPartyLocked = false;
            OnPartyDisbanded?.Invoke();
        }

        public void On_CharFleeCombat(CharController charController)
        {
            ToggleViewForChar(charController, false); 
            allCharInCombat.Remove(charController);
            OnCharFleeCombat?.Invoke(charController); 
        }

        public void On_CharFleeQuest(CharController charController)
        {
            ToggleViewForChar(charController, false);
            allCharsInPartyLocked.Remove(charController);
            charController.charModel.stateOfChar = StateOfChar.Fled;
            charController.fleeController.InitOnDayFledQ(2); // fled for 2 days 
            allCharfledQ.Add(charController);
            OnCharFleeQuest?.Invoke(charController);
        }
        #endregion

        #region CHAR SPWAN AND PARTY LOCK UNLOCK
        public CharController SpawnCompanions(CharNames charName)  // character factory 
        {
           // CharController charController = GetCharCtrlWithName(charName);
            
            CharacterSO charSO = GetAllySO(charName); 
            if(charSO == null)
            {
                charSO = BestiaryService.Instance.GetEnemySO(charName);// this one is pet
            }
           
            GameObject go = Instantiate(charSO.charPrefab, spawnPos, Quaternion.identity);
            CharController charController = go.AddComponent<CharController>();
          
             CharModel charModel = charController.InitiatizeController(charSO);

            if (charController.charModel.charName == CharNames.Abbas)
                AbbasStatusUpdate(charController); 

            charsInPlayControllers.Add(charController);
            allyInPlayControllers.Add(charController);
            LevelService.Instance.LevelUpInitAlly(charController); 
            charsInPlay.Add(go);
            allyInPlay.Add(go);
            allCharModels.Add(charModel);

            if (charModel.charName != CharNames.Abbas)
            {
                if (charModel.availOfChar == AvailOfChar.Available)
                {
                    allAvailComp.Add(charController);
                }
                if (charModel.stateOfChar == StateOfChar.UnLocked)
                {
                    allyUnLockedCompModels.Add(charModel);
                }
            }
            On_CharSpawn(charController); 
            return charController; 
        }

        public void UnLockChar(CharNames charName)
        {
            CharController charController  = allyInPlayControllers.Find(t=>t.charModel.charName == charName);
            charController.charModel.stateOfChar = StateOfChar.UnLocked;
            RosterService.Instance.rosterController.GetCharUnlockedWithStatusUpdated(); // updates the availofChar
        }

        void AbbasStatusUpdate(CharController charController)
        {
            On_CharAddToParty(charController);            
        }
        public void On_CharSpawn(CharController charController)
        {
            //foreach (var charController in charsInPlayControllers.ToList()) 
            //{
            //    if(charController != null)
                    OnCharSpawn?.Invoke(charController);
                //else
                //    charsInPlayControllers.Remove(charController);
          //  }
        
        }
    
        public void On_CharAddToParty(CharController charController)
        {
            if (allCharsInPartyLocked.Any(t => t.charModel.charID == charController.charModel.charID)) return;
            Debug.Log("On Char Added" + charController.charModel.charName);                    
            allCharsInPartyLocked.Add(charController);    
            OnCharAddedToParty?.Invoke(charController);
        }

        #endregion

        public List<int> ApplyBuffOnPartyExceptSelf(CauseType causeType, int causeName, int causeByCharID,
                                  AttribName statName, int value, TimeFrame timeFrame, int netTime, bool isBuff, CharMode charMode)
        {
            List<int> grpBuffIDs = new List<int>();
            foreach (CharController charController in CharService.Instance.allCharsInPartyLocked)
            {
                if (charController.charModel.charMode == charMode && charController.charModel.charID != causeByCharID)
                {
                    int buffID =
                    charController.buffController.ApplyBuff(causeType, causeName, causeByCharID
                                                  , statName, value, timeFrame, netTime, isBuff);
                    grpBuffIDs.Add(buffID);
                }
            }
            return grpBuffIDs;
        }
        #region SAVE AND LOAD 




        public void LoadCharServiceData(CharModel charModel)
        {
            //CharController charCtrl = null;
            //foreach (CharacterSO c in allCharSO)
            //{
            //    if (charModel.charName == c.charName)
            //    {
            //        GameObject go = Instantiate(c.charPrefab, spawnPos, Quaternion.identity);
            //        charCtrl = go.AddComponent<CharController>();

            //       // charCtrl.InitiatizeController(c, charModel, SaveService.Instance.slotSelect);// to be build up 

            //        CharMode charMode = charCtrl.charModel.charMode;
            //        if (charMode == CharMode.Ally)
            //        {
            //            allyInCombatControllers.Add(charCtrl);
            //            allyInPlay.Add(go);
            //        }
            //        if (charMode == CharMode.Enemy)
            //        {
            //            enemyInPlayControllers.Add(charCtrl);
            //            enemyInPlay.Add(go);
            //           // CombatService.Instance.enemyInCombat.Add(charCtrl);
            //        }
            //        CharsInPlayControllers.Add(charCtrl);
            //        charsInPlay.Add(go);
            //    }
            //}
        }

        public void RestoreState(string basePath)
        {
            allCharModels.Clear();
            string mydataPath = basePath + "/Char/charModels.txt";

            if (File.Exists(Application.dataPath + mydataPath))
            {                
                string str = File.ReadAllText(Application.dataPath + mydataPath);

                 allCharsJSONs = str.Split('|').ToList();

                foreach (string modelStr in allCharsJSONs)
                {
                    Debug.Log($"CHAR: {modelStr}");                   
                    if (String.IsNullOrEmpty(modelStr)) continue; // eliminate blank string
                      CharModel charModel = JsonUtility.FromJson<CharModel>(modelStr);
                    allCharModels.Add(charModel);
                   // LoadCharControllers(charModel);
                    Debug.Log(charModel.charName);    
                }
            }
            else
            {
                Debug.Log("Char Service SAVE FILE Does not Exist");
            }
        }

        public CharModel LoadCharModel(CharNames charName)
        {
            CharModel charModel = allCharModels.Find(t => t.charName == charName);
            if (charModel != null)
                return charModel;
            else
                Debug.LogError("Char model not loaded");
            return null; 

        }
        public void ClearState()
        {
            string mydataPath = "/SAVE_SYSTEM/savedFiles/" + SaveService.Instance.slotSelected.ToString()
             + "/Char/charModels.txt";
            File.WriteAllText(Application.dataPath + mydataPath, "");

        }
        public void SaveState()
        {
            if (charsInPlayControllers.Count <= 0)
            {
                Debug.Log("no chars in play");  return;
            }
            ClearState();
            foreach (CharController charCtrl in charsInPlayControllers)
            {
                charCtrl.charModel.SaveModel(); 
            }    
        }

        #endregion

        #region TOGGLE COLLIDERS 

        public void ToggleCharColliders(GameObject targetGO, bool turnON)
        {

            CharController targetController = targetGO.GetComponent<CharController>();

            foreach (CharController charController in allCharInCombat)
            {
                if(targetController != charController)
                {
                    //Collider collider = charController.GetComponent<Collider>();
                    //collider.enabled = turnON;
                    Collider[] allColliders = charController.gameObject.GetComponentsInChildren<Collider>();
                    for (int i = 0; i < allColliders.Length; i++)
                    {
                        allColliders[i].enabled = turnON;
                    }
                }else
                {
                    //Collider collider = charController.GetComponent<Collider>();
                    //collider.enabled = true;
                    BoxCollider2D[] allColliders = charController.gameObject.GetComponentsInChildren<BoxCollider2D>();
                    for (int i = 0; i < allColliders.Length; i++)
                    {
                        allColliders[i].enabled = true;
                    }
                }                
            }
        }

        public void TurnOnAllCharColliders()
        {
            foreach (CharController charController in charsInPlayControllers)
            {             
                    Collider[] allColliders = charController.gameObject.GetComponentsInChildren<Collider>();
                    for (int i = 0; i < allColliders.Length; i++)
                    {
                        allColliders[i].enabled = true;
                    }               
            }


        }

        public void ToggleViewForChar(CharController charController,   bool showChar)
        {
            charController.transform.GetChild(0).gameObject.SetActive(showChar);
            charController.transform.GetChild(2).gameObject.SetActive(showChar);
        }


        #endregion

        #region GETTER
        public CharController HasHighestStat(StatName _statNames, CharMode _charMode)
        {
            float maxValue = 0;
            CharController maxValChar = null; 
            foreach (CharController c in charsInPlayControllers)
            {
                if(c.charModel.charMode == _charMode)
                {
                    float currVal = c.GetStat(_statNames).currValue; 
                    if (currVal > maxValue)
                    {
                        maxValue = currVal;
                        maxValChar = c; 
                    }
                }
            }
            return maxValChar; 
        }

        public CharController HasLowestStat(AttribName _statNames, CharMode _charMode)
        {
            float minValue = 0;
            CharController minValChar = null;
            foreach (CharController c in charsInPlayControllers)
            {
                if (c.charModel.charMode == _charMode)
                {
                    float currVal = c.GetAttrib(_statNames).currValue;
                    if (currVal < minValue)
                    {
                        minValue = currVal;
                        minValChar = c;
                    }
                }
            }
            return minValChar;
        }

        public int GetLevel(CharNames _charName)
        { 
           return charsInPlayControllers.Find(t => t.charModel.charName == _charName).charModel.charLvl;          
        }

        #endregion

        #region CHAR DEATH 
        public void UpdateOnDeath()
        {
            if (charDiedinLastTurn.Count < 1) return;
            foreach (CharController charCtrl in charDiedinLastTurn.ToList())
            {
                GridService.Instance.UpdateGridOnCharDeath(charCtrl);
                CombatService.Instance.roundController.ReorderAfterCharDeathOnEOT(charCtrl);
            }
            charInGraveyard.AddRange(charDiedinLastTurn);
            charDiedinLastTurn.Clear();
        }
        public void On_CharDeath(CharController _charController, int causeByCharID)
        {
            if (GameService.Instance.currGameModel.gameScene != GameScene.InCombat) return;
            
            _charController.charModel.stateOfChar = StateOfChar.Dead; 
            _charController.gameObject.GetComponent<BoxCollider2D>().enabled= false;
            // grid service Unoccupied to be added
           // allCharInCombat.Remove(_charController); // rest of the list are update on EOT
            OnCharDeath?.Invoke(_charController);

            charDiedinLastTurn.Add(_charController);
            if (_charController.charModel.charName == CharNames.Abbas)
            {
                CombatService.Instance.OnCombatResult(CombatResult.Defeat, CombatEndCondition.Defeat_AbbasDied); 
            }
        }
        #endregion 

        #region GET NAME STRINTGS
        public string GetCharName(CharNames charName)
        {
            CharacterSO charSO = CharService.Instance.allCharSO.GetAllySO(charName);
            if (charSO != null)
                return charSO.charNameStr;
            else
                return "";
        }

        public string GetNPCName(NPCNames npcName)
        {
            NPCSO npcSO = CharService.Instance.allNpcSO.GetNPCSO(npcName);
            if (npcSO != null)
                return npcSO.npcNameStr;
            else
                return "";
        }
        #endregion

        #region  CHKS
        public bool ChkIfSOLO(CharController charController)
        {            
            int charCount = 0;
            CharController charChtrl1 = null;
            foreach (CharController charCtrl in allCharInCombat)
            {
                if (charCtrl.charModel.stateOfChar == StateOfChar.UnLocked)
                {
                    charChtrl1 = charCtrl;
                    charCount++;
                }
            }
            if (charCount == 1)
            {
                if (charChtrl1.charModel.charID == charController.charModel.charID)
                    return true; 
            }
            return false;
        }

        #endregion

        #region SET ABBAS CLASS

        public void SetAbbasClassOnQuickStart()
        {
            if (!GameService.Instance.gameController.isQuickStart) return; 
            ClassType classType =
                        GameService.Instance.currGameModel.abbasClassType; 
            CharController charController = GetAbbasController(CharNames.Abbas);
                
            CharModel charModel = charController.charModel;
            charModel.classType = classType;
        }

        #endregion

    }
}


