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

        private void Start()
        {
           
        }
        public void InitQuestModeBtn(QuestMenuEmbarkView questMenuEmbarkView, QuestModel questModel)
        {
            this.questMenuEmbarkView = questMenuEmbarkView; 
            this.questModel = questModel;

            transform.GetComponent<Image>().sprite =
                QuestMissionService.Instance.allQuestSO.GetQuestModeSprite(questModel.questMode); 

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (questMenuEmbarkView.questModeSelPanel.gameObject.activeInHierarchy)
                questMenuEmbarkView.questModeSelPanel.gameObject.SetActive(false);
            else
                questMenuEmbarkView.questModeSelPanel.gameObject.SetActive(true);
             
            
        }
    }
}