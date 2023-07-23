using Common;
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
        public QuestMode questMode;

        [Header("Bounty Quest Respawn and Unlock")]
      
        public CalDate calDate;
        public int days2Respawn;

        public QuestModel(QuestSO questSO)
        {
            questName = questSO.questName;
            QuestNameStr = questSO.questNameStr; 
            questState = questSO.questState;
            questType= questSO.questType;
            questMode= questSO.questMode;   
            foreach (ObjSO objSO in questSO.allObjSO)
            {
                ObjModel objModel = new ObjModel(objSO); 
                allObjModel.Add(objModel);
            }
           
            calDate = questSO.calDate.DeepClone();
            days2Respawn= questSO.days2Respawn;
        }

        public ObjModel GetObjModel(ObjNames objName)
        {
            int index = allObjModel.FindIndex(t=>t.objName== objName);
            if (index != -1)
                return allObjModel[index];
            else
                Debug.Log("Obj model not found" + objName);
            return null; 
        }

    }
}