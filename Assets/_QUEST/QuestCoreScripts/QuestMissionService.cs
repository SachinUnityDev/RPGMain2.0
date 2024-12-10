using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Town;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Quest
{
    public class QuestMissionService : MonoSingletonGeneric<QuestMissionService>, ISaveable

    {
        public Action<QuestNames> OnQuestStart; 
        public Action<QuestNames> OnQuestEnd;

        public Action<QuestNames, ObjNames> OnObjStart;
        public Action<QuestNames, ObjNames> OnObjEnd;


        public Action<QuestMode> OnQuestModeChg;
        public Action<QuestModel> OnBountyQUnboxed;
        public Action<QuestModel> OnBountyQReSpawn; 


        [Header("QuestMode")]
        public QuestMode currQuestMode;
        
        [Header(" Q Main TBR")]
        public AllQuestSO allQuestSO;        
        public QuestController questController;

        [Header(" Q Factory TBR")]
        public QuestFactory questFactory;

        [Header(" Quest View")]
        public Transform questView;
        public QuestEmbarkView questEmbarkView;

        public List<QuestModel> allQuestModels = new List<QuestModel>();
        public List<QuestBase> allQuestBase = new List<QuestBase>();
        [SerializeField] int questBaseCount = 0;

        [Header(" Current Quest")]
        public QuestModel questModel; 
        public ServicePath servicePath => ServicePath.QuestMissionService; 
        void Start()
        {
          SceneManager.sceneLoaded += OnSceneLoaded;    
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "TOWN")
            {
                questView = FindObjectOfType<QuestView>(true).transform;
                questEmbarkView = FindObjectOfType<QuestEmbarkView>(true); 
            }
        }
        public void InitQuestMission()
        {
            questController= GetComponent<QuestController>();   
            questFactory = GetComponent<QuestFactory>();
            questFactory.InitQuest();
            On_QuestModeChg(QuestMode.Exploration);
            
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            if (SaveService.Instance.DirectoryExists(path))
            {
                if (IsDirectoryEmpty(path))
                {
                    InitAllQuestModelFrmSO();
                    InitAllQuestbase();
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
        public void On_QuestModeChg(QuestMode questMode)
        {
            currQuestMode= questMode;
            OnQuestModeChg?.Invoke(questMode);

        }
        public QuestBase GetQuestBase(QuestNames questName)
        {
            int index = allQuestBase.FindIndex(t=>t.questName == questName);
            if(index != -1)
            {
                return allQuestBase[index];
            }
            else
            {
                Debug.Log("Quest base not found" + questName);
                return null; 
            }
        }
        public QuestModel ChgStateOfQuest(QuestNames questName, QuestState questState)
        {
            int index = allQuestModels.FindIndex(t => t.questName == questName);
            if (index != -1)
            {
                allQuestModels[index].questState= questState;
                return allQuestModels[index];
            }
            else
            {
                Debug.Log("Quest model not found" + questName);
            }
            return null;
        }
        public QuestModel GetQuestModel(QuestNames questName)
        {
            int index = allQuestModels.FindIndex(t=>t.questName == questName);
            if(index != -1)
            {
                return allQuestModels[index];   
            }
            else
            {
                Debug.Log("Quest model not found" + questName); 
            }
            return null; 
        }

        void InitAllQuestModelFrmSO()
        {
            allQuestModels.Clear();
            foreach (QuestSO questSO in allQuestSO.allQuestSO)
            {
                QuestModel questModel = new QuestModel(questSO);
                allQuestModels.Add(questModel);
            }
        }
    
        void InitAllQuestbase()
        {
            foreach (QuestModel questModel in allQuestModels)
            {
                Debug.Log("Questbase name" + questModel.questName);
                QuestBase qBase = questFactory.GetQuestBase(questModel.questName); 
                allQuestBase.Add(qBase);
                qBase.InitQuest(questModel);
            }
            questBaseCount = allQuestBase.Count; 
        }
        public List<QuestModel> GetQModelsOfType(QuestType questType)
        {
            List<QuestModel> questModels = new List<QuestModel>();  
            questModels = allQuestModels.Where(t=>t.questType == questType).ToList();
            return questModels;
        }
        public List<QuestMode> GetOtherQMode()
        {
            List<QuestMode> questModes = new List<QuestMode>(); 
            for (int i = 1; i < Enum.GetNames(typeof(QuestMode)).Length; i++)
            {
                QuestMode questModeN = (QuestMode)i;
                if(currQuestMode != questModeN)
                        questModes.Add(questModeN);
            }
            return questModes;
        }

        #region

        public void On_BountyQUnboxed(QuestModel questModel)
        {
            questModel.questState = QuestState.Locked; 
            OnBountyQUnboxed?.Invoke(questModel);
        }
        public void On_BountyQReSpawn(QuestModel questModel)
        {
            questModel.isUnBoxed = true;
            questModel.questState= QuestState.Locked;
            OnBountyQReSpawn?.Invoke(questModel);
        }

        #endregion


        #region FLEE 
        public void On_FleePressedInQuest()
        {
            // Abbas and whole party flee and Quest obj FAILS 
            // Quest Fail result go back to town




        }
        #endregion
        public void On_QuestResult(bool isSucccess)
        {
            // go to map 
            if (isSucccess)
            {
                // map moves to the town and return journey begin with encounters as defined
            }
            else
            {
                // map head goes to town no further meetings
            }
            // time in quest return journey to be added 

            // in town view for Quest failed return 
        }

        #region START and END OF the Quest

        public void On_QuestStart(QuestNames questName)
        {   
            ChgStateOfQuest(questName, QuestState.UnLocked);
            questModel = GetQuestModel(questName);
           
            QuestBase qBase = GetQuestBase(questName);
            qBase.QuestStarted();
            
            ObjModel objModel  = questModel.allObjModel[0];     ;
            On_ObjStart(questName, objModel.objName);

            OnQuestStart?.Invoke(questName);
        }
        public void On_QuestEnd(QuestNames questNames)
        {
            Debug.Log("QuestEnd");
            QuestModel questModel = GetQuestModel(questNames);
            questModel.OnQuestCompleted();
            QuestBase qBase = GetQuestBase(questNames);
            qBase.Quest_Completed(); 

           // On_ObjEnd(questNames, questController.objModel.objName);
            OnQuestEnd?.Invoke(questNames); 
        }

        public void On_ObjStart(QuestNames questName, ObjNames objName)
        {       
            ObjModel objModel = GetObjModel(questName, objName);
            //objModel.OnObjStart(); 
            ObjBase objBase = GetObjBase(questName, objName);   
            objBase.ObjStart();   
            questController.objModel = objModel;
     

            if(MapService.Instance.pathController.HasPath(questName, objName)) // if obj has path as ? in map it unlocks here
            {
                MapService.Instance.pathController.On_PathUnLock(questName, objName);
            }
            OnObjStart?.Invoke(questName, objName);
        }
        public void On_ObjEnd(QuestNames  questName, ObjNames objName)
        {
            Debug.Log(questName + " " + objName + " ");
            
            ObjBase objBase = GetObjBase(questName, objName);            
            objBase.ObjComplete();
            ObjModel objModel = GetObjModel(questName, objName);
            //if(objModel != null)
            //objModel.OnObjCompleted();
            //else
            //{
            //    Debug.LogError("ObjModel is nullc" + objName + questName);
            //}
            questController.Move2NextObj(questName, objName); // seq thru all obj and mark end of QUEST in case it's the last Obj

            try
            {
                OnObjEnd?.Invoke(questName, objName);
            }
            catch(Exception e)
            {
                Debug.LogError("OnObjEnd!!!" + e.Message);
                Debug.LogError("OnObjEnd" + e.StackTrace);
            }
            
        }
        public ObjModel GetObjModel(QuestNames questName, ObjNames objName)
        {
            QuestModel questModel = allQuestModels.Find(t => t.questName == questName);
            ObjModel objModel = questModel.allObjModel.Find(t => t.objName == objName);
            return objModel; 
        }

        public ObjBase GetObjBase(QuestNames questName, ObjNames objName)
        {
            QuestBase questBase = GetQuestBase(questName);
            ObjBase objBase = questBase.allObjBases.Find(t => t.objName == objName);
            return objBase; 
        }

        #region SAVE and LOAD
        public void SaveState()
        {
            if (allQuestModels.Count <= 0)
            {
                Debug.LogError("NO Q MISSION IN LIST"); return;
            }
            string path = SaveService.Instance.GetCurrSlotServicePath(servicePath);
            ClearState();
            
            foreach (QuestModel questModel in allQuestModels)
            {
                string questModelJSON = JsonUtility.ToJson(questModel);                
                string fileName = path + questModel.questName.ToString() + ".txt";
                File.WriteAllText(fileName, questModelJSON);
            }
        }
        public bool ChkSceneReLoad()
        {
            return allQuestModels.Count > 0; // if model list is pre populated then scene is reloaded  
        }

        public void OnSceneReLoad()
        {
            if (allQuestBase.Count == 0)
            {
                InitAllQuestbase();
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
                allQuestModels = new List<QuestModel>();
                foreach (string fileName in fileNames)
                {
                    // skip meta files
                    if (fileName.Contains(".meta")) continue;
                    string contents = File.ReadAllText(fileName);                   
                    QuestModel questModel = JsonUtility.FromJson<QuestModel>(contents);
                    allQuestModels.Add(questModel);
                }
                if(allQuestBase.Count == 0)
                {
                    InitAllQuestbase();
                }
                //else
                //{ // align base and Models
                //    foreach (QuestModel questModel in allQuestModels)
                //    {
                //        QuestBase qBase = GetQuestBase(questModel.questName);
                //        qBase.questState = questModel.questState;
                //    }
                //}
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