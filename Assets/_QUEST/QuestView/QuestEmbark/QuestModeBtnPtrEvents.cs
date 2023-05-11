using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Quest
{
    public class QuestModeBtnPtrEvents : MonoBehaviour, IPointerClickHandler
    {
        QuestMenuEmbarkView questMenuEmbarkView;
        [SerializeField] QuestModel questModel;
        [SerializeField] bool isSelPanelOpen;

        private void Start()
        {
           
        }
        public void InitQuestModeBtn(QuestMenuEmbarkView questMenuEmbarkView, QuestModel questModel)
        {
            this.questMenuEmbarkView = questMenuEmbarkView; 
            this.questModel = questModel;

            transform.GetComponent<Image>().sprite =
                QuestMissionService.Instance.allQuestMainSO.GetQuestModeSprite(questModel.questMode); 

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isSelPanelOpen)
            {
                questMenuEmbarkView.questSelPanel.gameObject.SetActive(false);
                isSelPanelOpen= false;
            }
            else
            {
                questMenuEmbarkView.questSelPanel.gameObject.SetActive(true);
                isSelPanelOpen= true;
            }
        }
    }
}