using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common; 
namespace Quest
{
    public class QRoomView : MonoBehaviour
    {
        [Header("Abbas Walk Anim GO")]
        [SerializeField] GameObject abbasGO;
        public QAbbasMovementController qAbbasMove; 

        [Header("ROOM END REF TBR")]
        [SerializeField] QRoomEndArrowW arrowW;
        [SerializeField] QRoomEndArrowS arrowS;

        [SerializeField] QRoomPreReqEndPtrEvents qRoomPrepEndArrow; 

        [SerializeField] Transform endCollider;

        [Header("QWalk Panel")]
        public QWalkBtmView qWalkBtmView;
        public QPreReqView qPreReqView;

        [Header("Global var")]
        [SerializeField] QuestNames questName; 

        void Awake()
        {
            qAbbasMove = abbasGO.GetComponent<QAbbasMovementController>();            
        }

        private void Start()
        {

            QSceneService.Instance.OnStartOfQScene += OnStartQRoomView;
            QSceneService.Instance.OnQRoomStateChg += OnQRoomStateChgView;
        }

        public void EndArrowShow()
        {
            arrowS.GetComponent<Image>().DOFade(1, 0.1f);
            arrowW.GetComponent<Image>().DOFade(1, 0.1f);
        }

        void OnStartQRoomView(QuestNames questName)
        {
            this.questName= questName;
        }

    

        void OnQRoomStateChgView(QRoomState qRoomState)
        {

            if (qRoomState == QRoomState.Prep)
            {
                qRoomPrepEndArrow.gameObject.SetActive(true);
                qPreReqView.gameObject.SetActive(true);
                qWalkBtmView.gameObject.SetActive(false);
                qPreReqView.InitQPreReqView(this);
            }
            if (qRoomState == QRoomState.AutoWalk)
            {
                qRoomPrepEndArrow.gameObject.SetActive(false);
                qWalkBtmView.gameObject.SetActive(true);
                qPreReqView.gameObject.SetActive(false);
                qWalkBtmView.QWalkInit(this);
            }
            if (qRoomState == QRoomState.Walk)
            {
                qRoomPrepEndArrow.gameObject.SetActive(false);
                //qWalkBtmView.gameObject.SetActive(true);
                //qPreReqView.gameObject.SetActive(false);
                //qWalkBtmView.QWalkInit(this);
            }

        }
    }
}