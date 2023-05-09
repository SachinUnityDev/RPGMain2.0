using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quest
{
    public class QuestMenuEmbarkView : MonoBehaviour, IPanel
    {
        QuestEmbarkView questEmbarkView;       
        [SerializeField] QuestSO questSO;

        [SerializeField] Transform questSelPanel;
        [SerializeField] Transform questModeBtnTrans; 
        
       
        void Start()
        {

        }

        public void InitQuestMenuEmbark(QuestEmbarkView questEmbarkView, QuestModel questModel, QuestSO questSO)
        {
            this.questEmbarkView= questEmbarkView;
          
            this.questSO = questSO;

            questModeBtnTrans.GetComponent<QuestModeBtnPtrEvents>().InitQuestModeBtn(this, questModel);
            FillMenuPanel(questModel);
            QuestSelBtnPtrEvents[] allSel = 
            questSelPanel.GetComponentsInChildren<QuestSelBtnPtrEvents>();
            foreach (QuestSelBtnPtrEvents sel in allSel)
            {
                sel.InitQuestPtrEvents(this, questModel);
            }


        }

        public void FillMenuPanel(QuestModel questModel)
        {
            Sprite sprite = 
                QuestMissionService.Instance.allQuestMainSO.GetQuestModeSprite(questModel.questMode);

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