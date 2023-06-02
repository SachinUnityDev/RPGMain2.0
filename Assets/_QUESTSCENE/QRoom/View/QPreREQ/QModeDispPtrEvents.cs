using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quest
{
    public class QModeDispPtrEvents : MonoBehaviour
    {
        [SerializeField] Image img;
        [SerializeField] QuestMode questMode;
        private void Start()
        {
            gameObject.SetActive(false);
        }
        public void InitQModeDisplay()
        {
            questMode = QuestMissionService.Instance.currQuestMode;
            img.sprite = QuestMissionService.Instance.allQuestMainSO
                                                    .GetQInfoSpriteLit(questMode);            
        }
    }
}