using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;


namespace Quest
{
    public class EncounterService : MonoSingletonGeneric<EncounterService>
    {
        [Header("City Encounter NTBR")]
        public CityEController cityEController;
        public CityEncounterFactory cityEFactory;

        [Header("City Encounter TBR")]
        public AllCityESO allCityESO;

        [Header("Map Encounter TBR")]
        public AllMapETriggerSO allMapETriggerSO; 
        public AllMapESO allMapESO;

        public MapEController mapEController;
        public MapEFactory mapEFactory; 




        void Start()
        {            
            cityEFactory = gameObject.GetComponent<CityEncounterFactory>();
            cityEController = gameObject.GetComponent<CityEController>();

            mapEFactory= gameObject.GetComponent<MapEFactory>();
            mapEController= gameObject.GetComponent<MapEController>();

        }
        public void EncounterInit()
        {
            cityEController.InitCityE(allCityESO);
            mapEController.InitMapE(allMapESO);
        }



    }
}