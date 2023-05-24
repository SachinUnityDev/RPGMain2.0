using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Common;
using Town;
namespace Quest
{
    public class PathExpView : MonoBehaviour
    {
        public Transform MapNodeContainer;
        public Transform MapENodeContainer; 
        [SerializeField] Transform pawnStone;

            

        void Start()
        {
            
        }

        public MapENodePtrEvents FindNode(NodeData nodeData)
        {
            return MapENodeContainer.GetComponentInChildren<MapENodePtrEvents>();
            //foreach (Transform trans in MapENodeContainer)
            //{
                
            //     //NodeData nodeData1 =
            //     //       trans.GetComponent<MapENodePtrEvents>().interNodeData.nodeTimeData.nodeData; 
            //     //  if(nodeData1.IsNodeDataMatch(nodeData))
            //     //           return trans.GetComponent<MapENodePtrEvents>();
            //}
            //return null; 
        }
        public void MovePawnStone(Vector3 pos, float time)
        {
            pawnStone.DOMove(pos, time); 
        }

        public MapExpBasePtrEvents GetQuestNodePtrEvents(NodeData nodeData)
        {
            foreach (Transform child in MapNodeContainer)
            {

                MapExpBasePtrEvents mapPtrEvents = child.GetComponent<MapExpBasePtrEvents>();                
                if (mapPtrEvents != null)
                    if(mapPtrEvents.nodeData== nodeData)
                     return child.GetComponent<MapExpBasePtrEvents>();
            }
            return null; 
        } 

        
         public void PathExpInit()
        {
            foreach (Transform node in MapNodeContainer)
            {
                MapExpBasePtrEvents ptrE = node.GetComponent<MapExpBasePtrEvents>();
                ptrE.InitQuestNodePtrEvents(this); 
            }

            foreach (Transform node in MapENodeContainer)
            {
                MapENodePtrEvents ptrE = node.GetComponent<MapENodePtrEvents>();
                ptrE.InitMapEPtrEvents(this);
            }
        }
        public void OnPathExit()
        {

        }





    }
}