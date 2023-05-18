using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [CreateAssetMenu(fileName = "AllQNodeSO", menuName = "Quest/AllQNodeSO")]
    public class AllQNodeSO : ScriptableObject
    {
        [Header("All SO s of all Quest Nodes")]
        public List<QNodeAllRoomSO> allQNodeSO = new List<QNodeAllRoomSO>();    


        public QNodeAllRoomSO GetQuestSceneSO(QuestNames questNames)
        {
            int index = allQNodeSO.FindIndex(t=>t.questNames== questNames); 

            if(index !=-1)
            {
                return allQNodeSO[index];
            }
            else
            {
                Debug.Log("not found Quest scene SO" + questNames);
                return null;
            }
        }
    }
}