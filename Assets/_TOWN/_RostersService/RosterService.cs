using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Town;
using System;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;

namespace Common
{
    // money rule to pay for the chars who are not their base location

    public class RosterService : MonoSingletonGeneric<RosterService>, ISaveable
    {
        public event Action <bool> OnPortraitDragResult;
        public event Action<CharModel> OnRosterScrollCharSelect;  // should activate on succes ful drop 
        public RosterModel rosterModel; 
        public RosterController rosterController;
        public RosterViewController rosterViewController;

        [Header("TO BE REF")]
        public GameObject rosterPrefab;   
        public RosterSO rosterSO;

        public GameObject rosterPanel;

        [Header("Drag and Drop ref")]
        public GameObject draggedGO;
        public PortraitDragNDrop portraitDragNDrop;

        [Header("Global Var")]
        public CharModel scrollSelectCharModel;
        public ServicePath servicePath => ServicePath.RosterService;
        void Start()
        {
            RosterModel rosterModel = new RosterModel();
            SceneManager.sceneLoaded += OnSceneLoaded; 
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "TOWN")
            {
                rosterViewController = FindObjectOfType<RosterViewController>(true);

                rosterViewController.GetComponent<IPanel>().Init();
            }
        }
        public void On_ScrollSelectCharModel(CharModel charModel)
        {
            scrollSelectCharModel = charModel;
            OnRosterScrollCharSelect?.Invoke(charModel);
   
            
        }
        public void Init()
        {
            //save Service implementation pending here
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {
                    
                    rosterModel = new RosterModel();    
                      
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
        
        
        public bool AddChar2Party(CharNames charNames)
        {
            if (CharService.Instance.isPartyLocked) return false;
                CharController charController = CharService.Instance.GetAllyController(charNames);
            if(charNames != CharNames.Abbas) // test fame behaviour for non abbas chars 
                if (!FameService.Instance.fameController.IsFameBehaviorMatching(charController)) return false; 

            if(rosterModel.charInParty.Contains(charNames)) return false;   
           rosterModel.charInParty.Add(charNames);
            CharService.Instance.On_CharAddToParty(charController);             
            return true; 
        }
        public bool RemoveCharFromParty(CharNames charNames)
        {
            if (CharService.Instance.isPartyLocked) return false;
            CharController charController = CharService.Instance.GetAllyController(charNames);
           rosterModel.charInParty.Remove(charNames);
            CharService.Instance.On_CharRemovedFrmParty(charController);
            return true;
        }
        public void On_PortraitDragResult(bool result)// connect this to doMove and
        {
            OnPortraitDragResult?.Invoke(result);
            Debug.Log("DRAGGGGG" + result); 
           // On_SelectCharModel(selectCharModel);
        }
        public void OpenRosterView()
        {
            if(rosterPanel == null)
            {
                rosterPanel = Instantiate(rosterPrefab);
                RectTransform rect = rosterPanel.GetComponent<RectTransform>();
                rect.anchoredPosition = Vector3.zero;
                rect.localScale = Vector3.one;

                // Rect settings for anchor and pivot to be done for UI Service
            }
            UIControlServiceGeneral.Instance.SetMaxSibling2Canvas(rosterPanel);
            rosterPanel.SetActive(true);
            rosterViewController = rosterPanel.GetComponent<RosterViewController>();
            rosterViewController.GetComponent<IPanel>().Init(); 

        }
        public void CloseRosterView()
        {
            rosterViewController.GetComponent<IPanel>().UnLoad();
        }

        #region SAVE_LOAD SERVICES
        public void LoadState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            rosterModel = new RosterModel();
            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    Debug.Log("  " + contents);
                    rosterModel = JsonUtility.FromJson<RosterModel>(contents);                    
                }
                foreach (CharNames charName in rosterModel.charInParty)
                {
                    CharController charController = CharService.Instance.GetAllyController(charName);
                    CharService.Instance.On_CharAddToParty(charController);
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
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            ClearState();
            string rosterModelJSON = JsonUtility.ToJson(rosterModel);
            string fileName = path + "RosterModel" + ".txt";
            File.WriteAllText(fileName, rosterModelJSON);
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

        #endregion

    }



}

