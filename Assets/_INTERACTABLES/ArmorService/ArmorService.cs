using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Town;
using System.IO;
namespace Interactables
{
    public class ArmorService : MonoSingletonGeneric<ArmorService>, ISaveable
    {
        // socket rules two divine and one support 
        // Btn socket for the support gem
        
        public AllArmorSO allArmorSO;

        [Header("Not TBR")]
        public ArmorModel armorModel;
        public List<ArmorModel> allArmorModels = new List<ArmorModel>();
        public List<ArmorController> allArmorController = new List<ArmorController>();
        public List<ArmorBase> allArmorBases = new List<ArmorBase>();
        // public GameObject armorPanel;
        ArmorFactory armorFactory; 
        public ArmorViewController armorViewController;

        public ServicePath servicePath => ServicePath.ArmorService; 
        public void Init()
        {
            // Add armor to all allies 
            armorFactory = GetComponent<ArmorFactory>();
       
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {

                    foreach (CharController charController in CharService.Instance.allyInPlayControllers)
                    {
                        ArmorController armorController = charController.gameObject.AddComponent<ArmorController>();
                        armorController.Init();
                        allArmorController.Add(armorController);
                    }
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



        public ArmorBase GetNewArmorBase(ArmorModel armorModel)
        {
            ArmorType armorType = armorModel.armorType;
            ArmorBase armorBase = armorFactory.GetArmorBase(armorType);         
            return armorBase;
        }

        public void OnArmorFortifyPressed(CharController charController,ArmorModel armorModel)
        {
            ArmorBase armorBase = charController.armorController.armorBase;
            armorBase.OnArmorFortify(); 
            
        }
   
        private void Start()
        {
            

        }

#region Button Controls

        public void OpenArmorPanel()
        {  
            armorViewController = InvService.Instance.invXLGO.GetComponentInChildren<ArmorViewController>();
            armorViewController.GetComponent<IPanel>().Load();
        }

        public void CloseArmorPanel()
        {
            armorViewController.GetComponent<IPanel>().UnLoad();
        }
        #endregion
        public ArmorModel GetArmorModel(CharNames charName)
        {
            int index = allArmorModels.FindIndex(t => t.charName == charName);
            if (index != -1)
            {
                return allArmorModels[index];
            }
            else
            {
                Debug.Log("armor model not found " + charName);
                return null;
            }
        }
        public bool IsArmorSocketable(CharController charController, GemNames gemName)
        {
            CharModel charModel = charController.charModel; 
                // CharService.Instance.GetAllyCharModel(charController.charModel.charName);

            return false; 
        }

        public bool SocketArmor(GemNames gemName)
        {
            //   CharNames charName = InvService.Instance.charSelect;
            CharController charController = InvService.Instance.charSelectController; 
            if (IsArmorSocketable(charController, gemName))
            {
                // get gembase enchant weapon 
                // Unlock the weapon skill
                return true;
            }
            else
            {
                // error message
                return false;
            }
        }

        public void SaveState()
        {
            if (CharService.Instance.charsInPlayControllers.Count <= 0)
            {
                Debug.LogError("no chars in play"); return;
            }
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            ClearState();
            // save all char models
            foreach (ArmorModel armorModel in allArmorModels)
            {
                string armorModelJson = JsonUtility.ToJson(armorModel);
                Debug.Log(armorModelJson);
                string fileName = path + armorModel.charName.ToString() + ".txt";
                File.WriteAllText(fileName, armorModelJson);
            }
        }

        public void LoadState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);

            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    Debug.Log("  " + contents);
                    ArmorModel armorModel = JsonUtility.FromJson<ArmorModel>(contents);
                    CharController charController = CharService.Instance.GetAllyController(armorModel.charName); 

                    ArmorController armorController = charController.gameObject.AddComponent<ArmorController>(); 
                    armorController.InitOnLoadModel(armorModel);
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
