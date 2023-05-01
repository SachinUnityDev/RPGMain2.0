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
        public virtual void CityEInit()
        {
            cityEModel = EncounterService.Instance.cityEController.GetCityEModel(encounterName); 
        }
        public abstract bool UnLockCondChk();
        public abstract bool PreReqChk();
        public abstract void OnChoiceASelect();
        public abstract void OnChoiceBSelect();

        public virtual void CityEContinuePressed()
        {
            cityEModel.state = CityEState.Solved;
            cityEModel.dayEventTaken = CalendarService.Instance.dayInGame;
        }
    }
}