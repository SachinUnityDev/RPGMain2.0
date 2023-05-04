using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Quest
{
    public class CityEPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler    
    {
        CityEPanelView cityEPanelView; 
        CityEModel cityEModel;

         
        void Start()
        {
            
        }

        public void InitCityEPanelPtrEvents(CityEPanelView cityEPanelView, CityEModel cityEModel)
        {
            this.cityEPanelView = cityEPanelView;
            this.cityEModel = cityEModel;
            if(cityEModel.state == CityEState.UnAvailable ||
                            cityEModel.state == CityEState.UnLockedNAvail)
            {
                gameObject.GetComponent<TextMeshProUGUI>().text = cityEModel.cityENameStr;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (cityEModel == null) return;
            if(cityEModel.state == CityEState.UnLockedNAvail)
            {
                EncounterService.Instance.cityEController.ShowCityE(cityEModel);
                cityEPanelView.GetComponent<IPanel>().UnLoad();
            }            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (cityEModel == null) return;
            if(cityEModel.state == CityEState.UnAvailable)
            {
                gameObject.GetComponent<TextMod>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(true);                 
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (cityEModel == null) return;
            transform.GetChild(0).gameObject.SetActive(false);
            gameObject.GetComponent<TextMod>().enabled = true;
        }
    }
}