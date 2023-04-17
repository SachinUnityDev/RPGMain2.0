using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [CreateAssetMenu(fileName = "ObjSO", menuName = "Quest/ObjSO")]
    public class ObjSO : ScriptableObject
    {
        public QuestObjNames questObj;
        [TextArea(5,10)]
        public string objName;       
        public QuestState objState;
    }
}