using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Interactables;
namespace Town
{
    public class TownViewController : MonoBehaviour
    {

        [Header("Left Town Btns")]
        [SerializeField] Button rosterBtn;
        [SerializeField] Button jobBtn;
        [SerializeField] Button inventoryBtn;


        [Header("Right Town Btns")]
        [SerializeField] Button eventBtn;
        [SerializeField] Button questScrollBtn;
        [SerializeField] Button mapBtn;

        [SerializeField] GameObject InteractionPanel;

        [SerializeField] List<GameObject> allPanels = new List<GameObject>();


        public BuildingNames selectBuild;
        [SerializeField] Transform buildContainer; 


        void Start()
        {
            buildContainer = transform.GetChild(0);
     
            //rosterBtn.onClick.AddListener(OnRosterBtnPressed);
            //jobBtn.onClick.AddListener(OnJobsBtnPressed);
            //inventoryBtn.onClick.AddListener(OnInvBtnPressed);

            //eventBtn.onClick.AddListener(OnEventBtnPressed);
            //questScrollBtn.onClick.AddListener(OnQuestScrollBtnPressed);
            //mapBtn.onClick.AddListener(OnMapBtnPressed);
        }




        public void OnBuildSelect(int index)
        {
            selectBuild = (BuildingNames)(index + 1); // correction for none
            for (int i = 0; i < buildContainer.childCount; i++)
            {                
              buildContainer.GetChild(i).GetComponent<BuildingPtrEvents>().OnDeSelect();                
            }
            
            
            // get inside the building
        }
        public void TownViewInit()
        {

            foreach (Transform child in buildContainer)
            {
               child.GetComponent<BuildingPtrEvents>().Init(this);
            }
        }



        void OnRosterBtnPressed()
        {
            RosterService.Instance.OpenRosterView(); 
        }
        void OnJobsBtnPressed()
        {

        }
        void OnInvBtnPressed()
        {
           // InventoryService.Instance.invViewController.

        }

        void OnEventBtnPressed()
        {


        }
        void OnQuestScrollBtnPressed()
        {

        }
        void OnMapBtnPressed()
        {

        }



    }



}
