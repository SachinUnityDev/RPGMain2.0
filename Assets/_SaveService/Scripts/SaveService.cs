using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System.IO;
using System;
using UnityEngine.UI;
using Quest;

namespace Common
{

   

    // Should define and send the save path for each of the service 
    // Each Service should either create the folder & File or save the data in already created file and folder
    //
    public class SaveService : MonoSingletonGeneric<SaveService>
    {
        [Header("Scriptable Object")]
        public SaveSO saveSO;
        public string basePath = "/SAVE_SYSTEM/"; 
        [Header("Save and Load Panel Ref")]
        public SaveView saveView;
        public LoadView loadView;

        public EscapePanelController escapePanelController; 
        
        public SaveMode saveMode;
        public SaveSlot currMBloodSlot; 

        public SaveSlot slotSelected; 
        public List<GameObject> allServices = new List<GameObject>();
        public List<ISaveable> allSaveService = new List<ISaveable>();

        [Header("Profile Controller")]
        public ProfileController profileController;
      
   


        public bool isLoading = false;
        public bool isSaving = false; 

        private void Start()
        {           
           saveView.GetComponent<IPanel>().UnLoad();
           slotSelected = SaveSlot.AutoSave; 
            foreach (Transform child in saveView.gameObject.transform)
            {
                if(child.GetComponent<Button>() != null)
                    child.GetComponent<Button>().onClick.AddListener(()=>OnSlotBtnPressed(child));
            }
            //CreateDefaultFolders();
        }
        // ONLY FOR THE FILE INIT 
        // save and load profile Model to be implemented 
        // connect profile Model and code Profile View 
        // Profile Controller will update based on profile view 
        
        public void Init()
        {// no init on load bcoz it always retrieve data from the allGameModel Files



        }
       


        void CreateDefaultFolders()
        {

            CreateAFolder(Application.dataPath + basePath);
            foreach (SaveSlot slot in Enum.GetValues(typeof(SaveSlot)))
            {
                string path = GetSlotPath(slot);
                CreateAFolder(path);
                CreateDefaultServiceFolder(slot);
            }
        }

        void CreateDefaultServiceFolder(SaveSlot saveSlot)
        {

            foreach (ServicePath serviceName in Enum.GetValues(typeof(ServicePath)))
            {
                string path = GetServicePath(saveSlot, serviceName);
                if(serviceName == ServicePath.EcoService)
                CreateAFolder(path);

            }
        }

        public string GetSlotPath(SaveSlot saveSlot)
        {
            profileController = GetComponent<ProfileController>();  
            string str = Application.dataPath + profileController.GetProfilePath();
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
#region 
        
        public string GetCurrSlotServicePath(ServicePath servicePath)
        {
             return GetServicePath(slotSelected, servicePath);
        }
        public string GetServicePath(SaveSlot saveSlot, ServicePath servicePath)
        {
            string str = GetSlotPath(saveSlot);
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


     

        public void OnAutoSaveTriggered()
        {


        }
        public void OnAutoSaveMBloodTriggered()
        {

        }

        public void OnQuickSavePressed()
        {

        }

        public void OnManualSavePressed()
        {

        }

        public void ReadSaveFiles(string folderPath)
        {
            // Get all the file paths in the specified folder with the ".txt" extension
            string[] saveFiles = Directory.GetFiles(folderPath, "*.txt");

            // Iterate through each save file
            foreach (string filePath in saveFiles)
            {
                // Read the contents of the file
                string fileContents = File.ReadAllText(filePath);

                // Process the file contents as needed
                // For example, you can parse the contents into objects or display them in the console
                Console.WriteLine(fileContents);
            }
        }


        public List<ISaveable> FindAllSaveables()
        {
            List<ISaveable> saveables = new List<ISaveable>();

            // Get all the game objects in the scene
            GameObject[] sceneObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

            // Iterate through each game object
            foreach (GameObject obj in sceneObjects)
            {
                // Get all the ISaveable components attached to the game object
                ISaveable[] components = obj.GetComponentsInChildren<ISaveable>();

                // Add the ISaveable components to the list
                saveables.AddRange(components);
            }

            return saveables;
        }
    
        public void ShowSavePanel()
        {           
            saveView.GetComponent<IPanel>().Load();           
        }

        public void ShowLoadPanel()
        {
            //Update bottom txt



        }
#endregion
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

        public void SaveStateMaster()
        {
            foreach (GameObject service in allServices)
            {
                 service.GetComponent<ISaveable>().SaveState();
            }
        }

      
        //public void LoadFileMaster() 
        //{
        //    string path = "/SAVE_SYSTEM/savedFiles/" + SaveService.Instance.slotSelected.ToString(); 
        //    foreach (GameObject service in allServices)
        //    {
        //        service.GetComponent<ISaveable>().LoadState(path);
        //    }
        //}

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.G))
            //{
            //    isLoading = false; isSaving = true;                
            //    UIControlServiceCombat.Instance.ToggleUIStateScale(saveView.gameObject, UITransformState.Open);                
            //}

            //if (Input.GetKeyDown(KeyCode.H))
            //{
            //    isLoading = true; isSaving = false;
            //    UIControlServiceCombat.Instance.ToggleUIStateScale(saveView.gameObject, UITransformState.Open);
            //}       

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
    public enum SaveMode
    {
        None,
        QuickSave, // press F5
        AutoSave, // at every check point
//        AutoSaveMB, // at every chekc point in MB mode .. no manual saving 
        ManualSave, // save
    }
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