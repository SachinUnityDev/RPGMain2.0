using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

namespace Quest
{
    public class ResultPgView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI heading;
        [SerializeField] TextMeshProUGUI desc;
        [SerializeField] Button continueBtn; 
        
        CityEncounterView cityEView;
        CityEncounterBase cityBase;
        CityEModel cityEModel;

        void Start()
        {
            continueBtn.onClick.AddListener(OnContinueBtnPressed);
        }

        public void OnContinueBtnPressed()
        {
            cityEView.GetComponent<IPanel>().UnLoad();
        }

        public void InitResultPage(CityEncounterView cityEView, CityEncounterBase cityBase)
        {
            this.cityEView = cityEView;
            this.cityBase = cityBase;
            cityEModel =
                     EncounterService.Instance.cityEController.GetCityEModel(cityBase.encounterName);
            FillPage();
        }
        void FillPage()
        {
            heading.text = cityEModel.cityEName.ToString().CreateSpace();
            desc.text = cityBase.resultStr; 
        }
        
    }
}