using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Quest
{
    public class QuestSelBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] QuestMode questMode; 

        QuestMenuEmbarkView questMenuEmbarkView;
        [SerializeField] QuestModel questModel;

        Sprite spriteLit;
        Sprite spriteN;
        Image infoImage; 
        private void Awake()
        {
            infoImage =
                 transform.parent.GetChild(1).GetComponent<Image>();
        }
        public void InitQuestPtrEvents(QuestMenuEmbarkView questMenuEmbarkView
                                        ,  QuestModel questModel)
        {           
            this.questMenuEmbarkView= questMenuEmbarkView;
            this.questModel= questModel;
            FillInfoSprite();     
        }

        void FillInfoSprite()
        {
            AllQuestSO allQuestSO = QuestMissionService.Instance.allQuestMainSO;

            spriteLit =
                        allQuestSO.GetQuestInfoSpriteLit(questMode);
            spriteN =
                        allQuestSO.GetQuestInfoSpriteN(questMode);            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            questModel.questMode = questMode;
            questMenuEmbarkView.FillMenuPanel(questModel); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            infoImage.sprite= spriteLit;
            transform.GetChild(1).gameObject.SetActive(true);
            infoImage.transform.GetChild(0).gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            infoImage.sprite = spriteN;
            transform.GetChild(1).gameObject.SetActive(false);
            infoImage.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}