using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Common; 

namespace Quest
{
    public abstract class CityEncounterBase
    {        
        public abstract CityENames encounterName { get; }
        public abstract int seq { get; }

        public CityEModel cityEModel; 

        public string resultStr; 

        public string strFX;
        public virtual void CityEInit(CityEModel cityEModel)
        {
            this.cityEModel = cityEModel;
            Debug.Log("On model assigned"+ cityEModel.cityENameStr );
        }
        public abstract bool UnLockCondChk();
        public abstract bool PreReqChk();
        public abstract void OnChoiceASelect();
        public abstract void OnChoiceBSelect();

        public virtual void CityEContinuePressed()
        {
          //  cityEModel = EncounterService.Instance.cityEController.GetCityEModel(encounterName,seq);


            if (cityEModel != null)
            {
                Debug.Log("On continue pressed" + cityEModel.cityEName);
                cityEModel.state = CityEState.Completed;
                cityEModel.dayEventTaken = CalendarService.Instance.calendarModel.dayInGame;
            }
            else
            {
                Debug.Log("On null found");
            }
        }
    }
}