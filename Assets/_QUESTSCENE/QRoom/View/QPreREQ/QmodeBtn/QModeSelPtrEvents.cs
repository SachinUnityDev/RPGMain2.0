using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Quest
{
    public class QModeSelPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        QModeNLandView qModeNLandView;
        [SerializeField] QuestMode questMode;

        [SerializeField] Image img;
        [SerializeField] TextMeshProUGUI nameTxt; 

        void Start()
        {

        }

        public void InitQModeSelBtnPtrEvents(QModeNLandView qModeNLandView, QuestMode questMode)
        {
            this.qModeNLandView = qModeNLandView;
            this.questMode = questMode;
            img.sprite = QuestMissionService.Instance.allQuestSO.GetQuestModeSprite(questMode);
            nameTxt.text = ""; 
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            QuestMissionService.Instance.On_QuestModeChg(questMode); 
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           nameTxt.text = questMode.ToString();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            nameTxt.text = ""; 
        }
    }
}