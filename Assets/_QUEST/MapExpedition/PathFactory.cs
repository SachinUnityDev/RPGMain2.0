using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq; 

namespace Quest
{
    public class PathInfo
    {
        public QuestNames questName; 
        public ObjNames objNames;
        public Type type;

        public PathInfo(QuestNames questName, ObjNames objNames, Type type)
        {
            this.questName = questName;
            this.objNames = objNames;
            this.type = type;
        }
    }
    public class PathFactory : MonoBehaviour
    {
        public List<PathInfo> allPathBases = new List<PathInfo>();   
        [SerializeField] int pathCount = 0;

        void Awake()
        {
            InitPathInfo();
        }
        public void InitPathInfo()
        {
            if (allPathBases.Count > 0) return;

            var getAllPathbase = Assembly.GetAssembly(typeof(PathBase)).GetTypes()
                            .Where(myType => myType.IsClass
                            && !myType.IsAbstract && myType.IsSubclassOf(typeof(PathBase)));

            foreach (var pathbase in getAllPathbase)
            {
                var t = Activator.CreateInstance(pathbase) as PathBase;
                allPathBases.Add(new PathInfo(t.questName, t.objName, pathbase));
            }
            pathCount = allPathBases.Count;
        }

        public PathBase GetPathBase(QuestNames questName, ObjNames objName)
        {
            foreach (var p in allPathBases)
            {
                if (p.questName == questName && p.objNames == objName)
                {
                    var t = Activator.CreateInstance(p.type) as PathBase;
                    return t;
                }
            }
            return null;
        }
    }
}