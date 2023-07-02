using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Quest
{
    public class PathExpView : MonoBehaviour
    {
        public Transform MapPathContainer;
        [SerializeField] Transform pawnStone;
        [SerializeField] Transform nekkiTrans;
            

        void Start()
        {
            
        }
        public void PathExpInit()
        {
            foreach (Transform node in MapPathContainer)
            {
                MapExpBasePtrEvents ptrE = node.GetComponent<MapExpBasePtrEvents>();
                if(ptrE!= null)
                ptrE.InitQuestNodePtrEvents(this);
            }
            foreach (Transform node in MapPathContainer)
            {
                MapENodePtrEvents ptrE = node.GetComponent<MapENodePtrEvents>();
                //if (ptrE != null)
                //    ptrE.InitMapEPtrEvents(this, );
            }

        }
     
        public void MovePawnStone(Vector3 pos, float time)
        {
            pawnStone.DOMove(pos, time); 
        }

        public void MovePawn(List<PathData> allNode)
        {
            pawnStone.GetComponent<PawnStonePtrEvents>().InitPawnMovement(allNode);
        }
        public void OnPathExit()
        {

        }
    }
}