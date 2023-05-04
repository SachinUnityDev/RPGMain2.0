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
        [SerializeField] TextMeshProUGUI resultStr;
        [SerializeField] TextMeshProUGUI buffStr; 
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
            cityBase.CityEContinuePressed();
        }

        public void InitResultPage(CityEncounterView cityEView, CityEncounterBase cityBase, CityEModel cityEModel)
        {
            this.cityEView = cityEView;
            this.cityBase = cityBase;
            this.cityEModel = cityEModel; 
                     
            FillPage();
        }
        void FillPage()
        {
            heading.text = cityEModel.cityEName.ToString().CreateSpace();
            resultStr.text = cityBase.resultStr; 
            FillBuffStr();
            FillBtnStr();
        }
        
        void FillBuffStr()
        {
            buffStr.text = cityBase.strFX; 
        }
        void FillBtnStr()
        {
            continueBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        }

    }
}