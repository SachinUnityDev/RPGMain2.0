using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Quest
{
    public class QRoomMapView : MonoBehaviour, IPanel
    {
        public Transform mapImgTrans;
        public Button bgBtn;
        [SerializeField] Transform headTrans;
        [SerializeField] QRoomModel qRoomModel; 
        private void Start()
        {
            UnLoad();
            bgBtn.onClick.AddListener(UnLoad);        
            //qRoomModel = QRoomService.Instance.qRoomController.qRoomModel;
            //if(qRoomModel != null )
            //{
            //    OnRoomChg(qRoomModel.questNames, qRoomModel.roomNo);
            //}
            
            QRoomService.Instance.OnRoomChg += OnRoomChg; 
        }
        private void OnDisable()
        {
            QRoomService.Instance.OnRoomChg -= OnRoomChg;
        }
        public void OnRoomChg(QuestNames questName, int roomNo)
        {
            if (roomNo < 0)
                return; 

            QRoomSO qRoomSO =
                    QRoomService.Instance.qNodeAllRoomSO.GetQRoomSO(roomNo);

            headTrans.DOLocalMoveX(qRoomSO.mapPortCord.x, 0.1f);
            headTrans.DOLocalMoveY(qRoomSO.mapPortCord.y, 0.1f);
        }
        public void Init()
        {
            qRoomModel = QRoomService.Instance.qRoomController.qRoomModel;
            if (qRoomModel != null)
            {
                OnRoomChg(qRoomModel.questName, qRoomModel.roomNo);
            }
        }


        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(bgBtn.gameObject, true); 
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(bgBtn.gameObject, false);

        }

    
    }
}