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
        public void ShowQuestEmbarkView(QuestNames questName, ObjNames objName, QuestNodePtrEvents nodePtrEvents)
        {
            questModel = QuestMissionService.Instance.GetQuestModel(questName);
            questBase = QuestMissionService.Instance.GetQuestBase(questName);   
            questSO= QuestMissionService.Instance.allQuestMainSO.GetQuestSO(questName);
            objModel = questModel.GetObjModel(objName);
            QuestMissionService.Instance.questEmbarkView.GetComponent<QuestEmbarkView>()
                                .ShowQuestEmbarkView(questModel, questSO, questBase,objModel, nodePtrEvents, currNodeData); 
        }

        


    }
}