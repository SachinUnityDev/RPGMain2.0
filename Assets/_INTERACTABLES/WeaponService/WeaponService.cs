using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Quest;
using System.IO;

namespace Interactables
{
    public class WeaponService : MonoSingletonGeneric<WeaponService>, ISaveable
    {
        public AllWeaponSO allWeaponSO;
        public List<WeaponModel> allWeaponModel = new List<WeaponModel>();
        public List<WeaponController> allWeaponController = new List<WeaponController>();
        public GameObject weaponInvPanel;

        [Header("Not TBR")]
        public WeaponViewController weaponViewController;

        public ServicePath servicePath => throw new System.NotImplementedException();

        // WeaponFactory weaponFactory; 
        public void Init()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {
                    // do nothing weapon controllers init in char service
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


        public WeaponModel GetWeaponModel(CharNames charName)
        {
            int index = allWeaponModel.FindIndex(t=>t.charName == charName);    
            if(index !=-1)           
            {
                return allWeaponModel[index];
            }
            else
            {
                Debug.Log("weapon model not found " + charName); 
                return null;
            }
        }
        public bool IsGemEnchantable(CharController charController, GemNames gemName)
        {
            CharModel charModel = charController.charModel; 
            GemNames charGemName = 
                        charModel.enchantableGem4Weapon;
            if (gemName != charGemName)
                return false;
            else
                return true;
        }
        public bool EnchantWeapon(GemNames gemName)
        {
            CharController charSelectCtrl = InvService.Instance.charSelectController;
            if (IsGemEnchantable(charSelectCtrl, gemName))
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
        #region SAVE and LOAD
        public void SaveState()
        {
            if (allWeaponModel.Count <= 0)
            {
                Debug.LogError("NO Q MISSION IN LIST"); return;
            }
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            ClearState();

            foreach (WeaponModel weaponModel in allWeaponModel)
            {
                string weaponModelJSON = JsonUtility.ToJson(weaponModel);
                string fileName = path + weaponModel.charName.ToString() + ".txt";
                File.WriteAllText(fileName, weaponModelJSON);
            }
        }

        public void LoadState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);

            if (SaveService.Instance.DirectoryExists(path))
            {
                string[] fileNames = Directory.GetFiles(path);
                allWeaponModel = new List<WeaponModel>();
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);
                    WeaponModel weaponModel = JsonUtility.FromJson<WeaponModel>(contents);
                    allWeaponModel.Add(weaponModel);                   
                }
                // find weapon Controller from weaponModel
                foreach (WeaponModel weaponModel in allWeaponModel)
                {
                    CharController charController = CharService.Instance.GetAllyController(weaponModel.charName);
                    WeaponController weaponController = charController.GetComponent<WeaponController>();
                    if(weaponController != null)
                        weaponController.InitWeaponController(weaponModel);
                    else
                        Debug.LogError("Weapon Controller not found for " + weaponModel.charName);  
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

