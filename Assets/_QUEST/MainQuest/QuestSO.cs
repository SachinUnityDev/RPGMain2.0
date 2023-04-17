using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    [CreateAssetMenu(fileName = "QuestSO", menuName = "Quest/QuestSO")]
    public class QuestSO : ScriptableObject
    {
        public QuestNames qMainName;
        public string QuestNameStr;
        public QuestState questState;
        public List<ObjSO> allObjSO = new List<ObjSO>();
        public QuestType questType; 

    }
}