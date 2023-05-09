using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace Quest
{
    public class QuestFactory : MonoBehaviour
    {

        public Dictionary<QuestNames, Type> allQuestTypes = new Dictionary<QuestNames, Type>();
        [SerializeField] int QuestCount = 0;

        void Start()
        {
            InitQuest();
        }
        public void InitQuest()
        {
            if (allQuestTypes.Count > 0) return;

            var getAllQuest = Assembly.GetAssembly(typeof(QuestBase)).GetTypes()
                            .Where(myType => myType.IsClass
                            && !myType.IsAbstract && myType.IsSubclassOf(typeof(QuestBase)));

            foreach (var quest in getAllQuest)
            {
                var t = Activator.CreateInstance(quest) as QuestBase;
                allQuestTypes.Add(t.questName, quest);
            }
            QuestCount = allQuestTypes.Count;
        }

        public QuestBase GetQuestBase(QuestNames questName)
        {
            foreach (var questType in allQuestTypes)
            {
                if (questType.Key == questName)
                {
                    var t = Activator.CreateInstance(questType.Value) as QuestBase;
                    return t;
                }
            }
            Debug.Log("Quest base" + questName);
            return null;
        }

    }
}