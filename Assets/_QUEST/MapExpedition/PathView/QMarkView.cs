using DG.Tweening;
using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Quest
{
    public class QMarkView : MonoBehaviour, IPointerClickHandler
    {
        public QuestNames questName;
        public ObjNames objName;
        public int nodeSeq;

        [Header("Global Var")]
        PathView pathView;
        PathQView pathQView;
        [SerializeField] PathModel pathModel;
        public void InitPathNodeView(PathView pathView, PathQView pathQView, PathModel pathModel)
        {
            this.pathView = pathView;
            this.pathQView = pathQView;
            questName = pathQView.questName;
            objName = pathQView.objName;
            // check if quest completed.. if not then show the ? mark
           this.pathModel = pathModel;

            if (!pathModel.isCompleted)
            {
                QuestMarkUp();  
            }
        }

        //public void LoadPathNodeView(PathView pathView, PathQView pathQView)
        //{
        //   InitPathNodeView(pathView, pathQView);
        //}

        void QuestMarkDown()
        {
            transform.DORotate(new Vector3(0, 0, 181), 0.2f)
                .OnComplete(() => transform.DOShakeRotation(1.5f, new Vector3(0, 0, 40), 4, 20, true));

            MapService.Instance.pathController.currPathModel  = pathModel;            
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

        public void OnPathEmbark()
        {
            pathView.OnPathEmbark(questName, objName, pathQView);
            // move pawn
            // set path Model and base as the ? mark selected 
            // Pawn scripts to control the node enter...
            // exit to be defined by the completion of external event
        }
        public void OnNodeInteractCancel()
        {
            // reset PathModel and pathbase .. Set PAth Q select to None
            QuestMarkUp();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!pathModel.isCompleted)
            {
                QuestMarkDown();
                ShowNodeClick();
                pathView.isQInProgress = true;
            }
        }
    }
}