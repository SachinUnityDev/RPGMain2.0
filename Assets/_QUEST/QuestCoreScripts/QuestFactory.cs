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

        public Dictionary<ObjNames, Type> allObjTypes = new Dictionary<ObjNames, Type>();
        [SerializeField] int ObjCount = 0;


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
            InitObjBase(); 
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

        public void InitObjBase()
        {
            if (allObjTypes.Count > 0) return;

            var getAllObj = Assembly.GetAssembly(typeof(ObjBase)).GetTypes()
                            .Where(myType => myType.IsClass
                            && !myType.IsAbstract && myType.IsSubclassOf(typeof(ObjBase)));

            foreach (var obj in getAllObj)
            {
                var t = Activator.CreateInstance(obj) as ObjBase;
                allObjTypes.Add(t.objName, obj);
            }
            ObjCount = allObjTypes.Count;
        }
        public ObjBase GetObjBase(QuestNames questName, ObjNames objName)
        {
            foreach (var objTypes in allObjTypes)
            {
                if (objTypes.Key == objName)
                {
                    var t = Activator.CreateInstance(objTypes.Value) as ObjBase;
                    return t;
                }
            }
            Debug.Log("ObjBase " + objName);
            return null;
        }
    }
}