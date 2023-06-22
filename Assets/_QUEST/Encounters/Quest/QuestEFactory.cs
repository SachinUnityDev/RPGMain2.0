using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;


namespace Quest
{
    public class QuestEFactory : MonoBehaviour
    {
        public Dictionary<QuestENames, Type> allQuestETypes = new Dictionary<QuestENames, Type>();
        [SerializeField] int questECount = 0;

        void Awake()
        {
            InitQuestE();
        }
        public void InitQuestE()
        {
            if (allQuestETypes.Count > 0) return;

            var getAllQuestE = Assembly.GetAssembly(typeof(QuestEbase)).GetTypes()
                            .Where(myType => myType.IsClass
                            && !myType.IsAbstract && myType.IsSubclassOf(typeof(QuestEbase)));

            foreach (var questE in getAllQuestE)
            {
                var t = Activator.CreateInstance(questE) as QuestEbase;
                allQuestETypes.Add(t.questEName, questE);
            }
            questECount = allQuestETypes.Count;
        }

        public QuestEbase GetQuestEBase(QuestENames questEName)
        {
            foreach (var questEtype in allQuestETypes)
            {
                if (questEtype.Key == questEName)
                {
                    var t = Activator.CreateInstance(questEtype.Value) as QuestEbase;
                    return t;
                }
            }
            return null;
        }

    }
}