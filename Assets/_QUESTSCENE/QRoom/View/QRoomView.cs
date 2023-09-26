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

        private void Start()
        {
            qAbbasMove = abbasGO.GetComponent<QAbbasMovementController>();
            QRoomService.Instance.OnQSceneStart += OnStartQRoomView;
            QRoomService.Instance.OnQRoomStateChg += OnQRoomStateChgView;
            QRoomService.Instance.OnRoomChg += OnRoomChg;

            OnQRoomStateChgView(QRoomState.Prep); 
        }
        private void OnDisable()
        {
            QRoomService.Instance.OnQSceneStart -= OnStartQRoomView;
            QRoomService.Instance.OnQRoomStateChg -= OnQRoomStateChgView;
            QRoomService.Instance.OnRoomChg -= OnRoomChg;
        }

        

        void OnRoomChg(QuestNames questName, int roomNo)
        {
            Sequence chgSeq = DOTween.Sequence();

            chgSeq
                  .Append(fadeScreen.GetComponent<Image>().DOFade(1f, 0.15f))
                  .AppendCallback(()=>AbbasColliderToggle(false))
                  .Append(abbasGO.transform.DOLocalMoveX(AbbasRoomInitPos, 0.1f))
                  .AppendCallback(() => AbbasColliderToggle(true))
                  .AppendInterval(0.5f)
                  .Append(fadeScreen.GetComponent<Image>().DOFade(0f, 1f))                 
                  ;

            chgSeq.Play();    
        }
     
        void AbbasColliderToggle(bool turnON)
        {
            abbasGO.GetComponent<BoxCollider2D>().enabled= turnON;
        }
        public void ShowEndArrow()
        {
            if (QRoomService.Instance.qRoomController.IsSArrowAvail())
            {
                arrowS.gameObject.SetActive(true);
                arrowS.GetComponent<Image>().DOFade(1, 0.1f);
            }   
            if (QRoomService.Instance.qRoomController.IsWArrowAvail())
            {
                arrowW.gameObject.SetActive(true);
                arrowW.GetComponent<Image>().DOFade(1, 0.1f);
            }
                
        }
        public void HideEndArrow()
        {
            arrowS.gameObject.SetActive(false);
            arrowW.gameObject.SetActive(false);
            arrowS.GetComponent<Image>().DOFade(0, 0.1f);
            arrowW.GetComponent<Image>().DOFade(0, 0.1f);
        }

        void OnStartQRoomView(QuestNames questName)
        {
            this.questName= questName;           
        }

        void OnQRoomStateChgView(QRoomState qRoomState)
        {
            QuestMode questMode = QuestMissionService.Instance.currQuestMode;
            qModeNLandView.InitQModeNLandView(questMode);

            if (qRoomState == QRoomState.Prep)
            {
                qRoomPrepEndArrow.gameObject.SetActive(true);
                qPreReqView.gameObject.SetActive(true);
                qWalkBtmView.gameObject.SetActive(false);
                qPreReqView.InitQPreReqView(this);
                AbbasRoomInitPos = -15f;
                UIControlServiceGeneral.Instance.helpName = HelpName.QuestPrep; 
            }
            if (qRoomState == QRoomState.AutoWalk)
            {
                qRoomPrepEndArrow.gameObject.SetActive(false);
                qWalkBtmView.gameObject.SetActive(true);
                qPreReqView.gameObject.SetActive(false);
                qWalkBtmView.QWalkInit(this);
                QRoomService.Instance.canAbbasMove = true;
                UIControlServiceGeneral.Instance.helpName = HelpName.QRoom;
            }
            if (qRoomState == QRoomState.Walk)
            {
                qRoomPrepEndArrow.gameObject.SetActive(false);
                AbbasRoomInitPos = -8f;
                UIControlServiceGeneral.Instance.helpName = HelpName.QRoom;
                QRoomService.Instance.canAbbasMove = true;
            }
        }

     
  
    }
}