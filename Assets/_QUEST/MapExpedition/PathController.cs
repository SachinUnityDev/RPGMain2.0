using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{
    public class PathController : MonoBehaviour
    {
        PathFactory pathFactory; 
        public List<PathBase> allPathBase = new List<PathBase>();
        public List<PathModel> allPathModel = new List<PathModel>();

        [Header(" Current data")]
        public PathSO pathSO;
        public PathBase pathBase;
        public PathModel pathModel;
        public Transform pawnPos;

        public InterNodeData interNodeData; 
        public NodeData endNode;
        public NodeData startNode;
        public MapExpBasePtrEvents mapExpBasePtrEvents; 

        void Start()
        {
            pathFactory = GetComponent<PathFactory>();
        }

        public void InitPath(AllPathSO allPathSO)
        {
            foreach (PathSO pathSO in allPathSO.allPathSO)
            {
                PathModel pathModel = new PathModel(pathSO); 
                allPathModel.Add(pathModel);
            }
            InitPathBases();
            startNode = new NodeData(Common.LocationName.Nekkisari); 
        }
        public void CrossTheCurrNode()
        {
            if(interNodeData != null) 
            interNodeData.isCrossed = true; 
        }
        void InitPathBases()
        {
            foreach (PathModel pathModel in allPathModel)
            {
                PathBase pathbaseNew =
                        pathFactory.GetPathBase(pathModel.startNode, pathModel.endNode);
                PathSO pathSO = MapService.Instance
                                .allPathSO.GetPathSO(pathModel.startNode, pathModel.endNode.nodeData);
                if (pathbaseNew != null)
                    pathbaseNew.OnInitPath(pathModel, pathSO);

                allPathBase.Add(pathbaseNew);
            }
        }

        public void OnPathEndNodeSelect( MapExpBasePtrEvents mapExpBasePtrEvents, NodeData endNode)
        {
            this.endNode= endNode;
            
            this.mapExpBasePtrEvents = mapExpBasePtrEvents;
            pathSO = MapService.Instance.allPathSO.GetPathSO(startNode, endNode);
            
            pathModel= GetPathModel(startNode, endNode);
            pathBase = GetPathBase(startNode, endNode);

            if (pathModel.IsAnyUnCrossedInterNode())
            {
                InterNodeData interNodeData = pathModel.GetNextUnCrossedInterNode();
                MapENames mapEName = pathModel.GetMapENameFromInterNodeBasedOnChance(interNodeData);
                //seq


            //    EncounterService.Instance.mapEController.ShowMapE(mapEName); 

            }

        }

        public PathModel GetPathModel(NodeData startNode, NodeData endNode)
        {
            int index = allPathModel.FindIndex(t => t.startNode.IsNodeDataMatch(startNode)
                                         && t.endNode.nodeData.IsNodeDataMatch(endNode));
            if (index != -1)
            {
                return allPathModel[index];
            }
            return null;
        }

        public PathBase GetPathBase(NodeData startNode, NodeData endNode)
        {
            int index = allPathBase.FindIndex(t=>t.startNode.IsNodeDataMatch(startNode)
                                             && t.endNode.nodeData.IsNodeDataMatch(endNode));
            if(index != -1)
            {
                return allPathBase[index];  
            }
            return null; 
        }
    }
}