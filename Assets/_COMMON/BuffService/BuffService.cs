using Combat;
using Quest;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Common
{
    public class BuffService : MonoSingletonGeneric<BuffService>
    {
        // save and load all the buff Data 
        //Copy Encounter Model adn Char States as most actions will be similar
        // to Control BuffController, CharType Buff and 

        // Time Buff Controller 
        // CharTypeBuffController
        //StatBuffController
        
        //public void Init()
        //{
        //    string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
        //    if (SaveService.Instance.DirectoryExists(path))
        //    {
        //        if (IsDirectoryEmpty(path))
        //        {
        //            // loop thru all the buff Controllers and Init them 
        //            foreach(CharController charController in CharService.Instance.charsInPlayControllers)
        //            {
        //               // charController.buffController.Init(new BuffModel(charController.charModel.charID));
        //            }
        //        }
        //        else
        //        {
        //            LoadState();
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogError("Service Directory missing" + servicePath);
        //    }
        //}

        //public void ClearState()
        //{

        //}
        //public void ClearStateBuffCtrl()
        //{
        //    string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
        //    path += "/Buff";
        //    string[] fileNames = Directory.GetFiles(path);

        //    foreach (string fileName in fileNames)
        //    {
        //        File.Delete(fileName);
        //    }
        //}

        //public void LoadState()
        //{
        //    string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
        //    string buffPath = path + "/Buff";

        //    if (SaveService.Instance.DirectoryExists(buffPath))
        //    {
        //        string[] fileNames = Directory.GetFiles(buffPath);

        //        foreach (string fileName in fileNames)
        //        {
        //            // skip meta files
        //            if (fileName.Contains(".meta")) continue;

        //            string contents = File.ReadAllText(fileName);
        //            BuffModel buffModel = JsonUtility.FromJson<BuffModel>(contents);

        //            foreach (CharController charController in CharService.Instance.charsInPlayControllers)
        //            {
        //                if (charController.charModel.charID == buffModel.charID)
        //                {
        //                    charController.buffController.InitOnLoad(buffModel);
        //                }
        //            }   
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogError("Service Directory missing");
        //    }


        //}
        //void LoadStateBuffCtrl()
        //{
            
        //}
        //public void SaveState()
        //{
        //    SaveStateBuffCtrl(); 
        //}
        //public void SaveStateBuffCtrl()
        //{
        //    string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
        //    string buffPath = path + "/Buff";
            
        //    if (CharService.Instance.allyInPlayControllers.Count <= 0)
        //    {
        //        Debug.LogError("no chars in play"); return;
        //    }            
        //    ClearState();
            
        //    foreach (CharController charCtrl in CharService.Instance.allyInPlayControllers)
        //    {
        //        CharModel charModel = charCtrl.charModel;
        //        string charModelJSON = JsonUtility.ToJson(charModel);
        //        string fileName = path + charModel.charName.ToString() + ".txt";
        //        File.WriteAllText(fileName, charModelJSON);
        //    }
        //}
    }
}