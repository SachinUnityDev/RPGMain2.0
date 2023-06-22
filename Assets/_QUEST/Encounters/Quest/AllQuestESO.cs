using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    [CreateAssetMenu(fileName = "AllQuestESO", menuName = "Quest/AllQuestESO")]
    public class AllQuestESO : ScriptableObject
    {
        public List<QuestESO> allQuestESO = new List<QuestESO>();

        public QuestESO GetQuestESO(QuestENames questEName)
        {
            int index = allQuestESO.FindIndex(t => t.questEName == questEName);
            if (index != -1)
            {
                return allQuestESO[index];
            }
            else
            {
                Debug.Log(" Quest E SO not found" + questEName);
                return null;
            }
        }
    }
}