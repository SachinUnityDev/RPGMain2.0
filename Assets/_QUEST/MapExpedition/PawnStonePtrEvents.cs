using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Quest
{


    public class PawnStonePtrEvents : MonoBehaviour
    {

        PathExpView mapExpView;
        [Header("Current Node Data")]
        public NodeData nodeData; 

        public void InitPawnStoneInit(PathExpView mapExpView)
        {
            this.mapExpView = mapExpView;
            // move to nekkisari .. make it disappear
        }

        public void MovePawn(NodeTimeData endNodeData, MapExpBasePtrEvents mapBasePtrEvents)
        {
            Transform targetTrans = mapBasePtrEvents.gameObject.transform;  
            transform.DOMove(targetTrans.position, endNodeData.time);    
            nodeData = endNodeData.nodeData;
        }



    }
}