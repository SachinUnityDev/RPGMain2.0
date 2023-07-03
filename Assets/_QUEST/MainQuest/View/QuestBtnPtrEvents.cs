using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Quest
{
    public class QuestBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] QuestType questType;
        [SerializeField] List<QuestModel> allQuestModels = new List<QuestModel>();  

        QuestView questView;
        Image img; 

        [Header("Btn Colors TBR")]
        [SerializeField] Color colorN;
        [SerializeField] Color colorD; 



        public void InitPtrEvents(QuestView questView)
        {
            this.questView = questView;
            img = GetComponent<Image>();
            ChgBtnState();
        }
        void ChgBtnState()
        {
            allQuestModels = QuestMissionService.Instance.GetAllQuestModelsOfType(questType);

            if (allQuestModels.Count == 0)
            {
                img.DOColor(colorD,0.1f); 
            }
            else
            {
                img.DOColor(colorN,0.1f);
            }
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (allQuestModels.Count == 0) return;
            FillQuestJurno();
        }
        
        public void FillQuestJurno()
        {
           
            allQuestModels = QuestMissionService.Instance.GetAllQuestModelsOfType(questType);
            
            ChgBtnState();           
            

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