using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbarkInfo
{
    public LocationName locationName;
    public LandscapeNames landscapeNames; 
    public float distanceInDays; 
}

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

        [Header("Embark Info")]
        public EmbarkInfo embarkInfo;

        public bool hasCamp;
        public QuestMode questMode; 

    }
}