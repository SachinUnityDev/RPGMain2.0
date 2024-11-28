using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public abstract class QuestBase 
    {
        public abstract QuestNames questName { get; }
        public QuestState questState { get; set; }
        public QuestModel questModel; 

        public List<ObjBase> allObjBases = new List<ObjBase>(); 

        public ObjBase GetObjBase(ObjNames objName)
        {
            int index = allObjBases.FindIndex(x => x.objName == objName);
            if(index == -1)
            {
                Debug.LogError("ObjBase not found");
                return null;
            }
            return allObjBases[index];
        }

        public  void InitQuest(QuestModel questModel)
        {
            this.questModel = questModel;
            allObjBases.Clear(); 
            foreach (ObjModel obj in questModel.allObjModel)
            {
                ObjBase objBase = QuestMissionService.Instance.questFactory.GetObjBase(questModel.questName,   obj.objName);
                objBase.InitQuest(obj);
                allObjBases.Add(objBase);
            }   
        }
            
        public abstract void QuestStarted();
        public abstract void Quest_Completed();        
    }
}