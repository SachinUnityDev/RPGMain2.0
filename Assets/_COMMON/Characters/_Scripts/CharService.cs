using Combat;
using Interactables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
namespace Common
{
    public class CharService : MonoSingletonGeneric<CharService>, ISaveableService
    {
        public event Action<CharNames> OnCharAddedToParty; 


        [Header("Character Scriptables")]
        public List<CharacterSO> allAllySO = new List<CharacterSO>();
        public List<NPCSO> allNPCSO = new List<NPCSO>();

        [Header("Load Strings ")]
        public List<string> allCharsJSONs = new List<string>();

        [Header("Sopporting SO")]
        public StatsVsChanceSO statChanceSO;
        public LvlNExpSO lvlNExpSO;
        public CharComplimentarySO charComplimentarySO;

        [Header("ALL Loaded char models")]
        public List <CharModel> allLoadedCharModel = new List<CharModel>();// to be populated by charCtrl

        [Header("GO list")]
        public List<GameObject> allyInPlay;       
        public List<GameObject> charsInPlay; // 


       // only for Quest  and Combat
        
   
        [Header("represents all the chars in the game")]
        // all Char In Game this service + bestiary 
        public List<CharController> charsInPlayControllers;

        [Header("Specific Sub list of Chars/ Allies")]
        public List<CharController> allyInPlayControllers;
        public List<CharModel> allyUnLockedCharModels;// char Unlocked in the game
        public List<CharModel> allAvailCharsModels;
        
        public List<CharController> allCharsInParty; // on party Locked and Set 

        public List<CharController> charDiedinLastTurn;
        [Header("Character Pos")]
        public Vector3 spawnPos = new Vector3(0, 0, 0);

        [Header("Recordable Params")]
        public int lastCharID;


        //public List<CharController> enemyInPlayControllers; // enemies to be dep 
        public List<GameObject> enemyInCombatPlay; // enemies to be dep 

        void Start()
        {
            lastCharID = 0;         
            CombatEventService.Instance.OnEOT += UpdateOnDeath;
       
            DontDestroyOnLoad(this.gameObject);
        }

        public void Init()
        {
            // get all so and Popyulate the list of controllers
            // if save slot is defined take from save slot other SO from here pass in charSO    
            List<CharNames> chars2Spawn = new List<CharNames> {CharNames.Abbas_Skirmisher
               , CharNames.Baran, CharNames.Cahyo, CharNames.Rayyan };

            foreach (CharNames charName in chars2Spawn)
            {
                SpawnCompanions(charName);
            }

            //CreateAllAlliesCtrls();
        }

