using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

namespace Quest
{
    public abstract class QuestBase 
    {
        public abstract QuestNames questName { get; }
        public QuestState questState { get; set; }
        public QuestModel questModel; 
        public  void InitQuest(QuestModel questModel)
        {
            this.questModel = questModel;
        }
        public abstract void StartObj(ObjNames objName);
        public abstract void OnObj_Completed(ObjNames objNames);
        public abstract void OnObj_Failed(ObjNames objNames);
        public abstract void EndQuest(); 
    }
}