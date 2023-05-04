using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Quest
{
    public class MainPgView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI heading;
        [SerializeField] TextMeshProUGUI desc;
        [SerializeField] Button choiceABtn;
        [SerializeField] Button choiceBBtn;


        CityEncounterView cityEView;
        CityEncounterBase cityBase; 
        CityEModel cityEModel;

        void Start()
        {
            choiceABtn.onClick.AddListener(OnChoiceAPressed);
            choiceBBtn.onClick.AddListener(OnChoiceBPressed);
        }
        void OnChoiceAPressed()
        {
            cityBase.OnChoiceASelect();
            cityEView.ShowResultPage();
        }
        void OnChoiceBPressed()
        {         
            cityBase.OnChoiceBSelect();
            cityEView.ShowResultPage();
        }
        public void InitMainPage(CityEncounterView cityEView, CityEncounterBase cityBase, CityEModel cityEModel)
        {
            this.cityEView = cityEView;
            this.cityBase = cityBase;
            this.cityEModel = cityEModel;
                     
            FillPage(); 
            FillBtnStr();
        }
        void FillPage()
        {
            heading.text = cityEModel.cityEName.ToString().CreateSpace();
            desc.text = cityEModel.descTxt; 
        }

        void FillBtnStr()
        {
            choiceABtn.GetComponentInChildren<TextMeshProUGUI>().text = cityEModel.choiceAStr;
            choiceBBtn.GetComponentInChildren<TextMeshProUGUI>().text = cityEModel.choiceBStr;
        }
    }
}