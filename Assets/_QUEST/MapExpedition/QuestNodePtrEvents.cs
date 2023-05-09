using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Quest
{
    public class QuestNodePtrEvents : MonoBehaviour, IPointerClickHandler
    {

        public Nodes nodeName;
       // public MapENames mapEName;
        public QuestNames questName;
        public QuestObjNames objName; 
        MapExpView mapExpView;

        Transform pawnStone; 

        void Start()
        {

        }

        public void InitNodes(MapExpView mapExpView, Transform pawnStone)
        {
            this.mapExpView = mapExpView;
            this.pawnStone= pawnStone;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            mapExpView.OnNodeClicked(nodeName);

            Sequence seq = DOTween.Sequence();
            seq.AppendCallback(() => QuestMissionService.Instance
                                    .questController.ShowQuestEmbarkView(questName, objName))
                                    .AppendCallback(QuestMarkDown);
            seq.Play(); 
        }

        public void MovePawnStone()
        {
            pawnStone.DOMove(transform.position+ new Vector3(0,40f,0), 0.8f); 
        }

        void QuestMarkDown()
        {
            transform.DORotate(new Vector3(0, 0, 181), 0.2f)
                .OnComplete(() => transform.DOShakeRotation(1.5f, new Vector3(0, 0, 40), 4, 20, true));
        }
        public void QuestMarkUp()
        {
            transform.DORotate(new Vector3(0, 0, 0), 0.2f);
        }

    }
}