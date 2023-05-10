using Common;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using TMPro;
using UnityEngine;

namespace Quest
{
    public class CityEncounterView : MonoBehaviour, IPanel
    {

        public Transform mainPage;
        public Transform resultPage;

        CityEncounterBase cityBase;
        CityEModel cityEModel; 
        private void Start()
        {
           
        }
        public void Init()
        {
            Load();
        }

        public void Load()
        {            
            UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(gameObject, true);
        }
        public void ShowMainPage()
        {
            mainPage.gameObject.SetActive(true);
            resultPage.gameObject.SetActive(false);
        }
        public void ShowResultPage()
        {
            mainPage.gameObject.SetActive(false);
            resultPage.gameObject.SetActive(true);
            resultPage.GetComponent<ResultPgView>().InitResultPage(this, cityBase, cityEModel);
        }
        public void InitEncounter(CityEModel cityEModel)
        {
            this.cityEModel= cityEModel;                
            cityBase = EncounterService.Instance.cityEController.GetCityEBase(cityEModel.cityEName, cityEModel.encounterSeq); 
            mainPage.GetComponent<MainPgView>().InitMainPage(this, cityBase, cityEModel);
            ShowMainPage();
            Load();
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    }
}