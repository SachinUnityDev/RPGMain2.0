using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;
using Interactables;
using System.IO; 
namespace Town
{ 
    /// <summary>
    ///  To provide a as singleton for the interior interactions of the building
    /// </summary>

    public class BuildingIntService : MonoSingletonGeneric<BuildingIntService>, ISaveable
    {
        public event Action<Iitems, TavernSlotType> OnItemWalled;
        public event Action<Iitems, TavernSlotType> OnItemWalledRemoved;
        public event Action<BuildingModel, BuildInteractType, bool> OnBuildIntUpgraded;
        public event Action<BuildingModel, BuildInteractType, bool> OnBuildIntUnLocked; 
        public event Action<BuildingModel, BuildView> OnBuildInit;
        public event Action<BuildingModel, BuildView> OnBuildUnload;




        [Header("Current Building")]
        public BuildingNames buildName; 

        [Header("Char and NPC Select")]
        public CharNames selectChar;
        public NPCNames selectNpc; 

        public AllBuildSO allBuildSO; 
        public List<BuildingModel> allBuildModel= new List<BuildingModel>();


        public HouseController houseController;
        public TavernController tavernController; 
        public TempleController templeController;
        public MarketController marketController; 
        public ShipController shipController;
        public SafekeepController safekeepController;
        public StableController stableController; 
        public ThievesGuildController thieveController;
        public CityHallController cityHallController;

        public ServicePath servicePath => ServicePath.BuildingIntService; 

