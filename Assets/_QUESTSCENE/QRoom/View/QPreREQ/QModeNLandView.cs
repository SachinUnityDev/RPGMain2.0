using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class QModeNLandView : MonoBehaviour
    {

        [SerializeField] QLandBtnPtrEvents qLandBtnPtrEvents;
        [SerializeField] QModeBtnPtrEvents qModeBtnPtrEvents;

        [SerializeField] QLandDispPtrEvents qLandDispPtrEvents;
        [SerializeField] QModeDispPtrEvents qModeDispPtrEvents;

        [SerializeField] Transform qSelPanel;
        [SerializeField] bool isSelPanelShown;


        private void Start()
        {
            QRoomService.Instance.OnQRoomStateChg += OnQRoomStateChg;
            QuestMissionService.Instance.OnQuestModeChg += (QuestMode questMode) => InitQModeNLandView(); 
        }
        void OnQRoomStateChg(QRoomState qRoomState)
        {

            
        }
        public void InitQModeNLandView()
        {
            qModeBtnPtrEvents.InitQModeBtn(this); 
            qLandBtnPtrEvents.InitLandBtn(this);
            List<QuestMode> modes = QuestMissionService.Instance.GetOtherQMode();
            for (int i = 0; i < 2; i++)
            {
                qSelPanel.GetChild(i).GetComponent<QModeSelPtrEvents>()
                                            .InitQModeSelBtnPtrEvents(this, modes[i]);
            }
            isSelPanelShown= false;
            TogglePanel();
        }

        public void TogglePanel()
        {
          
            if (isSelPanelShown)
            {
                qSelPanel.transform.DOLocalMoveX(0, 0.4f);
            }
            else
            {
                qSelPanel.transform.DOLocalMoveX(60, 0.4f);
            }
            isSelPanelShown = !isSelPanelShown;
        }
        public void ShowLandDisplay()
        {
            qLandDispPtrEvents.gameObject.SetActive(true);
            qLandDispPtrEvents.InitQLandDisplay();
        }
        public void HideLandDisplay()
        {
            qLandDispPtrEvents.gameObject.SetActive(false);
        }


        public void ShowQModeDisplay()
        {
            qModeDispPtrEvents.gameObject.SetActive(true);
            qModeDispPtrEvents.InitQModeDisplay();
        }
        public void HideQModeDisplay()
        {
            qModeDispPtrEvents.gameObject.SetActive(false);

        }




    }
}