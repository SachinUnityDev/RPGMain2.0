using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{

    [CreateAssetMenu(fileName = "AllPathSO", menuName = "Quest/AllPathSO")]
    public class AllPathSO : ScriptableObject
    {
        
        public List<PathSO> allPathSO= new List<PathSO>();
        
        public PathSO GetPathSO(QuestNames questName, ObjNames objName)
        {
            int index = allPathSO.FindIndex(t => t.questName == questName && t.objName == objName); 
            if(index != -1)
            {
               return allPathSO[index];
            }
            Debug.Log(" path so not found" + questName +" objname" + objName);
            return null; 
        }


    }
}