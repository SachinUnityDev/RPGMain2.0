using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System;
using System.IO; 
namespace Common
{

    public class PermaTraitsService : MonoSingletonGeneric<PermaTraitsService>, ISaveable
    {      
     
        public PermaTraitsFactory permaTraitsFactory;
        public event Action<PermaTraitModel> OnPermaTraitAdded;

        public AllPermaTraitSO allPermaTraitSO;
        public List<PermaTraitController> allPermaTraitControllers = new List<PermaTraitController>();

        [Header("Perma trait Card")]
        [SerializeField] GameObject permaTraitCardPrefab; 
        public GameObject permaTraitGO;

        [Header(" List for the save service")]
        public List<PermaTraitModel> allPermaTraitModels = new List<PermaTraitModel>(); 


        public ServicePath servicePath => ServicePath.PermaTraitsService; 

        void Start()
        {
            permaTraitsFactory = GetComponent<PermaTraitsFactory>();
            CreatePermaTraitCardGO(); 
        }

        public void On_PermaTraitAdded(PermaTraitModel permaTraitModel)
        {
            allPermaTraitModels.Add(permaTraitModel);
            OnPermaTraitAdded?.Invoke(permaTraitModel);
        }

        void CreatePermaTraitCardGO()
        {
            GameObject canvasGO = GameObject.FindGameObjectWithTag("Canvas");
            if (permaTraitGO == null)
            {
                permaTraitGO = Instantiate(permaTraitCardPrefab);
            }
            permaTraitGO.transform.SetParent(canvasGO.transform);
            permaTraitGO.transform.SetAsLastSibling();
            permaTraitGO.transform.localScale = Vector3.one;
            permaTraitGO.SetActive(false);
        }
      
        public void SaveState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            
            foreach (PermaTraitModel permaTraitMdodel in allPermaTraitModels)
            {
                string permaTraitModel = JsonUtility.ToJson(permaTraitMdodel);                
                string fileName = path + permaTraitMdodel.permaTraitName +
                    "_" + permaTraitMdodel.charID + ".txt";
                File.WriteAllText(fileName, permaTraitModel);
            }
        }

        public void LoadState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            if (ChkSceneReLoad())
            {
                OnSceneReLoad(); return;
            }
            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);

                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    Debug.Log("  " + contents);
                    PermaTraitModel permaTraitModel = JsonUtility.FromJson<PermaTraitModel>(contents);

                    allPermaTraitModels.Add(permaTraitModel);
                    LoadPermaModels2Controllers(permaTraitModel);
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }

        void LoadPermaModels2Controllers(PermaTraitModel permaModels)
        {
            CharController charController
                = CharService.Instance.GetCharCtrlWithCharID(permaModels.charID);
            PermaTraitController permaTraitController = charController.permaTraitController; 
            permaTraitController.LoadController(permaModels);
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

        public bool ChkSceneReLoad()
        {
            return allPermaTraitControllers.Count > 0; 
        }

        public void OnSceneReLoad()
        {
            Debug.Log(" On Scene Reload PermaTraitsService");     
        }
    }

}
