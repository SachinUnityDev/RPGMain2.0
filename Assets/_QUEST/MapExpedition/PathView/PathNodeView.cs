using DG.Tweening;
using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Quest
{

    public class PathNodeView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public QuestNames questName;
        public ObjNames objName;
        public int nodeSeq;

        [Header("Global Var")]
        PathView pathView;
        PathQView pathQView;

        PathBase pathBase;
        [SerializeField] PathModel pathModel; 
        int index; 
        public void InitPathNodeView(PathView pathView, PathQView pathQView)
        {
            this.pathView = pathView;
            this.pathQView= pathQView;
            questName = pathQView.questName;
            objName = pathQView.objName;
            nodeSeq = transform.GetSiblingIndex();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(!pathView.isQInProgress)
            {
                QuestMarkDown();
                ShowNodeClick();
                pathView.isQInProgress = true; 
            }   
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
        }
        public void OnNodeInteractCancel()
        {
            // reset PathModel and pathbase .. Set PAth Q select to None

            QuestMarkUp();
        }

        public void OnPathEmbark()
        {
            pathView.OnPathEmbark(questName, objName, pathQView);
          // move pawn
          // set path Model and base as the ? mark selected 
          // Pawn scripts to control the node enter...
          // exit to be defined by the completion of external event
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
          
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "PawnStone")
            {
                  pathBase = MapService.Instance.pathController.GetPathBase(questName, objName);
                if (pathBase != null)
                    OnNodeEnter();
                else
                    Debug.Log("Path base not found" + questName + objName); 
            }
        }
        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "PawnStone" 
                && EncounterService.Instance.mapEController.mapEOnDsply)
            {
                OnNodeExit();
            }
        }

        void OnNodeEnter()
        {
           
            switch (nodeSeq)
            {
                    case 0:
                    pathBase.OnNode0Enter(); 
                    break;
                    case 1:
                    pathBase.OnNode1Enter(); 
                    break; 
                    case 2:
                    pathBase.OnNode2Enter();
                    break; 
                    case 3:
                    pathBase.OnNode3Enter();
                    break;
                    case 4:
                    pathBase.OnNode4Enter();
                    break;
                    case 5:
                    pathBase.OnNode5Enter();
                    break;
                
                default:
                    break;
            }
        }
        void OnNodeExit()
        {
            int index = transform.GetSiblingIndex();
            switch (nodeSeq)
            {
                case 0:
                    pathBase.OnNode0Exit();
                    break;
                case 1:
                    pathBase.OnNode1Exit();
                    break;
                case 2:
                    pathBase.OnNode2Exit();
                    break;
                case 3:
                    pathBase.OnNode3Exit();
                    break;
                case 4:
                    pathBase.OnNode4Exit();
                    break;
                case 5:
                    pathBase.OnNode5Exit();
                    break;

                default:
                    break;
            }
            for (int i = 0; i < pathQView.transform.childCount; i++)
            {
                if(i == nodeSeq + 1)
                {
                    pathQView.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    pathQView.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            pathModel = MapService.Instance.pathController.GetPathModel(questName, objName);
            if (pathModel != null)
                pathModel.nodes[index].isChecked = true; 
        }
    }
}

// List<PathData> NodeList = new List<PathData>();
//  PathData pathData = new PathData(transform, nodeSeq); 
//NodeList.Add(pathData); 
//if (pathView.MapPathContainer.childCount > 0)
//{
//    for (int i = 0; i < pathView.MapPathContainer.childCount; i++)
//    {
//        PathNodePtrEvents interNode =
//        pathView.MapPathContainer.GetChild(i).GetComponent<PathNodePtrEvents>();
//        if (interNode == null) continue;
//        if (interNode.questName == questName)
//        {
//            PathData InterPathData = new PathData(interNode.transform, interNode.NodeSeq);
//            NodeList.Add(InterPathData);
//        }
//    }
//}
//pathView.MovePawn(NodeList);