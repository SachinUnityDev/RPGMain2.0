using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;

namespace Common
{
  
    public class TempTraitService : MonoSingletonGeneric<TempTraitService>, ISaveable
    {
        //  FUNCTIONALITY 
        //  will add all temp traits to itself
        // independent mono behaviours will inform when a trait has started to this service.  

        public event Action<TempTraitBuffData> OnTempTraitStart;
        public event Action<TempTraitBuffData> OnTempTraitEnd;
        public event Action<CharController, TempTraitModel> OnTempTraitHovered;

        public GameObject tempTraitCardGO;
        public GameObject tempTraitCardPrefab; 

        public List<TempTraitController> allTempTraitControllers = new List<TempTraitController>(); 

        public TempTraitsFactory temptraitsFactory;

        public AllTempTraitSO allTempTraitSO;

        public ServicePath servicePath => ServicePath.TempTraitService; 
       

        void OnEnable()
        {   
            SceneManager.activeSceneChanged += OnSceneLoad;
        }
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneLoad;
        }
        void OnSceneLoad(Scene scene, Scene newScene)
        {
            FindTempTraitCardGO();
        }

        void Start()
        {
           //TownEventService.Instance.OnQuestBegin += temptraitsFactory.InitTempTraits;       // working 
           FindTempTraitCardGO();
        }
        public void Init()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {
                    //do nothing 
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
        public void On_TempTraitStart(TempTraitBuffData tempTraitBuffData)
        {
            OnTempTraitStart?.Invoke(tempTraitBuffData);          
        }
        public void On_TempTraitEnd(TempTraitBuffData tempTraitBuffData)
        {
            OnTempTraitEnd?.Invoke(tempTraitBuffData);           
        }
        public  TempTraitBase GetNewTempTraitBase(TempTraitName tempTraitName)
        {
            TempTraitBase tempTraitBase = temptraitsFactory.GetNewTempTraitBase(tempTraitName);            
            return tempTraitBase;
        }
        void FindTempTraitCardGO()
        {
            GameObject canvasGO = GameObject.FindGameObjectWithTag("Canvas");
            if (tempTraitCardGO == null)
            {
                tempTraitCardGO = canvasGO.transform.GetComponentInChildren<TraitCardView>(true).gameObject;
            }
            tempTraitCardGO.transform.SetParent(canvasGO.transform);
            tempTraitCardGO.transform.SetAsLastSibling();
            tempTraitCardGO.transform.localScale = Vector3.one;
            tempTraitCardGO.SetActive(false);
        }  
        public bool IsAnyOneSick()
        {
            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                TempTraitController tempTraitController = c.tempTraitController;
                foreach (TempTraitBuffData model in tempTraitController.alltempTraitBuffData)
                {
                    TempTraitSO tempSO = TempTraitService.Instance.allTempTraitSO.GetTempTraitSO(model.tempTraitName);
                    if (tempSO.tempTraitType == TempTraitType.Sickness)
                    {
                        return true; 
                    }
                }
            }
            return false; 
        }
        void SaveTempTraitData()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "TempTraitBuffData/";
            foreach (TempTraitController tempTraitController in allTempTraitControllers)
            {
                foreach (TempTraitBuffData tempTraitBuffData in tempTraitController.alltempTraitBuffData)
                {
                    string tempTraitBuffDataJSON = JsonUtility.ToJson(tempTraitBuffData);
                    Debug.Log(tempTraitBuffDataJSON);
                    string fileName = path + tempTraitBuffData.tempTraitName +
                        "_" + tempTraitBuffData.modData.effectedCharID + ".txt";
                    File.WriteAllText(fileName, tempTraitBuffDataJSON);
                }
            }
        }
        void SaveImmunityData()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "ImmunityFrmType/";

            foreach (TempTraitController tempTraitController in allTempTraitControllers)
            {
                foreach (ImmunityFrmType immunityFrmTypeData in tempTraitController.allImmunitiesFrmType)
                {
                    string immunityJSON = JsonUtility.ToJson(immunityFrmTypeData);
                    Debug.Log(immunityJSON);
                    string fileName = path + immunityFrmTypeData.traitType
                                             + "_" + immunityFrmTypeData.modData.effectedCharID + ".txt";
                    File.WriteAllText(fileName, immunityJSON);
                }
            }
        }
        public void SaveState()
        {
            ClearState();
            SaveTempTraitData();
            SaveImmunityData();
        }
        void LoadStateImmunityFrmType()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "ImmunityFrmType/";
            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);

                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    Debug.Log("  " + contents);
                    ImmunityFrmType immunityFrmType = JsonUtility.FromJson<ImmunityFrmType>(contents);                  
                   LoadImmunityBuffData2Ctrl(immunityFrmType);
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }

        void LoadStateImmunityData()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "ImmunityData/";
            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    Debug.Log("  " + contents);
                    TempTraitBuffData TempTraitBuffData
                        = JsonUtility.FromJson<TempTraitBuffData>(contents);
                    LoadTempTraitImmunityBuffData2Controller(TempTraitBuffData);
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }
        void LoadImmunityBuffData2Ctrl(ImmunityFrmType immunityFrmType)
        {
            CharController charController
                = CharService.Instance.GetCharCtrlWithCharID(immunityFrmType.modData.effectedCharID);
            TempTraitController tempTraitController = charController.tempTraitController; 
            tempTraitController.LoadImmunityFrmData(immunityFrmType);
        }

        void LoadTempTraitBuffData2Controller(TempTraitBuffData tempTraitBuffData)
        {
            CharController charController
                = CharService.Instance.GetCharCtrlWithCharID(tempTraitBuffData.modData.effectedCharID);

            TempTraitController tempTraitController = charController.tempTraitController;
            tempTraitController.LoadTempTraitBuffData(tempTraitBuffData);
        }
        void LoadTempTraitImmunityBuffData2Controller(TempTraitBuffData tempTraitBuffData)
        {
            CharController charController
                = CharService.Instance.GetCharCtrlWithCharID(tempTraitBuffData.modData.effectedCharID);

            TempTraitController tempTraitController = charController.tempTraitController;
            tempTraitController.LoadTempTraitBuffData(tempTraitBuffData);
        }
        void LoadStateTempTraitBuffMod()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "TempTraitBuffData/";
            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    Debug.Log("  " + contents);
                    TempTraitBuffData TempTraitBuffData
                        = JsonUtility.FromJson<TempTraitBuffData>(contents);
                    LoadTempTraitBuffData2Controller(TempTraitBuffData);
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }
        public void LoadState()
        {
            // loop thru all ontroller and clear
            foreach (TempTraitController tempTraitController in allTempTraitControllers)
            {
                tempTraitController.ClearOldState();
            }
            LoadStateTempTraitBuffMod();
            LoadStateImmunityFrmType();
            LoadStateImmunityData(); 
        }

        void ClearTempTraitData()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "TempTraitBuffData/";
            DeleteAllFilesInDirectory(path);
        }
        void ClearImmunityTypeData()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "ImmunityFrmType/";
            DeleteAllFilesInDirectory(path);
        }
        void ClearImmunityData()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "ImmunityData/";
            DeleteAllFilesInDirectory(path);
        }

        public void ClearState()
        {
            ClearTempTraitData();
            ClearImmunityTypeData();
            ClearImmunityData(); 
        }

        public TempTraitController GetTempTraitController(int charID)
        {
            CharController charController = CharService.Instance.GetCharCtrlWithCharID(charID);
            if (charController != null)
            {
                return charController.tempTraitController;
            }
            else
            {
                Debug.Log(" Handle the case when the CharController is not found");
                return null; 
            }
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

