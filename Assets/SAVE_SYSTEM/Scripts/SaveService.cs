using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System.IO;
using System;
using UnityEngine.UI;

namespace Common
{
    public interface ISaveableService
    {
        void RestoreState();
        void ClearState();
        void SaveState(); 
    }

    public class SaveService : MonoSingletonGeneric<SaveService>
    {
        [Header("Scriptable Object")]
        public SaveSO saveSO;

        [Header("Save and Load Panel Ref")]
        public GameObject savePanel;
        public GameObject loadPanel;

        public EscapePanelController escapePanelController; 
        public SaveView saveView;
        public SaveController saveController;
        public SaveMode saveMode;
        public SaveSlot currMBloodSlot; 

        public string baseSavepath = "";

        public SaveSlot slotSelect; 
        public List<GameObject> allServices = new List<GameObject>();
        public List<ISaveableService> allSaveService = new List<ISaveableService>();

        
        public bool isLoading = false;
        public bool isSaving = false; 

        private void Start()
        {
           
           saveView.GetComponent<IPanel>().UnLoad();
           slotSelect = SaveSlot.New; 
            foreach (Transform child in saveView.gameObject.transform)
            {
                if(child.GetComponent<Button>() != null)
                    child.GetComponent<Button>().onClick.AddListener(()=>OnSlotBtnPressed(child));
            }
        }


#region 

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
        public void ShowSavePanel()
        {

            savePanel.GetComponent<IPanel>().Init();
            savePanel.GetComponent<IPanel>().Load();
           


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

            slotSelect = (SaveSlot)index;
            if(isSaving)
                SaveStateMaster();
            if(isLoading)
                LoadFileMaster();
        }

        public void SaveStateMaster()
        {
            foreach (GameObject service in allServices)
            {
                 service.GetComponent<ISaveableService>().SaveState();
            }
        }

      
        public void LoadFileMaster() 
        {
            foreach (GameObject service in allServices)
            {
                service.GetComponent<ISaveableService>().RestoreState();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                isLoading = false; isSaving = true;                
                UIControlServiceCombat.Instance.ToggleUIStateScale(saveView.gameObject, UITransformState.Open);                
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                isLoading = true; isSaving = false;
                UIControlServiceCombat.Instance.ToggleUIStateScale(saveView.gameObject, UITransformState.Open);
            }
       

        }

    }


    public enum SaveSlot
    {
        New,
        Slot1,
        Slot2,
        Slot3,
        Slot4,
        Slot5,
        Slot6,
        Slot7,
    }
    public enum SaveMode
    {
        None,
        QuickSave, // press F5
        AutoSave, // at every check point
        AutoSaveMB, // at every chekc point in MB mode .. no manual saving 
        ManualSave, // save
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