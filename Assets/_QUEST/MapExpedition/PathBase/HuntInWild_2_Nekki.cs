using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{
    public class HuntInWild_2_Nekki : PathBase
    {
        public override NodeData startNode => new NodeData(QuestNames.HuntInTheWilderness);
        public override NodeTimeData endNode => 
                            new NodeTimeData(new NodeData(LocationName.Nekkisari), 0f);

        MapENames mapENames = MapENames.None; 

        public override void OnStartNodeExit()
        {
            MapService.Instance.pathController.CrossTheCurrNode();

            InterNodeData nextInterNodeData = pathModel.GetAnyUnCrossedInterNode();
            if (nextInterNodeData != null)
            {
                QuestMode questMode = QuestMissionService.Instance.questController.questModel.questMode;
                mapENames =
                 pathModel.GetMapENameAfterChanceChk(nextInterNodeData.nodeTimeData, questMode);
                if (mapENames != MapENames.None)
                {
                    MapENodePtrEvents mapEPtrEvents =
                            MapService.Instance.pathExpView.FindNode(nextInterNodeData.nodeTimeData.nodeData);
                    mapEPtrEvents.OnMapEChecked(nextInterNodeData, mapENames); 
                    currNodeIndex++;
                }
            }
            else
            {  // no uncross path 
                PathController pathController = MapService.Instance.pathController; 
                MapExpBasePtrEvents endPtr = pathController.mapExpBasePtrEvents;
                MapService.Instance.pathExpView.MovePawnStone(endPtr.transform.position
                                                            , 0.4f);
            }
        }
        public override void OnEndNodeEnter()
        {
            // pathmodel uncross all pathData
            // pathController .. set data points endNode as null .. start node as current node
            // pathSO asn pathmodel as none
            // 

        }
       
       
    }
}
