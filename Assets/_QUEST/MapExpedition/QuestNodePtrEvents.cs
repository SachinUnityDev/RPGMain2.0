using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Quest
{
    public class QuestNodePtrEvents : MapExpBasePtrEvents, IPointerClickHandler
    {
     
        public QuestNames questName;
        public ObjNames objName;
        public override NodeData nodeData => new NodeData(QuestNames.HuntInTheWilderness);  
      
        public void OnPointerClick(PointerEventData eventData)
        {       
            Sequence seq = DOTween.Sequence();
                    seq.AppendCallback(QuestMarkDown)
                        .AppendCallback(OnEndNodeSelect)
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

        public override void OnEndNodeSelect()
        {
            base.OnEndNodeSelect();
            QuestMissionService.Instance
                                   .questController.ShowQuestEmbarkView(questName, objName, this);
        }
        public override void OnNodeInteractCancel()
        {
            QuestMarkUp();
        }

     
    }
}