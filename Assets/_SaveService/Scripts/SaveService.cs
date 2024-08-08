using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System.IO;
using System;
using UnityEngine.UI;
using Quest;
using UnityEngine.SceneManagement;

namespace Common
{

   

    // Should define and send the save path for each of the service 
    // Each Service should either create the folder & File or save the data in already created file and folder
    //
    public class SaveService : MonoSingletonGeneric<SaveService>
    {
        [Header(" Date and Month support")]
        public  AllMonthSO allMonthSO; 

        [Header("Scriptable Object")]
        public SaveSO saveSO;
        public string basePath = "/SAVE_SYSTEM/"; 



        [Header("Save and Load Panel Ref")]
        public SaveView saveView;
        public LoadView loadView;

        public EscapePanelController escapePanelController;
        public SaveController saveController; 

        public List<GameObject> allServices = new List<GameObject>();
        public List<ISaveable> allSaveService = new List<ISaveable>();
        private void OnEnable()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneLoaded;
            SceneManager.activeSceneChanged += OnActiveSceneLoaded;
        }
        private void Start()
        {          
           
            //foreach (Transform child in saveView.gameObject.transform)
            //{
            //    if(child.GetComponent<Button>() != null)
            //        child.GetComponent<Button>().onClick.AddListener(()=>OnSlotBtnPressed(child));
            //}
            //CreateDefaultFolders();
        }
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneLoaded;
        }

        void OnActiveSceneLoaded(Scene oldScene, Scene newScene)
        {
            // find save and load view 
            Debug.Log("Active Scene just loaded" + newScene.name + "OLD Scene" + oldScene.name);
            if (newScene.name == "CORE") return; 
            Canvas canvas = FindObjectOfType<Canvas>();
            if(canvas != null)
            {
                loadView = FindObjectOfType<LoadView>(true);
                saveView = FindObjectOfType<SaveView>(true);
                saveController = GetComponent<SaveController>();
            }
            else
            {
                Debug.LogError("Canvas not found"); 
            }
            
        }
        public string[] GetAllDirectories(string path)
        {
            try
            {
                // Get all directories in the specified path
                return Directory.GetDirectories(path);
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred while getting directories: {e.Message}");
                return new string[0];
            }
        }

        // Example usage within the class
        public void PrintAllDirectories(string path)
        {
            string[] directories = GetAllDirectories(path);
            foreach (string directory in directories)
            { 
                PrintAllDirectories(directory);
               // Debug.Log("DIR"+directory);
                string [] files = GetAllTxtFiles(directory);    
                foreach (string file in files)
                {
                    Debug.Log("FILE" + file);
                }
                string[] filesMeta = GetAllMetaFiles(directory);
                foreach (string file in filesMeta)
                {
                    Debug.Log("FILE" + file);
                }
            }

        }
        string[] GetAllMetaFiles(string path)
        {
            try
            {
                return Directory.GetFiles(path, "*.meta");
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred while getting .txt files: {e.Message}");
                return new string[0];
            }
        }
        string[] GetAllTxtFiles(string path)
        {
            try
            {
                return Directory.GetFiles(path, "*.txt");
            }
            catch (Exception e)
            {
                Debug.LogError($"An error occurred while getting .txt files: {e.Message}");
                return new string[0];
            }
        }
        public void Init()
        {     
        }

     
        public void ClearAllProfiles()
        {
            foreach (ProfileSlot profileSlot in Enum.GetValues(typeof(ProfileSlot)))
            {
                foreach (SaveSlot saveSlot in Enum.GetValues(typeof(SaveSlot)))
                {
                    GameService.Instance.SetProfileNSaveSlot(profileSlot, saveSlot);
                    List<ISaveable> saveables = FindAllSaveables();
                    foreach (ISaveable saveable in saveables)
                    {
                        saveable.ClearState();
                    }
                }
            }
        }

        void CreateDefaultFolders()
        {

            CreateAFolder(Application.dataPath + basePath);
            foreach (SaveSlot slot in Enum.GetValues(typeof(SaveSlot)))
            {
                string path = GetCurrentSlotPath(slot);
                CreateAFolder(path);
                CreateDefaultServiceFolder(slot);
            }
        }

        void CreateDefaultServiceFolder(SaveSlot saveSlot)
        {

            foreach (ServicePath serviceName in Enum.GetValues(typeof(ServicePath)))
            {
                string path = GetCurrServicePath(saveSlot, serviceName);
                if(serviceName == ServicePath.EcoService)
                CreateAFolder(path);
            }
        }
        public string GetSlotPath(ProfileSlot profileSlot, SaveSlot saveSlot)
        {
            string str = GetCurrProfilePath(profileSlot);
            switch (saveSlot)
            {
                case SaveSlot.AutoSave:
                    str += "AutoSave/";
                    break;
                case SaveSlot.QuickSave:
                    str += "QuickSave/";
                    break;
                case SaveSlot.Slot1:
                    str += "Slot1/";
                    break;
                case SaveSlot.Slot2:
                    str += "Slot2/";
                    break;
                case SaveSlot.Slot3:
                    str += "Slot3/";
                    break;
                case SaveSlot.Slot4:
                    str += "Slot4/";
                    break;
                default:
                    break;
            }
            return str;
        }
        public string GetCurrentSlotPath(SaveSlot saveSlot)
        {
            GameModel currGameModel = GameService.Instance.currGameModel; 
            string str = GetCurrProfilePath(currGameModel.profileSlot);
            switch (saveSlot)
            {
                case SaveSlot.AutoSave:
                    str += "AutoSave/";
                    break;
                case SaveSlot.QuickSave:
                    str += "QuickSave/";
                    break;
                case SaveSlot.Slot1:
                    str += "Slot1/";
                    break;
                case SaveSlot.Slot2:
                    str += "Slot2/";
                    break;
                case SaveSlot.Slot3:
                    str += "Slot3/";
                    break;
                case SaveSlot.Slot4:
                    str += "Slot4/";
                    break;
                default:
                    break;
            }
            return str; 
        }
        #region GET AND SET
        public string GetSlotServicePath(SaveSlot saveSlot, ServicePath servicePath)
        {
            return GetCurrServicePath(saveSlot, servicePath);
        }
        public string GetCurrSlotServicePath(ServicePath servicePath)
        {
             return GetCurrServicePath(GameService.Instance.saveSlot, servicePath);
        }
        public string GetServicePath(SaveSlot saveSlot, ServicePath servicePath, ProfileSlot profileSlot)
        {
            string str = GetSlotPath(profileSlot, saveSlot);
            switch (servicePath)
            {
                case ServicePath.Main:
                    str += "Main/";
                    break;
                case ServicePath.PlayerService:
                    str += "PlayerService/";
                    break;
                case ServicePath.WoodGameService:
                    str += "WoodGameService/";
                    break;
                case ServicePath.CurioService:
                    str += "CurioService/";
                    break;
                case ServicePath.EncounterService:
                    str += "EncounterService/";
                    break;
                case ServicePath.LandscapeService:
                    str += "LandscapeService/";
                    break;
                case ServicePath.LootService:
                    str += "LootService/";
                    break;
                case ServicePath.QuestEventService:
                    str += "QuestEventService/";
                    break;
                case ServicePath.QuestMissionService:
                    str += "QuestMissionService/";
                    break;
                case ServicePath.QRoomService:
                    str += "QRoomService/";
                    break;
                case ServicePath.ArmorService:
                    str += "ArmorService/";
                    break;
                case ServicePath.InvService:
                    str += "InvService/";
                    break;
                case ServicePath.WeaponService:
                    str += "WeaponService/";
                    break;
                case ServicePath.GewgawService:
                    str += "GewgawService/";
                    break;
                case ServicePath.LoreService:
                    str += "LoreService/";
                    break;
                case ServicePath.RecipeService:
                    str += "RecipeService/";
                    break;
                case ServicePath.ItemService:
                    str += "ItemService/";
                    break;
                case ServicePath.GridService:
                    str += "GridService/";
                    break;
                case ServicePath.CombatEventService:
                    str += "CombatEventService/";
                    break;
                case ServicePath.CombatService:
                    str += "CombatService/";
                    break;
                case ServicePath.PassiveSkillService:
                    str += "PassiveSkillService/";
                    break;
                case ServicePath.SkillService:
                    str += "SkillService/";
                    break;
                case ServicePath.WelcomeService:
                    str += "WelcomeService/";
                    break;
                case ServicePath.JobService:
                    str += "JobService/";
                    break;
                case ServicePath.BuildingIntService:
                    str += "BuildingIntService/";
                    break;
                case ServicePath.MapService:
                    str += "MapService/";
                    break;
                case ServicePath.TownService:
                    str += "TownService/";
                    break;
                case ServicePath.SceneMgmtService:
                    str += "SceneMgmtService/";
                    break;
                case ServicePath.BarkService:
                    str += "BarkService/";
                    break;
                case ServicePath.BuffService:
                    str += "BuffService/";
                    break;
                case ServicePath.CharStatesService:
                    str += "CharStatesService/";
                    break;
                case ServicePath.CharService:
                    str += "CharService/";
                    break;
                case ServicePath.DialogueService:
                    str += "DialogueService/";
                    break;
                case ServicePath.LevelService:
                    str += "LevelService/";
                    break;
                case ServicePath.PermaTraitsService:
                    str += "PermaTraitsService/";
                    break;
                case ServicePath.TempTraitService:
                    str += "TempTraitService/";
                    break;
                case ServicePath.CodexService:
                    str += "CodexService/";
                    break;
                case ServicePath.GameEventService:
                    str += "GameEventService/";
                    break;
                case ServicePath.GameService:
                    str += "GameService/";
                    break;
                case ServicePath.SettingService:
                    str += "SettingService/";
                    break;
                case ServicePath.IntroAudioService:
                    str += "IntroAudioService/";
                    break;
                case ServicePath.BestiaryService:
                    str += "BestiaryService/";
                    break;
                case ServicePath.MGService:
                    str += "MGService/";
                    break;
                case ServicePath.SaveService:
                    str += "SaveService/";
                    break;
                case ServicePath.FameService:
                    str += "FameService/";
                    break;
                case ServicePath.CalendarService:
                    str += "CalendarService/";
                    break;
                case ServicePath.RosterService:
                    str += "RosterService/";
                    break;
                case ServicePath.TownEventService:
                    str += "TownEventService/";
                    break;
                case ServicePath.TradeService:
                    str += "TradeService/";
                    break;
                case ServicePath.EcoService:
                    str += "EcoService/";
                    break;
                default:
                    break;
            }
            return str;
        }
        public string GetCurrServicePath(SaveSlot saveSlot, ServicePath servicePath)
        {   
            string str = GetCurrentSlotPath(saveSlot);
            switch (servicePath)
            {
                case ServicePath.Main:
                    str += "Main/";
                    break;
                case ServicePath.PlayerService:
                    str += "PlayerService/";
                    break;
                case ServicePath.WoodGameService:
                    str += "WoodGameService/";
                    break;
                case ServicePath.CurioService:
                    str += "CurioService/";
                    break;
                case ServicePath.EncounterService:
                    str += "EncounterService/"; 
                    break;
                case ServicePath.LandscapeService:
                    str += "LandscapeService/";
                    break;
                case ServicePath.LootService:
                    str += "LootService/";
                    break;
                case ServicePath.QuestEventService:
                    str += "QuestEventService/";    
                    break;
                case ServicePath.QuestMissionService:
                    str += "QuestMissionService/";
                    break;
                case ServicePath.QRoomService:
                    str += "QRoomService/"; 
                    break;
                case ServicePath.ArmorService:
                    str += "ArmorService/"; 
                    break;
                case ServicePath.InvService:
                    str += "InvService/";   
                    break;
                case ServicePath.WeaponService:
                    str += "WeaponService/";    
                    break;
                case ServicePath.GewgawService:
                    str += "GewgawService/";    
                    break;
                case ServicePath.LoreService:
                    str += "LoreService/";  
                    break;
                case ServicePath.RecipeService:
                    str += "RecipeService/";
                    break;
                case ServicePath.ItemService:
                    str += "ItemService/";  
                    break;
                case ServicePath.GridService:
                    str += "GridService/";
                    break;
                case ServicePath.CombatEventService:
                    str += "CombatEventService/";
                    break;
                case ServicePath.CombatService:
                    str += "CombatService/";    
                    break;
                case ServicePath.PassiveSkillService:
                    str += "PassiveSkillService/";
                    break;
                case ServicePath.SkillService:
                    str += "SkillService/"; 
                    break;
                case ServicePath.WelcomeService:
                    str += "WelcomeService/";
                    break;
                case ServicePath.JobService:
                    str += "JobService/";
                    break;
                case ServicePath.BuildingIntService:
                    str += "BuildingIntService/";
                    break;
                case ServicePath.MapService:
                    str += "MapService/";   
                    break;
                case ServicePath.TownService:
                    str += "TownService/";  
                    break;
                case ServicePath.SceneMgmtService:
                    str += "SceneMgmtService/";
                    break;
                case ServicePath.BarkService:
                    str += "BarkService/";
                    break;
                case ServicePath.BuffService:
                    str += "BuffService/";
                    break;
                case ServicePath.CharStatesService:
                    str += "CharStatesService/";
                    break;
                case ServicePath.CharService:
                    str += "CharService/";  
                    break;
                case ServicePath.DialogueService:
                    str += "DialogueService/";  
                    break;
                case ServicePath.LevelService:
                    str += "LevelService/";
                    break;
                case ServicePath.PermaTraitsService:
                    str += "PermaTraitsService/";
                    break;
                case ServicePath.TempTraitService:
                    str += "TempTraitService/";
                    break;
                case ServicePath.CodexService:
                    str += "CodexService/";
                    break;
                case ServicePath.GameEventService:
                    str += "GameEventService/"; 
                    break;
                case ServicePath.GameService:
                    str += "GameService/";  
                    break;
                case ServicePath.SettingService:
                    str += "SettingService/";   
                    break;
                case ServicePath.IntroAudioService:
                    str += "IntroAudioService/";
                    break;
                case ServicePath.BestiaryService:
                    str += "BestiaryService/";
                    break;
                case ServicePath.MGService:
                    str += "MGService/";
                    break;
                case ServicePath.SaveService:
                    str += "SaveService/";
                    break;
                case ServicePath.FameService:
                    str += "FameService/";
                    break;
                case ServicePath.CalendarService:
                    str += "CalendarService/";
                    break;
                case ServicePath.RosterService:
                    str += "RosterService/";
                    break;
                case ServicePath.TownEventService:
                    str += "TownEventService/";
                    break;
                case ServicePath.TradeService:
                    str += "TradeService/";
                    break;
                case ServicePath.EcoService:
                    str += "EcoService/";
                    break;
                default:
                    break;
            }
            return str;    
        }
