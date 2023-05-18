using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [CreateAssetMenu(fileName = "ObjSO", menuName = "Quest/ObjSO")]
    public class ObjSO : ScriptableObject
    {
        public ObjNames objName;
        [TextArea(5,10)]
        public string objNameStr;       
        public QuestState objState;

        [Header("LLD")]
        public LocationName locationName;
        public LandscapeNames landscapeName;
        public float distanceInDays;


        [Header("Description")]
        [TextArea(5, 10)]
        public string desc;
    }
}