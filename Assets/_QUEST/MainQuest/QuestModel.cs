using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [Serializable]
    public class QuestModel
    {
        public QuestNames questName;
        public string QuestNameStr;
        public QuestState questState;
        public List<ObjModel> allObjModel = new List<ObjModel>();
        public QuestType questType; 
        public QuestModel(QuestSO questSO)
        {
            questName = questSO.qMainName;
            QuestNameStr = questSO.QuestNameStr; 
            questState = questSO.questState;
            questType= questSO.questType;
            foreach (ObjSO objSO in questSO.allObjSO)
            {
                ObjModel objModel = new ObjModel(objSO); 
                allObjModel.Add(objModel);
            }
        }
    }
}