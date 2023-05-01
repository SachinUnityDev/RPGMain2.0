using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;


namespace Quest
{
    public class EncounterService : MonoSingletonGeneric<EncounterService>
    {
        public CityEController cityEController;
        public CityEncounterFactory cityEFactory;

        public AllCityESO allCityESO;

        void Start()
        {
            cityEController = gameObject.AddComponent<CityEController>(); 
            cityEFactory = gameObject.AddComponent<CityEncounterFactory>(); 

        }

        public void EncounterInit()
        {
            cityEController.InitCityE(allCityESO);
        }


    }
}