using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace Quest
{
    [Serializable]
    public class NodeInfo
    {
        public NodeData startNode; 
        public NodeTimeData endNode;
        public Type type;

        public NodeInfo(NodeData startNode, NodeTimeData endNode, Type type)
        {
            this.startNode = startNode;
            this.endNode = endNode;
            this.type = type;
        }
    }

    public class PathFactory : MonoBehaviour
    {
        public List<NodeInfo> allPathTypes = new List<NodeInfo>();
        [SerializeField] int pathECount = 0;

        void Start()
        {
            InitPaths();
        }
        public void InitPaths()
        {
            if (allPathTypes.Count > 0) return;

            var getAllPathBase = Assembly.GetAssembly(typeof(PathBase)).GetTypes()
                            .Where(myType => myType.IsClass
                            && !myType.IsAbstract && myType.IsSubclassOf(typeof(PathBase)));

            foreach (var path in getAllPathBase)
            {
                var t = Activator.CreateInstance(path) as PathBase;
                NodeInfo nodeInfo = new NodeInfo(t.startNode, t.endNode, path); 
                allPathTypes.Add(nodeInfo);
            }
            pathECount = allPathTypes.Count;
        }

        public PathBase GetPathBase(NodeData startNode, NodeTimeData endNode)
        {
            foreach (var pathType in allPathTypes)
            {
                if (pathType.startNode.IsNodeDataMatch(startNode)
                    && pathType.endNode.IsNodeTimeDataMatch(endNode))                    
                {
                    var t = Activator.CreateInstance(pathType.type) as PathBase;                   
                    return t;
                }
            }
            return null;
        }
    }
}