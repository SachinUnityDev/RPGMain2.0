using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class CityEPanelView : MonoBehaviour, IPanel, iHelp
    {
        [SerializeField] HelpName helpName;
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
            UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(gameObject, true);
            InitCityEPanelView();
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
        }

        public HelpName GetHelpName()
        {
            return helpName; 
        }
    }
}