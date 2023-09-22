using Common;
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
       
        public AllMapESO allMapESO;

        [Header("Map Encounter NTBR")]
        public MapEController mapEController;
        public MapEFactory mapEFactory;

        [Header("Quest Encounter NTBR")]
        public QuestEFactory questEFactory;
        public QuestEController questEController;

        [Header("Quest Encounter TBR")]
        public AllQuestESO allQuestESO;

        public bool isNewGInitDone = false;

        void Start()
        {            
            
            questEController= gameObject.GetComponent<QuestEController>();
            questEFactory= gameObject.GetComponent<QuestEFactory>();    

        }
        public void EncounterInit()
        {

            cityEFactory = gameObject.GetComponent<CityEncounterFactory>();
            cityEController = gameObject.GetComponent<CityEController>();

            mapEFactory = gameObject.GetComponent<MapEFactory>();
            mapEController = gameObject.GetComponent<MapEController>();
            
            questEController = gameObject.GetComponent<QuestEController>();

            cityEController.InitCityE(allCityESO);
            mapEController.InitMapE(allMapESO);
            questEController.InitQuestE(allQuestESO);

            isNewGInitDone = true;
        }

    }
}