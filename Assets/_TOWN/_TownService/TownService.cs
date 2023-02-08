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


        public AllBuildSO allbuildingSO;

        void Start()
        {
            townController = GetComponent<TownController>();
            fameController = GetComponent<FameViewController>();
            //templeController = buildingIntViewController.templePanel.GetComponent<TempleController>();           
        }
        public void Init(LocationName location)
        {  
            // load Scene here

            townModel = new TownModel(); // to be linke d to save Panels
            townModel.currTown = location;
            townModel.allCharInTown 
                = RosterService.Instance.rosterController.GetCharAvailableInTown(location);

            townViewController.TownViewInit();

           // RosterService.Instance.OpenRosterView();
            // can instantiate prefab etc too here 
          //  townController.GetCharAvailableInTown(townModel.currTown);

        }

    }


}

