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
        public BuildingIntViewController buildingIntViewController;

        public TownModel townModel;
        public TownController townController;
        public TownViewController townViewController;

        [Header("BUILDING CONTROLLERS")]
        public TempleController templeController;

        void Start()
        {
            townController = GetComponent<TownController>(); 
            fameController = GetComponent<FameViewController>();
            templeController = buildingIntViewController.templePanel.GetComponent<TempleController>();           
        }
        public void Init(LocationName location)
        {  
            // load Scene here

            townModel = new TownModel(); // to be linke d to save Panels
            townModel.currTown = location;
            townModel.allCharInTown 
                = RosterService.Instance.rosterController.GetCharAvailableInTown(location);
           // RosterService.Instance.OpenRosterView();
            // can instantiate prefab etc too here 
          //  townController.GetCharAvailableInTown(townModel.currTown);

        }

        private void Update()
        {
         
        }


    }


}

