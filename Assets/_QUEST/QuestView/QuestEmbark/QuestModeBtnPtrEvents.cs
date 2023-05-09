using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Quest
{
    public class QuestModeBtnPtrEvents : MonoBehaviour
    {
        QuestMenuEmbarkView questMenuEmbarkView;
        [SerializeField] QuestModel questModel; 

        public void InitQuestModeBtn(QuestMenuEmbarkView questMenuEmbarkView, QuestModel questModel)
        {
            this.questMenuEmbarkView = questMenuEmbarkView; 
            this.questModel = questModel;


            transform.GetComponent<Image>().sprite =
                QuestMissionService.Instance.allQuestMainSO.GetQuestModeSprite(questModel.questMode); 

        }
    }
}