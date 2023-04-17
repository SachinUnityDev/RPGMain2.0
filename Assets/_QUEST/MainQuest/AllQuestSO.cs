using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Quest
{
    [CreateAssetMenu(fileName = "AllQuestMainSO", menuName = "Quest/AllQuestMainSO")]

    public class AllQuestSO : ScriptableObject
    {
        public List<QuestSO>  allQuestSO = new List<QuestSO>();
        public QuestSO GetQuestSO(QuestNames questName)
        {
            int index = 
                    allQuestSO.FindIndex(t=>t.qMainName== questName);
            if(index != -1)
            {
                return allQuestSO[index];   
            }
            Debug.Log("Quest SO not found "); 
            return null;
        }
        public List<QuestSO> GetAllQuestTypes(QuestType questType)
        {
            List<QuestSO> questSOs = new List<QuestSO>();   
            questSOs =
                 allQuestSO.Where(t => t.questType == questType).ToList();
            return questSOs;
        }


    }
}