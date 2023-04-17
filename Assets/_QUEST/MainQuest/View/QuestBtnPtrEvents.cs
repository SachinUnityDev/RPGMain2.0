using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Quest
{
    public class QuestBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] QuestType questType;
        [SerializeField] List<QuestModel> allQuestModels = new List<QuestModel>();  

        QuestView questView; 
        public void InitPtrEvents(QuestView questView)
        {
            this.questView = questView;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            FillQuestJurno();
        }

        public void FillQuestJurno()
        {
           
            allQuestModels = QuestMissionService.Instance.GetAllQuestModelsOfType(questType);
            for (int i = 0; i < questView.questJournoTrans.childCount; i++)
            {
                if(i< allQuestModels.Count)
                {                       
                    questView.questJournoTrans.GetChild(i).GetComponent<QuestJournoPtrEvents>()
                        .QuestJurnoPtrInit(questView, allQuestModels[i]); 
                }
                else
                {                 
                    questView.questJournoTrans.GetChild(i).GetComponent<QuestJournoPtrEvents>()
                      .QuestJurnoPtrInit(questView, null);
                }
            }    
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }
    }
}