        void Start()
        {
            

        }

        
        public void InitNGBuildIntService()
        {
            houseController = GetComponent<HouseController>();
            templeController = GetComponent<TempleController>();
            tavernController = GetComponent<TavernController>();
            marketController = GetComponent<MarketController>();
            shipController = GetComponent<ShipController>();
            safekeepController = GetComponent<SafekeepController>();
            stableController = GetComponent<StableController>();
            thieveController = GetComponent<ThievesGuildController>();
            cityHallController= GetComponent<CityHallController>();


            // depending on slot and state get the save file
            // this NEw game Init 
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {
                    houseController.InitHouseController();
                    templeController.InitTempleController();
                    tavernController.InitTavernController();
                    marketController.InitMarketController();
                    shipController.InitShipController();

                    stableController.InitStableController();
                    thieveController.InitThievesGuildController();
                    cityHallController.InitCityHallController();
                }
                else
                {
                    LoadState();
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }

        public void On_BuildInit(BuildingModel buildModel, BuildView buildView)
        {
            buildName = buildModel.buildingName; 
            OnBuildInit?.Invoke(buildModel, buildView);
        }
        public void On_BuildUnload(BuildingModel buildModel, BuildView buildView)
        {
            buildName = BuildingNames.None; 
            OnBuildUnload?.Invoke(buildModel, buildView);
        }
        public void UnLockABuild(BuildingNames buildingName,bool UnLock)
        {  
            BuildingModel buildModel = GetBuildModel(buildingName);
          
            if(buildModel.buildState== BuildingState.Locked && UnLock)
            {
                buildModel.buildState= BuildingState.Available;
            }
            else if ((buildModel.buildState == BuildingState.Available || buildModel.buildState == BuildingState.Available)
                && !UnLock)
            {
                buildModel.buildState = BuildingState.Locked;
            }
        }
        public bool IsBuildUnlocked(BuildingNames buildingName)
        {
            BuildingModel buildModel = GetBuildModel(buildingName);
            if(buildModel.buildState == BuildingState.Locked)
                return false;
            else
                return true;
        }

        public BuildingModel GetBuildModel(BuildingNames buildName)
        {
            int index = allBuildModel.FindIndex(t=>t.buildingName== buildName);
            if(index != -1)
            {
                return allBuildModel[index];
            }
            else
            {
                Debug.Log(" building Model Not Found" + buildName);
            }
            return null; 
        }

        #region NPC AND CHAR 
        public void ChgNPCState(BuildingNames buildName, NPCNames npcName, NPCState npcState, bool InitBuild)
        {
            BuildingModel buildModel = GetBuildModel(buildName);
            int index = buildModel.npcInteractData.FindIndex(t => t.nPCNames == npcName); 
            if(index != -1)
            {
                buildModel.npcInteractData[index].npcState= npcState;   
            }
            else
            {
                Debug.Log("npc data not found " + npcName); 
            }
            if(InitBuild) 
            GetBuildView(buildName).Init();
        }
        public NPCState GetNPCState(BuildingNames buildName, NPCNames npcName)
        {
            BuildingModel buildModel = GetBuildModel(buildName);
            int index = buildModel.npcInteractData.FindIndex(t => t.nPCNames == npcName);
            if (index != -1)
            {
                return buildModel.npcInteractData[index].npcState; 
            }
            else
            {
                Debug.Log("npc data not found " + npcName);
            }
            return 0; 
        }

        public void ChgCharState(BuildingNames buildName, CharNames charName, NPCState npcState, bool initBuild)
        {
            BuildingModel buildModel = GetBuildModel(buildName);
            int index = buildModel.charInteractData.FindIndex(t => t.compName == charName);
            if (index != -1)
            {
                buildModel.charInteractData[index].compState = npcState;
            }
            else
            {
                Debug.Log("char data not found " + charName);
            }
            if(initBuild)
            GetBuildView(buildName).Init();
        }
        public NPCState GetCharState(BuildingNames buildName, CharNames charName)
        {
            BuildingModel buildModel = GetBuildModel(buildName);
            int index = buildModel.charInteractData.FindIndex(t => t.compName == charName);
            if (index != -1)
            {
                return buildModel.charInteractData[index].compState; 
            }
            else
            {
                Debug.Log("char data not found " + charName);
            }
            return 0; 
        }

        #endregion
        public void UnLockDiaInBuildNPC(BuildingNames buildName, NPCNames npcName, DialogueNames diaName, bool isUnLock)
        {
            BuildingModel buildModel = GetBuildModel(buildName);
            int index = buildModel.npcInteractData.FindIndex(t => t.nPCNames == npcName);
            if (index != -1)
            {
                IntTypeData intData = 
                buildModel.npcInteractData[index].allInteract.Find(t => t.nPCIntType == IntType.Talk);
                foreach (DialogueData dia in intData.allDialogueData)
                {
                    if(dia.dialogueName == diaName)
                    {
                        dia.isUnLocked = isUnLock;
                    }
                }
            }
            else
            {
                Debug.Log("npc data not found " + npcName);
            }
            DialogueService.Instance.UpdateDialogueState();  // update dialogue models
        }
        public void UnLockDiaInBuildChar(BuildingNames buildName, CharNames compName, DialogueNames diaName, bool isUnLock)
        {
            BuildingModel buildModel = GetBuildModel(buildName);
            int index = buildModel.charInteractData.FindIndex(t => t.compName == compName);
            if (index != -1)
            {
                IntTypeData intData =
                buildModel.charInteractData[index].allInteract.Find(t => t.nPCIntType == IntType.Talk);
                foreach (DialogueData dia in intData.allDialogueData)
                {
                    if (dia.dialogueName == diaName)
                    {
                        dia.isUnLocked = isUnLock;
                    }
                }
            }
            else
            {
                Debug.Log("Char data not found " + compName);
            }
            DialogueService.Instance.UpdateDialogueState();  // update dialogue models
        }

        #region  BUILD INT ACTIONS
        public void On_ItemWalledRemoved(Iitems item, TavernSlotType tavernSlotType)
        {
            OnItemWalledRemoved?.Invoke(item, tavernSlotType);
        }
        public void On_ItemWalled(Iitems item, TavernSlotType tavernSlotType)
        {               
            OnItemWalled?.Invoke(item, tavernSlotType);
        }
        public void On_BuildIntUpgraded(BuildingModel buildModel, BuildInteractType buildIntType, bool isUpgrade)
        {
            OnBuildIntUpgraded?.Invoke(buildModel, buildIntType, isUpgrade);
        }
        public void On_BuildIntUnLocked(BuildingModel buildModel, BuildInteractType buildIntType, bool isUnLocked)
        {
            OnBuildIntUnLocked?.Invoke(buildModel, buildIntType, isUnLocked);
        }
        #endregion 

        public BuildView GetBuildView(BuildingNames buildingNames)
        {
            switch (buildingNames)
            {
                case BuildingNames.None:
                    break;
                case BuildingNames.CityHall:                    
                    break;
                case BuildingNames.House:
                    return houseController.houseView;                    
                case BuildingNames.Marketplace:
                    return marketController.marketView;                    
                case BuildingNames.Safekeep:
                    break; 
                case BuildingNames.Ship:
                    return shipController.shipView;
                case BuildingNames.Stable:
                    break;
                case BuildingNames.Tavern:
                    return tavernController.tavernView;
                case BuildingNames.Temple:
                    return templeController.templeView; 
                case BuildingNames.ThievesGuild:
                    break;
                default:
                    break;
            }
            return null; 
        }

        public void SaveState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            ClearState();
            // save all char models

            foreach (BuildingModel  buildModel in allBuildModel)
            {
                string buildJson = JsonUtility.ToJson(buildModel);
                Debug.Log(buildJson);
                string fileName = path + buildModel.buildingName.ToString() + ".txt";
                File.WriteAllText(fileName, buildJson);
            }

        }

        public void LoadState()
        {
            // get all Files and use swtich to classify all build Model into house model, tavern model etc
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);

            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    Debug.Log("BuilsModel  " + contents);
                    BuildingModel buildModel = JsonUtility.FromJson<BuildingModel>(contents);
                    ClassifyAndInitBuildModel(buildModel);
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }


        }

        void ClassifyAndInitBuildModel(BuildingModel buildingModel)
        {

            switch (buildingModel.buildingName) 
            {
                case BuildingNames.None:
                    break;
                case BuildingNames.CityHall:
                    cityHallController.InitCityHallController(buildingModel); 
                    break;
                case BuildingNames.House:
                    houseController.InitHouseController(buildingModel);
                    break;
                case BuildingNames.Marketplace:
                    marketController.InitMarketController(buildingModel);
                    break;
                case BuildingNames.Safekeep:
                    //safekeepController.InitSafekeepController((SafekeepModel)buildingModel);
                    break;
                case BuildingNames.Ship:
                    shipController.InitShipController(buildingModel);
                    break;  
                case BuildingNames.Stable:
                    stableController.InitStableController(buildingModel);
                    break;
                case BuildingNames.Tavern:
                    tavernController.InitTavernController(buildingModel);
                    break;
                case BuildingNames.Temple:
                    templeController.InitTempleController(buildingModel);
                    break;
                case BuildingNames.ThievesGuild:
                    break;
                default:
                    break;
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
        }
    }
}
