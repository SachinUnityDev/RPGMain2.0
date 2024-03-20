using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Town;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Quest
{
    public class QuestMissionService : MonoSingletonGeneric<QuestMissionService>
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
            InitAllQuestModel();
            InitAllQuestbase();

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

        void InitAllQuestModel()
        {
            foreach (QuestSO questSO in allQuestSO.allQuestSO)
            {
                QuestModel questModel = new QuestModel(questSO);
                allQuestModels.Add(questModel);
            }
        }
        void InitAllQuestbase()
        {
            foreach (QuestModel quest in allQuestModels)
            {
                Debug.Log("Questbase name" + quest.questName);
                QuestBase qBase = questFactory.GetQuestBase(quest.questName); 
                allQuestBase.Add(qBase);
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
            questController.questModel = ChgStateOfQuest(questName, QuestState.UnLocked);
            ObjModel objModel  = questController.questModel.allObjModel[0];     ;
            On_ObjStart(questName, objModel.objName);
            OnQuestStart?.Invoke(questName);
        }
        public void On_QuestEnd(QuestNames questNames)
        {
            Debug.Log("QuestEnd");
            QuestModel questModel = GetQuestModel(questNames);
            questModel.OnQuestCompleted();
            On_ObjEnd(questNames, questController.objModel.objName);
            OnQuestEnd?.Invoke(questNames); 
        }

        public void On_ObjStart(QuestNames questName, ObjNames objName)
        {       
            ObjModel objModel = questController.questModel.GetObjModel(objName);
            objModel.OnObjStart(); 
            questController.objModel = objModel;
            if(MapService.Instance.pathController.HasPath(questName, objName)) // if obj has path as ? in map it unlocks here
            {
                MapService.Instance.pathController.On_PathUnLock(questName, objName);
            }
            OnObjStart?.Invoke(questName, objName);
        }
        public void On_ObjEnd(QuestNames  questName, ObjNames objName)
        {
            ObjModel objModel = questController.questModel.GetObjModel(objName);
            objModel.OnObjCompleted();
            questController.Move2NextObj(objModel); // seq thru all obj and mark end of QUEST in case it's the last Obj
            OnObjEnd?.Invoke(questName, objName);
        }

    

        #endregion 

    }
}