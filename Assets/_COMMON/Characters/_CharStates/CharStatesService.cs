using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Combat;
using System.IO; 



namespace Common
{
    public class CharStatesService : MonoSingletonGeneric<CharStatesService>, ISaveable
    {
        // apply n remove char States....Start Here.. 
        // event and view control 
        // view control .. hover upon a Icon the scripts directs here with controller name and stateName
        // view controll logic easy access point 

        public event Action<CharStateModData> OnCharStateStart;  // events will drive the animations
        public event Action<CharStateModData> OnCharStateEnd;   

        public event Action<ImmunityBuffData> OnImmunityBuffStart;  
        public event Action<ImmunityBuffData> OnImmunityBuffEnd;    

        public event Action<CharStateName, CharController> OnStateHovered; 

        public List<CharStateSO1> allCharStateSOs = new List<CharStateSO1>();

        public AllCharStateSO allCharStateSO; 
        public CharStatesFactory charStateFactory;


        [Header(" all List for save and load")]
        public List<CharStateController> allCharStateController = new List<CharStateController>();
        public List<CharStateBuffData> allCharStateModData = new List<CharStateBuffData>(); // All char State In Use
        public List<ImmunityBuffData> allImmunityBuffData = new List<ImmunityBuffData>(); // All immunity buff in use

        public List<CharStateModel> allCharStateModel = new List<CharStateModel>(); // All char State In Use

        public ServicePath servicePath => ServicePath.CharStatesService;
        void Start()
        {
            charStateFactory = GetComponent<CharStatesFactory>();         
        }

        public void On_CharStateStart(CharStateBuffData charStateBuffData)
        {
            OnCharStateStart?.Invoke(charStateBuffData.charStateModData); 
            allCharStateModData.Add(charStateBuffData);
        }

        public void On_CharStateEnd(CharStateBuffData charStateBuffData)
        {
            OnCharStateEnd?.Invoke(charStateBuffData.charStateModData);
            allCharStateModData.Remove(charStateBuffData);
        }

        public void On_ImmunityBuffStart(ImmunityBuffData immunityBuffData)
        {
            OnImmunityBuffStart?.Invoke(immunityBuffData);
            allImmunityBuffData.Add(immunityBuffData);
        }
        public void On_ImmunityBuffEnd(ImmunityBuffData immunityBuffData)
        {
            OnImmunityBuffEnd?.Invoke(immunityBuffData);
            allImmunityBuffData.Remove(immunityBuffData);
        }

        public CharStatesBase GetNewCharState(CharStateName charStateName)
        {
            CharStatesBase charStateBase = charStateFactory.GetCharState(charStateName);
            // create a charState Model and Add to the list
            return charStateBase;   
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
        void SaveCharStateData()
        {            
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "CharStateModData/";
            
            foreach (CharStateBuffData charStateBuffData in allCharStateModData)
            {                
                string charStateModJSON = JsonUtility.ToJson(charStateBuffData);
                Debug.Log(charStateModJSON);
                string fileName = path + charStateBuffData.charStateModData.charStateName +
                    "_"+charStateBuffData.charStateModData.effectedCharID + ".txt";
                File.WriteAllText(fileName, charStateModJSON);
            }
        }
        void SaveImmunityData()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "Immunity/";

            foreach (ImmunityBuffData immunityData in allImmunityBuffData)
            {
                string immunityJSON = JsonUtility.ToJson(immunityData);
                Debug.Log(immunityJSON);
                string fileName = path + immunityData.charStateModData.charStateName
                                         + "_" + immunityData.charStateModData.effectedCharID + ".txt";
                File.WriteAllText(fileName, immunityJSON);
            }
        }
        public void SaveState()
        {
            ClearState(); 
            SaveCharStateData();
            SaveImmunityData();
        }
        void LoadStateImmunity()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "Immunity/";
            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);

                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    Debug.Log("  " + contents);
                    ImmunityBuffData immunityBuffData = JsonUtility.FromJson<ImmunityBuffData>(contents);
                    allImmunityBuffData.Add(immunityBuffData);
                    LoadImmunityBuffData2Ctrl(immunityBuffData);
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }

        void LoadImmunityBuffData2Ctrl(ImmunityBuffData immunityBuffData)
        {
            CharController charController
                = CharService.Instance.GetCharCtrlWithCharID(immunityBuffData.charStateModData.effectedCharID);
            CharStateController charStateController = charController.charStateController;
            charStateController.LoadImmunityBuffData(immunityBuffData);
        }   

        void LoadCharStateBuffData2Controller(CharStateBuffData charStateBuffData)
        {            
            CharController charController 
                = CharService.Instance.GetCharCtrlWithCharID(charStateBuffData.charStateModData.effectedCharID);

            CharStateController charStateController = charController.charStateController;   
            charStateController.LoadCharStateBuffData(charStateBuffData);
        }

        void LoadStateCharStateMod()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "CharStateModData/";    
            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    Debug.Log("  " + contents);
                    CharStateBuffData charStateBuffData 
                        = JsonUtility.FromJson<CharStateBuffData>(contents);
                    allCharStateModData.Add(charStateBuffData);
                    LoadCharStateBuffData2Controller(charStateBuffData);    
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }
        public void LoadState()
        {
            // loop thru all charStateController and clear
            if (ChkSceneReLoad())
            {
                OnSceneReLoad();
                return;
            }

            foreach (CharStateController charStateController in allCharStateController)
            {
                charStateController.ClearOldState();
            }
            LoadStateCharStateMod();    
            LoadStateImmunity();
        }

        void ClearCharStateData()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "CharStateModData/";
            DeleteAllFilesInDirectory(path);
        }
        void ClearImmunityBuffData()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "Immunity/";    
            DeleteAllFilesInDirectory(path);
        }
        public void ClearState()
        {       
            ClearCharStateData();
            ClearImmunityBuffData();
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

        public bool ChkSceneReLoad()
        {
            return allCharStateModel.Count > 0;
        }

        public void OnSceneReLoad()
        {
            Debug.Log("scene reloaded CharStatesService"); 
        }
    }


}