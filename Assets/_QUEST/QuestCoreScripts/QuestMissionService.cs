using Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;


namespace Quest
{
    public class QuestMissionService : MonoSingletonGeneric<QuestMissionService>
    {
        public QuestMode questMode;
        
        [Header(" Q Main ")]
        public AllQuestSO allQuestMainSO;        
        public QuestController qMainController;

        [Header(" Quest View")]
        public Transform QuestMissionView;

        public List<QuestModel> allQuestModels = new List<QuestModel>();

        void Start()
        {
            qMainController = gameObject.AddComponent<QuestController>();

            InitAllQuest(); 
        }

        void InitAllQuest()
        {
            foreach (QuestSO questSO in allQuestMainSO.allQuestSO)
            {
                QuestModel questModel = new QuestModel(questSO);
                allQuestModels.Add(questModel);
            }
        }

        public List<QuestModel> GetAllQuestModelsOfType(QuestType questType)
        {
            List<QuestModel> questModels = new List<QuestModel>();  
            questModels = allQuestModels.Where(t=>t.questType == questType).ToList();
            return questModels;
        }
    }
}