using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Quest
{
    public class QRoomMapView : MonoBehaviour, IPanel
    {
        public Transform mapImgTrans;
        public Button bgBtn;
        [SerializeField] Transform headTrans; 
        private void Start()
        {
            UnLoad();
            bgBtn.onClick.AddListener(UnLoad);
            QRoomService.Instance.OnRoomChg += OnRoomChg; 
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