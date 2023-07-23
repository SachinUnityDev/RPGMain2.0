using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quest;
using Common; 

namespace Quest
{
    public class QuestController : MonoBehaviour
    {
        public QuestModel questModel;
        public QuestSO questSO;
        public QuestBase questBase;
        public ObjModel objModel;

        public NodeData currNodeData;
        private void Start()
        {
            CalendarService.Instance.OnStartOfCalDate += UnBoxBountyQuest;
            CalendarService.Instance.OnStartOfCalDate += (CalDate calDate)=> UpdateBountyQRespawn();
        }
        public void ShowQuestEmbarkView(QuestNames questName, ObjNames objName, QuestNodePtrEvents nodePtrEvents)
        {
            questModel = QuestMissionService.Instance.GetQuestModel(questName);
            questBase = QuestMissionService.Instance.GetQuestBase(questName);   
            questSO= QuestMissionService.Instance.allQuestMainSO.GetQuestSO(questName);
            objModel = questModel.GetObjModel(objName);
            QuestMissionService.Instance.questEmbarkView.GetComponent<QuestEmbarkView>()
                                .ShowQuestEmbarkView(questModel, questSO, questBase,objModel, nodePtrEvents, currNodeData); 
        }

        
        public void UnBoxBountyQuest(CalDate calDate)
        {
            foreach (QuestModel model in QuestMissionService.Instance
                .GetQModelsOfType(QuestType.Bounty))
            {
                if(model.questType == QuestType.Bounty)
                if(model.calDate.monthName == calDate.monthName
                    && model.calDate.day == calDate.day)
                {
                    model.isUnBoxed = true; 
                    QuestMissionService.Instance.On_BountyQUnboxed(model);
                }
            }
        }

        public void UpdateBountyQRespawn()
        {
            foreach (QuestModel model in QuestMissionService.Instance
                .GetQModelsOfType(QuestType.Bounty))
            {   
                if (model.questState == QuestState.Completed)
                {
                    if (model.days2Respawn >= model.dayAfterQComplete)
                    {
                        model.dayAfterQComplete = 0;
                        QuestMissionService.Instance.On_BountyQReSpawn(model);                       
                    }
                    else
                    {
                        model.dayAfterQComplete++; 
                    }
                }
            }
        }
    }
}