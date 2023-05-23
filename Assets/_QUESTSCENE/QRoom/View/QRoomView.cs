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
        
        [Header("ROOM END REF TBR")]
        [SerializeField] QRoomEndArrowW arrowW;
        [SerializeField] QRoomEndArrowS arrowS;

        [SerializeField] QRoomPreReqEndPtrEvents qRoomEndArrow; 

        [SerializeField] Transform endCollider;

        [Header("QWalk Panel")]
        public QWalkBtmView qWalkBtmView;
        public QPreReqView qPreReqView;
        private void Awake()
        {
          
        }

        void Start()
        {  
            StartQRoomScene();
            QSceneService.Instance.OnStartOfQScene += (QuestNames questName)=>StartQRoomScene();
            QSceneService.Instance.OnQRoomStateChg += OnQRoomWalkStart;
        }

        public void EndArrowShow()
        {
            arrowS.GetComponent<Image>().DOFade(1, 0.1f);
            arrowW.GetComponent<Image>().DOFade(1, 0.1f);
        }
        void StartQRoomScene()
        {
            qRoomEndArrow.gameObject.SetActive(true);
            qPreReqView.gameObject.SetActive(true);
            qWalkBtmView.gameObject.SetActive(false);
            qPreReqView.InitQPreReqView(this); 
        }

        void OnQRoomWalkStart(QRoomState qRoomState)
        {
            if (qRoomState == QRoomState.Walk)
            {
                qWalkBtmView.gameObject.SetActive(true);
                qPreReqView.gameObject.SetActive(false);
                qWalkBtmView.QWalkInit(this);
            }
        }



    }
}