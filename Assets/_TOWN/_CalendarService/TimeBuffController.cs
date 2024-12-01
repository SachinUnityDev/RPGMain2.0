using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Town;
using System.Linq;
using System.IO;

namespace Common
{
    public class TimeBuffModel
    {
        public int charID = -1; 
        public List<DayBuffData> allBuffData; 

        public TimeBuffModel(int charID)
        {
            allBuffData = new List<DayBuffData>();
            this.charID = charID;
        }
    }


    [Serializable]
    public class DayBuffData
    {
        public CauseData causeData;
        public int buffID;
        public DayName dayName;
        public AttribName attribName;
        public int valChg;
        public int minValChg;
        public int maxValChg;
        public TimeFrame timeFrame;
        public int timeVal;
        public int currTime = 0; 
        public bool isBuff;

        public DayBuffData(CauseData causeData, int buffID, DayName dayName, AttribName attribName
            , int valChg, TimeFrame timeFrame, int timeVal, bool isBuff)
        {
            this.causeData = causeData;
            this.buffID = buffID;
            this.dayName = dayName;
            this.attribName = attribName;
            this.valChg = valChg;
            this.timeFrame = timeFrame;
            this.timeVal = timeVal;
            this.isBuff = isBuff;
        }

        public DayBuffData(CauseData causeData, int buffID, DayName dayName, AttribName attribName
                            , int minValChg, int maxValChg, TimeFrame timeFrame, int timeVal, bool isBuff)
        {
            this.causeData = causeData;
            this.buffID = buffID;
            this.dayName = dayName;
            this.attribName = attribName;
            this.minValChg = minValChg;
            this.maxValChg = maxValChg;
            this.timeFrame = timeFrame;
            this.timeVal = timeVal;
            this.isBuff = isBuff;
        }
    }
    public class TimeBuffController : MonoBehaviour, ISaveable
    {

        CharController charController;
        public TimeBuffModel timeBuffModel =null;
        public ServicePath servicePath => ServicePath.BuffService;

        private void Start()
        {
            CalendarService.Instance.OnStartOfCalDay +=  DayTickSub;
            charController = GetComponent<CharController>();
        }
        private void OnDisable()
        {
            CalendarService.Instance.OnStartOfCalDay -= DayTickSub;
        }
        public void InitOnLoad(TimeBuffModel timeBuffModel)
        {
            this.timeBuffModel = timeBuffModel.DeepClone();
        }
        public void Init()
        {
            if (timeBuffModel == null)
            {
                charController = GetComponent<CharController>();
                int charID = charController.charModel.charID;
                timeBuffModel = new TimeBuffModel(charID); //pass in char Id      
            }
        }

        public int ApplyDayBuff(CauseType causeType, int causeName, int causebyCharID, DayName dayName
                               , AttribName attribName, int valChg, int timeVal, bool isBuff)
        {
            charController = GetComponent<CharController>();

            CauseData causeData = new CauseData(causeType, causeName, causebyCharID, charController.charModel.charID);

            int buffID =
                    charController.buffController.ApplyBuff(causeType, (int)causeName, causebyCharID,
                                                     attribName, valChg, TimeFrame.EndOfDay, timeVal, isBuff);

            DayBuffData dayBuffData = new DayBuffData(causeData, buffID, dayName, attribName
                                                        , valChg, TimeFrame.EndOfDay, timeVal, isBuff);
            if(timeBuffModel == null)
            {
                Init();
            }
            timeBuffModel.allBuffData.Add(dayBuffData);
            return buffID;
        }

        void DayTickSub(int day)
        {
            DayTick(); 
        }
        public void DayTick()
        {
            foreach (DayBuffData buff in timeBuffModel.allBuffData.ToList())
            {
                if (buff.timeFrame == TimeFrame.EndOfDay && buff.currTime >= buff.timeVal)
                {                
                    RemoveBuffPerma(buff.buffID);
                }
                else
                {
                    buff.currTime++; 
                }
            }
        }
        // create a day tick... 
        void RemoveBuffFX(int buffID)
        {
            charController.buffController.RemoveBuff(buffID);
        }
        public void RemoveBuffPerma(int buffID)
        {
            RemoveBuffFX(buffID);
            int index = timeBuffModel.allBuffData.FindIndex(t => t.buffID == buffID);
            if (index != -1)
                timeBuffModel.allBuffData.RemoveAt(index);
            else
                Debug.Log("buff not found" + buffID);
        }
        #region SAVE_LOAD   
        public void SaveState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            string timeBuffPath = path + "/TimeBuff/";

            ClearState();
            string buffModelJSON = JsonUtility.ToJson(timeBuffModel);
            string fileName = timeBuffPath + charController.charModel.charName + ".txt";
            File.WriteAllText(fileName, buffModelJSON);
        }

        public void LoadState()
        {
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            string buffPath = path + "/TimeBuff/";
            charController = GetComponent<CharController>();

            if (ChkSceneReLoad())
            {
                OnSceneReLoad();
                return;
            }
            if (SaveService.Instance.DirectoryExists(buffPath))
            {
                string[] fileNames = Directory.GetFiles(buffPath);

                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    if (fileName.Contains(charController.charModel.charName.ToString()))
                    {
                        string contents = File.ReadAllText(fileName);
                        TimeBuffModel timeBuffModel = JsonUtility.FromJson<TimeBuffModel>(contents);
                        InitOnLoad(timeBuffModel);
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
            path += "/TimeBuff/";
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

        public bool ChkSceneReLoad()
        {
            return timeBuffModel != null; 
        }

        public void OnSceneReLoad()
        {
            Debug.Log("scene reloaded TimeBuffController"); 
        }
        #endregion
    }
}