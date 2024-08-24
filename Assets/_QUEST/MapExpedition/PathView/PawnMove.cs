using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.UI; 

namespace Quest
{
    public class PawnMove : MonoBehaviour
    {
        PathView pathView;
        PathQView pathQView;
        PathModel pathModel;

        [SerializeField] int nodeSeq;
        [SerializeField] int targetNodeSeq = -1;
        public void PawnMoveInit(PathView pathView, PathQView pathQView, PathModel pathModel)
        {
            this.pathQView = pathQView;
            this.pathView = pathView;
            this.pathModel = pathModel;
            pathModel.currNode= pathModel.nodes[0];
            targetNodeSeq = -1; 
            Move(); 
        }
        
    
        public void Move()
        {
            // pathModel.. check current node index 
            // move to next node in the 
           
            //nodeSeq....is a local counter has no track of actual node index
            // get Node seq from PathQView and control the movement of pawn


            transform.GetComponent<BoxCollider2D>().enabled = true;
            Sequence seq = DOTween.Sequence();
            seq
            .Append(transform.GetComponent<Image>().DOFade(1.0f, 0.4f))
            .Append(transform.DOLocalMove(GetNextPos().localPosition, 2.0f));
            ;
            //  if(EncounterService.Instance.mapEController.mapEOnDsply)
            seq.Play().OnComplete(CheckTownArrival).OnComplete(() => { nodeSeq = targetNodeSeq; });

        }
        public void Move2TownOnFail()
        {
            transform.GetComponent<BoxCollider2D>().enabled= false; 
            Sequence unSuccessSeq = DOTween.Sequence();
            unSuccessSeq
           .Append(transform.GetComponent<Image>().DOFade(1.0f, 0.4f))
           .Append(transform.DOLocalMove(pathQView.transform.GetChild(0).localPosition, 2.0f))
            .AppendCallback(()=>transform.GetComponent<BoxCollider2D>().enabled= true);
            ;
            unSuccessSeq.Play().OnComplete(()=>MapService.Instance.mapController.mapView.GetComponent<IPanel>().UnLoad());
            UpdatePathModelOnQFail();
        }

        void UpdatePathModelOnQFail()
        {
            for (int i = 1; i < pathModel.nodes.Count; i++)
            {
                pathModel.nodes[i].isChecked = false; 
            }
            pathModel.nodes[0].isChecked = true;
        }


        void CheckTownArrival()
        {
            if (pathModel.nodes[nodeSeq].isChecked && nodeSeq == 0)
            {
                MapService.Instance.mapController.mapView.GetComponent<IPanel>().UnLoad();
            }
        }
        //Transform GetCurrPos()
        //{
        //    for (int i = 0; i < pathModel.nodes.Count; i++)
        //    {
        //        if (!pathModel.nodes[i].isChecked)
        //        {
        //            nodeSeq = i; 
        //            return pathQView.transform.GetChild(i);
        //        }
        //        if (pathModel.nodes[i].isChecked && i == 0)
        //        {
        //            nodeSeq = 0; 
        //        }
        //    }
        //    Debug.Log("node seq not MATCHED");
        //    return null; 
        //}

        Transform GetNextPos()
        {
            if(nodeSeq < pathModel.nodes.Count-1)
            {
                targetNodeSeq =  nodeSeq+1;
                return pathQView.transform.GetChild(targetNodeSeq);
            }
            else
            {
                targetNodeSeq = 0; 
                return pathQView.transform.GetChild(0);
            }            
        }

    }
}