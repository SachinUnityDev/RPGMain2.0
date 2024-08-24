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

       // public NodeData currNodeData;
        private void Start()
        {
            CalendarService.Instance.OnStartOfCalDate += UnBoxBountyQuest;
            CalendarService.Instance.OnStartOfCalDate += (CalDate calDate)=> UpdateBountyQRespawn();
        }


        public void Move2NextObj(ObjModel objModel)
        {
            int index = questModel.allObjModel.FindIndex(t=> t == objModel);
            if(index != -1)
            {
                if(index < questModel.allObjModel.Count-1)
                {
                    QuestMissionService.Instance.On_ObjStart(questModel.questName, questModel.allObjModel[index + 1].objName);
                }
                else
                {
                    QuestMissionService.Instance.On_QuestEnd(questModel.questName); 
                }
            }
            else
            {
                Debug.LogError(" Obj not found" + objModel.objName); 
            }
        }

        public void ShowQuestEmbarkView(QuestNames questName, ObjNames objName, QMarkView qMarkView)
        {
            questModel = QuestMissionService.Instance.GetQuestModel(questName);
            questBase = QuestMissionService.Instance.GetQuestBase(questName);   
            questSO= QuestMissionService.Instance.allQuestSO.GetQuestSO(questName);
            objModel = questModel.GetObjModel(objName);
            
            QuestEmbarkView qEView = QuestMissionService.Instance.questEmbarkView; 
            qEView.ShowQuestEmbarkView(questModel, questSO, questBase, objModel, qMarkView); 
        }

        
        public void UnBoxBountyQuest(CalDate calDate)
        {
            foreach (QuestModel model in QuestMissionService.Instance.GetQModelsOfType(QuestType.Bounty))
            {
                if(model.questType == QuestType.Bounty)
                if(model.calDate.monthName == calDate.monthName && model.calDate.day == calDate.day)
                {
                    model.isUnBoxed = true; 
                    QuestMissionService.Instance.On_BountyQUnboxed(model);
                }
            }
        }
        public void UnBoxBountyQuest(QuestNames questName)
        {
            QuestModel questModel = QuestMissionService.Instance.GetQuestModel(questName);
            questModel.isUnBoxed = true; 
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