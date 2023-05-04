using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class CityEPanelView : MonoBehaviour, IPanel
    {

        public Transform cityEContainer;
        private void Start()
        {
           
        }
        public void Init()
        {
           InitCityEPanelView();
        }

        public void InitCityEPanelView()
        {
            //get list from the model
            // init the txt 
            int i = 0; 
            foreach(CityEModel cityEModel in EncounterService.Instance.cityEController.allCityEModels)
            {
                if(cityEModel.state == CityEState.UnLockedNAvail || cityEModel.state == CityEState.UnAvailable)
                {
                    cityEContainer.GetChild(i).gameObject.SetActive(true);
                    cityEContainer.GetChild(i).GetComponent<CityEPtrEvents>().InitCityEPanelPtrEvents(this, cityEModel);
                    i++;
                }
            }
        }

        public void Load()
        {
            InitCityEPanelView();
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, true); 
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }
    }
}