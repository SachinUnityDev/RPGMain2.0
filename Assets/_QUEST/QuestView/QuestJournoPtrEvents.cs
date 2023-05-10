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
            if (questModel == null)
            {
                txt.text = "";
            }
            else
            {   
                    txt.text = questModel.QuestNameStr;
                    
                    if (questModel.questState == QuestState.Completed)
                        txt.fontStyle = FontStyles.Strikethrough;                
            }
        }
        void FillObjPanel()
        {
            if (questModel == null || questModel.questName == QuestNames.None) return;
            questView.objPanel.gameObject.SetActive(true);
            bool objTobeTaken = false; 
            Transform container = questView.objPanel.GetChild(1); 
            for (int i = 0; i < container.childCount; i++)
            {
                TextMeshProUGUI txt = container.GetChild(i).GetComponent<TextMeshProUGUI>();

                if (i < questModel.allObjModel.Count && !objTobeTaken)
                {
                    if (questModel.allObjModel[i].objState == QuestState.Completed)
                    {
                        txt.fontStyle = FontStyles.Strikethrough;
                        txt.text = $"{i + 1}. " + questModel.allObjModel[i].objNameStr;
                    }
                    else if (questModel.allObjModel[i].objState == QuestState.ToBeTaken)
                    {
                        txt.fontStyle = FontStyles.Normal;
                        txt.text = $"{i + 1}. " + questModel.allObjModel[i].objNameStr;
                        objTobeTaken = true;
                    }
                    else
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