using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class EncounterService : MonoSingletonGeneric<EncounterService>
    {
        public CityEController cityEController;
        public CityEncounterFactory cityEFactory; 
        void Start()
        {
            cityEController = gameObject.AddComponent<CityEController>(); 
            cityEFactory = gameObject.AddComponent<CityEncounterFactory>(); 
        }

        


    }
}