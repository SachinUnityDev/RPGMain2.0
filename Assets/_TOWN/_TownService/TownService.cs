using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.SceneManagement;
namespace Town
{
    public class TownService : MonoSingletonGeneric<TownService>
    {        
        public FameViewController fameController;    
        public BuildView buildView;

        public TownModel townModel;
        public TownController townController;
        public TownViewController townViewController;

        [Header("BUILDING CONTROLLERS")]
        public TempleController templeController;


        [Header("TOWN LOCATION")]
        public BuildingNames selectBuildingName;


        public AllBuildSO allbuildSO;
        [Header("Game Init")]
        public bool isNewGInitDone = false;

        void Start()
        {
            townController = GetComponent<TownController>();
            fameController = GetComponent<FameViewController>();
            //templeController = buildingIntViewController.templePanel.GetComponent<TempleController>();           
        }
        public void Init(LocationName location)
        {  
            townModel = new TownModel(); // to be linke d to save Panels
            townModel.currTown = location;
            townModel.allCharInTown 
                = RosterService.Instance.rosterController.GetCharAvailableInTown(location);
            
            townViewController.TownViewInit(CalendarService.Instance.currtimeState);
            isNewGInitDone = true;
        }

    }


}

