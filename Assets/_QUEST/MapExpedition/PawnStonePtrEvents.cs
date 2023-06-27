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


        [Header(" Pawn Path definition")]
        [SerializeField] int index;
        [SerializeField] List<Transform> allNodes = new List<Transform>();
        
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
        public void InitPawnMovement(List<Transform> allNodes)
        {  
            this.allNodes.Clear();
            this.allNodes = allNodes;
            index = -1; 

        }
       public void Move2Next()
       {
            index++;
            transform.DOLocalMove(allNodes[index].position, 0.4f); 
       }
            
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("TRIGGER 2");
        }
    }
}