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
            pathModel.currNode= pathModel.nodes[0];

            Move(); 
        }
        
    
        public void Move()
        {
            // pathModel.. check current node index 
            // move to next node in the 
            Sequence seq = DOTween.Sequence();
            seq
            .Append(transform.GetComponent<Image>().DOFade(1.0f, 0.4f))
            .Append(transform.DOLocalMove(GetNextPos().localPosition, 2.0f));
            ;
            seq.Play().OnComplete(CheckTownArrival);

        }
        void CheckTownArrival()
        {
            if (pathModel.nodes[nodeSeq].isChecked && nodeSeq == 0)
            {
                MapService.Instance.mapController.mapView.GetComponent<IPanel>().UnLoad();
            }
        }
        Transform GetCurrPos()
        {
            for (int i = 0; i < pathModel.nodes.Count; i++)
            {
                if (!pathModel.nodes[i].isChecked)
                {
                    nodeSeq = i; 
                    return pathQView.transform.GetChild(i);
                }
                if (pathModel.nodes[i].isChecked && i == 0)
                {
                    nodeSeq = 0; 
                }
            }
            Debug.Log("node seq not MATCHED");
            return null; 
        }
        Transform GetNextPos()
        {
            if(nodeSeq < pathModel.nodes.Count-1)
            {
                nodeSeq++;
                return pathQView.transform.GetChild(nodeSeq);
            }
            else
            {
                nodeSeq = 0; 
                return pathQView.transform.GetChild(0);
            }
            
        }

    }
}