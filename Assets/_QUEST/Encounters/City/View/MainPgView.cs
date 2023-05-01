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
        [SerializeField] Button ChoiceABtn;
        [SerializeField] Button ChoiceBtn;


        CityEncounterView cityEView;
        CityEncounterBase cityBase; 
        CityEModel encounterModel;

        void Start()
        {
            ChoiceABtn.onClick.AddListener(OnChoiceAPressed);
            ChoiceBtn.onClick.AddListener(OnChoiceBPressed);
        }
        void OnChoiceAPressed()
        {
            cityBase.OnChoiceASelect(); 
        }
        void OnChoiceBPressed()
        {
            cityBase.OnChoiceBSelect();
        }
        public void InitMainPage(CityEncounterView cityEView, CityEncounterBase cityBase)
        {
            this.cityEView = cityEView;
            this.cityBase = cityBase;
            encounterModel = 
                     EncounterService.Instance.cityEController.GetCityEModel(cityBase.encounterName);
            FillPage(); 
        }
        void FillPage()
        {
            heading.text = encounterModel.cityEName.ToString().CreateSpace();
            desc.text = encounterModel.descTxt; 
        }
    }
}