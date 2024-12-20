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
        [SerializeField] bool isSelPanelShown ;


        private void OnEnable()
        {
          //  QRoomService.Instance.OnQRoomStateChg += OnQRoomStateChg;
            QuestMissionService.Instance.OnQuestModeChg += InitQModeNLandView; 
            TogglePanel();
        }
        private void OnDisable()
        {
          //  QRoomService.Instance.OnQRoomStateChg -= OnQRoomStateChg;
            QuestMissionService.Instance.OnQuestModeChg -= InitQModeNLandView;
        }
     
        public void InitQModeNLandView(QuestMode questMode)
        {
            qModeBtnPtrEvents.InitQModeBtn(this); 
            qLandBtnPtrEvents.InitLandBtn(this);
            List<QuestMode> modes = QuestMissionService.Instance.GetOtherQMode();
            for (int i = 0; i < 2; i++)
            {
                qSelPanel.GetChild(i).GetComponent<QModeSelPtrEvents>()
                                            .InitQModeSelBtnPtrEvents(this, modes[i]);
            }
            isSelPanelShown= true;
            TogglePanel(); // toggle off
        }

        public void TogglePanel()
        { 
            if (isSelPanelShown)
            {
                qSelPanel.transform.DOLocalMoveX(120, 0.4f); // off state
            }
            else
            {
                qSelPanel.transform.DOLocalMoveX(0, 0.4f); // on state
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