#endregion
        public List<ISaveable> FindAllSaveables()
        {
            List<ISaveable> saveables = new List<ISaveable>();

            string[] allScenes = GetAllScenes();
            foreach (string scene in allScenes)
            {
                GameObject[] sceneObjects = SceneManager.GetActiveScene().GetRootGameObjects();
                
                // Iterate through each game object
                foreach (GameObject obj in sceneObjects)
                {
                    // Get all the ISaveable components attached to the game object
                    ISaveable[] components = obj.GetComponentsInChildren<ISaveable>();

                    // Add the ISaveable components to the list
                    saveables.AddRange(components);
                }
            }
            return saveables;
        }
        public string[] GetAllScenes()
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            string[] scenes = new string[sceneCount];

            for (int i = 0; i < sceneCount; i++)
            {
                scenes[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            }

            return scenes;
        }
        public void ToggleSavePanel(bool load)
        {           
            if(load)
                saveView.GetComponent<IPanel>().Load();           
            else
                saveView.GetComponent<IPanel>().UnLoad();
        }

        public void ToggleLoadPanel(bool load)
        {
            if (load)
                loadView.GetComponent<IPanel>().Load();
            else
                loadView.GetComponent<IPanel>().UnLoad();
        }

        public void OnSlotBtnPressed(Transform child)
        {
            GameObject btn = child.gameObject; 
            int index = child.GetSiblingIndex();
            index++;  // for new val correction

        //    slotSelected = (SaveSlot)index;
        //    if(isSaving)
        //        SaveStateMaster();
        //    if(isLoading)
        //        LoadFileMaster();
        }

        public string GetCurrProfilePath(ProfileSlot profileSlot)
        {
           // GameModel gameModel = GameService.Instance.GetCurrentGameModel((int)profileSlot);
            string basePath1 = Application.dataPath + basePath; 
            switch (profileSlot)
            {
                case ProfileSlot.Profile1:
                    return basePath1 + "profile1/";
                case ProfileSlot.Profile2:
                    return basePath1 + "profile2/";
                case ProfileSlot.Profile3:
                    return basePath1 + "profile3/";
                case ProfileSlot.Profile4:
                    return basePath1 + "profile4/";
                case ProfileSlot.Profile5:
                    return basePath1 + "profile5/";
                case ProfileSlot.Profile6:
                    return basePath1 + "profile6/";
                default:
                    return basePath1 + "profile1/";
            }
        }
        public string GetProfilePath(ProfileSlot profileSlot, SaveSlot saveSlot)
        {
            GameModel gameModel = GameService.Instance.GetCurrentGameModel((int)profileSlot);
            string basePath1 = Application.dataPath + basePath;
            switch (profileSlot)
            {
                case ProfileSlot.Profile1:
                    return basePath1 + "profile1/";
                case ProfileSlot.Profile2:
                    return basePath1 + "profile2/";
                case ProfileSlot.Profile3:
                    return basePath1 + "profile3/";
                case ProfileSlot.Profile4:
                    return basePath1 + "profile4/";
                case ProfileSlot.Profile5:
                    return basePath1 + "profile5/";
                case ProfileSlot.Profile6:
                    return basePath1 + "profile6/";
                default:
                    return basePath1 + "profile1/";
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                PrintAllDirectories(Application.dataPath + basePath);
            }
        }


    }

    public enum SaveSlot
    {
        AutoSave,
        QuickSave,
        Slot1,
        Slot2,
        Slot3,
        Slot4,        
    }
    //public enum SaveMode
    //{
    //    None,
    //    QuickSave, // press F5
    //    AutoSave, // at every check point// MB Mode has only auto save
    //    ManualSave, // save
    //}
    public enum ProfileSlot
    {
        Profile1,
        Profile2,
        Profile3,
        Profile4,
        Profile5,
        Profile6,
    }
    public enum ServicePath
    {
        Main,
        PlayerService,
        WoodGameService,
        CurioService,
        EncounterService,
        LandscapeService,
        LootService,
        QuestEventService,
        QuestMissionService,
        QRoomService,
        ArmorService,
        InvService,
        WeaponService,
        GewgawService,
        LoreService,
        RecipeService,
        ItemService,
        GridService,
        CombatEventService,
        CombatService,
        PassiveSkillService,
        SkillService,
        WelcomeService,
        JobService,
        BuildingIntService,
        MapService,
        TownService,
        SceneMgmtService,
        BarkService,
        BuffService,
        CharStatesService,
        CharService,
        DialogueService,
        LevelService,
        PermaTraitsService,
        TempTraitService,
        CodexService,
        GameEventService,
        GameService,
        SettingService,
        IntroAudioService,
        BestiaryService,
        MGService,
        SaveService,
        FameService,
        CalendarService,
        RosterService,
        TownEventService,
        TradeService,
        EcoService,

    }

    public interface ISaveable
    {
        ServicePath servicePath { get; }
        void SaveState();
        void LoadState();
        void ClearState();
    }

}


