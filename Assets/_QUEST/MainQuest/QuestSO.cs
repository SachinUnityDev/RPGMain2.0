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
        public QuestNames questName;
        public string questNameStr;
        public QuestState questState;
        public List<ObjSO> allObjSO = new List<ObjSO>();
        public QuestType questType;

        [Header("Embark Info")]
        public EmbarkInfo embarkInfo;

        public bool hasCamp;
        public QuestMode questMode;

        [Header("Exp gained")]
        public int expBase;
        public int expMultiplier;

        [Header("Bounty Quest Respawn and Unlock")]
        public bool isUnBoxed = false; 
        public CalDate calDate; 
        public int days2Respawn; 


        public ObjSO GetObjSO(ObjNames objName)
        {
            int index =
                allObjSO.FindIndex(t => t.objName == objName); 

            if(index != -1)
            {
               return allObjSO[index];
            }
            else
            {
                Debug.Log("Quest Obj SO not found" + objName); 
            }
            return null; 
        }
      
    }
}