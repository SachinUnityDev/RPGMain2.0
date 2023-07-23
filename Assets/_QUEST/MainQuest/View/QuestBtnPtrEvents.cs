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
            allQuestModels = QuestMissionService.Instance.GetQModelsOfType(questType);

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
           allQuestModels.Clear();
            allQuestModels = QuestMissionService.Instance.GetQModelsOfType(questType);            
            ChgBtnState();
            int i = 0; 
            foreach (QuestModel model in allQuestModels) 
            {
                questView.questJournoTrans.GetChild(i).GetComponent<QuestJournoPtrEvents>()
                    .QuestJurnoPtrInit(questView, allQuestModels[i]);
                questView.questJournoTrans.GetChild(i).gameObject.SetActive(true);
                i++; 
            }
            for (int j = i; j < questView.questJournoTrans.childCount; j++)
            {
                questView.questJournoTrans.GetChild(j).gameObject.SetActive(false);
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