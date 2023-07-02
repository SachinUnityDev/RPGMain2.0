using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Quest
{
    [Serializable]
    public class PathData
    {
        public int pathSeq;
        public Transform transform; 

        public PathData(Transform transform, int pathSeq)
        {
            this.transform = transform;
            this.pathSeq = pathSeq;
        }
    }

    public class QuestNodePtrEvents : MapExpBasePtrEvents, IPointerClickHandler
    {
     
        public QuestNames questName;
        public ObjNames objName;
        public int nodeSeq;
        private void Start()
        {
            nodeSeq = GetComponent<PathNodePtrEvents>().NodeSeq; 
        }
        public void OnPointerClick(PointerEventData eventData)
        {       
            Sequence seq = DOTween.Sequence();
                    seq.AppendCallback(QuestMarkDown)
                        .AppendCallback(ShowNodeClick)
                        //.AppendCallback(()=>pathExpView
                        //.MovePawnStone(this.transform.position,pathModel.endNode.time))
                    //.AppendCallback(pathBase.OnStartNodeExit)
                        ;
                                  
            seq.Play();
               
        }
        void QuestMarkDown()
        {
            transform.DORotate(new Vector3(0, 0, 181), 0.2f)
                .OnComplete(() => transform.DOShakeRotation(1.5f, new Vector3(0, 0, 40), 4, 20, true));
        }
        void QuestMarkUp()
        {
            transform.DORotate(new Vector3(0, 0, 0), 0.2f);
        }
        void ShowNodeClick()
        {  
            QuestMissionService.Instance
                                   .questController.ShowQuestEmbarkView(questName, objName, this);

            // find pathModel for the QuestPath

        }
        public override void OnNodeInteractCancel()
        {
            QuestMarkUp();
        }

        public override void OnEndNodeSelect()
        {
            List<PathData> NodeList = new List<PathData>();
          //  PathData pathData = new PathData(transform, nodeSeq); 
            //NodeList.Add(pathData); 
            if(pathExpView.MapPathContainer.childCount > 0)
            {
                for (int i = 0; i < pathExpView.MapPathContainer.childCount; i++)
                {
                    PathNodePtrEvents interNode =
                    pathExpView.MapPathContainer.GetChild(i).GetComponent<PathNodePtrEvents>();
                    if (interNode == null) continue; 
                    if (interNode.questName == questName)
                    {
                        PathData InterPathData = new PathData(interNode.transform, interNode.NodeSeq);
                        NodeList.Add(InterPathData);
                    }
                }
            }            
            pathExpView.MovePawn(NodeList);
        }
    }
}