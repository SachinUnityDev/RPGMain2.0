using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quest
{
    public class QuestMenuEmbarkView : MonoBehaviour, IPanel
    {
        
        [Header(" TBR")]
        public Transform questModeSelPanel;
        [SerializeField] Transform questModeBtnTrans;

        [Header(" global var")]
        QuestEmbarkView questEmbarkView;
        [SerializeField] QuestSO questSO;
        [SerializeField] QuestModel questModel;


        [SerializeField] List<QuestSelBtnPtrEvents> allQuestSel= new List<QuestSelBtnPtrEvents>();
       
        void Start()
        {
            questModeSelPanel.gameObject.SetActive(false);  
        }

        public void InitQuestMenuEmbark(QuestEmbarkView questEmbarkView, QuestModel questModel, QuestSO questSO)
        {
            this.questEmbarkView= questEmbarkView;          
            this.questSO = questSO;
            this.questModel = questModel;

            questModeBtnTrans.GetComponent<QuestModeBtnPtrEvents>().InitQuestModeBtn(this, questModel);
            FillMenuPanel(questModel);
    
            foreach (QuestSelBtnPtrEvents sel in allQuestSel)
            {
                sel.InitQuestPtrEvents(this, questModel);
            }
        }

        public void FillMenuPanel(QuestModel questModel)
        {
            Sprite sprite = 
                QuestMissionService.Instance.allQuestSO.GetQuestModeSprite(questModel.questMode);

            transform.GetChild(0).GetComponent<Image>().sprite = sprite;   
        }
        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }

        public void Init()
        {
            UnLoad();
        }
    }
}