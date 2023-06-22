using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Quest 
{
    public class QuestEMainPgView : MonoBehaviour
    {
        [Header("TBR")]
        [SerializeField] TextMeshProUGUI heading;
        [SerializeField] TextMeshProUGUI desc;
        [SerializeField] Button choiceABtn;
        [SerializeField] Button choiceBBtn;


        [SerializeField] QuestEView questEView;
        [SerializeField] QuestEbase questEBase;
        [SerializeField] QuestEModel questEModel;

        void Start()
        {
            choiceABtn.onClick.AddListener(OnChoiceAPressed);
            choiceBBtn.onClick.AddListener(OnChoiceBPressed);
        }
        void OnChoiceAPressed()
        {
            questEBase.OnChoiceASelect();
            questEView.ShowResultPage();
        }
        void OnChoiceBPressed()
        {
            questEBase.OnChoiceBSelect();
            questEView.ShowResultPage();
        }
        public void InitMainPage(QuestEView questEView, QuestEbase questEBase, QuestEModel questEModel)
        {
            this.questEView = questEView;
            this.questEBase = questEBase;
            this.questEModel = questEModel;

            FillPage();
            FillBtnStr();
        }
        void FillPage()
        {
            heading.text = questEModel.questEName.ToString().CreateSpace();
            desc.text = questEModel.descTxt;
        }
        void FillBtnStr()
        {
            choiceABtn.GetComponentInChildren<TextMeshProUGUI>().text = questEBase.choiceAStr;
            choiceBBtn.GetComponentInChildren<TextMeshProUGUI>().text = questEBase.choiceBStr;
        }
    }
}

