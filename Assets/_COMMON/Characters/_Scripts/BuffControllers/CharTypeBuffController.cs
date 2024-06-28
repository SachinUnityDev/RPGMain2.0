using Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace Common
{
    [Serializable]
    public class RaceCultClassBuffData
    {
        public CauseData causeData; 
        public int buffID;
        public AttribName attribName;
        public int valChg; 
        public RaceType raceType;
        public ClassType classType;
        public CultureType cultureType;

        public RaceCultClassBuffData(CauseData causeData, int buffID, RaceType raceType, AttribName attribName,
         int valChg)
        {
            this.attribName= attribName;
            this.valChg= valChg;
            this.causeData = causeData;
            this.buffID = buffID;
            this.raceType = raceType;
        }
        public RaceCultClassBuffData(CauseData causeData, int buffID, CultureType cultureType, AttribName attribName,
         int valChg)
        {
            this.attribName = attribName;
            this.valChg = valChg;
            this.causeData = causeData;
            this.buffID = buffID;
            this.cultureType = cultureType;
        }
        public RaceCultClassBuffData(CauseData causeData, int buffID, ClassType classType, AttribName attribName,
         int valChg)
        {
            this.attribName = attribName;
            this.valChg = valChg;
            this.causeData = causeData;
            this.buffID = buffID;
            this.classType = classType;
        }
    }

    [Serializable]
    public class CauseData
    {
        public CauseType causeType;  // add cause name here 
        public int causeName;
        public int causeByCharID;
        public int effectedCharNameID;

        public CauseData(CauseType causeType, int causeName, int causeByCharID, int effectedCharNameID)
        {
            this.causeType = causeType;
            this.causeName = causeName;
            this.causeByCharID = causeByCharID;
            this.effectedCharNameID = effectedCharNameID;
        }
    }
    public class CharTypeBuffController : MonoBehaviour, ISaveable
    {
        public CharTypeBuffModel charTypeBuffModel; 
        CharController charController;

        public ServicePath servicePath => ServicePath.BuffService;

        private void Start()
        {
            charController = GetComponent<CharController>();    
        }
        public void InitOnLoad(CharTypeBuffModel charTypeBuffModel)
        {
            this.charTypeBuffModel = charTypeBuffModel.DeepClone();
        }
        public void Init()
        {
            if (charTypeBuffModel == null)
            {
                int charID = charController.charModel.charID;
                charTypeBuffModel = new CharTypeBuffModel(charID); //pass in char Id      
            }
        }
        public int ApplyRaceBuff(CauseType causeType, int causeName, CharController causeByCtrl, RaceType raceType, AttribName attribName, int valChg, bool isBuff)
        {
            if (charController.charModel.raceType != raceType)
                return -1;

            int causeByCharID = causeByCtrl.charModel.charID;
            CauseData causeData = new CauseData(causeType, causeName, causeByCharID, charController.charModel.charID);
           
            int buffID = 
                charController.buffController.ApplyBuff(causeType, (int)causeName, causeByCharID,
                                                    attribName, valChg, TimeFrame.Infinity, -1, isBuff); 
           

            RaceCultClassBuffData charTypeBuffData = new RaceCultClassBuffData(causeData,buffID , raceType, attribName, valChg);
            if (charTypeBuffModel == null)
            {
                Init();
            }
            charTypeBuffModel.allBuffData.Add(charTypeBuffData);
            return buffID;
        }

        public int ApplyClassBuff(CauseType causeType, int causeName, CharController causeByCtrl,  ClassType classType, AttribName attribName, int valChg, bool isBuff)
        {
            if (charController.charModel.classType != classType)
                return -1;

            int causeByCharID = causeByCtrl.charModel.charID;
            CauseData causeData = new CauseData(causeType, causeName, causeByCharID, charController.charModel.charID);


            int buffID =
                charController.buffController.ApplyBuff(causeData.causeType, causeData.causeName, causeData.causeByCharID,
                attribName, valChg, TimeFrame.Infinity, -1, isBuff);


            RaceCultClassBuffData charTypeBuffData = new RaceCultClassBuffData(causeData, buffID, classType, attribName, valChg);
            if (charTypeBuffModel == null)
            {
                Init();
            }
            charTypeBuffModel.allBuffData.Add(charTypeBuffData);
            return buffID;
        }
        public int ApplyCultBuff(CauseType causeType, int causeName, CharController causeByCtrl, CultureType cultType, AttribName attribName, int valChg, bool isBuff)
        {
            if (charController.charModel.cultType != cultType)
                return -1;

            int causeByCharID = causeByCtrl.charModel.charID;
            CauseData causeData = new CauseData(causeType, causeName, causeByCharID, charController.charModel.charID);

            int buffID =
            charController.buffController.ApplyBuff(causeData.causeType, causeData.causeName, causeData.causeByCharID,
                attribName, valChg, TimeFrame.Infinity, -1, isBuff);


            RaceCultClassBuffData charTypeBuffData = new RaceCultClassBuffData(causeData, buffID, cultType, attribName, valChg);
            if (charTypeBuffModel == null)
            {
                Init();
            }
            charTypeBuffModel.allBuffData.Add(charTypeBuffData);
            return buffID;
        }
        public void RemoveRaceBuff(RaceType raceType) 
        {
            foreach (RaceCultClassBuffData buff in charTypeBuffModel.allBuffData)
            {
                if(buff.raceType == raceType)
                {
                    charController.buffController.RemoveBuff(buff.buffID); 
                }
            }        
        }
        public void RemoveClassBuff(ClassType classType)
        {
            foreach (RaceCultClassBuffData buff in charTypeBuffModel.allBuffData)
            {
                if (buff.classType == classType)
                {
                    charController.buffController.RemoveBuff(buff.buffID);
                }
            }
        }
        public void RemoveCultBuff(CultureType cultType)
        {
            foreach (RaceCultClassBuffData buff in charTypeBuffModel.allBuffData)
            {
                if (buff.cultureType == cultType)
                {
                    charController.buffController.RemoveBuff(buff.buffID);
                }
            }
        }

        #region SAVE_LOAD   
        public void SaveState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            string charTypeBuffPath = path + "/CharTypeBuff/";
            if(!SaveService.Instance.DirectoryExists(charTypeBuffPath))
            {
                SaveService.Instance.CreateAFolder(charTypeBuffPath);
            }
            ClearState();
            string charTypeJSON = JsonUtility.ToJson(charTypeBuffModel);
            string fileName = charTypeBuffPath + charController.charModel.charName + ".txt";
            File.WriteAllText(fileName, charTypeJSON);
        }

        public void LoadState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            string charTypeBuff = path + "/CharTypeBuff/";
            charController = GetComponent<CharController>();
            if (SaveService.Instance.DirectoryExists(charTypeBuff))
            {
                string[] fileNames = Directory.GetFiles(charTypeBuff);

                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    if (fileName.Contains(charController.charModel.charName.ToString()))
                    {
                        string contents = File.ReadAllText(fileName);
                        CharTypeBuffModel charTypeBuffModel = JsonUtility.FromJson<CharTypeBuffModel>(contents);
                        InitOnLoad(this.charTypeBuffModel);
                    }
                }
            }
            else
            {
                Debug.LogError("Service Directory missing");
            }
        }

        public void ClearState()
        {
            // clear only specific file in the given path
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            path += "/CharTypeBuff/";
            string[] fileNames = Directory.GetFiles(path);

            foreach (string fileName in fileNames)
            {
                if ((fileName.Contains(".meta")) &&
                 (fileName.Contains(charController.charModel.charName.ToString())))
                    File.Delete(fileName);
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
            if (Input.GetKeyDown(KeyCode.F6))
            {
                ClearState();
            }
        }
        #endregion
    }
} 