        #region GETTERS
        public CharacterSO GetCharSO( CharNames charName)
        {            
                CharacterSO charSO = allAllySO.Find(x => x.charName == charName);
            if (charSO != null)
                return charSO;
            else
                Debug.LogError("Char SO not found" + charName);
            return null;
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
        public GameObject GetCharGOWithName(CharNames _charName, int _charID)   //change it to charID based only 
        {
            GameObject charGO = charsInPlay.Find(x => x.GetComponent<CharController>().charModel.charName == _charName
                                && x.GetComponent<CharController>().charModel.charID == _charID);
            return charGO; 
        }

        public CharController GetCharCtrlWithCharID(int  _charID)
        {
            CharController charCtrl = charsInPlayControllers.Find(x => x.charModel.charID == _charID ); 
            return charCtrl; 
        }
        public CharController GetCharCtrlWithName(CharNames charName)
        {
            //for (int i = 0; i < charsInPlayControllers.Count; i++)
            //{
            //    Debug.Log("NAMES" +charsInPlayControllers[i].charModel.charName);

            //}
            CharController charCtrl = charsInPlayControllers.Find(x => x.charModel.charName == charName);
            if (charCtrl != null)
                return charCtrl;
            else
                Debug.Log("CharController not found" + charName);
            return null;
        }

        public CharModel GetAllyCharModel(CharNames charName)
        {          
            CharModel charModel = allyUnLockedCharModels
                            .Find(t => t.charName == charName);
            if (charModel != null)
                return charModel;
            else
                Debug.Log("CharModel not found" + charName);
            return null; 
        }

#endregion

       
        public void CreateAllAlliesCtrls()
        {
            
            foreach (CharacterSO c in allAllySO)
            {
                    GameObject charGO = new GameObject();
                    CharController charCtrl = charGO.AddComponent<CharController>();
                    charCtrl.InitiatizeController(c);
                    Debug.Log("Name1 " +charCtrl.charModel.charName); 
                    allyInPlayControllers.Add(charCtrl);
                    charsInPlayControllers.Add(charCtrl);
                    allyInPlay.Add(charGO);    
                    charsInPlay.Add(charGO); 
                    if(c.availOfChar == AvailOfChar.Available)
                    {
                        allAvailCharsModels.Add(charCtrl.charModel);                               
                    }
                    if(c.stateOfChar == StateOfChar.UnLocked)
                    {
                        allyUnLockedCharModels.Add(charCtrl.charModel);
                    }

                    LevelService.Instance.LevelUpInit(charCtrl);              
            }
        }

       // public CharController CreateCharsCtrl(CharNames charName)  // LATEST ONE 
     //   {
            //CharController charCtrl = new CharController();
            //foreach (CharacterSO c in allAllySO)
            //{
            //    if (charName == c.charName)
            //    {        
            //        charCtrl.InitiatizeController(c);

            //        CharMode charMode = charCtrl.charModel.charMode;
            //        if (charMode == CharMode.Ally)
            //        {
            //            allyInPlayControllers.Add(charCtrl);
                      
            //        }
            //        //if (charMode == CharMode.Enemy)
            //        //{
            //        //    enemyInPlayControllers.Add(charCtrl);
            //        //    enemyInPlay.Add(go);
            //        //    //  CombatService.Instance.enemyInCombat.Add(charCtrl); 

            //        //}
            //        charsInPlayControllers.Add(charCtrl);
            //        charsInPlay.Add(go);
            //        LevelService.Instance.LevelUpInit(charCtrl);

            //    }
            //}
           // return charCtrl;
      //  }

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
            charsInPlayControllers.Add(charController);
            allyInPlayControllers.Add(charController);
            charsInPlay.Add(go);
            allyInPlay.Add(go);
            if (charModel.availOfChar == AvailOfChar.Available)
            {
                allAvailCharsModels.Add(charController.charModel);
            }
            if (charModel.stateOfChar == StateOfChar.UnLocked)
            {
                allyUnLockedCharModels.Add(charController.charModel);
            }

          //  LevelService.Instance.LevelUpInit(charController);


            // add in combat Service rather than here

            //CharController charCtrl = null; 
            //foreach (CharacterSO c in allCharSO)
            //{
            //    if (charName == c.charName)
            //    {
            //        GameObject go = Instantiate(c.charPrefab, spawnPos, Quaternion.identity);
            //        charCtrl = go.AddComponent<CharController>();

            //        charCtrl.InitiatizeController(c, null, SaveSlot.New);

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
            //          //  CombatService.Instance.enemyInCombat.Add(charCtrl); 
            //        }
            //        CharsInPlayControllers.Add(charCtrl);
            //        charsInPlay.Add(go);
            //    }
            //}
            //return charCtrl;
            return charController; 
        }

        public void On_CharAddToParty(CharController charController)
        {
            Debug.Log("On Char Added" + charController.charModel.charName);
            allCharsInParty.Add(charController);
            OnCharAddedToParty?.Invoke(charController.charModel.charName);
        }

        public void LoadCharControllers(CharModel charModel)
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