// save controller will process data from each of the "SYSTEM and save them "
// Save Service will load the data from the path and 

// Add an option for deleting the slots too

// Auto Save: AutoSave for default mode ..... Set limit 
//  Quick Save : will always one file 
// Continue button will open the last Auto Save
// New Game if Mortal Blood Selected : will have a save slot .. with AutoSave feature
// Default Game ... can saved manually too (Autosave and Quick save being other 2 options)
// during manual save you can save on Save slot too .. manually when clicked during the game. 
// 


// laod game main menu to open the save slots 



//Auto Save :: every 3 consequtive game calendar 

//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


//public void SaveFile()
//{

//    if (!File.Exists(Application.dataPath + "/SAVE_SYSTEM/savedFiles/save.txt"))
//    {
//        Debug.Log("does not exist");
//        File.CreateText(Application.dataPath + "/SAVE_SYSTEM/savedFiles/save.txt");
//    }
//    File.AppendAllText(Application.dataPath + "/SAVE_SYSTEM/savedFiles/save.txt", "hello save file");




//    //// find the controller and pass in the file
//    //if (!Directory.Exists(Application.dataPath + "/SAVE_SYSTEM/save.txt"))
//    //{
//    //    File.CreateText(Application.dataPath + "/SAVE_SYSTEM/save.txt");
//    //    File.WriteAllText(Application.dataPath + "/SAVE_SYSTEM/save.txt", "moving beyond hello world");

//    //}else
//    //{
//    //    Debug.Log("Error during saving"); 
//    //}


//public void ClearFiles4ProfileNSlot(ProfileSlot profileSlot, SaveSlot saveSlot)
//{
//    string path = Application.dataPath + GetCurrProfilePath(profileSlot);



//    //{
//    //    // Get all .txt files in the directory and its subdirectories
//    //    string[] textFiles = Directory.GetFiles(path, "*.txt", SearchOption.AllDirectories);
//    //    string[] metaFiles = Directory.GetFiles(path, "*.meta", SearchOption.AllDirectories);

//    //    foreach (string file in metaFiles)
//    //    {
//    //        File.Delete(file); 
//    //    }
//    //    foreach (string file in textFiles)
//    //    {
//    //        File.Delete(file);
//    //    }
//    //}
//    //catch (Exception ex)
//    //{
//    //    Console.WriteLine($"An error occurred: {ex.Message}");
//    //}

//}