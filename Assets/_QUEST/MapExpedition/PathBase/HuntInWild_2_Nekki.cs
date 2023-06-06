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

        public override void OnEndNodeEnter()
        {
            // pathmodel uncross all pathData
            // pathController .. set data points endNode as null .. start node as current node
            // pathSO asn pathmodel as none
            // 

        }

        public override void OnEmbarkPressed()
        {
            MapService.Instance.pathController.CrossTheCurrNode();

            InterNodeData nextInterNodeData = pathModel.GetNextUnCrossedInterNode();
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
               // PathController pathController = MapService.Instance.pathController;
               // MapExpBasePtrEvents endPtr = pathController.mapExpBasePtrEvents;
                MapService.Instance.pathExpView.MovePawnStone(endNodeTrans.position
                                                                , 0.4f);
            }
        }

        public override void OnEndNodeClicked(Transform endNodeTrans)
        {
            this.endNodeTrans = endNodeTrans;
           // QuestMissionService.Instance.questController.ShowQuestEmbarkView(questName, objName, this); 
        }
    }
}
