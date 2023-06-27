using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Common; 
namespace Quest
{
    public class QuestEResultView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI heading;
        [SerializeField] TextMeshProUGUI resultStr;
        [SerializeField] TextMeshProUGUI buffStr;
        [SerializeField] Button continueBtn;

        QuestEView questEView;
        QuestEbase questEBase;
        QuestEModel questEModel;

        void Start()
        {
            continueBtn.onClick.AddListener(OnContinueBtnPressed);
        }

        public void OnContinueBtnPressed()
        {
            questEView.interactEColEvents.OnContinue(); 
            questEView.UnLoad();
            questEBase.QuestEContinuePressed();
        }

        public void InitResultPage(QuestEView questEView, QuestEbase questEBase, QuestEModel questEModel)
        {
            this.questEView = questEView;
            this.questEBase = questEBase;
            this.questEModel = questEModel;
            FillPage();
        }
        void FillPage()
        {
            heading.text = questEModel.questEName.ToString().CreateSpace();
            resultStr.text = questEBase.resultStr;
            FillBuffStr();
            FillBtnStr();
        }

        void FillBuffStr()
        {
            buffStr.text = questEBase.buffstr;
        }
        void FillBtnStr()
        {
            continueBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        }


    }
}