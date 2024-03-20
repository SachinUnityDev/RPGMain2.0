using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Quest
{

    public class QuestJournoPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] QuestModel questModel = null;
        [SerializeField] QuestView questView; 
    
        public void QuestJurnoPtrInit(QuestView questView, QuestModel questModel)
        {
            this.questView = questView;
            this.questModel = questModel;
            FillJurnoTxt();
        }

        void FillJurnoTxt()
        {
            TextMeshProUGUI txt = transform.GetComponent<TextMeshProUGUI>();
            txt.gameObject.SetActive(true);
            if (questModel!= null ) 
            {
                txt.gameObject.SetActive(true);
                if (questModel.questState == QuestState.UnLocked)
                {
                    txt.text = questModel.questNameStr;
                    txt.fontStyle = FontStyles.Normal; 
                }                   
                else if (questModel.questState == QuestState.Completed)
                {
                    txt.text = questModel.questNameStr;
                    txt.fontStyle = FontStyles.Strikethrough;
                }
                else
                {
                    txt.text = "";
                }                    
            }
            else
            {
                txt.text = "";
                txt.gameObject.SetActive(false);
            }
        }
        void FillObjPanel()
        {
            if (questModel == null || questModel.questName == QuestNames.None) return;
            questView.objPanel.gameObject.SetActive(true);
          
            Transform container = questView.objPanel.GetChild(1); 
            for (int i = 0; i < container.childCount; i++)
            {
                TextMeshProUGUI txt = container.GetChild(i).GetComponent<TextMeshProUGUI>();

                if (i < questModel.allObjModel.Count)                   
                {
                    if (questModel.allObjModel[i].objState == QuestState.UnLocked)
                    { 
                        txt.text = $"{i + 1}. " + questModel.allObjModel[i].objNameStr;
                        txt.fontStyle = FontStyles.Normal;
                    }
                    else if (questModel.allObjModel[i].objState == QuestState.Completed)
                    {
                        txt.text = $"{i + 1}. " + questModel.allObjModel[i].objNameStr;
                        txt.fontStyle = FontStyles.Strikethrough;               
                    }
                    else if(questModel.allObjModel[i].objState == QuestState.Locked)
                    {
                        txt.text = "";              
                    }
                }
                else
                    txt.text = "";
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {                     
            FillObjPanel();            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            questView.objPanel.gameObject.SetActive(false);
        }
    }
}