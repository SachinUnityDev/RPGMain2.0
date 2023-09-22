using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class QuestEView : MonoBehaviour
    {
        public Transform page1Trans;
        public Transform page2Trans;

        QuestEbase questEBase;
        [SerializeField]QuestEModel questEModel;
        public InteractEColEvents interactEColEvents; 
        private void Start()
        {
          
        }
        public void Init()
        {
            Load();
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true);
        }
        public void ShowMainPage()
        {
            page1Trans.gameObject.SetActive(true);
            page2Trans.gameObject.SetActive(false);
        }
        public void ShowResultPage()
        {
            page1Trans.gameObject.SetActive(false);
            page2Trans.gameObject.SetActive(true);
            page2Trans.GetComponent<QuestEResultView>().InitResultPage(this, questEBase, questEModel);
        }
        public void InitEncounter(QuestEModel questEModel, InteractEColEvents interactEColEvents)
        {
            this.questEModel = questEModel;
            this.interactEColEvents= interactEColEvents;    
            questEBase = EncounterService.Instance.questEController
                            .GetQuestEBase(questEModel.questEName);
            questEBase.QuestEInit(questEModel); 
            page1Trans.GetComponent<QuestEMainPgView>().InitMainPage(this, questEBase, questEModel);
            ShowMainPage();
            Load();
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
            Destroy(gameObject);
        }
    }
}