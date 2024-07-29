using Combat;
using Interactables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Town;
using System.Net.Http.Headers;
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


       // only for Quest  and Combat
        
        [Header("represents all the chars in the game")]
        // all "ACTIVE" Char In Game this service + bestiary 
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
        public ServicePath servicePath => ServicePath.CharService;
        void Start()
        {
            isPartyLocked= false;
            GameEventService.Instance.OnCombatEnter += OnCombatEnter;
            DontDestroyOnLoad(this.gameObject);
        }
        private void OnDisable()
        {
            GameEventService.Instance.OnCombatEnter -= OnCombatEnter;
        }
        
        void OnCombatEnter()
        {
            CombatEventService.Instance.OnEOT -= UpdateOnDeath;
            CombatEventService.Instance.OnEOT += UpdateOnDeath;
        }
        void OnCombatExit()
        {
            CombatEventService.Instance.OnEOT -= UpdateOnDeath;
        }
        public void Init() 
        {
           string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            if (SaveService.Instance.DirectoryExists(path))
            {
                if(IsDirectoryEmpty(path))
                {
                    List<CharNames> chars2Spawn = new List<CharNames> { CharNames.Abbas,
                                  CharNames.Baran, CharNames.Rayyan, CharNames.Cahyo };
                    charMainModel = new CharMainModel();
                    foreach (CharNames charName in chars2Spawn)
                    {
                        SpawnCompNG(charName);
                    }                    
                }else
                {
                    LoadState();
                }
                SetAbbasClassOnQuickStartFrmGameService();
            }
            else
            {
                Debug.LogError("Service Directory missing");               
            }                   
        }
        public CharModel GetCharModel(CharNames charName)
        {
            CharModel charModel = allCharModels.Find(t => t.charName == charName);
            if (charModel != null)
                return charModel;
            else
                Debug.LogError("Char model not loaded");
            return null;
        }


        #region SAVE LOAD CLEAR AND INIT
        public void LoadState()
        {
            // browse thru all files in the folder and load them
            // as char Models 
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);

            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    Debug.Log("  "+ contents);
                    CharModel charModel = JsonUtility.FromJson<CharModel>(contents);
                    SpawnComp(charModel);                
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
            if (allyInPlayControllers.Count <= 0)
            {
                Debug.LogError("no chars in play"); return;
            }
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            ClearState();
            // save all char models


            foreach (CharController charCtrl in allyInPlayControllers)
            {
                CharModel charModel = charCtrl.charModel;
                string charModelJSON = JsonUtility.ToJson(charModel);
                Debug.Log(charModelJSON);
                string fileName = path + charModel.charName.ToString() + ".txt";           
                File.WriteAllText(fileName, charModelJSON);
            }
        }

        #endregion
        #region   GETTERS
        public CharacterSO GetCharSO( CharNames charName)
        {            
            return allCharSO.GetCharSO(charName);                
        }
        public CharacterSO GetCharSO(CharModel charModel)
        {
            CharNames charName = charModel.charName;
            CharacterSO charSO = allCharSO.GetCharSO(charName);
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
        public CharController GetAllyController(CharNames charName)
        {
            CharController charCtrl = charsInPlayControllers.Find(x => x.charModel.charName == charName);
            if (charCtrl != null)
                return charCtrl;
            else
                Debug.Log("ally CharController not found" + charName);
            return null;
        }
        #endregion

        #region LOCK and UNLOCK and FLEE

        public void On_PartyLocked()
        {
            isPartyLocked= true;
            OnPartyLocked?.Invoke();
            foreach (CharController c in allCharsInPartyLocked)
            {
                    if (c.charModel.charName == CharNames.Abbas) continue; 
                
                    List<ItemDataWithQtyNFameType> qtyWithFamedata = new List<ItemDataWithQtyNFameType>();
                    qtyWithFamedata = c.charModel.provisionItems;
                    if(qtyWithFamedata.Count>0)
                    foreach (ItemDataWithQtyNFameType item in qtyWithFamedata)
                    {
                            ItemDataWithQty itemDataWithQty = item.itemDataQty;
                            for (int i = 0; i < itemDataWithQty.quantity; i++)
                            {
                                ItemData itemData = new ItemData(itemDataWithQty.itemData.itemType
                                        , itemDataWithQty.itemData.itemName);
                                Iitems Iitem = ItemService.Instance.GetNewItem(itemData);
                                InvService.Instance.invMainModel.EquipItem2PotionProvSlot(Iitem, c); // refirbish the provision
                            }
                    }                   
                
            }
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

        public CharController SpawnComp(CharModel charModel)  // loaded up charModel 
        {
            CharacterSO charSO = GetCharSO(charModel.charName);

            GameObject go = Instantiate(charSO.charPrefab, spawnPos, Quaternion.identity);
            CharController charController = go.AddComponent<CharController>();

            charController.InitController(charModel);  // loaded up charModel
            Add2List(charController, charModel, go);
            return charController;
        }

        public CharController SpawnCompNG(CharNames charName)  // character factory 
        {   
            CharacterSO charSO = GetCharSO(charName); 
            
            GameObject go = Instantiate(charSO.charPrefab, spawnPos, Quaternion.identity);
            CharController charController = go.AddComponent<CharController>();
          
            CharModel charModel = charController.InitController(charSO);  // to be build up
            Add2List(charController, charModel, go);
            return charController;

        }

        void Add2List(CharController charController, CharModel charModel, GameObject go)
        {
            if (charController.charModel.charName == CharNames.Abbas)
                AbbasStatusUpdate(charController);

            charsInPlayControllers.Add(charController);
            allyInPlayControllers.Add(charController);
            LevelService.Instance.LevelUpInitAlly(charController);
            //charsInPlay.Add(go);
            //allyInPlay.Add(go);
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
           
        }


        void AddController2ls(CharController charController)
        {

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

        public void SetAbbasClassOnQuickStartFrmGameService()
        {
            if (!GameService.Instance.gameController.isQuickStart) return; 
            ClassType classType =
                        GameService.Instance.currGameModel.abbasClassType; 
            CharController charController = GetAllyController(CharNames.Abbas);
                
            CharModel charModel = charController.charModel;
            charModel.classType = classType;
        }

        #endregion

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


