using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Quest
{


    public class QModeBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] TextMeshProUGUI heading;
        [SerializeField] Image img;

        [SerializeField] QModeNLandView qModeNLandView; 
        QuestMode qMode;
        public void InitQModeBtn(QModeNLandView qModeNLandView)
        {
            this.qModeNLandView= qModeNLandView;
            qMode = QuestMissionService.Instance.currQuestMode;      
            if(qMode != QuestMode.None)
                img.sprite = QuestMissionService.Instance.allQuestMainSO.GetQuestModeSprite(qMode);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if(QRoomService.Instance.qRoomState == QRoomState.Prep)
            {
                qModeNLandView.TogglePanel();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowTxt();
            qModeNLandView.ShowQModeDisplay();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Hidetxt();
            qModeNLandView.HideQModeDisplay();
        }

        void ShowTxt()
        {
            string str = qMode.ToString();
            heading.alignment = TextAlignmentOptions.Right;
            heading.text = str;
        }
        void Hidetxt()
        {
            heading.text = "";
        }
        void Awake()
        {
            img = GetComponent<Image>();    
            heading = transform.parent.GetChild(2).GetComponent<TextMeshProUGUI>();
            Hidetxt();
        }
    }
}