        public void RestoreState()
        {
            allLoadedCharModel.Clear();
            string mydataPath = "/SAVE_SYSTEM/savedFiles/" + SaveService.Instance.slotSelect.ToString()
               + "/Char/charModels.txt";

            if (File.Exists(Application.dataPath + mydataPath))
            {
                Debug.Log("File found!");
                string str = File.ReadAllText(Application.dataPath + mydataPath);

                 allCharsJSONs = str.Split('|').ToList();

                foreach (string modelStr in allCharsJSONs)
                {
                    Debug.Log($"CHAR: {modelStr}");                   
                    if (String.IsNullOrEmpty(modelStr)) continue; // eliminate blank string
                      CharModel charModel = JsonUtility.FromJson<CharModel>(modelStr);
                    allLoadedCharModel.Add(charModel);
                   // LoadCharControllers(charModel);
                    Debug.Log(charModel.charName);    
                }
            }
            else
            {
                Debug.Log("File Does not Exist");
            }
        }

        public CharModel LoadCharModel(CharNames charName)
        {
            CharModel charModel = allLoadedCharModel.Find(t => t.charName == charName);
            if (charModel != null)
                return charModel;
            else
                Debug.LogError("Char model not loaded");
            return null; 

        }
        public void ClearState()
        {
            string mydataPath = "/SAVE_SYSTEM/savedFiles/" + SaveService.Instance.slotSelect.ToString()
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

#region ALLY AND ENEMY COMMON CONTROLLERS 

        public void ToggleCharColliders(GameObject targetGO)
        {

            CharController targetController = targetGO.GetComponent<CharController>();

            foreach (CharController charController in charsInPlayControllers)
            {
                if(targetController != charController)
                {
                    Collider[] allColliders = charController.gameObject.GetComponentsInChildren<Collider>();
                    for (int i = 0; i < allColliders.Length; i++)
                    {
                        allColliders[i].enabled = false;
                    }
                }else
                {
                    Collider[] allColliders = charController.gameObject.GetComponentsInChildren<Collider>();
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
        public GameObject GetGO4CharID(int charID)
        {
            return null; 
        }


        #region GETTER
        public CharController HasHighestStat(StatsName _statNames, CharMode _charMode)
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

        public CharController HasLowestStat(StatsName _statNames, CharMode _charMode)
        {
            float minValue = 0;
            CharController minValChar = null;
            foreach (CharController c in charsInPlayControllers)
            {
                if (c.charModel.charMode == _charMode)
                {
                    float currVal = c.GetStat(_statNames).currValue;
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

        public void UpdateOnDeath()
        {
            if (charDiedinLastTurn.Count < 1) return;
            foreach (CharController charCtrl in charDiedinLastTurn.ToList())
            {
                Debug.Log("CHAR DIED " + charCtrl.charModel.charID);
                CharMode charMode = charCtrl.charModel.charMode;
                GameObject charGO = charCtrl.gameObject;
                if (charMode == CharMode.Ally)
                {
                    allyInPlay.Remove(charGO);
                    allyInPlayControllers.Remove(charCtrl);
                    if (allyInPlayControllers.Count <= 0) // COMBAT END CONDITION   
                    {
                        CombatEventService.Instance.On_CombatLoot(false);
                    }
                }
                else if (charMode == CharMode.Enemy)   // COMBAT END CONDITION.. ALL ENEMY KILLED
                {
                    enemyInCombatPlay.Remove(charGO);
                    
                    //enemyInPlayControllers.Remove(charCtrl);
                    //if (enemyInPlayControllers.Count <= 0)  // end of combat
                    //{
                    //    CombatEventService.Instance.On_CombatLoot(true);
                    //}
                }
                charsInPlay.Remove(charGO);
                charsInPlayControllers.Remove(charCtrl);
                GridService.Instance.UpdateGridOnCharDeath(charCtrl);  // subscribe to event once tested
                CombatEventService.Instance.On_CharDeath(charCtrl);
                CombatService.Instance.roundController.ReorderAfterCharDeath(charCtrl);
            }

            charDiedinLastTurn.Clear();
        }

#endregion
    }


}
