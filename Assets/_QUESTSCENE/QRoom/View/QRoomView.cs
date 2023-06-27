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
        [Header(" Screen Fade")]
        [SerializeField] Transform fadeScreen; 

        [Header("Abbas Walk Anim GO")]
        [SerializeField] GameObject abbasGO;
        public QAbbasMovementController qAbbasMove;
        [SerializeField] float AbbasRoomInitPos = -8; 

        [Header("ROOM END REF TBR")]
        [SerializeField] QRoomEndArrowW arrowW;
        [SerializeField] QRoomEndArrowS arrowS;

        [SerializeField] QRoomPreReqEndPtrEvents qRoomPrepEndArrow; 

        [SerializeField] Transform endCollider;

        [Header("QWalk Panel")]
        public QWalkBtmView qWalkBtmView;
        public QPreReqView qPreReqView;

        [Header("QLand")]
        public QModeNLandView qModeNLandView;

        [Header("Q Room Map View")]
        public QRoomMapView qRoomMapView;   


        [Header("Global var")]
        [SerializeField] QuestNames questName;

        [Header("TEST")]
        [SerializeField] QuestEView questEView;

        void Awake()
        {
            qAbbasMove = abbasGO.GetComponent<QAbbasMovementController>();            
        }

        private void Start()
        {
            QRoomService.Instance.OnStartOfQScene += OnStartQRoomView;
            QRoomService.Instance.OnQRoomStateChg += OnQRoomStateChgView;
            QRoomService.Instance.OnRoomChg += (QuestNames questName, int roomNo)=>OnRoomChg(); 
        }
        void OnRoomChg()
        {
            Sequence chgSeq = DOTween.Sequence();

            chgSeq
                 //.AppendCallback(() =>
                 //{
                 //    abbasGO.GetComponent<BoxCollider2D>().enabled = false;
                 //  //  abbasGO.GetComponent<QAbbasMovementController>().movement = 0;
                 //})

                  .Append(fadeScreen.GetComponent<Image>().DOFade(1f, 0.15f))                 
                  .Append(abbasGO.transform.DOLocalMoveX(AbbasRoomInitPos, 0.1f))
                  //.OnComplete(()=> { abbasGO.GetComponent<BoxCollider2D>().enabled = true; })
                  .AppendInterval(0.5f)
                  .Append(fadeScreen.GetComponent<Image>().DOFade(0f, 1f))                 
                  ;

            chgSeq.Play();    
        }
     

        public void ShowEndArrow()
        {
            if(QRoomService.Instance.qRoomController.IsSArrowAvail())
                arrowS.GetComponent<Image>().DOFade(1, 0.1f);            
                
            if (QRoomService.Instance.qRoomController.IsWArrowAvail())
                arrowW.GetComponent<Image>().DOFade(1, 0.1f);
        }
        public void HideEndArrow()
        {
            arrowS.GetComponent<Image>().DOFade(0, 0.1f);
            arrowW.GetComponent<Image>().DOFade(0, 0.1f);
        }

        void OnStartQRoomView(QuestNames questName)
        {
            this.questName= questName;           
        }

        void OnQRoomStateChgView(QRoomState qRoomState)
        {
            qModeNLandView.InitQModeNLandView();
           
            if (qRoomState == QRoomState.Prep)
            {
                qRoomPrepEndArrow.gameObject.SetActive(true);
                qPreReqView.gameObject.SetActive(true);
                qWalkBtmView.gameObject.SetActive(false);
                qPreReqView.InitQPreReqView(this);
                AbbasRoomInitPos = -15f; 
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
                AbbasRoomInitPos = -8f; 
            }
        }

     
  
    }
}