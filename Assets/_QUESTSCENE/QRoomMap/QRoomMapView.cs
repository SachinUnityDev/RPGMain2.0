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
        private void Start()
        {
            UnLoad();
            bgBtn.onClick.AddListener(UnLoad);
            SceneManager.sceneLoaded += OnSceneLoaded;
            QRoomService.Instance.OnQRoomStateChg += QuestStartInit;
            QRoomService.Instance.OnRoomChg += OnRoomChg; 
        }
        private void OnDisable()
        {
            QRoomService.Instance.OnRoomChg -= OnRoomChg;
        }
        void OnSceneLoaded(Scene current, LoadSceneMode loadMode)
        {
         
                
        }
        void QuestStartInit(QRoomState questState)
        {
            OnRoomChg(QRoomService.Instance.questName, 1);
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