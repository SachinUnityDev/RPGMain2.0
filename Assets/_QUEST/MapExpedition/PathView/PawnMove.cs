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
        public void PawnMoveInit(PathView pathView, PathQView pathQView, PathModel pathModel)
        {
            this.pathQView = pathQView;
            this.pathView = pathView;
            this.pathModel = pathModel;
            pathModel.currNode = pathModel.nodes[0];
            pathQView.Move2NextNode(true); // move from 0 Node 
        }
        
        public void PawnMoveLoad(PathView pathView, PathQView pathQView, PathModel pathModel)
        {
            this.pathQView = pathQView;
            this.pathView = pathView;
            this.pathModel = pathModel;         
            pathQView.Move2NextNode(true);
        }    
        public void Move(int currentNode)
        {
            // pathModel.. check current node index 
            // move to next node in the 
            
            transform.GetComponent<BoxCollider2D>().enabled = true;
            Sequence seq = DOTween.Sequence();
            seq
            .Append(transform.GetComponent<Image>().DOFade(1.0f, 0.4f))
            .Append(transform.DOLocalMove(GetNextPos(currentNode).localPosition, 2.0f));
            ;
            //  if(EncounterService.Instance.mapEController.mapEOnDsply)
            seq.Play()
                //.OnComplete(CheckTownArrival)
                ;

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
            unSuccessSeq.Play()
                //.OnComplete(()=>MapService.Instance.mapController.mapView.GetComponent<IPanel>().UnLoad())
                  //              .OnComplete(CheckTownArrival)
                                ;
        }

        public void FadeOut()
        {
            transform.GetComponent<Image>().DOFade(0.0f, 0.4f); 
        }    

        Transform GetNextPos(int currentNode)
        {
            int nodeCount = pathQView.transform.childCount-2;// -1 bcoz of qMark -1 more bcoz oc count from 0 
            if(currentNode < nodeCount)
            {
                return pathQView.transform.GetChild(currentNode + 1);
            }
            else
            {               
                return pathQView.transform.GetChild(0);
            }
                    
        }

    }
}