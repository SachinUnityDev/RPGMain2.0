using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;


namespace Quest
{
    public class QuestMissionService : MonoSingletonGeneric<QuestMissionService>
    {
        public Action<QuestMode> OnQuestModeChg; 


        [Header("QuestMode")]
        public QuestMode currQuestMode;
        
        [Header(" Q Main ")]
        public AllQuestSO allQuestMainSO;        
        public QuestController questController;

        [Header(" Quest View")]
        public Transform QuestView;
        public QuestEmbarkView questEmbarkView;

        public List<QuestModel> allQuestModels = new List<QuestModel>();
        public List<QuestBase> allQuestBase = new List<QuestBase>();
        [SerializeField] int questBaseCount = 0; 
        public QuestFactory questFactory; 

        void Start()
        {
            currQuestMode = QuestMode.Stealth; 
            questFactory = GetComponent<QuestFactory>();
            questController = GetComponent<QuestController>();  
        }

        public void InitQuestMission()
        {
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
            foreach (QuestSO questSO in allQuestMainSO.allQuestSO)
            {
                QuestModel questModel = new QuestModel(questSO);
                allQuestModels.Add(questModel);
            }
        }
        void InitAllQuestbase()
        {
            foreach (QuestModel quest in allQuestModels)
            {
                Debug.Log("Quest name" + quest.questName);
                QuestBase qBase = questFactory.GetQuestBase(quest.questName); 
                allQuestBase.Add(qBase);
            }
            questBaseCount = allQuestBase.Count; 
        }
        public List<QuestModel> GetAllQuestModelsOfType(QuestType questType)
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

    }
}