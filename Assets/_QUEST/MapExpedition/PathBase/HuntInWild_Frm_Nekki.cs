using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{

    public class HuntInWild_Frm_Nekki : PathBase
    {
        public override NodeData startNode => new NodeData(LocationName.Nekkisari);

        public override NodeTimeData endNode => new NodeTimeData(new NodeData(QuestNames.HuntInTheWilderness), 0f);

        MapENames mapENames = MapENames.None;
        public override void OnEndNodeEnter()
        {
          
        }

        public override void OnEndNodeClicked(Transform endNode)
        {
            


        }


        public override void OnEmbarkPressed()
        {
           

            MapService.Instance.pathController.CrossTheCurrNode();

            InterNodeData nextInterNodeData = pathModel.GetAnyUnCrossedInterNode();
            if (nextInterNodeData != null)
            {
                

                QuestMode questMode = QuestMissionService.Instance.currQuestMode;
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

        
    }
}